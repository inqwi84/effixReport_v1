using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using EffixReportSystem.Helper.Classes;
using EffixReportSystem.Helper.Interfaces;

namespace EffixReportSystem.Views.Publication.ViewModels
{
    internal class ViewPublicationViewModel : ObservableObject, IPageViewModel
    {
        private bool _canEdit;

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

        public PublicationHelper PublicationHelper;
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
        private void GetAllDepartments()
        {
            using (var model = new EntitiesModel())
            {
                _allDepartments = new ObservableCollection<EF_Project>(model.EF_Projects);
                foreach (var dp in _allDepartments)
                {
                    dp.Name = dp.Project_name;
                    dp.Children = new ObservableCollection<Year>() {new Year() {Name = "2012"}};
                    foreach (var year in dp.Children)
                    {
                        year.Children = new ObservableCollection<Month>() {new Month() {Name = "01"}};
                        foreach (var months in year.Children)
                        {
                            months.Children = new ObservableCollection<Day>() {new Day() {Name = "01"}};
                        }
                    }
                }
            }
        }

        private void GetAllPublications()
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
                                var day1 = day;
                                day.Children = new ObservableCollection<EF_Publication>(AllPublications.Where(
                                    item => item.P_day == day1.Name &&
                                            item.P_month == months1.Name &&
                                            item.P_year == year1.Name &&
                                            item.Project_id == dept1.Project_id
                                                                                            ));
                            }
                        }
                    }
                }
            }
        }

        public ViewPublicationViewModel(IPageViewModel parentViewModel)
        {
            ParentViewModel = parentViewModel;
            GetAllDepartments();
            GetAllPublications();
            AllDepartments = _allDepartments;
        }
    }
}

