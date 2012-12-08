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
        public PublicationHelper PublicationHelper;


        public ObservableCollection<EF_SMI> Smi { get { return PublicationHelper.Instance.Smi; }}
        public ObservableCollection<EF_Tonality> Tonalities { get { return PublicationHelper.Instance.Tonalities; } }
        public ObservableCollection<EF_SMI_Type> SmiTypes { get { return PublicationHelper.Instance.SmiTypes; }}
        public ObservableCollection<EF_Exclusivity> Exclusivities { get { return PublicationHelper.Instance.Exclusivities; }}
        public ObservableCollection<EF_SMI_priority> Priorities { get { return PublicationHelper.Instance.Priorities; }}
        public ObservableCollection<EF_Dictionary> Initiated { get { return PublicationHelper.Instance.Initiated; }}
        public ObservableCollection<EF_Dictionary> Planed { get { return PublicationHelper.Instance.Planed; }}
        public ObservableCollection<EF_Dictionary> Photo { get { return PublicationHelper.Instance.Photo; }}

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
        private EF_Publication _currentPublication;
        public EF_Publication CurrentPublication
        {
            get { return this._currentPublication; }
            set
            {
                    if (CurrentPublication == value)
                        return;

                    _currentPublication = value;
                    this.OnPropertyChanged("CurrentPublication");
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

        public IPageViewModel ParentViewModel;
        public EditPublicationViewModel(IPageViewModel parentViewModel)
        {
            ParentViewModel = parentViewModel;
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
