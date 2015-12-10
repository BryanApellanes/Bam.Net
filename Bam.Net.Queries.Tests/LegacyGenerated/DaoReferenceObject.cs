/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Data;
using System.Data.Common;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Schema;
using Bam.Net.Data;	 
using Bam.Net.Data.Qi;

namespace Bam.Net.DaoReferenceObjects.Data
{
    [Bam.Net.Data.Table("DaoReferenceObject", "DaoReferenceObjects")]
    public partial class DaoReferenceObject: Dao
    {
        public DaoReferenceObject():base()
		{
			this.KeyColumnName = "Id";
		}

        public DaoReferenceObject(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
		}

	// property:Id, columnName:Id	
	[Bam.Net.Data.KeyColumn(Name="Id", ExtractedType="BigInt", MaxLength="8")]
	public long? Id
	{
		get
		{
			return GetLongValue("Id");
		}
		set
		{
			SetValue("Id", value);
		}
	}

	// property:IntProperty, columnName:IntProperty	
	[Bam.Net.Data.Column(Name="IntProperty", ExtractedType="Int", MaxLength="4", AllowNull=true)]
	public int? IntProperty
	{
		get
		{
			return GetIntValue("IntProperty");
		}
		set
		{
			SetValue("IntProperty", value);
		}
	}

	// property:DecimalProperty, columnName:DecimalProperty	
	[Bam.Net.Data.Column(Name="DecimalProperty", ExtractedType="Decimal", MaxLength="9", AllowNull=true)]
	public decimal? DecimalProperty
	{
		get
		{
			return GetDecimalValue("DecimalProperty");
		}
		set
		{
			SetValue("DecimalProperty", value);
		}
	}

	// property:LongProperty, columnName:LongProperty	
	[Bam.Net.Data.Column(Name="LongProperty", ExtractedType="BigInt", MaxLength="8", AllowNull=true)]
	public long? LongProperty
	{
		get
		{
			return GetLongValue("LongProperty");
		}
		set
		{
			SetValue("LongProperty", value);
		}
	}

	// property:DateTimeProperty, columnName:DateTimeProperty	
	[Bam.Net.Data.Column(Name="DateTimeProperty", ExtractedType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime DateTimeProperty
	{
		get
		{
			return GetDateTimeValue("DateTimeProperty");
		}
		set
		{
			SetValue("DateTimeProperty", value);
		}
	}

	// property:BoolProperty, columnName:BoolProperty	
	[Bam.Net.Data.Column(Name="BoolProperty", ExtractedType="Bit", MaxLength="1", AllowNull=true)]
	public bool? BoolProperty
	{
		get
		{
			return GetBooleanValue("BoolProperty");
		}
		set
		{
			SetValue("BoolProperty", value);
		}
	}

	// property:GuidProperty, columnName:GuidProperty	
	[Bam.Net.Data.Column(Name="GuidProperty", ExtractedType="UniqueIdentifier", MaxLength="16", AllowNull=true)]
	public string GuidProperty
	{
		get
		{
			return GetStringValue("GuidProperty");
		}
		set
		{
			SetValue("GuidProperty", value);
		}
	}

	// property:DoubleProperty, columnName:DoubleProperty	
	[Bam.Net.Data.Column(Name="DoubleProperty", ExtractedType="Float", MaxLength="8", AllowNull=true)]
	public string DoubleProperty
	{
		get
		{
			return GetStringValue("DoubleProperty");
		}
		set
		{
			SetValue("DoubleProperty", value);
		}
	}

	// property:ByteArrayProperty, columnName:ByteArrayProperty	
	[Bam.Net.Data.Column(Name="ByteArrayProperty", ExtractedType="VarBinaryMax", MaxLength="MAX", AllowNull=true)]
	public byte[] ByteArrayProperty
	{
		get
		{
			return GetByteValue("ByteArrayProperty");
		}
		set
		{
			SetValue("ByteArrayProperty", value);
		}
	}

	// property:StringProperty, columnName:StringProperty	
	[Bam.Net.Data.Column(Name="StringProperty", ExtractedType="VarChar", MaxLength="50", AllowNull=true)]
	public string StringProperty
	{
		get
		{
			return GetStringValue("StringProperty");
		}
		set
		{
			SetValue("StringProperty", value);
		}
	}


				
	
	public DaoReferenceObjectWithForeignKeyCollection DaoReferenceObjectWithForeignKeyCollectionByDaoReferenceObjectId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("DaoReferenceObjectWithForeignKey_DaoReferenceObjectId"))
			{
				var coll = new DaoReferenceObjectWithForeignKeyCollection(
							Select<DaoReferenceObjectWithForeignKeyColumns>
								.From<DaoReferenceObjectWithForeignKey>()
								.Where((c) => c.DaoReferenceObjectId == this.Id), this, "DaoReferenceObjectId");

				this.ChildCollections.Add("DaoReferenceObjectWithForeignKey_DaoReferenceObjectId", coll);
			}

			return (DaoReferenceObjectWithForeignKeyCollection)this.ChildCollections["DaoReferenceObjectWithForeignKey_DaoReferenceObjectId"];
		}
	}
			
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new DaoReferenceObjectColumns();
			return (colFilter.Id == IdValue);
		}

		public override void Delete(Database database = null)
		{
			Database db;
			SqlStringBuilder sql = GetSqlStringBuilder(out db);
			if(database != null)
			{
				sql = GetSqlStringBuilder(database);
				db = database;				
			}
			
			if(AutoDeleteChildren)
			{					   			
				WriteChildDeletes(sql);
			}
			WriteDelete(sql);
			sql.Execute(db);
		}
		   
		public static DaoReferenceObjectCollection Where(WhereDelegate<DaoReferenceObjectColumns> where, Database db = null)
		{
			return new DaoReferenceObjectCollection(Select<DaoReferenceObjectColumns>.From<DaoReferenceObject>().Where(where, db));
		}

		public static DaoReferenceObjectCollection Where(QiQuery where, Database db = null)
		{
			return new DaoReferenceObjectCollection(Select<DaoReferenceObjectColumns>.From<DaoReferenceObject>().Where(where, db));
		}

		public static DaoReferenceObject OneWhere(WhereDelegate<DaoReferenceObjectColumns> where, Database db = null)
		{
			var results = new DaoReferenceObjectCollection(Select<DaoReferenceObjectColumns>.From<DaoReferenceObject>().Where(where, db));
			return OneOrThrow(results);
		}

		public static DaoReferenceObject OneWhere(QiQuery where, Database db = null)
		{
			var results = new DaoReferenceObjectCollection(Select<DaoReferenceObjectColumns>.From<DaoReferenceObject>().Where(where, db));
			return OneOrThrow(results);
		}

		private static DaoReferenceObject OneOrThrow(DaoReferenceObjectCollection c)
		{
			if(c.Count == 1)
			{
				return c[0];
			}
			else if(c.Count > 1)
			{
				throw new MultipleEntriesFoundException();
			}

			return null;
		}

		public static DaoReferenceObject FirstOneWhere(WhereDelegate<DaoReferenceObjectColumns> where, Database db = null)
		{
			var results = new DaoReferenceObjectCollection(Select<DaoReferenceObjectColumns>.From<DaoReferenceObject>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static DaoReferenceObjectCollection Top(int count, WhereDelegate<DaoReferenceObjectColumns> where, Database db = null)
        {
            DaoReferenceObjectColumns c = new DaoReferenceObjectColumns();
            IQueryFilter filter = where(c);         
            QuerySet query = new QuerySet();
            query.Top<DaoReferenceObject>(count);
            query.Where(filter);

            if (db == null)
            {
                db = Db.For<DaoReferenceObject>();
            }

            query.Execute(db);
            return query.Results.As<DaoReferenceObjectCollection>(0);
        }

		public static long Count(WhereDelegate<DaoReferenceObjectColumns> where, Database db = null)
		{
			DaoReferenceObjectColumns c = new DaoReferenceObjectColumns();
			IQueryFilter filter = where(c) ;
			QuerySet query = new QuerySet();
			query.Count<DaoReferenceObject>();
			query.Where(filter);

			if(db == null)
			{
				db = Db.For<DaoReferenceObject>();
			}
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
