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
	public partial class EF_SMI : INotifyPropertyChanged
	{
		private long _smi_id;
		public virtual long Smi_id
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
		private string _smi_name;
		public virtual string Smi_name
		{
		   get
		   {
		       return this._smi_name;
		   }
		   set
		   {
		       if( Smi_name == value )
		  return;
		 
		  _smi_name = value;
		  this.OnPropertyChanged("Smi_name");
		   }
		}
		private long? _smi_type_id;
		public virtual long? Smi_type_id
		{
		   get
		   {
		       return this._smi_type_id;
		   }
		   set
		   {
		       if( Smi_type_id == value )
		  return;
		 
		  _smi_type_id = value;
		  this.OnPropertyChanged("Smi_type_id");
		   }
		}
		private string _smi_descr;
		public virtual string Smi_descr
		{
		   get
		   {
		       return this._smi_descr;
		   }
		   set
		   {
		       if( Smi_descr == value )
		  return;
		 
		  _smi_descr = value;
		  this.OnPropertyChanged("Smi_descr");
		   }
		}
		private string _smi_edition;
		public virtual string Smi_edition
		{
		   get
		   {
		       return this._smi_edition;
		   }
		   set
		   {
		       if( Smi_edition == value )
		  return;
		 
		  _smi_edition = value;
		  this.OnPropertyChanged("Smi_edition");
		   }
		}
		private string _smi_edition_descr;
		public virtual string Smi_edition_descr
		{
		   get
		   {
		       return this._smi_edition_descr;
		   }
		   set
		   {
		       if( Smi_edition_descr == value )
		  return;
		 
		  _smi_edition_descr = value;
		  this.OnPropertyChanged("Smi_edition_descr");
		   }
		}
		private string _smi_url;
		public virtual string Smi_url
		{
		   get
		   {
		       return this._smi_url;
		   }
		   set
		   {
		       if( Smi_url == value )
		  return;
		 
		  _smi_url = value;
		  this.OnPropertyChanged("Smi_url");
		   }
		}
		private int? _smi_edition_id;
		public virtual int? Smi_edition_id
		{
		   get
		   {
		       return this._smi_edition_id;
		   }
		   set
		   {
		       if( Smi_edition_id == value )
		  return;
		 
		  _smi_edition_id = value;
		  this.OnPropertyChanged("Smi_edition_id");
		   }
		}
		private long? _mass_media_id;
		public virtual long? Mass_media_id
		{
		   get
		   {
		       return this._mass_media_id;
		   }
		   set
		   {
		       if( Mass_media_id == value )
		  return;
		 
		  _mass_media_id = value;
		  this.OnPropertyChanged("Mass_media_id");
		   }
		}
		private int? _is_deleted;
		public virtual int? Is_deleted
		{
		   get
		   {
		       return this._is_deleted;
		   }
		   set
		   {
		       if( Is_deleted == value )
		  return;
		 
		  _is_deleted = value;
		  this.OnPropertyChanged("Is_deleted");
		   }
		}
		private EF_SMI_Type _eF_SMI_Type;
		public virtual EF_SMI_Type EF_SMI_Type
		{
		   get
		   {
		       return this._eF_SMI_Type;
		   }
		   set
		   {
		       if( EF_SMI_Type == value )
		  return;
		 
		  _eF_SMI_Type = value;
		  this.OnPropertyChanged("EF_SMI_Type");
		   }
		}
		private EF_Edition _eF_Edition;
		public virtual EF_Edition EF_Edition
		{
		   get
		   {
		       return this._eF_Edition;
		   }
		   set
		   {
		       if( EF_Edition == value )
		  return;
		 
		  _eF_Edition = value;
		  this.OnPropertyChanged("EF_Edition");
		   }
		}
		private EF_MassMedia _eF_MassMedium;
		public virtual EF_MassMedia EF_MassMedium
		{
		   get
		   {
		       return this._eF_MassMedium;
		   }
		   set
		   {
		       if( EF_MassMedium == value )
		  return;
		 
		  _eF_MassMedium = value;
		  this.OnPropertyChanged("EF_MassMedium");
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
