using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
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

        private  EF_Department _currentDepartament;
        public EF_Department CurrentDepartament
        {
            get { return _currentDepartament; }
            set
            {
                if (CurrentDepartament == value)
                    return;

                _currentDepartament = value;
                try
                {
                    PublicationList =
    DataHelper.GetPublicationByDepartmentId(_currentDepartament.Department_id);
                }
                catch (Exception)
                {
                }

                this.OnPropertyChanged("CurrentDepartament");
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
            if (_filterString == null)
                return;
            Task.Factory.StartNew(() =>
            {
                PublicationList = new ObservableCollection<EF_Publication>();
                var coll =
                    CollectionViewSource.GetDefaultView(DataHelper.GetPublicationByDepartmentId(CurrentDepartament.Department_id));
                coll.Filter = NameFilter;
                PublicationList =
                    new ObservableCollection<EF_Publication>(coll.Cast<EF_Publication>());
            });


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

        public ObservableCollection<EF_Project> AllDepartments
        {
            get { return this._allDepartments; }
            set
            {
                if (AllDepartments == value)
                    return;

                _allDepartments = value;
                this.OnPropertyChanged("AllDepartments");
            }
        }

        public IPageViewModel ParentViewModel;
        public void GetAllDepartments()
        {
            using (var model = new EntitiesModel())
            {
                _allDepartments = new ObservableCollection<EF_Project>(model.EF_Projects);
                foreach (var dp in _allDepartments)
                {
                    var dp1 = dp;
                    dp.Name = dp1.Project_name;
                    dp.Children=new ObservableCollection<Year>();
                    var years= model.EF_Publications.Where(item => item.Project_id == dp1.Project_id).GroupBy(item=>item.P_year);
                    foreach (var year in years)
                    {
                        dp.Children.Add(new Year { Name = year.Key, Parent = dp, Children = new ObservableCollection<Month>() });

                    }
                    foreach (var year in dp.Children)
                    {
                        var year1 = year;
                        var months = model.EF_Publications.Where(item => item.Project_id == dp1.Project_id&&item.P_year==year1.Name).GroupBy(item => item.P_month);
                        foreach (var month in months)
                        {
                            year.Children.Add(new Month { Name = month.Key ,Parent = year,Children = new ObservableCollection<Day>() });
                        }
                        foreach (var month in year.Children)
                        {
                            var month1 = month;
                            var days = model.EF_Publications.Where(item => item.Project_id == dp1.Project_id && item.P_year == year1.Name&&item.P_month==month1.Name).GroupBy(item => item.P_day);
                            foreach (var day in days)
                            {
                                month.Children.Add( new Day { Name = day.Key,Parent = month});
                            }
                        }

                    }
                }
            }
        }

        public void Ctor()
        {
            using (var model = new EntitiesModel())
            {
                var hierarchicalList = model.EF_Departments.ToList().Select(flatItem =>
                                                                            new EF_Department
                                                                                {
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

        public void GetAllPublications()
        {
            using (var model = new EntitiesModel())
            {
                AllPublications = new ObservableCollection<EF_Publication>(model.EF_Publications);
                foreach (var dept in _allDepartments)
                {
                    var dept1 = dept;
                    foreach (var year in dept1.Children)
                    {
                        year.Parent = dept1;
                        var year1 = year;
                        foreach (var months in year1.Children)
                        {
                            months.Parent = year1;
                            var months1 = months;
                            foreach (var day in months1.Children)
                            {
                                day.Parent = months1;
                            }
                        }
                    }
                }
            }
        }

        public ViewPublicationViewModel(IPageViewModel parentViewModel)
        {
            Ctor();
            ParentViewModel = parentViewModel;
            //GetAllDepartments();
           // GetAllPublications();
           // AllDepartments = _allDepartments;
        }
    }
}

