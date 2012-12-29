using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EffixReportSystem.Helper.Classes;
using EffixReportSystem.Helper.Classes.Enums;
using EffixReportSystem.Helper.Interfaces;
using EffixReportSystem.Views.MassMedia.ViewModels;

namespace EffixReportSystem.Views.MassMedia
{
    class MassMediaViewModel : ObservableObject, IPageViewModel
    {
        public string Name { get { return "СМИ"; } }
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
        public MassMediaViewModel()
        {
            PageViewModels.Add(new ViewMassMediaViewModel()); //9
            CurrentPageViewModel = PageViewModels[0];
        }
    }
}
