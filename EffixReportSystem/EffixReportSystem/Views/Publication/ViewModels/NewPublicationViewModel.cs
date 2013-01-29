using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using EffixReportSystem.Helper.Classes;
using EffixReportSystem.Helper.Classes.Enums;
using EffixReportSystem.Helper.Interfaces;
using Telerik.OpenAccess;

namespace EffixReportSystem.Views.Publication.ViewModels
{
    
    class NewPublicationViewModel : ObservableObject, IPageViewModel
    {

        private ViewMode _snapShotMode;
        public ViewMode SnapShotMode { get; set; }
       private readonly EntitiesModel model = new EntitiesModel();
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

        private EF_Department _currentDepartment;
        public EF_Department CurrentDepartment
        {
            get { return _currentDepartment; }
            set
            {
                if (CurrentDepartment == value)
                    return;

                _currentDepartment = value;
                this.OnPropertyChanged("CurrentDepartment");
            }
        }


        public List<EF_SMI> Smi { set;get; }

        public List<EF_SMI> SmiList;
        public List<EF_Tonality> Tonalities { get; set; }
        public List<EF_SMI_Type> SmiTypes { get; set; }
        public List<EF_Exclusivity> Exclusivities { get; set; }
        public List<EF_SMI_priority> Priorities { get; set; }
        public List<EF_Initiated> Initiated { get; set; }
        public List<EF_Planed> Planed { get; set; }
        public List<EF_Photo> Photo { get; set; }

        public string Name { get; private set; }
        public void RefreshSmiList()
        {
            try
            {
            }
            catch (Exception)
            {

            }

        }
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

                CurrentPublication.Project_id =
                    model.EF_Projects.FirstOrDefault(item => item.Project_descr == CurrentPublication.Project_name).
                           Project_id;

                //CurrentPublication.EF_Project =
                //    _model.EF_Projects.FirstOrDefault(item => item.Project_id == CurrentDepartment.Department_project_id);

                CurrentPublication.Name = CurrentPublication.EF_SMI.Smi_descr.Replace('.', '_') + "_" +
                                          CurrentPublication.P_year + "_" + CurrentPublication.P_month + "_" +
                                          CurrentPublication.P_day;
                //  CurrentPublication.Project_name = CurrentPublication.EF_Project.Project_descr;
                // CurrentPublication.Publication_name = CurrentPublication.EF_SMI.Smi_descr;

                CurrentPublication.Url_path = CurrentUrl;
                var mainDept =
                    model.EF_Departments.FirstOrDefault(
                        dept => dept.Department_project_id == CurrentPublication.Project_id);
                var yearDept =
                    model.EF_Departments.FirstOrDefault(
                        dept =>
                        dept.Department_name.Trim() == CurrentPublication.P_year.Trim() &&
                        dept.Department_parent_id == mainDept.Department_id);
                if (yearDept == null)
                {
                    yearDept = new EF_Department
                        {
                            Department_type = "year",
                            Department_name = CurrentPublication.P_year,
                            Department_description = CurrentPublication.P_year + " год",
                            Department_parent_id = mainDept.Department_id,
                            Department_id = +model.EF_Departments.Max(item => item.Department_id) + 1
                        };
                    model.Add(yearDept);
                    model.SaveChanges();
                }
                var monthDept =
                    model.EF_Departments.FirstOrDefault(
                        dept =>
                        dept.Department_name.Trim() == CurrentPublication.P_month.Trim() &&
                        dept.Department_parent_id == yearDept.Department_id);
                if (monthDept == null)
                {
                    monthDept = new EF_Department
                        {
                            Department_type = "month",
                            Department_name = CurrentPublication.P_month,
                            Department_parent_id = yearDept.Department_id,
                            Department_id = +model.EF_Departments.Max(item => item.Department_id) + 1
                        };
                    model.Add(monthDept);
                    model.SaveChanges();
                }
                var dayDepth = model.EF_Departments.FirstOrDefault(
                    dept =>
                    dept.Department_name.Trim() == CurrentPublication.P_day.Trim() &&
                    dept.Department_parent_id == monthDept.Department_id);
                if (dayDepth == null)
                {
                    dayDepth = new EF_Department
                        {
                            Department_type = "day",
                            Department_name = CurrentPublication.P_day,
                            Department_parent_id = monthDept.Department_id,
                            Department_id = +model.EF_Departments.Max(item => item.Department_id) + 1
                        };
                    model.Add(dayDepth);
                    model.SaveChanges();
                }
                CurrentPublication.Department_id = dayDepth.Department_id;
                model.Add(CurrentPublication);
                model.SaveChanges();
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
            Process.Start(Properties.Settings.Default.PhotoshopExecutable, CurrentImageTile.ImagePath);
        }

        public IPageViewModel ParentViewModel;
        public NewPublicationViewModel(IPageViewModel parentViewModel)
        {
                ParentViewModel = parentViewModel;
                CurrentUrl = String.Empty;
                ImageTileList = new ObservableCollection<DataHelper.ImageTile>();
               // Smi = new List<EF_SMI>(_model.EF_SMIs);
              //  Tonalities = new List<EF_Tonality>(_model.EF_Tonalities);
              //  SmiTypes = new List<EF_SMI_Type>(_model.EF_SMI_Types);
               // Exclusivities = new List<EF_Exclusivity>(_model.EF_Exclusivities);
              //  Priorities = new List<EF_SMI_priority>(_model.EF_SMI_priorities);
              //  Initiated =new List<EF_Initiated>(_model.EF_Initiateds);
               // Planed =new List<EF_Planed>(_model.EF_Planeds);
               // Photo =new List<EF_Photo>(_model.EF_Photos);
           // _model.Dispose();
            //{
            //    new DataHelper.ImageTile() {ImageName = "1",Image = new BitmapImage(new Uri("c:\\1.png")),ImagePath ="c:\\1.png" },
            //    new DataHelper.ImageTile() {ImageName = "2",Image = new BitmapImage(new Uri("c:\\2.png")),ImagePath ="c:\\2.png" },
            //    new DataHelper.ImageTile() {ImageName = "3",Image = new BitmapImage(new Uri("c:\\3.png")),ImagePath ="c:\\3.png" },
            //    new DataHelper.ImageTile() {ImageName = "4",Image = new BitmapImage(new Uri("c:\\4.png")),ImagePath ="c:\\4.png" },
            //};
        }
        public void GetData()
        {
                try
                {
                    Smi = new List<EF_SMI>(model.EF_SMIs);
                    Tonalities = new List<EF_Tonality>(model.EF_Tonalities);
                    SmiTypes = new List<EF_SMI_Type>(model.EF_SMI_Types);
                    Exclusivities = new List<EF_Exclusivity>(model.EF_Exclusivities);
                    Priorities = new List<EF_SMI_priority>(model.EF_SMI_priorities);
                    Initiated = new List<EF_Initiated>(model.EF_Initiateds);
                    Planed = new List<EF_Planed>(model.EF_Planeds);
                    Photo = new List<EF_Photo>(model.EF_Photos);
                }
                catch (Exception)
                {

                }

            }

        }
    }
