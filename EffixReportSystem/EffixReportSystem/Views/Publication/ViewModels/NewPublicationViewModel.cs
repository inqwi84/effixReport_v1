using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using EffixReportSystem.Helper.Classes;
using EffixReportSystem.Helper.Interfaces;

namespace EffixReportSystem.Views.Publication.ViewModels
{
    class NewPublicationViewModel : ObservableObject, IPageViewModel
    {
        public PublicationHelper PublicationHelper;


        public ObservableCollection<EF_SMI> Smi { get { return PublicationHelper.Instance.Smi; } }
        public ObservableCollection<EF_Tonality> Tonalities { get { return PublicationHelper.Instance.Tonalities; } }
        public ObservableCollection<EF_SMI_Type> SmiTypes { get { return PublicationHelper.Instance.SmiTypes; } }
        public ObservableCollection<EF_Exclusivity> Exclusivities { get { return PublicationHelper.Instance.Exclusivities; } }
        public ObservableCollection<EF_SMI_priority> Priorities { get { return PublicationHelper.Instance.Priorities; } }
        public ObservableCollection<EF_Dictionary> Initiated { get { return PublicationHelper.Instance.Initiated; } }
        public ObservableCollection<EF_Dictionary> Planed { get { return PublicationHelper.Instance.Planed; } }
        public ObservableCollection<EF_Dictionary> Photo { get { return PublicationHelper.Instance.Photo; } }

        public string Name { get; private set; }

        private EF_Publication _currentPublication;
        public EF_Publication CurrentPublication
        {
            get { return this._currentPublication; }
            set
            {
                if (CurrentPublication == value)
                    return;

                _currentPublication = value;
                this.OnPropertyChanged("CurrentPublication");
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
        public IPageViewModel ParentViewModel;
        public NewPublicationViewModel(IPageViewModel parentViewModel)
        {
            ParentViewModel = parentViewModel;
        }
    }
}
