using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using EffixReportSystem.Helper.Classes;
using EffixReportSystem.Helper.Interfaces;

namespace EffixReportSystem	
{
    public partial class EF_Project : IName
    {
        public string Name { get; set; }

        private ObservableCollection<Year> _children;
        public virtual ObservableCollection<Year> Children
		{
		   get
		   {
               return _children;
		   }
		   set
		   {
               if (Children == value)
		  return;

               _children = value;
               OnPropertyChanged("Children");
		   }
		}
    }

    public partial class EF_Publication : IName
    {
        public string Name { get; set; }

        private EF_SMI _smi;
        private EF_Tonality _tonality;
        private EF_Dictionary _photo;
        private EF_Exclusivity _exclusivity;
        private EF_Dictionary _initiated;
        private EF_Dictionary _planed;
        private EF_SMI_priority _priority;
        //Tonality
        public virtual EF_Tonality Tonality
        {
            get
            {
                _tonality = PublicationHelper.Instance.Tonalities.FirstOrDefault(item => item.Tonality_id == Tonality_id);
                return _tonality;
            }
            set
            {
                if (Tonality == value)
                    return;
                _tonality = value;
                OnPropertyChanged("Tonality");
            }
        }
        public virtual EF_SMI Smi
        {
            get
            {
                _smi = PublicationHelper.Instance.Smi.FirstOrDefault(item => item.Smi_id == Smi_id);
                return _smi;
            }
            set
            {
                if (Smi == value)
                    return;
                _smi = value;
                OnPropertyChanged("Smi");
            }
        }
        public virtual EF_Dictionary Photo
        {
            get
            {
                _photo = PublicationHelper.Instance.Photo.FirstOrDefault(item => item.Value == Has_photo);
                return _photo;
            }
            set
            {
                if (Photo == value)
                    return;
                _photo = value;
                OnPropertyChanged("Photo");
            }
        }
        public virtual EF_Exclusivity Exclusivity
        {
            get
            {
                _exclusivity = PublicationHelper.Instance.Exclusivities.FirstOrDefault(item => item.Exclusivity_id == Exclusivity_id);
                return _exclusivity;
            }
            set
            {
                if (Exclusivity == value)
                    return;
                _exclusivity = value;
                OnPropertyChanged("Exclusivity");
            }
        }
        public virtual EF_Dictionary Initiated
        {
            get
            {
                _initiated = PublicationHelper.Instance.Initiated.FirstOrDefault(item => item.Value == Is_initiated);
                return _initiated;
            }
            set
            {
                if (Initiated == value)
                    return;
                _initiated = value;
                OnPropertyChanged("Initiated");
            }
        }
        public virtual EF_Dictionary Planed
        {
            get
            {
                _planed = PublicationHelper.Instance.Planed.FirstOrDefault(item => item.Value == Is_planed);
                return _planed;
            }
            set
            {
                if (Planed == value)
                    return;
                _planed = value;
                OnPropertyChanged("Planed");
            }
        }

        public virtual EF_SMI_priority Priority
        {
            get
            {
                _priority = PublicationHelper.Instance.Priorities.FirstOrDefault(item => item.Priority_id == Priority_id);
                return _priority;
            }
            set
            {
                if (Priority == value)
                    return;
                _priority = value;
                OnPropertyChanged("Priority");
            }
        }
    }
    public partial class EF_SMI_Type : IName
    {
        private ObservableCollection<Year> _children;
        public virtual ObservableCollection<Year> Children
        {
            get
            {
                return _children;
            }
            set
            {
                if (Children == value)
                    return;

                _children = value;
                OnPropertyChanged("Children");
            }
        }

        public string Name { get;  set; }
    }
    public class Year : INotifyPropertyChanged, IName
    {
        private EF_Project _parent;
        public virtual EF_Project Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                if (Parent == value)
                    return;

                _parent = value;
                OnPropertyChanged("Parent");
            }
        }
        private ObservableCollection<Month> _children;
        public virtual ObservableCollection<Month> Children
        {
            get
            {
                return _children;
            }
            set
            {
                if (Children == value)
                    return;

                _children = value;
                OnPropertyChanged("Children");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        public string Name { get; set; }
    }
    public class Month : INotifyPropertyChanged,IName
    {
        private Year _parent;
        public virtual Year Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                if (Parent == value)
                    return;

                _parent = value;
                OnPropertyChanged("Parent");
            }
        }
        private ObservableCollection<Day> _children;
        public virtual ObservableCollection<Day> Children
        {
            get
            {
                return _children;
            }
            set
            {
                if (Children == value)
                    return;

                _children = value;
                OnPropertyChanged("Children");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        public string Name { get;  set; }
    }
    public class Day : INotifyPropertyChanged, IName
    {
        private Month _parent;
        public virtual Month Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                if (Parent == value)
                    return;

                _parent = value;
                OnPropertyChanged("Parent");
            }
        }
        private ObservableCollection<EF_Publication> _children;
        public virtual ObservableCollection<EF_Publication> Children
        {
            get
            {
                return _children;
            }
            set
            {
                if (Children == value)
                    return;

                _children = value;
                OnPropertyChanged("Children");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        public string Name { get;  set; }
    }
}
