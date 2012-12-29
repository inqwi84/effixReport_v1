using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using EffixReportSystem.Helper.Classes;
using EffixReportSystem.Helper.Classes.Enums;
using EffixReportSystem.Helper.Interfaces;

namespace EffixReportSystem.Views.MassMedia.ViewModels
{
    class ViewMassMediaViewModel :ObservableObject, IPageViewModel
    {

        private ViewMode _mode;

        public ViewMode Mode
        {
            get { return _mode; }
            set
            {
                if (Mode != value)
                {
                    _mode = value;
                    OnPropertyChanged("Mode");
                }
            }
        }

        public void CreateMassMedia()
        {
            
        }
        public void RemoveMassMedia()
        {
           _model.Delete(_model.EF_MassMedias.FirstOrDefault(item=>item.Mass_media_type_id==CurrentMassMediaDepartament.Mass_media_type_id));
            _model.SaveChanges();
            ReloadMassMedia();
        }
        public void RenameMassMedia()
        {
            
        }

        public void ReloadMassMedia()
        {
            using (var model = new EntitiesModel())
            {
                var hierarchicalList = model.EF_MassMedias.ToList().Select(flatItem =>
                                                                            new EF_MassMedia
                                                                            {
                                                                                Mass_media_type_name =
                                                                                    flatItem
                                                                                    .
                                                                                    Mass_media_type_name,
                                                                                Mass_media_type_descr = flatItem.Mass_media_type_descr,

                                                                                Mass_media_type_id
                                                                                    =
                                                                                    flatItem
                                                                                    .Mass_media_type_id,

                                                                                Parent_type_id =
                                                                                    flatItem.Parent_type_id,
                                                                            }).ToList();

                MassMediaDepartments =
                    new ObservableCollection<EF_MassMedia>(
                        hierarchicalList.GroupJoin(hierarchicalList,
                                                   parentItem =>
                                                   parentItem.Mass_media_type_id,
                                                   childItem =>
                                                   childItem.Parent_type_id,
                                                   (parent, children) =>
                                                   {
                                                       parent.Children
                                                           =
                                                           children.
                                                               ToList();
                                                       return parent;
                                                   }).Where(
                                                           item =>
                                                           item.Parent_type_id ==
                                                           null).ToList());

            }
        }

        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;
        private EntitiesModel _model;
        public List<IPageViewModel> PageViewModels
        {
            get { return _pageViewModels ?? (_pageViewModels = new List<IPageViewModel>()); }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value && value != null)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }

        private ObservableCollection<EF_MassMedia> _massMediaDepartments;
        public ObservableCollection<EF_MassMedia> MassMediaDepartments
        {
            get { return _massMediaDepartments; }
            set
            {
                if (MassMediaDepartments == value)
                    return;

                _massMediaDepartments = value;
                this.OnPropertyChanged("MassMediaDepartments");
            }
        }
        public ObservableCollection<EF_SMI> GetSmiByMassMediaDepartmentId(EF_MassMedia massMediaDepartment)
        {
            var result = new ObservableCollection<EF_SMI>();
                if (massMediaDepartment.Parent_type_id == null)
                {
                    result = new ObservableCollection<EF_SMI>(_model.EF_SMIs);
                }
                else
                {
                    //   var tmp = TreeToFlat(massMediaDepartment);
                    var tmp2 = massMediaDepartment.Children.Traverse();
                    if (tmp2.Count == 0)
                    {
                        result = new ObservableCollection<EF_SMI>(_model.EF_SMIs.Where(smi => smi.Mass_media_id == massMediaDepartment.Mass_media_type_id));
                    }
                    else
                    {
                        foreach (var efMassMedia in tmp2)
                        {
                            var media = efMassMedia;
                            var smiList = _model.EF_SMIs.Where(smi => smi.Mass_media_id == media.Mass_media_type_id);
                            foreach (var efSmi in smiList)
                            {
                                result.Add(efSmi);
                            }
                        }
                    }
                }
            return result;
        }

       private EF_MassMedia _currentMassMediaDepartament;
        public EF_MassMedia CurrentMassMediaDepartament
        {
            get { return _currentMassMediaDepartament; }
            set
            {
                if (CurrentMassMediaDepartament == value)
                    return;

                _currentMassMediaDepartament = value;
                SmiList =GetSmiByMassMediaDepartmentId(_currentMassMediaDepartament);
                this.OnPropertyChanged("CurrentMassMediaDepartament");
            }
        }

        private ObservableCollection<EF_SMI> _smiList;
        public  ObservableCollection<EF_SMI> SmiList
        {
            get { return _smiList; }
            set
            {
                if (SmiList == value)
                    return;

                _smiList = value;
                this.OnPropertyChanged("SmiList");
            }
        }

        private EF_SMI _currentSmi;
        public EF_SMI CurrentSmi
        {
            get { return _currentSmi; }
            set
            {
                if (CurrentSmi == value)
                    return;

                _currentSmi = value;
                this.OnPropertyChanged("CurrentSmi");
            }
        }
        public void SaveSmi()
        {
                _model.Add(CurrentSmi);
                _model.SaveChanges();
        }
        public void SaveNewSmi()
        {
            _model.Add(CurrentSmi);
            _model.SaveChanges();
        }
        public void RestoreSmi()
        {

            var tmp = _model.EF_SMIs.FirstOrDefault(item => item.Smi_id == CurrentSmi.Smi_id);
                CurrentSmi = null;
                CurrentSmi = tmp;

        }

        internal void PreapreContextOperationsForItem(EF_MassMedia actionItem)
        {
            //if (actionItem != null)
            //{
            //    this.ContextOperations = DataProvider.GetContextoperations(actionItem.ItemType);
            //}
            //else
            //{
            //    this.ContextOperations = DataProvider.GetContextoperations(SolutionItemType.Solution);
            //}
        }

        public void Ctor()
        {
            using (var model = new EntitiesModel())
            {
                var hierarchicalList = model.EF_MassMedias.ToList().Select(flatItem =>
                                                                            new EF_MassMedia
                                                                            {
                                                                                Mass_media_type_name = 
                                                                                    flatItem
                                                                                    .
                                                                                    Mass_media_type_name,
                                                                                Mass_media_type_descr = flatItem.Mass_media_type_descr,

                                                                                Mass_media_type_id
                                                                                    =
                                                                                    flatItem
                                                                                    .Mass_media_type_id,

                                                                                Parent_type_id =
                                                                                    flatItem.Parent_type_id,
                                                                            }).ToList();

                MassMediaDepartments =
                    new ObservableCollection<EF_MassMedia>(
                        hierarchicalList.GroupJoin(hierarchicalList,
                                                   parentItem =>
                                                   parentItem.Mass_media_type_id,
                                                   childItem =>
                                                   childItem.Parent_type_id,
                                                   (parent, children) =>
                                                   {
                                                       parent.Children
                                                           =
                                                           children.
                                                               ToList();
                                                       return parent;
                                                   }).Where(
                                                           item =>
                                                           item.Parent_type_id ==
                                                           null).ToList());

            }
        }

        public ViewMassMediaViewModel()
        {
          Ctor();
            _model=new EntitiesModel();
        }

        public string Name { get; private set; }
    }
}
