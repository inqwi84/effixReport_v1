using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EffixReportSystem.Helper.Classes;
using EffixReportSystem.Helper.Interfaces;
using EffixReportSystem.Views.MassMedia;
using EffixReportSystem.Views.Person;
using EffixReportSystem.Views.Publication;
using EffixReportSystem.Views.Report;
using EffixReportSystem.Views.Settings;

namespace EffixReportSystem
{
    internal class ApplicationViewModel : ObservableObject,IPageViewModel
    {
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

        public ApplicationViewModel()
        {

            PageViewModels.Add(new PublicationViewModel()); //0}
            PageViewModels.Add(new MassMediaViewModel()); //1}
            PageViewModels.Add(new PersonViewModel()); //2} 
            PageViewModels.Add(new ReportViewModel()); //3}
            PageViewModels.Add(new SettingsViewModel()); //4}
            CurrentPageViewModel = PageViewModels[0];
        }
        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        public string Name { get; private set; }
    }
}
