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
	public partial class EF_SMI_priority : INotifyPropertyChanged
	{
		private long _priority_id;
		public virtual long Priority_id
		{
		   get
		   {
		       return this._priority_id;
		   }
		   set
		   {
		       if( Priority_id == value )
		  return;
		 
		  _priority_id = value;
		  this.OnPropertyChanged("Priority_id");
		   }
		}
		private string _priority_name;
		public virtual string Priority_name
		{
		   get
		   {
		       return this._priority_name;
		   }
		   set
		   {
		       if( Priority_name == value )
		  return;
		 
		  _priority_name = value;
		  this.OnPropertyChanged("Priority_name");
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
