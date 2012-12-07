using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EffixReportSystem.Helper.Classes;
using EffixReportSystem.Helper.Interfaces;

namespace EffixReportSystem.Views.Publication
{
    class PublicationViewModel : ObservableObject, IPageViewModel
    {
        public string Name { get { return "Публикации"; } }

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
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }
        public PublicationViewModel()
        {
            PageViewModels.Add(new ViewModels.ViewPublicationViewModel(this));//0
            PageViewModels.Add(new ViewModels.EditPublicationViewModel());//1
            PageViewModels.Add(new ViewModels.NewPublicationViewModel());//2
            CurrentPageViewModel = PageViewModels[0];
        }
    }
}
