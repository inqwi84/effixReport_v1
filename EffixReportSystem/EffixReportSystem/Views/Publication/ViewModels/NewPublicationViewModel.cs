using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using EffixReportSystem.Helper.Classes;
using EffixReportSystem.Helper.Interfaces;
using Telerik.OpenAccess;

namespace EffixReportSystem.Views.Publication.ViewModels
{
    
    class NewPublicationViewModel : ObservableObject, IPageViewModel
    {
        private readonly EntitiesModel _model = new EntitiesModel();
        public PublicationHelper PublicationHelper;
        private string _currentProjectName;
        public string CurrentProjectName
        {
            get { return _currentProjectName; }
            set
            {
                if (_currentProjectName == value)
                    return;

                _currentProjectName = value;
                this.OnPropertyChanged("CurrentProjectName");
            }
        }

        public List<EF_SMI> Smi { get; set; }
        public List<EF_Tonality> Tonalities { get; set; }
        public List<EF_SMI_Type> SmiTypes { get; set; }
        public List<EF_Exclusivity> Exclusivities { get; set; }
        public List<EF_SMI_priority> Priorities { get; set; }
        public List<EF_Initiated> Initiated { get; set; }
        public List<EF_Planed> Planed { get; set; }
        public List<EF_Photo> Photo { get; set; }

        public string Name { get; private set; }

        private bool _canSave;
        public bool CanSave
        {
            get { return _canSave; }
            set
            {
                if (CanSave == value)
                    return;

                _canSave = value;
                this.OnPropertyChanged("CanSave");
            }
        }
        public void SaveCurrentPublication()
        {
                CurrentPublication.P_year = CurrentPublication.Publication_date.Value.Year.ToString();
                CurrentPublication.P_month = CurrentPublication.Publication_date.Value.Month.ToString();
                CurrentPublication.P_day = CurrentPublication.Publication_date.Value.Day.ToString();
                _model.Add(CurrentPublication);
                _model.SaveChanges();
        }

        private void SetCanSaveOption()
        {
            //try
            //{
            //    CanSave = true;
            //    if (CurrentPublication.Has_photo == null || CurrentPublication.Is_initiated == null ||
            //        CurrentPublication.Is_planed == null || CurrentPublication.Exclusivity == null ||
            //        CurrentPublication.Publication_date == null || CurrentPublication.Smi == null ||
            //        CurrentPublication.Tonality == null) return;
            //    if (CurrentPublication.Project_name.ToLower().Contains("arteks"))
            //    {
            //        CanSave = CurrentPublication.Priority != null;
            //    }
            //    else
            //    {
            //        CanSave = true;
            //    }
            //    CanSave = true;
            //}
            //catch (Exception)
            //{
            //}

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

        private EF_Publication _currentPublication;
        public EF_Publication CurrentPublication
        {
            get
            {
                return this._currentPublication;
            }
            set
            {
                if (CurrentPublication == value)
                    return;

                _currentPublication = value;
                this.OnPropertyChanged("CurrentPublication");
            }
        }

        private BitmapImage _publicationBitmapImage;
        public BitmapImage PublicationBitmapImage
        {
            get { return _publicationBitmapImage; }
            set
            {
                if (Equals(PublicationBitmapImage, value))
                    return;

                _publicationBitmapImage = value;
                this.OnPropertyChanged("PublicationBitmapImage");
            } 
        }
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

        private string _currentUrl;
        public string CurrentUrl
        {
            get { return this._currentUrl; }
            set
            {
                if (CurrentUrl == value)
                    return;

                _currentUrl = value;
                this.OnPropertyChanged("CurrentUrl");
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

        public void OpenInPaint(DataHelper.ImageTile tile)
        {
            CurrentImageTile = tile;
            Process.Start("mspaint", CurrentImageTile.ImagePath);
        }

        public IPageViewModel ParentViewModel;
        public NewPublicationViewModel(IPageViewModel parentViewModel)
        {
                ParentViewModel = parentViewModel;
                CurrentUrl = String.Empty;
                ImageTileList = new ObservableCollection<DataHelper.ImageTile>();
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
                //{
                //    new DataHelper.ImageTile() {ImageName = "1",Image = new BitmapImage(new Uri("c:\\1.png")),ImagePath ="c:\\1.png" },
                //    new DataHelper.ImageTile() {ImageName = "2",Image = new BitmapImage(new Uri("c:\\2.png")),ImagePath ="c:\\2.png" },
                //    new DataHelper.ImageTile() {ImageName = "3",Image = new BitmapImage(new Uri("c:\\3.png")),ImagePath ="c:\\3.png" },
                //    new DataHelper.ImageTile() {ImageName = "4",Image = new BitmapImage(new Uri("c:\\4.png")),ImagePath ="c:\\4.png" },
                //};
        }
    }
}
