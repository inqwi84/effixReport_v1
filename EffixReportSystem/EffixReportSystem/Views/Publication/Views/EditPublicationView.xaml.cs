using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommonLibraries.Log;
using EffixReportSystem.Helper.Classes;
using EffixReportSystem.Helper.Classes.Enums;
using EffixReportSystem.Views.Publication.ViewModels;
using Telerik.Windows.Controls;
using Image = System.Windows.Controls.Image;

namespace EffixReportSystem.Views.Publication.Views
{
    /// <summary>
    /// Interaction logic for EditPublicationView.xaml
    /// </summary>
    public partial class EditPublicationView : UserControl
    {
        private readonly string _tempDirectory = Properties.Settings.Default.TempDirectory;
        private readonly string _baseDirectory = Properties.Settings.Default.BaseDirectory;

        public EditPublicationView()
        {
            InitializeComponent();
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as EditPublicationViewModel;
            //проверить, если изменилась дата публикации
            if (ctx.CurrentPublication.Publication_date != ctx.GetOldPublicationDate())
            {
                ctx.DeleteBlobFiles();

            }
            try
            {
                ctx.SaveCurrentPublication();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.TraceError(ex.Message);
            }
            (ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel = (ctx.ParentViewModel as PublicationViewModel).PageViewModels[0];
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as EditPublicationViewModel;
            try
            {
                ctx.RestoreDefaultVlues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.TraceError(ex.Message);
            }
            (ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel = (ctx.ParentViewModel as PublicationViewModel).PageViewModels[0];
        }

        private void Image_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var ctx = DataContext as EditPublicationViewModel;
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
            var tile = DataHelper.FindAncestor(sender as Image, typeof(RadTileViewItem));
            (tile as RadTileViewItem).TileState=TileViewItemState.Maximized;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ctx = DataContext as EditPublicationViewModel;
                if (ctx.ImageTileList == null)
                {
                    ctx.ImageTileList = new ObservableCollection<DataHelper.ImageTile>();
                }
                
                var tempDirectory = new DirectoryInfo(_tempDirectory);
                var dlg = new Microsoft.Win32.OpenFileDialog
                {
                    Multiselect = true,
                    Title = "Select configuration",
                    DefaultExt = ".png",
                    // Filter = "PNG-file (.png)|*.png|JPEG-file (.jpg)|*.jpg|BMP-file (.bmp)|*.bmp",
                    Filter = "BMP|*.bmp|GIF|*.gif|JPG|*.jpg;*.jpeg|PNG|*.png|TIFF|*.tif;*.tiff|"
        + "Все типы изображений|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff",
                    CheckFileExists = true,
                    FilterIndex = 6
                };
                if (dlg.ShowDialog() != true) return;
                var index = tempDirectory.GetFiles("*.*").Count();
                foreach (var filepath in dlg.FileNames)
                {
                    using (var stream = new FileStream(filepath, FileMode.Open))
                    {
                        var ms = new MemoryStream();
                        stream.CopyTo(ms);
                        var newFile = tempDirectory + "\\" + index + ".png";

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
                ctx.CurrentPublication.Image_count = index;
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
    }
}
