/*
	This file was generated and should not be modified directly
*/
// Model is Table
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.Data.Repositories.Tests.ClrTypes.Daos
{
	// schema = TestDaoSchema
	// connection Name = TestDaoSchema
	[Serializable]
	[Bam.Net.Data.Table("HouseParent", "TestDaoSchema")]
	public partial class HouseParent: Dao
	{
		public HouseParent():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public HouseParent(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public HouseParent(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public HouseParent(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator HouseParent(DataRow data)
		{
			return new HouseParent(data);
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



	// start HouseId -> HouseId
	[Bam.Net.Data.ForeignKey(
        Table="HouseParent",
		Name="HouseId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="House",
		Suffix="1")]
	public long? HouseId
	{
		get
		{
			return GetLongValue("HouseId");
		}
		set
		{
			SetValue("HouseId", value);
		}
	}

	House _houseOfHouseId;
	public House HouseOfHouseId
	{
		get
		{
			if(_houseOfHouseId == null)
			{
				_houseOfHouseId = Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.House.OneWhere(c => c.KeyColumn == this.HouseId, this.Database);
			}
			return _houseOfHouseId;
		}
	}
	
	// start ParentId -> ParentId
	[Bam.Net.Data.ForeignKey(
        Table="HouseParent",
		Name="ParentId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Parent",
		Suffix="2")]
	public long? ParentId
	{
		get
		{
			return GetLongValue("ParentId");
		}
		set
		{
			SetValue("ParentId", value);
		}
	}

	Parent _parentOfParentId;
	public Parent ParentOfParentId
	{
		get
		{
			if(_parentOfParentId == null)
			{
				_parentOfParentId = Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Parent.OneWhere(c => c.KeyColumn == this.ParentId, this.Database);
			}
			return _parentOfParentId;
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
				var colFilter = new HouseParentColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the HouseParent table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static HouseParentCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<HouseParent>();
			Database db = database ?? Db.For<HouseParent>();
			var results = new HouseParentCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static async Task BatchAll(int batchSize, Action<IEnumerable<HouseParent>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				HouseParentColumns columns = new HouseParentColumns();
				var orderBy = Order.By<HouseParentColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, (c) => c.KeyColumn > 0, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (c) => c.KeyColumn > topId, orderBy, database);
				}
			});			
		}	 

		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<HouseParent>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		public static async Task BatchQuery(int batchSize, WhereDelegate<HouseParentColumns> where, Action<IEnumerable<HouseParent>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				HouseParentColumns columns = new HouseParentColumns();
				var orderBy = Order.By<HouseParentColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (HouseParentColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static HouseParent GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static HouseParent GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static HouseParent GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static HouseParent GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		public static HouseParentCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static HouseParentCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<HouseParentColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a HouseParentColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between HouseParentColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static HouseParentCollection Where(Func<HouseParentColumns, QueryFilter<HouseParentColumns>> where, OrderBy<HouseParentColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<HouseParent>();
			return new HouseParentCollection(database.GetQuery<HouseParentColumns, HouseParent>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HouseParentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseParentColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static HouseParentCollection Where(WhereDelegate<HouseParentColumns> where, Database database = null)
		{		
			database = database ?? Db.For<HouseParent>();
			var results = new HouseParentCollection(database, database.GetQuery<HouseParentColumns, HouseParent>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HouseParentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseParentColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static HouseParentCollection Where(WhereDelegate<HouseParentColumns> where, OrderBy<HouseParentColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<HouseParent>();
			var results = new HouseParentCollection(database, database.GetQuery<HouseParentColumns, HouseParent>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;HouseParentColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static HouseParentCollection Where(QiQuery where, Database database = null)
		{
			var results = new HouseParentCollection(database, Select<HouseParentColumns>.From<HouseParent>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static HouseParent GetOneWhere(QueryFilter where, Database database = null)
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
		public static HouseParent OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<HouseParentColumns> whereDelegate = (c) => where;
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
		public static HouseParent GetOneWhere(WhereDelegate<HouseParentColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				HouseParentColumns c = new HouseParentColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single HouseParent instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HouseParentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseParentColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static HouseParent OneWhere(WhereDelegate<HouseParentColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<HouseParentColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static HouseParent OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HouseParentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseParentColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static HouseParent FirstOneWhere(WhereDelegate<HouseParentColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a HouseParentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseParentColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static HouseParent FirstOneWhere(WhereDelegate<HouseParentColumns> where, OrderBy<HouseParentColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a HouseParentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseParentColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static HouseParent FirstOneWhere(QueryFilter where, OrderBy<HouseParentColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<HouseParentColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a HouseParentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseParentColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static HouseParentCollection Top(int count, WhereDelegate<HouseParentColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a HouseParentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseParentColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static HouseParentCollection Top(int count, WhereDelegate<HouseParentColumns> where, OrderBy<HouseParentColumns> orderBy, Database database = null)
		{
			HouseParentColumns c = new HouseParentColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<HouseParent>();
			QuerySet query = GetQuerySet(db); 
			query.Top<HouseParent>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<HouseParentColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<HouseParentCollection>(0);
			results.Database = db;
			return results;
		}

		public static HouseParentCollection Top(int count, QueryFilter where, Database database)
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
		public static HouseParentCollection Top(int count, QueryFilter where, OrderBy<HouseParentColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<HouseParent>();
			QuerySet query = GetQuerySet(db);
			query.Top<HouseParent>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<HouseParentColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<HouseParentCollection>(0);
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
		public static HouseParentCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<HouseParent>();
			QuerySet query = GetQuerySet(db);
			query.Top<HouseParent>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<HouseParentCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of HouseParents
		/// </summary>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<HouseParent>();
            QuerySet query = GetQuerySet(db);
            query.Count<HouseParent>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HouseParentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseParentColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<HouseParentColumns> where, Database database = null)
		{
			HouseParentColumns c = new HouseParentColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<HouseParent>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<HouseParent>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static HouseParent CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<HouseParent>();			
			var dao = new HouseParent();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static HouseParent OneOrThrow(HouseParentCollection c)
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
