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
	public partial class EF_Tonality : INotifyPropertyChanged
	{
		private long _tonality_id;
		public virtual long Tonality_id
		{
		   get
		   {
		       return this._tonality_id;
		   }
		   set
		   {
		       if( Tonality_id == value )
		  return;
		 
		  _tonality_id = value;
		  this.OnPropertyChanged("Tonality_id");
		   }
		}
		private string _name;
		public virtual string Name
		{
		   get
		   {
		       return this._name;
		   }
		   set
		   {
		       if( Name == value )
		  return;
		 
		  _name = value;
		  this.OnPropertyChanged("Name");
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
