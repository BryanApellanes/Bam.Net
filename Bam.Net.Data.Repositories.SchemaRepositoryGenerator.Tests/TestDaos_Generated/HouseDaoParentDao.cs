/*
	This file was generated and should not be modified directly
*/
// Model is Table
using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_
{
	// schema = TestDaoSchema
	// connection Name = TestDaoSchema
	[Serializable]
	[Bam.Net.Data.Table("HouseDaoParentDao", "TestDaoSchema")]
	public partial class HouseDaoParentDao: Dao
	{
		public HouseDaoParentDao():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public HouseDaoParentDao(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public HouseDaoParentDao(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public HouseDaoParentDao(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator HouseDaoParentDao(DataRow data)
		{
			return new HouseDaoParentDao(data);
		}

		private void SetChildren()
		{
						
		}

	// property:Id, columnName:Id	
	[Bam.Net.Exclude]
	[Bam.Net.Data.KeyColumn(Name="Id", DbDataType="BigInt", MaxLength="19")]
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

	// property:Uuid, columnName:Uuid	
	[Bam.Net.Data.Column(Name="Uuid", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Uuid
	{
		get
		{
			return GetStringValue("Uuid");
		}
		set
		{
			SetValue("Uuid", value);
		}
	}



	// start HouseDaoId -> HouseDaoId
	[Bam.Net.Data.ForeignKey(
        Table="HouseDaoParentDao",
		Name="HouseDaoId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="HouseDao",
		Suffix="1")]
	public long? HouseDaoId
	{
		get
		{
			return GetLongValue("HouseDaoId");
		}
		set
		{
			SetValue("HouseDaoId", value);
		}
	}

	HouseDao _houseDaoOfHouseDaoId;
	public HouseDao HouseDaoOfHouseDaoId
	{
		get
		{
			if(_houseDaoOfHouseDaoId == null)
			{
				_houseDaoOfHouseDaoId = Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.HouseDao.OneWhere(c => c.KeyColumn == this.HouseDaoId, this.Database);
			}
			return _houseDaoOfHouseDaoId;
		}
	}
	
	// start ParentDaoId -> ParentDaoId
	[Bam.Net.Data.ForeignKey(
        Table="HouseDaoParentDao",
		Name="ParentDaoId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="ParentDao",
		Suffix="2")]
	public long? ParentDaoId
	{
		get
		{
			return GetLongValue("ParentDaoId");
		}
		set
		{
			SetValue("ParentDaoId", value);
		}
	}

	ParentDao _parentDaoOfParentDaoId;
	public ParentDao ParentDaoOfParentDaoId
	{
		get
		{
			if(_parentDaoOfParentDaoId == null)
			{
				_parentDaoOfParentDaoId = Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.ParentDao.OneWhere(c => c.KeyColumn == this.ParentDaoId, this.Database);
			}
			return _parentDaoOfParentDaoId;
		}
	}
	
				
		

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance's.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			if(UniqueFilterProvider != null)
			{
				return UniqueFilterProvider();
			}
			else
			{
				var colFilter = new HouseDaoParentDaoColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the HouseDaoParentDao table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static HouseDaoParentDaoCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<HouseDaoParentDao>();
			Database db = database ?? Db.For<HouseDaoParentDao>();
			var results = new HouseDaoParentDaoCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static async Task BatchAll(int batchSize, Func<HouseDaoParentDaoCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				HouseDaoParentDaoColumns columns = new HouseDaoParentDaoColumns();
				var orderBy = Order.By<HouseDaoParentDaoColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, (c) => c.KeyColumn > 0, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (c) => c.KeyColumn > topId, orderBy, database);
				}
			});			
		}	 

		public static async Task BatchQuery(int batchSize, QueryFilter filter, Func<HouseDaoParentDaoCollection, Task> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		public static async Task BatchQuery(int batchSize, WhereDelegate<HouseDaoParentDaoColumns> where, Func<HouseDaoParentDaoCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				HouseDaoParentDaoColumns columns = new HouseDaoParentDaoColumns();
				var orderBy = Order.By<HouseDaoParentDaoColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (HouseDaoParentDaoColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static HouseDaoParentDao GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static HouseDaoParentDao GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static HouseDaoParentDao GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static HouseDaoParentDao GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		public static HouseDaoParentDaoCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static HouseDaoParentDaoCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<HouseDaoParentDaoColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a HouseDaoParentDaoColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between HouseDaoParentDaoColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static HouseDaoParentDaoCollection Where(Func<HouseDaoParentDaoColumns, QueryFilter<HouseDaoParentDaoColumns>> where, OrderBy<HouseDaoParentDaoColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<HouseDaoParentDao>();
			return new HouseDaoParentDaoCollection(database.GetQuery<HouseDaoParentDaoColumns, HouseDaoParentDao>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HouseDaoParentDaoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseDaoParentDaoColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static HouseDaoParentDaoCollection Where(WhereDelegate<HouseDaoParentDaoColumns> where, Database database = null)
		{		
			database = database ?? Db.For<HouseDaoParentDao>();
			var results = new HouseDaoParentDaoCollection(database, database.GetQuery<HouseDaoParentDaoColumns, HouseDaoParentDao>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HouseDaoParentDaoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseDaoParentDaoColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static HouseDaoParentDaoCollection Where(WhereDelegate<HouseDaoParentDaoColumns> where, OrderBy<HouseDaoParentDaoColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<HouseDaoParentDao>();
			var results = new HouseDaoParentDaoCollection(database, database.GetQuery<HouseDaoParentDaoColumns, HouseDaoParentDao>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;HouseDaoParentDaoColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static HouseDaoParentDaoCollection Where(QiQuery where, Database database = null)
		{
			var results = new HouseDaoParentDaoCollection(database, Select<HouseDaoParentDaoColumns>.From<HouseDaoParentDao>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static HouseDaoParentDao GetOneWhere(QueryFilter where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				result = CreateFromFilter(where, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static HouseDaoParentDao OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<HouseDaoParentDaoColumns> whereDelegate = (c) => where;
			var result = Top(1, whereDelegate, database);
			return OneOrThrow(result);
		}

		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static HouseDaoParentDao GetOneWhere(WhereDelegate<HouseDaoParentDaoColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				HouseDaoParentDaoColumns c = new HouseDaoParentDaoColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single HouseDaoParentDao instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HouseDaoParentDaoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseDaoParentDaoColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static HouseDaoParentDao OneWhere(WhereDelegate<HouseDaoParentDaoColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<HouseDaoParentDaoColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static HouseDaoParentDao OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HouseDaoParentDaoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseDaoParentDaoColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static HouseDaoParentDao FirstOneWhere(WhereDelegate<HouseDaoParentDaoColumns> where, Database database = null)
		{
			var results = Top(1, where, database);
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}
		
		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HouseDaoParentDaoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseDaoParentDaoColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static HouseDaoParentDao FirstOneWhere(WhereDelegate<HouseDaoParentDaoColumns> where, OrderBy<HouseDaoParentDaoColumns> orderBy, Database database = null)
		{
			var results = Top(1, where, orderBy, database);
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Shortcut for Top(1, where, orderBy, database)
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HouseDaoParentDaoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseDaoParentDaoColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static HouseDaoParentDao FirstOneWhere(QueryFilter where, OrderBy<HouseDaoParentDaoColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<HouseDaoParentDaoColumns> whereDelegate = (c) => where;
			var results = Top(1, whereDelegate, orderBy, database);
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Execute a query and return the specified number
		/// of values. This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a HouseDaoParentDaoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseDaoParentDaoColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static HouseDaoParentDaoCollection Top(int count, WhereDelegate<HouseDaoParentDaoColumns> where, Database database = null)
		{
			return Top(count, where, null, database);
		}

		/// <summary>
		/// Execute a query and return the specified number of values.  This method
		/// will issue a sql TOP clause so only the specified number of values
		/// will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a HouseDaoParentDaoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseDaoParentDaoColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static HouseDaoParentDaoCollection Top(int count, WhereDelegate<HouseDaoParentDaoColumns> where, OrderBy<HouseDaoParentDaoColumns> orderBy, Database database = null)
		{
			HouseDaoParentDaoColumns c = new HouseDaoParentDaoColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<HouseDaoParentDao>();
			QuerySet query = GetQuerySet(db); 
			query.Top<HouseDaoParentDao>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<HouseDaoParentDaoColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<HouseDaoParentDaoCollection>(0);
			results.Database = db;
			return results;
		}

		public static HouseDaoParentDaoCollection Top(int count, QueryFilter where, Database database)
		{
			return Top(count, where, null, database);
		}
		/// <summary>
		/// Execute a query and return the specified number of values.  This method
		/// will issue a sql TOP clause so only the specified number of values
		/// will be returned.
		/// of values
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A QueryFilter used to filter the 
		/// results
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static HouseDaoParentDaoCollection Top(int count, QueryFilter where, OrderBy<HouseDaoParentDaoColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<HouseDaoParentDao>();
			QuerySet query = GetQuerySet(db);
			query.Top<HouseDaoParentDao>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<HouseDaoParentDaoColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<HouseDaoParentDaoCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the specified number of values.  This method
		/// will issue a sql TOP clause so only the specified number of values
		/// will be returned.
		/// of values
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A QueryFilter used to filter the 
		/// results
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static HouseDaoParentDaoCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<HouseDaoParentDao>();
			QuerySet query = GetQuerySet(db);
			query.Top<HouseDaoParentDao>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<HouseDaoParentDaoCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of HouseDaoParentDaos
		/// </summary>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<HouseDaoParentDao>();
            QuerySet query = GetQuerySet(db);
            query.Count<HouseDaoParentDao>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HouseDaoParentDaoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseDaoParentDaoColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<HouseDaoParentDaoColumns> where, Database database = null)
		{
			HouseDaoParentDaoColumns c = new HouseDaoParentDaoColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<HouseDaoParentDao>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<HouseDaoParentDao>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static HouseDaoParentDao CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<HouseDaoParentDao>();			
			var dao = new HouseDaoParentDao();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static HouseDaoParentDao OneOrThrow(HouseDaoParentDaoCollection c)
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

	}
}																								
