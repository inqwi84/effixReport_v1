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
        private readonly EntitiesModel _model = new EntitiesModel();

        //public ObservableCollection<EF_SMI> Smi { get { return PublicationHelper.Instance.Smi; }}
        //public ObservableCollection<EF_Tonality> Tonalities { get { return PublicationHelper.Instance.Tonalities; } }
        //public ObservableCollection<EF_SMI_Type> SmiTypes { get { return PublicationHelper.Instance.SmiTypes; }}
        //public ObservableCollection<EF_Exclusivity> Exclusivities { get { return PublicationHelper.Instance.Exclusivities; }}
        //public ObservableCollection<EF_SMI_priority> Priorities { get { return PublicationHelper.Instance.Priorities; }}
        //public ObservableCollection<EF_Initiated> Initiated { get { return PublicationHelper.Instance.Initiated; }}
        //public ObservableCollection<EF_Planed> Planed { get { return PublicationHelper.Instance.Planed; }}
        //public ObservableCollection<EF_Photo> Photo { get { return PublicationHelper.Instance.Photo; }}

        public List<EF_SMI> Smi { get; set; }
        public List<EF_Tonality> Tonalities { get; set; }
        public List<EF_SMI_Type> SmiTypes { get; set; }
        public List<EF_Exclusivity> Exclusivities { get; set; }
        public List<EF_SMI_priority> Priorities { get; set; }
        public List<EF_Initiated> Initiated { get; set; }
        public List<EF_Planed> Planed { get; set; }
        public List<EF_Photo> Photo { get; set; }

        public void SetCurrentPublication(long publicationId)
        {
            CurrentPublication = _model.EF_Publications.FirstOrDefault(item => item.Publication_id == publicationId);
        }

        public void SaveCurrentPublication()
        {
            _model.SaveChanges();
        }

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

        private EF_Project _currentProject;
        public EF_Project CurrentProject
        {
            get { return this._currentProject; }
            set
            {
                if (CurrentProject == value)
                    return;

                _currentProject = value;
                this.OnPropertyChanged("CurrentProject");
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
            Smi = new List<EF_SMI>(_model.EF_SMIs);
            Tonalities = new List<EF_Tonality>(_model.EF_Tonalities);
            SmiTypes = new List<EF_SMI_Type>(_model.EF_SMI_Types);
            Exclusivities = new List<EF_Exclusivity>(_model.EF_Exclusivities);
            Priorities = new List<EF_SMI_priority>(_model.EF_SMI_priorities);
            Initiated =
                new List<EF_Initiated>(_model.EF_Initiateds);
            Planed =
                new List<EF_Planed>(
                    _model.EF_Planeds);
            Photo =
                new List<EF_Photo>(
                    _model.EF_Photos);
        }
        public void OpenInPaint(DataHelper.ImageTile tile)
        {
            CurrentImageTile = tile;
            Process.Start("mspaint", CurrentImageTile.ImagePath);
        }
    }
}
