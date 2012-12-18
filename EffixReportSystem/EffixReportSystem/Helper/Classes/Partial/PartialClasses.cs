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


        public EF_Publication(EF_Department department)
        {
            var currentProject = DataHelper.GetParentProject(department.Department_id);
            _project_Id = currentProject.Department_id;
            _project_Name = currentProject.Department_description;
        }

        public EF_Publication()
        {
            
        }
        //private EF_SMI _smi;
        //private EF_Tonality _tonality;
        //private EF_Photo _photo;
        //private EF_Exclusivity _exclusivity;
        //private EF_Initiated _initiated;
        //private EF_Planed _planed;
        //private EF_SMI_priority _priority;
        ////Tonality
        //public virtual EF_Tonality Tonality
        //{
        //    get
        //    {
        //        return _tonality;
        //    }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            _tonality = value;
        //            OnPropertyChanged("Tonality");
        //        }
        //    }
        //}
        //public virtual EF_SMI Smi
        //{
        //    get
        //    {
        //        return _smi;
        //    }
        //    set
        //    {
        //        if (Smi == value)
        //            return;
        //        _smi = value;
        //        OnPropertyChanged("Smi");
        //    }
        //}
        //public virtual EF_Photo Photo
        //{
        //    get
        //    {
        //        return _photo;
        //    }
        //    set
        //    {
        //        if (Photo == value)
        //            return;
        //        _photo = value;
        //        OnPropertyChanged("Photo");
        //    }
        //}
        //public virtual EF_Exclusivity Exclusivity
        //{
        //    get
        //    {
        //        return _exclusivity;
        //    }
        //    set
        //    {
        //        if (Exclusivity == value)
        //            return;
        //        _exclusivity = value;
        //        OnPropertyChanged("Exclusivity");
        //    }
        //}
        //public virtual EF_Initiated Initiated
        //{
        //    get
        //    {
        //        return _initiated;
        //    }
        //    set
        //    {
        //        if (Initiated == value)
        //            return;
        //        _initiated = value;
        //        OnPropertyChanged("Initiated");
        //    }
        //}
        //public virtual EF_Planed Planed
        //{
        //    get
        //    {
        //        return _planed;
        //    }
        //    set
        //    {
        //        if (Planed == value)
        //            return;
        //        _planed = value;
        //        OnPropertyChanged("Planed");
        //    }
        //}

        //public virtual EF_SMI_priority Priority
        //{
        //    get
        //    {
        //        return _priority;
        //    }
        //    set
        //    {
        //        if (Priority == value)
        //            return;
        //        _priority = value;
        //        OnPropertyChanged("Priority");
        //    }
        //}
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
