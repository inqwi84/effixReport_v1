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
	public partial class EF_User_Activity : INotifyPropertyChanged
	{
		private long _user_id;
		public virtual long User_id
		{
		   get
		   {
		       return this._user_id;
		   }
		   set
		   {
		       if( User_id == value )
		  return;
		 
		  _user_id = value;
		  this.OnPropertyChanged("User_id");
		   }
		}
		private string _session_id;
		public virtual string Session_id
		{
		   get
		   {
		       return this._session_id;
		   }
		   set
		   {
		       if( Session_id == value )
		  return;
		 
		  _session_id = value;
		  this.OnPropertyChanged("Session_id");
		   }
		}
		private DateTime _time;
		public virtual DateTime Time
		{
		   get
		   {
		       return this._time;
		   }
		   set
		   {
		       if( Time == value )
		  return;
		 
		  _time = value;
		  this.OnPropertyChanged("Time");
		   }
		}
		private string _ip;
		public virtual string Ip
		{
		   get
		   {
		       return this._ip;
		   }
		   set
		   {
		       if( Ip == value )
		  return;
		 
		  _ip = value;
		  this.OnPropertyChanged("Ip");
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
