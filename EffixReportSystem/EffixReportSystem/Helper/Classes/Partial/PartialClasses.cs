using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EffixReportSystem	
{
    public partial class EF_Project 
    {

        private ObservableCollection<EF_SMI_Type> _smiTypeList;
        public virtual ObservableCollection<EF_SMI_Type> SmiTypeList
		{
		   get
		   {
               return _smiTypeList;
		   }
		   set
		   {
               if (SmiTypeList == value)
		  return;

               _smiTypeList = value;
               OnPropertyChanged("SmiTypeList");
		   }
		}
    }
    public partial class EF_SMI_Type
    {
        private ObservableCollection<Year> _yearList;
        public virtual ObservableCollection<Year> YearList
        {
            get
            {
                return _yearList;
            }
            set
            {
                if (YearList == value)
                    return;

                _yearList = value;
                OnPropertyChanged("YearList");
            }
        }
    }
    public class Year : INotifyPropertyChanged
    {
        private ObservableCollection<Month> _monthList;
        public virtual ObservableCollection<Month> MonthList
        {
            get
            {
                return _monthList;
            }
            set
            {
                if (MonthList == value)
                    return;

                _monthList = value;
                OnPropertyChanged("MonthList");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }
    }
    public class Month : INotifyPropertyChanged
    {
        private ObservableCollection<Day> _dayList;
        public virtual ObservableCollection<Day> DayList
        {
            get
            {
                return _dayList;
            }
            set
            {
                if (DayList == value)
                    return;

                _dayList = value;
                OnPropertyChanged("DayList");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }
    }
    public class Day : INotifyPropertyChanged
    {
        private ObservableCollection<EF_Publication> _publicationList;
        public virtual ObservableCollection<EF_Publication> PublicationList
        {
            get
            {
                return _publicationList;
            }
            set
            {
                if (PublicationList == value)
                    return;

                _publicationList = value;
                OnPropertyChanged("PublicationList");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }
    }
}
