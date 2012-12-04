using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using EffixReportSystem.Helper.Classes;
using EffixReportSystem.Helper.Interfaces;

namespace EffixReportSystem.Views.Publication.ViewModels
{
    class ViewPublicationViewModel : ObservableObject, IPageViewModel
    {
        public string Name { get;  set; }

        private ObservableCollection<EF_Project> _allDepartments;
        private ObservableCollection<EF_Publication> _allPublications;
        public ObservableCollection<EF_Project> AllDepartments
        {
            get
            {
                return this._allDepartments;
            }
            set
            {
                if (AllDepartments == value)
                    return;

                _allDepartments = value;
                this.OnPropertyChanged("AllDepartments");
            }
        }
        private void GetAllDepartments()
        {
            using (var model = new EntitiesModel())
            {
                _allDepartments = new ObservableCollection<EF_Project>(model.EF_Projects);
                foreach (var dp in _allDepartments)
                {
                    dp.Children=new ObservableCollection<EF_SMI_Type>(model.EF_SMI_Types);
                    foreach (var smi in dp.Children)
                    {
                        smi.Children=new ObservableCollection<Year>(){new Year(){Name = "2012"}};
                        foreach (var year in smi.Children)
                        {
                            year.Children=new ObservableCollection<Month>(){new Month(){Name = "01"}};
                            foreach (var months in year.Children)
                            {
                                months.Children=new ObservableCollection<Day>(){new Day(){Name = "01"}};
                            }
                        }
                    }
                }
            }
        }
        private void GetAllPublications()
        {
            using (var model = new EntitiesModel())
            {
                _allPublications = new ObservableCollection<EF_Publication>(model.EF_Publications);
                foreach (var dept in _allDepartments)
                {
                    foreach (var years in dept.Children)
                    {
                        foreach (var year in years.Children)
                        {
                            var year1 = year;
                            foreach (var months in year1.Children)
                            {
                                var months1 = months;
                                foreach (var day in months1.Children)
                                {
                                    var day1 = day;
                                    day.Children=new ObservableCollection<EF_Publication>(model.EF_Publications.Where(item=>item.P_day==day1.Name&&item.P_month==months1.Name&&item.P_year==year1.Name));
                                }
                            }
                        }
                    }
                }
            }
        }
        public ViewPublicationViewModel()
        {
            GetAllDepartments();
            GetAllPublications();
            AllDepartments = _allDepartments;
        }
    }
}
