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
	public partial class EF_Person : INotifyPropertyChanged
	{
		private long _person_id;
		public virtual long Person_id
		{
		   get
		   {
		       return this._person_id;
		   }
		   set
		   {
		       if( Person_id == value )
		  return;
		 
		  _person_id = value;
		  this.OnPropertyChanged("Person_id");
		   }
		}
		private string _lastname;
		public virtual string Lastname
		{
		   get
		   {
		       return this._lastname;
		   }
		   set
		   {
		       if( Lastname == value )
		  return;
		 
		  _lastname = value;
		  this.OnPropertyChanged("Lastname");
		   }
		}
		private string _firstname;
		public virtual string Firstname
		{
		   get
		   {
		       return this._firstname;
		   }
		   set
		   {
		       if( Firstname == value )
		  return;
		 
		  _firstname = value;
		  this.OnPropertyChanged("Firstname");
		   }
		}
		private string _midname;
		public virtual string Midname
		{
		   get
		   {
		       return this._midname;
		   }
		   set
		   {
		       if( Midname == value )
		  return;
		 
		  _midname = value;
		  this.OnPropertyChanged("Midname");
		   }
		}
		private string _notes;
		public virtual string Notes
		{
		   get
		   {
		       return this._notes;
		   }
		   set
		   {
		       if( Notes == value )
		  return;
		 
		  _notes = value;
		  this.OnPropertyChanged("Notes");
		   }
		}
		private string _employment_name;
		public virtual string Employment_name
		{
		   get
		   {
		       return this._employment_name;
		   }
		   set
		   {
		       if( Employment_name == value )
		  return;
		 
		  _employment_name = value;
		  this.OnPropertyChanged("Employment_name");
		   }
		}
		private long? _smi_id;
		public virtual long? Smi_id
		{
		   get
		   {
		       return this._smi_id;
		   }
		   set
		   {
		       if( Smi_id == value )
		  return;
		 
		  _smi_id = value;
		  this.OnPropertyChanged("Smi_id");
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