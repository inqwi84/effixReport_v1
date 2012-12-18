﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EffixReportSystem.Helper.Classes;
using EffixReportSystem.Views.Publication.ViewModels;
using Telerik.OpenAccess;
using Telerik.Windows.Controls;
using Brushes = System.Windows.Media.Brushes;
using Image = System.Windows.Controls.Image;
using Point = System.Windows.Point;

namespace EffixReportSystem.Views.Publication.Views
{
    /// <summary>
    /// Interaction logic for NewPublicationView.xaml
    /// </summary>
    public partial class NewPublicationView : UserControl
    {
        private string _tempDirectory=String.Empty;
        private string _baseDirectory = String.Empty;
        public NewPublicationView()
        {
            InitializeComponent();
            _tempDirectory = "c:\\storage\\temp";
            _baseDirectory = "c:\\storage";

        }

        private void ClearSearchTextBox(object sender, RoutedEventArgs e)
        {

        }

        private void ClearDirectory(string dirPath)
        {
            var directory = new DirectoryInfo(dirPath);
            var files=directory.GetFiles("*.*");
            foreach (var fileInfo in files)
            {
                fileInfo.Delete();
            }
        }
        private void MakeSnaphotsButton_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as NewPublicationViewModel;
            ctx.ImageTileList=new ObservableCollection<DataHelper.ImageTile>();
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

            var image = BitmapImage2Bitmap((BitmapImage) img.Source);
            for (int i = 0; i < pointsList.Count - 1; i++)
            {
                CropImage(image,
                          new System.Drawing.Rectangle(
                              (int)pointsList[i].X,
                              (int)pointsList[i].Y,
                              (int)tmp.ActualWidth,
                              (int)(pointsList[i + 1].Y - pointsList[i].Y)), i.ToString(CultureInfo.InvariantCulture), _tempDirectory);
            }
        }

        private void CropImage(Bitmap img, System.Drawing.Rectangle cropArea, string index,string dirPath)
        {
            try
            {
                var ctx = DataContext as NewPublicationViewModel;
                var bmpCrop = img.Clone(cropArea, img.PixelFormat);


                var memoStream = new MemoryStream();
                bmpCrop.Save(memoStream, ImageFormat.Png);
                var image2 = RHelper.MemoryStreamToBitmapImage(memoStream);

                var tile = new DataHelper.ImageTile {Image = image2, ImageName = "_"+index + ".png"};
                tile.ImagePath = _tempDirectory + "\\" + tile.ImageName;
                ctx.ImageTileList.Add(tile);
                bmpCrop.Save(tile.ImagePath, ImageFormat.Png);
            }
            catch (Exception)
            {

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
            // BitmapImage bitmapImage = new BitmapImage(new Uri("../Images/test.png", UriKind.Relative));

            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                // return bitmap; <-- leads to problems, stream is closed/closing ...
                return new Bitmap(bitmap);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as NewPublicationViewModel;
            (ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel = (ctx.ParentViewModel as PublicationViewModel).PageViewModels[0];
            ((ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel as ViewPublicationViewModel).GetAllDepartments();
            ((ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel as ViewPublicationViewModel).GetAllPublications();
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as NewPublicationViewModel;
            var date = ctx.CurrentPublication.Publication_date.Value;
            // проверка, если больше нет такой записи в БД!
            var alreadyExists = DataHelper.CheckIfPublicationExists((long) ctx.CurrentPublication.Project_id,
                                                                     ctx.CurrentPublication.EF_SMI.Smi_id,
                                                                     date.Year,
                                                                     date.Month,
                                                                     date.Day);
            //Существует
            if (alreadyExists)
            {
                var dialogResult =
                    MessageBox.Show("Публикация с данными параметрами уже существует. Сохранить публикацию как новую?","Сохранение",MessageBoxButton.YesNoCancel);

                switch (dialogResult)
                {
                    case MessageBoxResult.Yes:
                                      var destinationDirectory =
                    new DirectoryInfo(_baseDirectory + "\\" +
                                      ctx.CurrentProjectName + "\\" +
                                      date.Year + "\\" +
                                      date.Month + "\\" +
                                      date.Day);


                var sourceDirectory = new DirectoryInfo(_tempDirectory);

                var files = sourceDirectory.GetFiles("*.*");

                var index = 0;
                foreach (var fileInfo in files)
                {
                    if (!destinationDirectory.Exists)
                    {
                        destinationDirectory.Create();
                    }
                    var filePath = destinationDirectory + "(v2)"+
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
                ctx.CurrentPublication.Image_count = index;
                ctx.SaveCurrentPublication();
                (ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel =
                    (ctx.ParentViewModel as PublicationViewModel).PageViewModels[0];
                ((ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel as ViewPublicationViewModel).
                    GetAllDepartments();
                ((ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel as ViewPublicationViewModel).
                    GetAllPublications();
                        break;
                    case MessageBoxResult.No:
                      //  this.Close();
                        break;
                }
            }
            else
            {
                var destinationDirectory =
                    new DirectoryInfo(_baseDirectory + "\\" +
                                      ctx.CurrentProjectName + "\\" +
                                      date.Year + "\\" +
                                      date.Month + "\\" +
                                      date.Day);


                var sourceDirectory = new DirectoryInfo(_tempDirectory);

                var files = sourceDirectory.GetFiles("*.*");

                var index = 0;
                foreach (var fileInfo in files)
                {
                    if (!destinationDirectory.Exists)
                    {
                        destinationDirectory.Create();
                    }
                    var filePath = destinationDirectory + "\\" +
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
                ctx.CurrentPublication.Image_count = index;
                ctx.SaveCurrentPublication();
                (ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel =
                    (ctx.ParentViewModel as PublicationViewModel).PageViewModels[0];
                ((ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel as ViewPublicationViewModel).
                    GetAllDepartments();
                ((ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel as ViewPublicationViewModel).
                    GetAllPublications();
            }
        }

        private void UrlTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void UrlTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            var ctx = DataContext as NewPublicationViewModel;
            if (ctx != null)
            {
                ctx.CurrentUrl = (sender as TextBox).Text;
                GetSnapshot();
            }
        }
        private void GetSnapshot()
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

        private void hc_HtmlImageCapture(object sender, Uri url, Bitmap image)
        {
            var memoStream = new MemoryStream();
            image.Save(memoStream, ImageFormat.Png);
            var t = RHelper.MemoryStreamToBitmapImage(memoStream);
            var image2 = t;

            //(DataContext as ImageEditControlViewModel).UrlImageSource = image2;
            //ImageEditor.img.Source = image2;
            img.Source = image2;
        }

        private string GetUrl(string url)
        {
            var tmp = url.Split('/');
            var rst = ExtractDomainFromURL(url);
            return rst.Replace("www.", string.Empty);
        }
        private string ExtractDomainFromURL(string sURL)
        {

            Regex rg = new Regex("://(?<host>([a-z\\d][-a-z\\d]*[a-z\\d]\\.)*[a-z][-a-z\\d]+[a-z])");

            if (rg.IsMatch(sURL))
            {
                return rg.Match(sURL).Result("${host}");
            }
            else
            {
                return string.Empty;
            }
        }

        private void AddSplitterButton_Click(object sender, RoutedEventArgs e)
        {
            var splitterName = SetLastSplitterName();
            var gridBefore = new Grid { Name = splitterName + "before", MinHeight = 20 };
            DockPanel.SetDock(gridBefore, Dock.Top);

            var gridAfter = new Grid { Name = splitterName + "after", MinHeight = 20 };
            DockPanel.SetDock(gridAfter, Dock.Top);

            var split = new DockPanelSplitter { Name = splitterName };
            DockPanel.SetDock(split, Dock.Top);
            split.ProportionalResize = true;
            split.Background = Brushes.Black;

            DockPanel.Children.Add(gridBefore);
            DockPanel.Children.Add(split);
            DockPanel.Children.Add(gridAfter);
        }

        private string SetLastSplitterName()
        {
            try
            {
                var lastChildName = DockPanel.Children.OfType<DockPanelSplitter>().OrderBy(item => item.Name).Last().Name;
                var number = int.Parse(lastChildName[lastChildName.Length - 1].ToString(CultureInfo.InvariantCulture)) + 1;
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
                var removeGridBefore = DockPanel.Children.OfType<Grid>().FirstOrDefault(gridBefore => gridBefore.Name == removableName + "before");
                var removeGridAfter = DockPanel.Children.OfType<Grid>().FirstOrDefault(gridAfter => gridAfter.Name == removableName + "after");

                if (!Equals(removeSplitter.Background, Brushes.Red))
                {
                    DockPanel.Children.Remove(removeSplitter);
                    DockPanel.Children.Remove(removeGridBefore);
                    DockPanel.Children.Remove(removeGridAfter);
                }
            }
            catch (Exception)
            {

            }
        }

        private void Image_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var ctx = DataContext as NewPublicationViewModel;
            var parent = DataHelper.FindAncestor(sender as Image, typeof(RadTileViewItem));
            var parentCtx = (parent as RadTileViewItem).DataContext as DataHelper.ImageTile;
            ctx.OpenInPaint(parentCtx);
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var tile = DataHelper.FindAncestor(sender as Image, typeof(RadTileViewItem));
            (tile as RadTileViewItem).TileState = TileViewItemState.Maximized;
        }
    }
    }

