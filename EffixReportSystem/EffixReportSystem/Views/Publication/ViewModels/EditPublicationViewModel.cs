using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using EffixReportSystem.Helper.Classes;
using EffixReportSystem.Helper.Interfaces;

namespace EffixReportSystem.Views.Publication.ViewModels
{
    class EditPublicationViewModel:ObservableObject, IPageViewModel
    {
        public string Name { get; private set; }
        private ObservableCollection<DataHelper.ImageTile> _imageTileList;
        public ObservableCollection<DataHelper.ImageTile> ImageTileList
        {
            get { return _imageTileList; }
            set
            {
                if (ImageTileList == value)
                    return;

                _imageTileList = value;
                this.OnPropertyChanged("ImageTileList");
            }
        }
        private DataHelper.ImageTile _currentImageTile;
        public DataHelper.ImageTile CurrentImageTile
        {
            get { return _currentImageTile; }
            set
            {
                if (CurrentImageTile == value)
                    return;

                _currentImageTile = value;
                this.OnPropertyChanged("CurrentImageTile");
            }
        }
        

        public EditPublicationViewModel()
        {
            ImageTileList=new ObservableCollection<DataHelper.ImageTile>
                              {
                                  new DataHelper.ImageTile() {ImageName = "1",Image = new BitmapImage(new Uri("c:\\1.png")),ImagePath ="c:\\1.png" },
                                  new DataHelper.ImageTile() {ImageName = "2",Image = new BitmapImage(new Uri("c:\\2.png")),ImagePath ="c:\\2.png" },
                                  new DataHelper.ImageTile() {ImageName = "3",Image = new BitmapImage(new Uri("c:\\3.png")),ImagePath ="c:\\3.png" },
                                  new DataHelper.ImageTile() {ImageName = "4",Image = new BitmapImage(new Uri("c:\\4.png")),ImagePath ="c:\\4.png" },
                              };
        }
        public void OpenInPaint(DataHelper.ImageTile tile)
        {
            CurrentImageTile = tile;
            Process.Start("mspaint", CurrentImageTile.ImagePath);
        }
    }
}
