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
using EffixReportSystem;


namespace EffixReportSystem	
{
	public partial class EF_Project : INotifyPropertyChanged
	{
		private long _project_Id;
		public virtual long Project_id
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
		private string _project_Descr;
		public virtual string Project_descr
		{
		   get
		   {
		       return this._project_Descr;
		   }
		   set
		   {
		       if( Project_descr == value )
		  return;
		 
		  _project_Descr = value;
		  this.OnPropertyChanged("Project_descr");
		   }
		}
		private IList<EF_Publication> _eF_Publications = new List<EF_Publication>();
		public virtual IList<EF_Publication> EF_Publications
		{
		   get
		   {
		       return this._eF_Publications;
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
