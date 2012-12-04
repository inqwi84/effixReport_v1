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
	public partial class EF_User : INotifyPropertyChanged
	{
		private long _usr_Id;
		public virtual long Usr_id
		{
		   get
		   {
		       return this._usr_Id;
		   }
		   set
		   {
		       if( Usr_id == value )
		  return;
		 
		  _usr_Id = value;
		  this.OnPropertyChanged("Usr_id");
		   }
		}
		private string _usr_Login;
		public virtual string Usr_login
		{
		   get
		   {
		       return this._usr_Login;
		   }
		   set
		   {
		       if( Usr_login == value )
		  return;
		 
		  _usr_Login = value;
		  this.OnPropertyChanged("Usr_login");
		   }
		}
		private string _pass;
		public virtual string Pass
		{
		   get
		   {
		       return this._pass;
		   }
		   set
		   {
		       if( Pass == value )
		  return;
		 
		  _pass = value;
		  this.OnPropertyChanged("Pass");
		   }
		}
		private string _fname;
		public virtual string Fname
		{
		   get
		   {
		       return this._fname;
		   }
		   set
		   {
		       if( Fname == value )
		  return;
		 
		  _fname = value;
		  this.OnPropertyChanged("Fname");
		   }
		}
		private string _sname;
		public virtual string Sname
		{
		   get
		   {
		       return this._sname;
		   }
		   set
		   {
		       if( Sname == value )
		  return;
		 
		  _sname = value;
		  this.OnPropertyChanged("Sname");
		   }
		}
		private string _mname;
		public virtual string Mname
		{
		   get
		   {
		       return this._mname;
		   }
		   set
		   {
		       if( Mname == value )
		  return;
		 
		  _mname = value;
		  this.OnPropertyChanged("Mname");
		   }
		}
		private string _usr_Position;
		public virtual string Usr_position
		{
		   get
		   {
		       return this._usr_Position;
		   }
		   set
		   {
		       if( Usr_position == value )
		  return;
		 
		  _usr_Position = value;
		  this.OnPropertyChanged("Usr_position");
		   }
		}
		private string _s_Tel;
		public virtual string S_tel
		{
		   get
		   {
		       return this._s_Tel;
		   }
		   set
		   {
		       if( S_tel == value )
		  return;
		 
		  _s_Tel = value;
		  this.OnPropertyChanged("S_tel");
		   }
		}
		private string _m_Tel;
		public virtual string M_tel
		{
		   get
		   {
		       return this._m_Tel;
		   }
		   set
		   {
		       if( M_tel == value )
		  return;
		 
		  _m_Tel = value;
		  this.OnPropertyChanged("M_tel");
		   }
		}
		private string _post_Address;
		public virtual string Post_address
		{
		   get
		   {
		       return this._post_Address;
		   }
		   set
		   {
		       if( Post_address == value )
		  return;
		 
		  _post_Address = value;
		  this.OnPropertyChanged("Post_address");
		   }
		}
		private int _usr_Priority;
		public virtual int Usr_priority
		{
		   get
		   {
		       return this._usr_Priority;
		   }
		   set
		   {
		       if( Usr_priority == value )
		  return;
		 
		  _usr_Priority = value;
		  this.OnPropertyChanged("Usr_priority");
		   }
		}
		private int? _usr_Type;
		public virtual int? Usr_type
		{
		   get
		   {
		       return this._usr_Type;
		   }
		   set
		   {
		       if( Usr_type == value )
		  return;
		 
		  _usr_Type = value;
		  this.OnPropertyChanged("Usr_type");
		   }
		}
		private System.Nullable<System.DateTimeOffset> _datecrt;
		public virtual System.Nullable<System.DateTimeOffset> Datecrt
		{
		   get
		   {
		       return this._datecrt;
		   }
		   set
		   {
		       if( Datecrt == value )
		  return;
		 
		  _datecrt = value;
		  this.OnPropertyChanged("Datecrt");
		   }
		}
		private string _email;
		public virtual string Email
		{
		   get
		   {
		       return this._email;
		   }
		   set
		   {
		       if( Email == value )
		  return;
		 
		  _email = value;
		  this.OnPropertyChanged("Email");
		   }
		}
		private long? _department_Id;
		public virtual long? Department_id
		{
		   get
		   {
		       return this._department_Id;
		   }
		   set
		   {
		       if( Department_id == value )
		  return;
		 
		  _department_Id = value;
		  this.OnPropertyChanged("Department_id");
		   }
		}
		private string _cert;
		public virtual string Cert
		{
		   get
		   {
		       return this._cert;
		   }
		   set
		   {
		       if( Cert == value )
		  return;
		 
		  _cert = value;
		  this.OnPropertyChanged("Cert");
		   }
		}
		private System.Nullable<System.DateTimeOffset> _lock_Period;
		public virtual System.Nullable<System.DateTimeOffset> Lock_period
		{
		   get
		   {
		       return this._lock_Period;
		   }
		   set
		   {
		       if( Lock_period == value )
		  return;
		 
		  _lock_Period = value;
		  this.OnPropertyChanged("Lock_period");
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
