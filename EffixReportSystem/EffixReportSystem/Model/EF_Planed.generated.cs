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
	public partial class EF_Planed : INotifyPropertyChanged
	{
		private string _isPlaned;
		public virtual string IsPlaned
		{
		   get
		   {
		       return this._isPlaned;
		   }
		   set
		   {
		       if( IsPlaned == value )
		  return;
		 
		  _isPlaned = value;
		  this.OnPropertyChanged("IsPlaned");
		   }
		}
		private int _iD;
		public virtual int ID
		{
		   get
		   {
		       return this._iD;
		   }
		   set
		   {
		       if( ID == value )
		  return;
		 
		  _iD = value;
		  this.OnPropertyChanged("ID");
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