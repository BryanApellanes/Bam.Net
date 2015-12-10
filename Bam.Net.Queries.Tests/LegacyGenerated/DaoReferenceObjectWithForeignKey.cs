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
    [Bam.Net.Data.Table("DaoReferenceObjectWithForeignKey", "DaoReferenceObjects")]
    public partial class DaoReferenceObjectWithForeignKey: Dao
    {
        public DaoReferenceObjectWithForeignKey():base()
		{
			this.KeyColumnName = "Id";
		}

        public DaoReferenceObjectWithForeignKey(DataRow data): base(data)
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

	// property:Name, columnName:Name	
	[Bam.Net.Data.Column(Name="Name", ExtractedType="NVarChar", MaxLength="50", AllowNull=false)]
	public string Name
	{
		get
		{
			return GetStringValue("Name");
		}
		set
		{
			SetValue("Name", value);
		}
	}


	// start DaoReferenceObjectId -> DaoReferenceObjectId
	[Bam.Net.Data.ForeignKey(
        Table="DaoReferenceObjectWithForeignKey",
		Name="DaoReferenceObjectId", 
		ExtractedType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="DaoReferenceObject",
		Suffix="1")]
	public long? DaoReferenceObjectId
	{
		get
		{
			return GetLongValue("DaoReferenceObjectId");
		}
		set
		{
			SetValue("DaoReferenceObjectId", value);
		}
	}

	public DaoReferenceObject DaoReferenceObjectOfDaoReferenceObjectId
	{
		get
		{
			return DaoReferenceObject.OneWhere(f => f.Id == this.DaoReferenceObjectId);
		}
	}
	
				
		
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new DaoReferenceObjectWithForeignKeyColumns();
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
		   
		public static DaoReferenceObjectWithForeignKeyCollection Where(WhereDelegate<DaoReferenceObjectWithForeignKeyColumns> where, Database db = null)
		{
			return new DaoReferenceObjectWithForeignKeyCollection(Select<DaoReferenceObjectWithForeignKeyColumns>.From<DaoReferenceObjectWithForeignKey>().Where(where, db));
		}

		public static DaoReferenceObjectWithForeignKeyCollection Where(QiQuery where, Database db = null)
		{
			return new DaoReferenceObjectWithForeignKeyCollection(Select<DaoReferenceObjectWithForeignKeyColumns>.From<DaoReferenceObjectWithForeignKey>().Where(where, db));
		}

		public static DaoReferenceObjectWithForeignKey OneWhere(WhereDelegate<DaoReferenceObjectWithForeignKeyColumns> where, Database db = null)
		{
			var results = new DaoReferenceObjectWithForeignKeyCollection(Select<DaoReferenceObjectWithForeignKeyColumns>.From<DaoReferenceObjectWithForeignKey>().Where(where, db));
			return OneOrThrow(results);
		}

		public static DaoReferenceObjectWithForeignKey OneWhere(QiQuery where, Database db = null)
		{
			var results = new DaoReferenceObjectWithForeignKeyCollection(Select<DaoReferenceObjectWithForeignKeyColumns>.From<DaoReferenceObjectWithForeignKey>().Where(where, db));
			return OneOrThrow(results);
		}

		private static DaoReferenceObjectWithForeignKey OneOrThrow(DaoReferenceObjectWithForeignKeyCollection c)
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

		public static DaoReferenceObjectWithForeignKey FirstOneWhere(WhereDelegate<DaoReferenceObjectWithForeignKeyColumns> where, Database db = null)
		{
			var results = new DaoReferenceObjectWithForeignKeyCollection(Select<DaoReferenceObjectWithForeignKeyColumns>.From<DaoReferenceObjectWithForeignKey>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static DaoReferenceObjectWithForeignKeyCollection Top(int count, WhereDelegate<DaoReferenceObjectWithForeignKeyColumns> where, Database db = null)
        {
            DaoReferenceObjectWithForeignKeyColumns c = new DaoReferenceObjectWithForeignKeyColumns();
            IQueryFilter filter = where(c);         
            QuerySet query = new QuerySet();
            query.Top<DaoReferenceObjectWithForeignKey>(count);
            query.Where(filter);

            if (db == null)
            {
                db = Db.For<DaoReferenceObjectWithForeignKey>();
            }

            query.Execute(db);
            return query.Results.As<DaoReferenceObjectWithForeignKeyCollection>(0);
        }

		public static long Count(WhereDelegate<DaoReferenceObjectWithForeignKeyColumns> where, Database db = null)
		{
			DaoReferenceObjectWithForeignKeyColumns c = new DaoReferenceObjectWithForeignKeyColumns();
			IQueryFilter filter = where(c) ;
			QuerySet query = new QuerySet();
			query.Count<DaoReferenceObjectWithForeignKey>();
			query.Where(filter);

			if(db == null)
			{
				db = Db.For<DaoReferenceObjectWithForeignKey>();
			}
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
