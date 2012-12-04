using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using EffixReportSystem.Helper.Interfaces;

namespace EffixReportSystem	
{
    public partial class EF_Project : IName
    {
        private ObservableCollection<EF_SMI_Type> _children;
        public virtual ObservableCollection<EF_SMI_Type> Children
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
