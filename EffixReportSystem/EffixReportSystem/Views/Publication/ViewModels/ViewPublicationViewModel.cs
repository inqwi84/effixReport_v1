using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using CommonLibraries.Log;
using EffixReportSystem.Helper.Classes;
using EffixReportSystem.Helper.Interfaces;

namespace EffixReportSystem.Views.Publication.ViewModels
{
    internal class ViewPublicationViewModel : ObservableObject, IPageViewModel
    {
        public enum FilterMode
        {
            ProjectMode,
            YearMode,
            MonthMode,
            DayMode
        }

        private ObservableCollection<EF_Department> _departments;
        public ObservableCollection<EF_Department> Departments
        {
            get { return _departments; }
            set
            {
                if (Departments == value)
                    return;

                _departments = value;
                this.OnPropertyChanged("Departments");
            }
        }

        private  EF_Department _currentDepartment;
        public EF_Department CurrentDepartment
        {
            get { return _currentDepartment; }
            set
            {
                if (value != null)
                {
                    if (CurrentDepartment == value)
                        return;

                    _currentDepartment = value;
                    try
                    {
                        Task.Factory.StartNew(() =>
                                              PublicationList =
                                              DataHelper.GetPublicationByDepartmentId(_currentDepartment.Department_id));
                    }
                    catch (Exception ex)
                    {
                        Logger.TraceError(ex.Message);
                    }

                    this.OnPropertyChanged("CurrentDepartament");
                }
            }
        }


        private Year _currentYear;
        public Year CurrentYear
        {
            get { return _currentYear; }
            set
            {
                if (_currentYear == value)
                    return;

                _currentYear = value;
                this.OnPropertyChanged("CurrentYear");
            }
        }

        private Month _currentMonth;
        public Month CurrentMonth
        {
            get { return _currentMonth; }
            set
            {
                if (_currentMonth == value)
                    return;

                _currentMonth = value;
                this.OnPropertyChanged("CurrentMonth");
            }
        }

        private Day _currentDay;
        public Day CurrentDay
        {
            get { return _currentDay; }
            set
            {
                if (_currentDay == value)
                    return;

                _currentDay = value;
                this.OnPropertyChanged("CurrentDay");
            }
        }
        private FilterMode _currentMode;
        public FilterMode CurrentMode
        {
            get { return _currentMode; }
            set
            {
                if (_currentMode == value)
                    return;

                _currentMode = value;
                this.OnPropertyChanged("CurrentMode");
            }
        }

        private bool _canEdit;

        private string _filterString;
        public string FilterString
        {
            get { return _filterString; }
            set
            {
                if (_filterString == value)
                    return;

                _filterString = value;
                Filter();
                this.OnPropertyChanged("FilterString");
            }
        }

        private void Filter()
        {
            try
            {
                if (_filterString == null)
                    return;
                Task.Factory.StartNew(() =>
                {
                    PublicationList = new ObservableCollection<EF_Publication>();
                    var coll =
                        CollectionViewSource.GetDefaultView(DataHelper.GetPublicationByDepartmentId(CurrentDepartment.Department_id));
                    coll.Filter = NameFilter;
                    PublicationList =
                        new ObservableCollection<EF_Publication>(coll.Cast<EF_Publication>());
                });
            }
            catch (Exception)
            {

            }



        }

        public bool NameFilter(object item)
        {
            try
            {
                var publication = item as EF_Publication;
                if (publication != null)
                {
                    return
                        ((publication.Project_name.ToLower().Split().Any(
                            word => word.StartsWith(FilterString.ToLower().TrimStart()))) ||
                         (publication.Publication_name.ToLower().Split().Any(
                             word => word.StartsWith(FilterString.ToLower().TrimStart()))) ||
                         (publication.P_month.ToLower().Split().Any(
                             word => word.StartsWith(FilterString.ToLower().TrimStart()))) ||
                         (publication.P_day.ToLower().Split().Any(
                             word => word.StartsWith(FilterString.ToLower().TrimStart()))));
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception)
            {
                return false;
            }
        }

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

        private EF_Project _currentProject;
        public EF_Project CurrentProject
        {
            get { return _currentProject; }
            set
            {
                if (_currentProject == value)
                    return;

                _currentProject = value;
                this.OnPropertyChanged("CurrentProject");
            }
        }

       // public PublicationHelper PublicationHelper;
        public string Name { get; set; }

        private ObservableCollection<EF_Project> _allDepartments;
        public ObservableCollection<EF_Publication> AllPublications;
        private ObservableCollection<EF_Publication>_publicationList;
        private EF_Publication _currentPublication;

        public bool CanEdit
        {
            get { return this._canEdit; }
            set
            {
                if (CanEdit == value)
                    return;

                _canEdit = value;
                this.OnPropertyChanged("CanEdit");
            }
        }

        public EF_Publication CurrentPublication
        {
            get { return this._currentPublication; }
            set
            {
                if (value==null)
                {
                    CanEdit = false;
                    _currentPublication = null;
                    this.OnPropertyChanged("CurrentPublication");
                }
                else
                {
                    if (CurrentPublication == value)
                        return;
                    CanEdit = true;
                    _currentPublication = value;
                    this.OnPropertyChanged("CurrentPublication");
                }

            }
        }
        public ObservableCollection<EF_Publication> PublicationList
        {
            get { return this._publicationList; }
            set
            {
                if (PublicationList == value)
                    return;

                _publicationList = value;
                this.OnPropertyChanged("PublicationList");
            }
        }


        public IPageViewModel ParentViewModel;


        private void Ctor()
        {
            using (var model = new EntitiesModel())
            {
                var hierarchicalList = model.EF_Departments.ToList().Select(flatItem =>
                                                                            new EF_Department
                                                                                {
                                                                                    Department_project_id = flatItem.Department_project_id,
                                                                                    Department_type = flatItem.Department_type,
                                                                                    Department_name
                                                                                        =
                                                                                        flatItem
                                                                                        .
                                                                                        Department_name,
                                                                                        Department_description = flatItem.Department_description,

                                                                                    Department_id
                                                                                        =
                                                                                        flatItem
                                                                                        .
                                                                                        Department_id,
                                                                                    Department_parent_id
                                                                                        =
                                                                                        flatItem
                                                                                        .
                                                                                        Department_parent_id,
                                                                                }).ToList();


                Departments =
                    new ObservableCollection<EF_Department>(
                        hierarchicalList.GroupJoin(hierarchicalList,
                                                   parentItem =>
                                                   parentItem.Department_id,
                                                   childItem =>
                                                   childItem.Department_parent_id,
                                                   (parent, children) =>
                                                   {
                                                       parent.Children
                                                           =
                                                           children.
                                                               ToList();
                                                       return parent;
                                                   }).Where(
                                                           item =>
                                                           item.Department_parent_id ==
                                                           null).ToList());
            }
        }
          public void RemoveCurrentPublication()
          {

              var id = CurrentPublication.Publication_id;
                  Task.Factory.StartNew(() =>
                      {
                          using (var model = new EntitiesModel())
                          {
                              var rem =
                                  model.EF_Publications.FirstOrDefault(
                                      item => item.Publication_id == id);
                              model.Delete(rem);
                              model.SaveChanges();
                          }
                      });
                  PublicationList.Remove(
                      PublicationList.FirstOrDefault(item => item.Publication_id == CurrentPublication.Publication_id));
          }

        public void ReloadDepartments()
        {
             using (var model = new EntitiesModel())
            {
                var hierarchicalList = model.EF_Departments.ToList().Select(flatItem =>
                                                                            new EF_Department
                                                                                { 
                                                                                    Department_project_id = flatItem.Department_project_id,
                                                                                    Department_type = flatItem.Department_type,
                                                                                    Department_name
                                                                                        =
                                                                                        flatItem
                                                                                        .
                                                                                        Department_name,
                                                                                        Department_description = flatItem.Department_description,

                                                                                    Department_id
                                                                                        =
                                                                                        flatItem
                                                                                        .
                                                                                        Department_id,
                                                                                    Department_parent_id
                                                                                        =
                                                                                        flatItem
                                                                                        .
                                                                                        Department_parent_id,
                                                                                }).ToList();


                Departments =
                    new ObservableCollection<EF_Department>(
                        hierarchicalList.GroupJoin(hierarchicalList,
                                                   parentItem =>
                                                   parentItem.Department_id,
                                                   childItem =>
                                                   childItem.Department_parent_id,
                                                   (parent, children) =>
                                                   {
                                                       parent.Children
                                                           =
                                                           children.
                                                               ToList();
                                                       return parent;
                                                   }).Where(
                                                           item =>
                                                           item.Department_parent_id ==
                                                           null).ToList());

            }
        }
        public ViewPublicationViewModel(IPageViewModel parentViewModel)
        {
            Ctor();
            ParentViewModel = parentViewModel;
        }
    }
}

