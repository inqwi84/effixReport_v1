using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EffixReportSystem.Helper.Classes;
using EffixReportSystem.Helper.Interfaces;
using EffixReportSystem.Views.Person.ViewModels;

namespace EffixReportSystem.Views.Person
{
    class PersonViewModel : ObservableObject, IPageViewModel
    {
        public string Name { get { return "Контакты"; } }
        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get { return _currentPageViewModel; }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }
        public PersonViewModel()
        {
            PageViewModels.Add(new ViewPersonViewModel()); //9
            CurrentPageViewModel = PageViewModels[0];
        }
    }
}
