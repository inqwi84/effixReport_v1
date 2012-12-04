#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using System.ComponentModel;


namespace EffixReportSystem	
{
	public partial class EF_Publication : INotifyPropertyChanged
	{
		private long _publication_Id;
		public virtual long Publication_id
		{
		   get
		   {
		       return this._publication_Id;
		   }
		   set
		   {
		       if( Publication_id == value )
		  return;
		 
		  _publication_Id = value;
		  this.OnPropertyChanged("Publication_id");
		   }
		}
		private long? _tonality_Id;
		public virtual long? Tonality_id
		{
		   get
		   {
		       return this._tonality_Id;
		   }
		   set
		   {
		       if( Tonality_id == value )
		  return;
		 
		  _tonality_Id = value;
		  this.OnPropertyChanged("Tonality_id");
		   }
		}
		private string _publication_Name;
		public virtual string Publication_name
		{
		   get
		   {
		       return this._publication_Name;
		   }
		   set
		   {
		       if( Publication_name == value )
		  return;
		 
		  _publication_Name = value;
		  this.OnPropertyChanged("Publication_name");
		   }
		}
		private long? _smi_Id;
		public virtual long? Smi_id
		{
		   get
		   {
		       return this._smi_Id;
		   }
		   set
		   {
		       if( Smi_id == value )
		  return;
		 
		  _smi_Id = value;
		  this.OnPropertyChanged("Smi_id");
		   }
		}
		private bool? _has_Photo;
		public virtual bool? Has_photo
		{
		   get
		   {
		       return this._has_Photo;
		   }
		   set
		   {
		       if( Has_photo == value )
		  return;
		 
		  _has_Photo = value;
		  this.OnPropertyChanged("Has_photo");
		   }
		}
		private long? _exclusivity_Id;
		public virtual long? Exclusivity_id
		{
		   get
		   {
		       return this._exclusivity_Id;
		   }
		   set
		   {
		       if( Exclusivity_id == value )
		  return;
		 
		  _exclusivity_Id = value;
		  this.OnPropertyChanged("Exclusivity_id");
		   }
		}
		private long? _project_Id;
		public virtual long? Project_id
		{
		   get
		   {
		       return this._project_Id;
		   }
		   set
		   {
		       if( Project_id == value )
		  return;
		 
		  _project_Id = value;
		  this.OnPropertyChanged("Project_id");
		   }
		}
		private DateTime? _ceation_Date;
		public virtual DateTime? Ceation_date
		{
		   get
		   {
		       return this._ceation_Date;
		   }
		   set
		   {
		       if( Ceation_date == value )
		  return;
		 
		  _ceation_Date = value;
		  this.OnPropertyChanged("Ceation_date");
		   }
		}
		private DateTime? _publication_Date;
		public virtual DateTime? Publication_date
		{
		   get
		   {
		       return this._publication_Date;
		   }
		   set
		   {
		       if( Publication_date == value )
		  return;
		 
		  _publication_Date = value;
		  this.OnPropertyChanged("Publication_date");
		   }
		}
		private bool? _is_Initiated;
		public virtual bool? Is_initiated
		{
		   get
		   {
		       return this._is_Initiated;
		   }
		   set
		   {
		       if( Is_initiated == value )
		  return;
		 
		  _is_Initiated = value;
		  this.OnPropertyChanged("Is_initiated");
		   }
		}
		private bool? _is_Planed;
		public virtual bool? Is_planed
		{
		   get
		   {
		       return this._is_Planed;
		   }
		   set
		   {
		       if( Is_planed == value )
		  return;
		 
		  _is_Planed = value;
		  this.OnPropertyChanged("Is_planed");
		   }
		}
		private string _screenshot_Path;
		public virtual string Screenshot_path
		{
		   get
		   {
		       return this._screenshot_Path;
		   }
		   set
		   {
		       if( Screenshot_path == value )
		  return;
		 
		  _screenshot_Path = value;
		  this.OnPropertyChanged("Screenshot_path");
		   }
		}
		private long? _priority_Id;
		public virtual long? Priority_id
		{
		   get
		   {
		       return this._priority_Id;
		   }
		   set
		   {
		       if( Priority_id == value )
		  return;
		 
		  _priority_Id = value;
		  this.OnPropertyChanged("Priority_id");
		   }
		}
		private string _url_Path;
		public virtual string Url_path
		{
		   get
		   {
		       return this._url_Path;
		   }
		   set
		   {
		       if( Url_path == value )
		  return;
		 
		  _url_Path = value;
		  this.OnPropertyChanged("Url_path");
		   }
		}
		private long? _user_Id;
		public virtual long? User_id
		{
		   get
		   {
		       return this._user_Id;
		   }
		   set
		   {
		       if( User_id == value )
		  return;
		 
		  _user_Id = value;
		  this.OnPropertyChanged("User_id");
		   }
		}
		private string _blob_Path;
		public virtual string Blob_path
		{
		   get
		   {
		       return this._blob_Path;
		   }
		   set
		   {
		       if( Blob_path == value )
		  return;
		 
		  _blob_Path = value;
		  this.OnPropertyChanged("Blob_path");
		   }
		}
		private int? _image_Count;
		public virtual int? Image_count
		{
		   get
		   {
		       return this._image_Count;
		   }
		   set
		   {
		       if( Image_count == value )
		  return;
		 
		  _image_Count = value;
		  this.OnPropertyChanged("Image_count");
		   }
		}
		private string _project_Name;
		public virtual string Project_name
		{
		   get
		   {
		       return this._project_Name;
		   }
		   set
		   {
		       if( Project_name == value )
		  return;
		 
		  _project_Name = value;
		  this.OnPropertyChanged("Project_name");
		   }
		}
		private int? _p_Year;
		public virtual int? P_year
		{
		   get
		   {
		       return this._p_Year;
		   }
		   set
		   {
		       if( P_year == value )
		  return;
		 
		  _p_Year = value;
		  this.OnPropertyChanged("P_year");
		   }
		}
		private int? _p_Month;
		public virtual int? P_month
		{
		   get
		   {
		       return this._p_Month;
		   }
		   set
		   {
		       if( P_month == value )
		  return;
		 
		  _p_Month = value;
		  this.OnPropertyChanged("P_month");
		   }
		}
		private int? _p_Day;
		public virtual int? P_day
		{
		   get
		   {
		       return this._p_Day;
		   }
		   set
		   {
		       if( P_day == value )
		  return;
		 
		  _p_Day = value;
		  this.OnPropertyChanged("P_day");
		   }
		}
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged( string info )
		{
			if( this.PropertyChanged != null)
				this.PropertyChanged( this, new PropertyChangedEventArgs( info ) );
		}
	}
}
