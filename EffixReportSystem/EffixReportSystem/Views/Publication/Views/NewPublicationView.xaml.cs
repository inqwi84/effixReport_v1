using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CommonLibraries.Log;
using EffixReportSystem.Helper.Classes;
using EffixReportSystem.Helper.Classes.Enums;
using EffixReportSystem.Views.Publication.ViewModels;
using Telerik.Windows.Controls;
using log4net;
using Brushes = System.Windows.Media.Brushes;
using Image = System.Windows.Controls.Image;
using Point = System.Windows.Point;

namespace EffixReportSystem.Views.Publication.Views
{
    public class AutoFilteredComboBox : ComboBox
    {
        private int silenceEvents = 0;

        /// <summary>
        /// Creates a new instance of <see cref="AutoFilteredComboBox" />.
        /// </summary>
        public AutoFilteredComboBox()
        {
            DependencyPropertyDescriptor textProperty = DependencyPropertyDescriptor.FromProperty(
                ComboBox.TextProperty, typeof(AutoFilteredComboBox));
            textProperty.AddValueChanged(this, this.OnTextChanged);

            this.RegisterIsCaseSensitiveChangeNotification();
        }

        #region IsCaseSensitive Dependency Property
        /// <summary>
        /// The <see cref="DependencyProperty"/> object of the <see cref="IsCaseSensitive" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsCaseSensitiveProperty =
            DependencyProperty.Register("IsCaseSensitive", typeof(bool), typeof(AutoFilteredComboBox), new UIPropertyMetadata(false));

        /// <summary>
        /// Gets or sets the way the combo box treats the case sensitivity of typed text.
        /// </summary>
        /// <value>The way the combo box treats the case sensitivity of typed text.</value>
        [System.ComponentModel.Description("The way the combo box treats the case sensitivity of typed text.")]
        [System.ComponentModel.Category("AutoFiltered ComboBox")]
        [System.ComponentModel.DefaultValue(true)]
        public bool IsCaseSensitive
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (bool)this.GetValue(IsCaseSensitiveProperty);
            }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                this.SetValue(IsCaseSensitiveProperty, value);
            }
        }

        protected virtual void OnIsCaseSensitiveChanged(object sender, EventArgs e)
        {
            if (this.IsCaseSensitive)
                this.IsTextSearchEnabled = false;

            this.RefreshFilter();
        }

        private void RegisterIsCaseSensitiveChangeNotification()
        {
            System.ComponentModel.DependencyPropertyDescriptor.FromProperty(IsCaseSensitiveProperty, typeof(AutoFilteredComboBox)).AddValueChanged(
                this, this.OnIsCaseSensitiveChanged);
        }
        #endregion

        #region DropDownOnFocus Dependency Property
        /// <summary>
        /// The <see cref="DependencyProperty"/> object of the <see cref="DropDownOnFocus" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty DropDownOnFocusProperty =
            DependencyProperty.Register("DropDownOnFocus", typeof(bool), typeof(AutoFilteredComboBox), new UIPropertyMetadata(true));

        /// <summary>
        /// Gets or sets the way the combo box behaves when it receives focus.
        /// </summary>
        /// <value>The way the combo box behaves when it receives focus.</value>
        [System.ComponentModel.Description("The way the combo box behaves when it receives focus.")]
        [System.ComponentModel.Category("AutoFiltered ComboBox")]
        [System.ComponentModel.DefaultValue(true)]
        public bool DropDownOnFocus
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (bool)this.GetValue(DropDownOnFocusProperty);
            }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                this.SetValue(DropDownOnFocusProperty, value);
            }
        }
        #endregion

        #region | Handle selection |
        /// <summary>
        /// Called when <see cref="ComboBox.ApplyTemplate()"/> is called.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.EditableTextBox.SelectionChanged += this.EditableTextBox_SelectionChanged;
        }

        /// <summary>
        /// Gets the text box in charge of the editable portion of the combo box.
        /// </summary>
        protected TextBox EditableTextBox
        {
            get
            {
                return ((TextBox)base.GetTemplateChild("PART_EditableTextBox"));
            }
        }

        private int start = 0, length = 0;

        private void EditableTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (this.silenceEvents == 0)
            {
                this.start = ((TextBox)(e.OriginalSource)).SelectionStart;
                this.length = ((TextBox)(e.OriginalSource)).SelectionLength;

                this.RefreshFilter();
            }
        }
        #endregion

        #region | Handle focus |
        /// <summary>
        /// Invoked whenever an unhandled <see cref="UIElement.GotFocus" /> event
        /// reaches this element in its route.
        /// </summary>
        /// <param name="e">The <see cref="RoutedEventArgs" /> that contains the event data.</param>
        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);

            if (this.ItemsSource != null && this.DropDownOnFocus)
            {
                this.IsDropDownOpen = true;
            }
        }
        #endregion

        #region | Handle filtering |
        private void RefreshFilter()
        {
            if (this.ItemsSource != null)
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(this.ItemsSource);
                view.Refresh();
                this.IsDropDownOpen = true;
            }
        }

        private bool FilterPredicate(object value)
        {
            // We don't like nulls.
            if (value == null)
                return false;

            // If there is no text, there's no reason to filter.
            if (this.Text.Length == 0)
                return true;

            string prefix = this.Text;

            // If the end of the text is selected, do not mind it.
            if (this.length > 0 && this.start + this.length == this.Text.Length)
            {
                prefix = prefix.Substring(0, this.start);
            }

            return value.ToString()
                .StartsWith(prefix, !this.IsCaseSensitive, CultureInfo.CurrentCulture);
        }
        #endregion

        /// <summary>
        /// Called when the source of an item in a selector changes.
        /// </summary>
        /// <param name="oldValue">Old value of the source.</param>
        /// <param name="newValue">New value of the source.</param>
        protected override void OnItemsSourceChanged(System.Collections.IEnumerable oldValue, System.Collections.IEnumerable newValue)
        {
            if (newValue != null)
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(newValue);
                view.Filter += this.FilterPredicate;
            }

            if (oldValue != null)
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(oldValue);
                view.Filter -= this.FilterPredicate;
            }

            base.OnItemsSourceChanged(oldValue, newValue);
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            if (!this.IsTextSearchEnabled && this.silenceEvents == 0)
            {
                this.RefreshFilter();

                // Manually simulate the automatic selection that would have been
                // available if the IsTextSearchEnabled dependency property was set.
                if (this.Text.Length > 0)
                {
                    foreach (object item in CollectionViewSource.GetDefaultView(this.ItemsSource))
                    {
                        int text = item.ToString().Length, prefix = this.Text.Length;
                        this.SelectedItem = item;

                        this.silenceEvents++;
                        this.EditableTextBox.Text = item.ToString();
                        this.EditableTextBox.Select(prefix, text - prefix);
                        this.silenceEvents--;
                        break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Interaction logic for NewPublicationView.xaml
    /// </summary>
    public partial class NewPublicationView : UserControl
    {
        public static readonly ILog Log = LogManager.GetLogger(typeof(NewPublicationView));
        private readonly string _tempDirectory = Properties.Settings.Default.TempDirectory;
        private readonly string _baseDirectory = Properties.Settings.Default.BaseDirectory;

        public NewPublicationView()
        {
            InitializeComponent();
            //_tempDirectory = "c:\\storage\\temp";
            //_baseDirectory = "c:\\storage";
            KeyDown += UserControl_KeyDown;
           // Log.Info("!!");
        }

        private void ClearSearchTextBox(object sender, RoutedEventArgs e)
        {

        }

        private void ClearDirectory(string dirPath)
        {
            try
            {
                Log.Info("Start ClearDirectory");
                var directory = new DirectoryInfo(dirPath);
                var files = directory.GetFiles("*.*");
                foreach (var fileInfo in files)
                {
                    fileInfo.Delete();
                    Log.Info("ClearDirectory:" + fileInfo + "deleted");
                }
            }
            catch (Exception ex)
            {
                Log.Info("ClearDirectory:" + "deleted Exception"+ex);
            }
            Log.Info("End ClearDirectory");
        }

        private void MakeSnaphotsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Log.Info("Start MakeSnaphotsButton_Click");
                var ctx = DataContext as NewPublicationViewModel;
                ctx.SnapShotMode = ViewMode.MakeSnapshot;
                ctx.ImageTileList = new ObservableCollection<DataHelper.ImageTile>();
                ClearDirectory(_tempDirectory);
                var pointsList = new ObservableCollection<Point>();
                var tmp =
                    this.ChildrenOfType<DockPanelSplitter>().First(item => Equals(item.Background, Brushes.Black));
                foreach (
                    var child in
                        this.ChildrenOfType<DockPanelSplitter>().Where(
                            dock => Equals(dock.Background, Brushes.Black))
                    )
                {
                    pointsList.Add(child.TransformToVisual(this.qw1).Transform(new Point(0, 0)));
                }

                var image = BitmapImage2Bitmap((BitmapImage)img.Source);
                for (int i = 0; i < pointsList.Count - 1; i++)
                {
                    CropImage(image,
                              new Rectangle(
                                  (int)pointsList[i].X,
                                  (int)pointsList[i].Y,
                                  (int)tmp.ActualWidth,
                                  (int)(pointsList[i + 1].Y - pointsList[i].Y)), i.ToString(CultureInfo.InvariantCulture),
                              _tempDirectory);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log.Info("MakeSnaphotsButton_Click Exception:"+ex);
            }
            Log.Info("End MakeSnaphotsButton_Click");
        }

        private void CropImage(Bitmap img, Rectangle cropArea, string index, string dirPath)
        {
            try
            {
                Log.Info("Start CropImage");
                var ctx = DataContext as NewPublicationViewModel;
                var bmpCrop = img.Clone(cropArea, img.PixelFormat);


                var memoStream = new MemoryStream();
                bmpCrop.Save(memoStream, ImageFormat.Png);
                var image2 = RHelper.MemoryStreamToBitmapImage(memoStream);

                var tile = new DataHelper.ImageTile {Image = image2, ImageName = "_" + index + ".png"};
                tile.ImagePath = _tempDirectory + "\\" + tile.ImageName;
                ctx.ImageTileList.Add(tile);
                bmpCrop.Save(tile.ImagePath, ImageFormat.Png);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log.Info("Start CropImage Exception"+ex);
            }

        }

        public static BitmapImage Bitmap2BitmapImage(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                ms.Position = 0;
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = ms;
                bi.EndInit();
                return bi;
            }
        }

        private Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            try
            {
                using (var outStream = new MemoryStream())
                {
                    BitmapEncoder enc = new BmpBitmapEncoder();
                    enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                    enc.Save(outStream);
                    var bitmap = new Bitmap(outStream);
                    return new Bitmap(bitmap);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log.Info("BitmapImage2Bitmap Exception" + ex);
                return null;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Log.Info("Start CancelButton_Click");
                var ctx = DataContext as NewPublicationViewModel;
                ctx.ImageTileList.Clear();
                ctx.CurrentImageTile = null;
                var dir = new DirectoryInfo(Properties.Settings.Default.TempDirectory);
                foreach (var file in dir.GetFiles("*.*"))
                {
                   file.Delete(); 
                }
                (ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel =
                    (ctx.ParentViewModel as PublicationViewModel).PageViewModels[0];
                ((ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel as ViewPublicationViewModel)
    .CurrentDepartment = ctx.CurrentDepartment;
            }
            catch (Exception ex)
            {
                Log.Info("End CancelButton_Click Exception"+ex);
            }
             Log.Info("End CancelButton_Click");
        }

        private bool ValidatePublicationContext()
        {
            var ctx = DataContext as NewPublicationViewModel;
            if (ctx == null)
            {
                Log.Info("ValidatePublicationContext - context is null");
                return false;
            }
            return true;
        }
        private bool ValidateCurrentPublication()
        {
            var ctx = DataContext as NewPublicationViewModel;
            if (ctx.CurrentPublication == null)
            {
                Log.Info("ValidateCurrentPublication - ctx.CurrentPublication is null");
                return false;
            }
            return true;
        }
        private bool ValidatePublicationName()
        {
            var ctx = DataContext as NewPublicationViewModel;
            if (ctx.CurrentPublication.Publication_name == null)
            {
                Log.Info("ValidatePublicationName - ctx.CurrentPublication.Publication_name is null");
                return false;
            }
            return true;
        }
        private bool ValidateCurrentPublicationEF_SMI()
        {
            var ctx = DataContext as NewPublicationViewModel;
            if (ctx.CurrentPublication.EF_SMI == null)
            {
                Log.Info("ValidateCurrentPublicationEF_SMI - ctx.CurrentPublication.EF_SMI is null");
                return false;
            }
            return true;
        }

        private bool ValidateCurrentPublicationDate()
        {
            var ctx = DataContext as NewPublicationViewModel;
            if (ctx.CurrentPublication.Publication_date == null)
            {
                Log.Info("ValidateCurrentPublicationDate - ctx.CurrentPublication.Publication_date is null");
                return false;
            }
            return true;
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as NewPublicationViewModel;
            var canMake = false;
            canMake = ValidatePublicationContext();
            if (!canMake)
            {
                MessageBox.Show("Несуществующий контекст");
                Log.Info("Несуществующий контекст - ValidatePublicationContext");
                return;
            }
            canMake = ValidateCurrentPublication();
            if (!canMake)
            {
                MessageBox.Show("Несуществующая публикация");
                Log.Info("Несуществующая публикация- ValidateCurrentPublication");
                return;
            }
            canMake = ValidatePublicationName();
            if (!canMake)
            {
                MessageBox.Show("Неправильное наименование публикации");
                Log.Info("Неправильное наименование публикации - ValidatePublicationName");
                return;
            }
            canMake = ValidateCurrentPublicationEF_SMI();
            if (!canMake)
            {
                MessageBox.Show("Не найдено СМИ");
                Log.Info("Не найдено СМИ - ValidateCurrentPublicationEF_SMI");
                return;
            }
            canMake = ValidateCurrentPublicationDate();
            if (!canMake)
            {
                MessageBox.Show("Неправильная дата");
                Log.Info("Неправильная дата - ValidateCurrentPublicationDate");
                return;
            }


            //if (ctx == null)
            //{
            //    MessageBox.Show("DataContext as NewPublicationViewModel is Null");
            //    return;
            //}
            //if (ctx.CurrentPublication == null)
            //{
            //    MessageBox.Show("ctx.CurrentPublication is Null");
            //    return;
            //}
            //try
            //{
            //    if (ctx.CurrentPublication.Publication_name == null)
            //    {
            //        MessageBox.Show("Вы не ввели наименование публикации");
            //        return;
            //    }
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Вы не ввели наименование публикации");
            //    return;
            //}
            //try
            //{
            //    if (ctx.CurrentPublication.EF_SMI == null)
            //    {
            //        MessageBox.Show("Вы не выбрали СМИ");
            //        return;
            //    }
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Вы не выбрали СМИ");
            //    return;
            //}
            //try
            //{
            //    if (ctx.CurrentPublication.Publication_date == null)
            //    {
            //        MessageBox.Show("Вы не ввели дату публикации");
            //        return;
            //    }
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Вы не ввели дату публикации");
            //    return;
            //}

            try
            {
                Log.Info("Все проверки пройдены начало сохранения в БД!!!!!!!!--------------------------------------");
                var date = ctx.CurrentPublication.Publication_date.Value;
                // проверка, если больше нет такой записи в БД!
                var alreadyExists = DataHelper.CheckIfPublicationExists(ctx.CurrentPublication.Project_name,
                                                                        ctx.CurrentPublication.EF_SMI.Smi_id,
                                                                        date.Year,
                                                                        date.Month,
                                                                        date.Day);

                //Существует
                if (alreadyExists)
                {
                    Log.Info("alreadyExists");
                    var dialogResult =
                        MessageBox.Show("Публикация с данными параметрами уже существует. Сохранить публикацию как новую?",
                                        "Сохранение", MessageBoxButton.YesNoCancel);

                    switch (dialogResult)
                    {
                        case MessageBoxResult.Yes:
                            var destinationDirectory =
                                new DirectoryInfo(_baseDirectory + "\\" +
                                                  ctx.CurrentPublication.Project_name + "\\" +
                                                  date.Year + "\\" +
                                                  date.Month + "\\" +
                                                  date.Day);


                            var sourceDirectory = new DirectoryInfo(_tempDirectory);
                         
                            var files = sourceDirectory.GetFiles("*.*");
                            Log.Info("Найдено файлов files -"+files.Count().ToString());
                            ctx.CurrentPublication.Url_path = UrlTextBox.Text;
                            Log.Info("ctx.CurrentPublication.Url_path - " + ctx.CurrentPublication.Url_path);
                            ctx.CurrentPublication.Image_count = files.Count();
                            Log.Info("ctx.CurrentPublication.Image_count -" + ctx.CurrentPublication.Image_count.ToString());
                            ctx.CurrentPublication.Ceation_date = DateTime.Now;
                            Log.Info("ctx.CurrentPublication.Ceation_date- " + ctx.CurrentPublication.Ceation_date.ToString());

                            ctx.SaveCurrentPublication();
                            Log.Info("ctx.SaveCurrentPublication- Saved!");
                            var index = 0;
                            Log.Info("Сохранение файлов в папку проекта и в хранилище данных");
                            foreach (var fileInfo in files)
                            {
                                if (!destinationDirectory.Exists)
                                {
                                    destinationDirectory.Create();
                                }
                                var filePath = destinationDirectory + "\\"+ctx.CurrentPublication.Publication_id +
                                               ctx.CurrentPublication.EF_SMI.Smi_descr.Replace('.', '_') +
                                               fileInfo.Name.Replace(" ", "_");
                                fileInfo.MoveTo(filePath);
                                index++;
                                DataHelper.SaveFilesInAzureStorage(filePath);
                                if (index <= 1)
                                {
                                    ctx.CurrentPublication.Blob_path = filePath;
                                }
                            }
                            Log.Info("Сохранение файлов в папку проекта и в хранилище данных - ЗАВЕРШЕНО");

                            ctx.UpdateCurrentPublication();
                            Log.Info("ctx.UpdateCurrentPublication(); - ЗАВЕРШЕНО");
                            (ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel =
                                (ctx.ParentViewModel as PublicationViewModel).PageViewModels[0];

                            break;
                        case MessageBoxResult.No:
                            //  this.Close();
                            break;
                    }
                }
                else
                {
                    if (ctx.SnapShotMode == ViewMode.MakeSnapshot)
                    {
                        Log.Info("ViewMode.MakeSnapshot");
                        var destinationDirectory =
                            new DirectoryInfo(_baseDirectory + "\\" +
                                              ctx.CurrentPublication.Project_name + "\\" +
                                              date.Year + "\\" +
                                              date.Month + "\\" +
                                              date.Day);


                        var sourceDirectory = new DirectoryInfo(_tempDirectory);
                       
                        ctx.CurrentPublication.Url_path = UrlTextBox.Text;
                        Log.Info("ctx.CurrentPublication.Url_path - " + ctx.CurrentPublication.Url_path);
                        var files = sourceDirectory.GetFiles("*.*");
                        Log.Info("Найдено файлов files -" + files.Count().ToString());
                        ctx.CurrentPublication.Ceation_date = DateTime.Now;
                        Log.Info("ctx.CurrentPublication.Ceation_date- " + ctx.CurrentPublication.Ceation_date.ToString());
                        ctx.CurrentPublication.Image_count = files.Count();
                        Log.Info("ctx.CurrentPublication.Image_count -" + ctx.CurrentPublication.Image_count.ToString());
                        ctx.SaveCurrentPublication();
                        Log.Info("ctx.SaveCurrentPublication- Saved!");
                        var index = 0;
                        Log.Info("Сохранение файлов в папку проекта и в хранилище данных");
                        foreach (var fileInfo in files)
                        {
                            if (!destinationDirectory.Exists)
                            {
                                destinationDirectory.Create();
                            }
                            var filePath = destinationDirectory + "\\" +ctx.CurrentPublication.Publication_id+
                                           ctx.CurrentPublication.EF_SMI.Smi_descr.Replace('.', '_') +
                                           fileInfo.Name.Replace(" ", "_");
                            fileInfo.MoveTo(filePath);
                            index++;
                            DataHelper.SaveFilesInAzureStorage(filePath);
                            if (index <= 1)
                            {
                                ctx.CurrentPublication.Blob_path = filePath;
                            }
                        }
                        Log.Info("Сохранение файлов в папку проекта и в хранилище данных - ЗАВЕРШЕНО");
                        ctx.UpdateCurrentPublication();
                        Log.Info("ctx.UpdateCurrentPublication(); - ЗАВЕРШЕНО");
                        (ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel =
                            (ctx.ParentViewModel as PublicationViewModel).PageViewModels[0];
                        ((ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel as ViewPublicationViewModel)
                            .ReloadDepartments();
                        ((ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel as ViewPublicationViewModel)
                            .CurrentDepartment = ctx.CurrentDepartment;
                    }
                    else
                    {
                        Log.Info(" ELSE   ViewMode.MakeSnapshot");
                        var destinationDirectory =
                            new DirectoryInfo(_baseDirectory + "\\" +
                                              ctx.CurrentPublication.Project_name + "\\" +
                                              date.Year + "\\" +
                                              date.Month + "\\" +
                                              date.Day);
                        var sourceDirectory = new DirectoryInfo(_tempDirectory);
                        var files = sourceDirectory.GetFiles("*.*");
                        Log.Info("Найдено файлов files -" + files.Count().ToString());
                        var index = 0;

                        ctx.CurrentPublication.Url_path = UrlTextBox.Text;
                        Log.Info("ctx.CurrentPublication.Url_path - " + ctx.CurrentPublication.Url_path);
                        ctx.CurrentPublication.Image_count = files.Count();
                        Log.Info("ctx.CurrentPublication.Image_count -" + ctx.CurrentPublication.Image_count.ToString());
                        ctx.CurrentPublication.Ceation_date = DateTime.Now;
                        Log.Info("ctx.CurrentPublication.Ceation_date- " + ctx.CurrentPublication.Ceation_date.ToString());
                        ctx.SaveCurrentPublication();
                        Log.Info("ctx.SaveCurrentPublication- Saved!");
                        Log.Info("Сохранение файлов в папку проекта и в хранилище данных");
                        foreach (var fileInfo in files)
                        {
                            if (!destinationDirectory.Exists)
                            {
                                destinationDirectory.Create();
                            }
                            var filePath = destinationDirectory + "\\" +ctx.CurrentPublication.Publication_id+
                                           ctx.CurrentPublication.EF_SMI.Smi_descr.Replace('.', '_') +
                                           fileInfo.Name.Replace(" ", "_");
                            fileInfo.CopyTo(filePath);
                            index++;
                            DataHelper.SaveFilesInAzureStorage(filePath);
                            if (index <= 1)
                            {
                                ctx.CurrentPublication.Blob_path = filePath;
                            }
                        }
                        Log.Info("Сохранение файлов в папку проекта и в хранилище данных - ЗАВЕРШЕНО");
                        ctx.UpdateCurrentPublication();
                        Log.Info("ctx.UpdateCurrentPublication(); - ЗАВЕРШЕНО");
                        (ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel =
                            (ctx.ParentViewModel as PublicationViewModel).PageViewModels[0];
                        ((ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel as ViewPublicationViewModel)
                            .ReloadDepartments();
                        ((ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel as ViewPublicationViewModel)
                            .CurrentDepartment = ctx.CurrentDepartment;
                    }
                }
                Log.Info("Конец сохранения в БД!!!!!!!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log.Info("Конец сохранения в БД!!!!!!!! -Exception"+ex);
            }
        }

        private void UrlTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void UrlTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key != Key.Enter) return;
                var ctx = DataContext as NewPublicationViewModel;
                if (ctx == null) return;
                ctx.CurrentUrl = (sender as TextBox).Text;
                GetSnapshot();
                //webBrowser.Navigate(new Uri(ctx.CurrentUrl));
                //var tt=  WebRequest.Create("mail.ru");
                var original = ctx.CurrentUrl;
                if (!original.StartsWith("http:"))
                    original = "http://" + original;
                Uri uri;
                if (!Uri.TryCreate(original, UriKind.Absolute, out uri))
                {
                    //Bad bad bad!
                }
                webBrowser.Navigate(uri);
            }
            catch (Exception ex)
            {
                Logger.TraceError(ex.Message);
            }
        }

        private void GetSnapshot()
        {
            try
            {
                var ctx = DataContext as NewPublicationViewModel;
                var hc = new HtmlCapture();
                hc.HtmlImageCapture +=
                    hc_HtmlImageCapture;
                if (ctx != null)
                {
                    hc.Create(ctx.CurrentUrl);
                }
            }
            catch (Exception ex)
            {
                Logger.TraceError(ex.Message);
            }
        }

        private void hc_HtmlImageCapture(object sender, Uri url, Bitmap image)
        {
            try
            {
                var memoStream = new MemoryStream();
                image.Save(memoStream, ImageFormat.Png);
                var t = RHelper.MemoryStreamToBitmapImage(memoStream);
                var image2 = t;
                img.Source = image2;
            }
            catch (Exception ex)
            {
                Logger.TraceError(ex.Message);
            }
        }

        private string GetUrl(string url)
        {
            var tmp = url.Split('/');
            var rst = ExtractDomainFromURL(url);
            return rst.Replace("www.", string.Empty);
        }

        private string ExtractDomainFromURL(string sURL)
        {
            var rg = new Regex("://(?<host>([a-z\\d][-a-z\\d]*[a-z\\d]\\.)*[a-z][-a-z\\d]+[a-z])");
            return rg.IsMatch(sURL) ? rg.Match(sURL).Result("${host}") : string.Empty;
        }

        private void AddSplitterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var splitterName = SetLastSplitterName();
                var gridBefore = new Grid { Name = splitterName + "before", MinHeight = 20 };
                gridBefore.SizeChanged += gridBefore_SizeChanged;
                DockPanel.SetDock(gridBefore, Dock.Top);

                var gridAfter = new Grid { Name = splitterName + "after", MinHeight = 20 };
                gridAfter.SizeChanged += gridAfter_SizeChanged;
                DockPanel.SetDock(gridAfter, Dock.Top);

                var split = new DockPanelSplitter { Name = splitterName };
                DockPanel.SetDock(split, Dock.Top);
                split.ProportionalResize = true;
                split.Background = Brushes.Black;

                DockPanel.Children.Add(gridBefore);
                DockPanel.Children.Add(split);
                DockPanel.Children.Add(gridAfter);

                //Добавить вертикальную засечку
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.TraceError(ex.Message);
            }
        }

        void gridAfter_SizeChanged(object sender, SizeChangedEventArgs e)
        {
         //  vruler.RefreshVerticalChips(e.NewSize.Height);
        }

        void gridBefore_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        
        }

        private string SetLastSplitterName()
        {
            try
            {
                var lastChildName =
                    DockPanel.Children.OfType<DockPanelSplitter>().OrderBy(item => item.Name).Last().Name;
                var number = int.Parse(lastChildName[lastChildName.Length - 1].ToString(CultureInfo.InvariantCulture)) +
                             1;
                return "TopSplitter" + number;
            }
            catch (Exception)
            {
                return "TopSplitter1";
            }
        }

        private void RemoveSplitterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var removeSplitter = DockPanel.Children.OfType<DockPanelSplitter>().OrderBy(item => item.Name).Last();
                var removableName = removeSplitter.Name;
                var removeGridBefore =
                    DockPanel.Children.OfType<Grid>()
                             .FirstOrDefault(gridBefore => gridBefore.Name == removableName + "before");
                var removeGridAfter =
                    DockPanel.Children.OfType<Grid>()
                             .FirstOrDefault(gridAfter => gridAfter.Name == removableName + "after");

                if (Equals(removeSplitter.Background, Brushes.Red)) return;
                DockPanel.Children.Remove(removeSplitter);
                DockPanel.Children.Remove(removeGridBefore);
                DockPanel.Children.Remove(removeGridAfter);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.TraceError(ex.Message);
            }
        }

        private void Image_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var ctx = DataContext as NewPublicationViewModel;
                var parent = DataHelper.FindAncestor(sender as Image, typeof(RadTileViewItem));
                var parentCtx = (parent as RadTileViewItem).DataContext as DataHelper.ImageTile;
                ctx.OpenInPaint(parentCtx);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.TraceError(ex.Message);
            }

        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var tile = DataHelper.FindAncestor(sender as Image, typeof (RadTileViewItem));
            (tile as RadTileViewItem).TileState = TileViewItemState.Maximized;
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (Keyboard.Modifiers == ModifierKeys.Control && (e.Key == Key.Add) || (e.Key == Key.OemPlus))
                {
                    transform.ScaleX *= 1.25;
                    transform.ScaleY *= 1.25;
                    transform.CenterX = 0;
                    transform.CenterY = 0;
                    hruler.Zoom *= 1.25;
                    vruler.Zoom *= 1.25;
                    vruler.Chip *= 1.25;
                }
                if ((Keyboard.Modifiers != ModifierKeys.Control || (e.Key != Key.Subtract)) && (e.Key != Key.OemMinus))
                    return;
                transform.ScaleX /= 1.25;
                transform.ScaleY /= 1.25;
                transform.CenterX = 0;
                transform.CenterY = 0;
                hruler.Zoom /= 1.25;
                vruler.Zoom /= 1.25;
                vruler.Chip /= 1.25;
            }
            catch (Exception ex)
            {
                Logger.TraceError(ex.Message);
            }
        }

        private void ImportSnapshotButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ctx = DataContext as NewPublicationViewModel;
                ctx.SnapShotMode=ViewMode.ImportSnapshot;
                ctx.ImageTileList=new ObservableCollection<DataHelper.ImageTile>();
                var tempDirectory = new DirectoryInfo(_tempDirectory);
                var dlg = new Microsoft.Win32.OpenFileDialog
                {
                    Multiselect = true,
                    Title = "Select configuration",
                    DefaultExt = ".png",
                   // Filter = "PNG-file (.png)|*.png|JPEG-file (.jpg)|*.jpg|BMP-file (.bmp)|*.bmp",
                   Filter = "BMP|*.bmp|GIF|*.gif|JPG|*.jpg;*.jpeg|PNG|*.png|TIFF|*.tif;*.tiff|"
       + "Все типы изображений|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff",
                    CheckFileExists = true,FilterIndex = 6
                };
                if (dlg.ShowDialog() != true) return;
                var index = 0;
                foreach (var file in tempDirectory.GetFiles("*.*"))
                {
                    file.Delete();
                }
                foreach (var filepath in dlg.FileNames)
                {
                    using (var stream = new FileStream(filepath,FileMode.Open))
                    {
                        var ms = new MemoryStream();
                        stream.CopyTo(ms);
                        var extension = "." + filepath.Split('\\').LastOrDefault().Split('.').LastOrDefault();
                        var newFile = tempDirectory + "\\" + "_" + index + ".png";
                        
                        var tmpBitmap = new Bitmap(ms);
                        var bitmapImage = Bitmap2BitmapImage(tmpBitmap);
                        ctx.ImageTileList.Add(new DataHelper.ImageTile
                        {
                            Image = bitmapImage,
                            ImageName = "_" + index + ".png",
                            ImagePath = newFile
                        });
                        index++;
                        SaveMemoryStream(ms, newFile);
                        ms.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.TraceError(ex.Message);
            }
        }
        public static void SaveMemoryStream(MemoryStream ms, string fileName)
        {
            FileStream outStream = File.OpenWrite(fileName);
            ms.WriteTo(outStream);
            outStream.Flush();
            outStream.Close();
        }

        private double _heigth;
        private double _width;
        private void TopSplitter1before_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            _heigth = e.NewSize.Height;
        }

        private void TopSplitter1_OnLostMouseCapture(object sender, MouseEventArgs e)
        {
             vruler.RefreshVerticalChips(_heigth,21);
        }

        private void LeftSplitter_OnLostMouseCapture(object sender, MouseEventArgs e)
        {
            hruler.RefreshVerticalChips(_width,16);
        }

        private void LeftGrid_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            _width=e.NewSize.Width;
        }
    }
}


