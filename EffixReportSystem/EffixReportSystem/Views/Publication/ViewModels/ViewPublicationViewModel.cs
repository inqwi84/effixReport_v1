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
        private void GetAllDepartments()
        {
            using (var model = new EntitiesModel())
            {
                _allDepartments = new ObservableCollection<EF_Project>(model.EF_Projects);
            }
        }
        private void GetAllPublications()
        {
            using (var model = new EntitiesModel())
            {
                _allPublications = new ObservableCollection<EF_Publication>(model.EF_Publications);
                var tfirst = _allPublications.GroupBy(item => item.Project_id);
                var tsecond = _allPublications.GroupBy(item => item.Project_id);
                foreach (var dept in _allDepartments)
                {
                    foreach (var years in dept.Children)
                    {
                        foreach (var year in years.Children)
                        {
                            foreach (var months in year.Children)
                            {
                                foreach (var day in months.Children)
                                {
                                    var day1 = day;
                                    day.Children=new ObservableCollection<EF_Publication>(model.EF_Publications.Where(item=>item.P_day==day1.Name));
                                }
                            }
                        }
                    }
                }
                //foreach (var pb in tfirst)
                //{
                //    foreach (var department in _allDepartments)
                //    {
                //        if(pb.Key==department.Project_id)
                //        {
                //            var years = pb.GroupBy(item => item.P_year);
                //            foreach (var year in years)
                //            {
                //                if(year.Key)
                //            }
                //            foreach (var efPublication in pb)
                //            {
                //                efPublication.
                //            }
                //        }
                //    }
                    
                //}

            }
        }
        public ViewPublicationViewModel()
        {
            GetAllPublications();
        }
    }
}
