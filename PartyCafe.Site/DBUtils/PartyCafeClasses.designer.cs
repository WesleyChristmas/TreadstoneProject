﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PartyCafe.Site.DBUtils
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="PartyCafeDB_2")]
	public partial class PartyCafeClassesDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Определения метода расширяемости
    partial void OnCreated();
    #endregion
		
		public PartyCafeClassesDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["PartyCafeDB_2ConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public PartyCafeClassesDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PartyCafeClassesDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PartyCafeClassesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PartyCafeClassesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Events> Events
		{
			get
			{
				return this.GetTable<Events>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Events")]
	public partial class Events
	{
		
		private int _IdRecord;
		
		private string _Name;
		
		private System.DateTime _EventDate;
		
		private int _IdPhoto;
		
		private System.Nullable<System.Guid> _UserCreate;
		
		private System.Nullable<System.Guid> _UserUpdate;
		
		private System.DateTime _DateCreate;
		
		private System.Nullable<System.DateTime> _DateUpdate;
		
		public Events()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdRecord", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
		public int IdRecord
		{
			get
			{
				return this._IdRecord;
			}
			set
			{
				if ((this._IdRecord != value))
				{
					this._IdRecord = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this._Name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EventDate", DbType="DateTime NOT NULL")]
		public System.DateTime EventDate
		{
			get
			{
				return this._EventDate;
			}
			set
			{
				if ((this._EventDate != value))
				{
					this._EventDate = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdPhoto", DbType="Int NOT NULL")]
		public int IdPhoto
		{
			get
			{
				return this._IdPhoto;
			}
			set
			{
				if ((this._IdPhoto != value))
				{
					this._IdPhoto = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserCreate", DbType="UniqueIdentifier")]
		public System.Nullable<System.Guid> UserCreate
		{
			get
			{
				return this._UserCreate;
			}
			set
			{
				if ((this._UserCreate != value))
				{
					this._UserCreate = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserUpdate", DbType="UniqueIdentifier")]
		public System.Nullable<System.Guid> UserUpdate
		{
			get
			{
				return this._UserUpdate;
			}
			set
			{
				if ((this._UserUpdate != value))
				{
					this._UserUpdate = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateCreate", DbType="DateTime NOT NULL")]
		public System.DateTime DateCreate
		{
			get
			{
				return this._DateCreate;
			}
			set
			{
				if ((this._DateCreate != value))
				{
					this._DateCreate = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateUpdate", DbType="DateTime")]
		public System.Nullable<System.DateTime> DateUpdate
		{
			get
			{
				return this._DateUpdate;
			}
			set
			{
				if ((this._DateUpdate != value))
				{
					this._DateUpdate = value;
				}
			}
		}
	}
}
#pragma warning restore 1591