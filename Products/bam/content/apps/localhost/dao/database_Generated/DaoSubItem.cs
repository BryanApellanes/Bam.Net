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

namespace Bam.Net.Data
{
	// schema = DaoTestData
	// connection Name = DaoTestData
	[Serializable]
	[Bam.Net.Data.Table("DaoSubItem", "DaoTestData")]
	public partial class DaoSubItem: Dao
	{
		public DaoSubItem():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DaoSubItem(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DaoSubItem(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DaoSubItem(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator DaoSubItem(DataRow data)
		{
			return new DaoSubItem(data);
		}

		private void SetChildren()
		{
						
		}

	// property:Id, columnName:Id	
	[Exclude]
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

	// property:Name, columnName:Name	
	[Bam.Net.Data.Column(Name="Name", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
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

	// property:Created, columnName:Created	
	[Bam.Net.Data.Column(Name="Created", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? Created
	{
		get
		{
			return GetDateTimeValue("Created");
		}
		set
		{
			SetValue("Created", value);
		}
	}



	// start DaoBaseItemId -> DaoBaseItemId
	[Bam.Net.Data.ForeignKey(
        Table="DaoSubItem",
		Name="DaoBaseItemId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="DaoBaseItem",
		Suffix="1")]
	public long? DaoBaseItemId
	{
		get
		{
			return GetLongValue("DaoBaseItemId");
		}
		set
		{
			SetValue("DaoBaseItemId", value);
		}
	}

	DaoBaseItem _daoBaseItemOfDaoBaseItemId;
	public DaoBaseItem DaoBaseItemOfDaoBaseItemId
	{
		get
		{
			if(_daoBaseItemOfDaoBaseItemId == null)
			{
				_daoBaseItemOfDaoBaseItemId = Bam.Net.Data.DaoBaseItem.OneWhere(c => c.KeyColumn == this.DaoBaseItemId, this.Database);
			}
			return _daoBaseItemOfDaoBaseItemId;
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
				var colFilter = new DaoSubItemColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the DaoSubItem table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static DaoSubItemCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<DaoSubItem>();
			Database db = database ?? Db.For<DaoSubItem>();
			var results = new DaoSubItemCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static async Task BatchAll(int batchSize, Func<DaoSubItemCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				DaoSubItemColumns columns = new DaoSubItemColumns();
				var orderBy = Order.By<DaoSubItemColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, (c) => c.KeyColumn > 0, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (c) => c.KeyColumn > topId, orderBy, database);
				}
			});			
		}	 

		public static async Task BatchQuery(int batchSize, QueryFilter filter, Func<DaoSubItemCollection, Task> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		public static async Task BatchQuery(int batchSize, WhereDelegate<DaoSubItemColumns> where, Func<DaoSubItemCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				DaoSubItemColumns columns = new DaoSubItemColumns();
				var orderBy = Order.By<DaoSubItemColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (DaoSubItemColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static DaoSubItem GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static DaoSubItem GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static DaoSubItem GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static DaoSubItem GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		public static DaoSubItemCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static DaoSubItemCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<DaoSubItemColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a DaoSubItemColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between DaoSubItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static DaoSubItemCollection Where(Func<DaoSubItemColumns, QueryFilter<DaoSubItemColumns>> where, OrderBy<DaoSubItemColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<DaoSubItem>();
			return new DaoSubItemCollection(database.GetQuery<DaoSubItemColumns, DaoSubItem>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DaoSubItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DaoSubItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static DaoSubItemCollection Where(WhereDelegate<DaoSubItemColumns> where, Database database = null)
		{		
			database = database ?? Db.For<DaoSubItem>();
			var results = new DaoSubItemCollection(database, database.GetQuery<DaoSubItemColumns, DaoSubItem>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DaoSubItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DaoSubItemColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static DaoSubItemCollection Where(WhereDelegate<DaoSubItemColumns> where, OrderBy<DaoSubItemColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<DaoSubItem>();
			var results = new DaoSubItemCollection(database, database.GetQuery<DaoSubItemColumns, DaoSubItem>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;DaoSubItemColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static DaoSubItemCollection Where(QiQuery where, Database database = null)
		{
			var results = new DaoSubItemCollection(database, Select<DaoSubItemColumns>.From<DaoSubItem>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static DaoSubItem GetOneWhere(QueryFilter where, Database database = null)
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
		public static DaoSubItem OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<DaoSubItemColumns> whereDelegate = (c) => where;
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
		public static DaoSubItem GetOneWhere(WhereDelegate<DaoSubItemColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				DaoSubItemColumns c = new DaoSubItemColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single DaoSubItem instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DaoSubItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DaoSubItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static DaoSubItem OneWhere(WhereDelegate<DaoSubItemColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<DaoSubItemColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static DaoSubItem OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DaoSubItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DaoSubItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static DaoSubItem FirstOneWhere(WhereDelegate<DaoSubItemColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DaoSubItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DaoSubItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static DaoSubItem FirstOneWhere(WhereDelegate<DaoSubItemColumns> where, OrderBy<DaoSubItemColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DaoSubItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DaoSubItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static DaoSubItem FirstOneWhere(QueryFilter where, OrderBy<DaoSubItemColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<DaoSubItemColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a DaoSubItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DaoSubItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static DaoSubItemCollection Top(int count, WhereDelegate<DaoSubItemColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DaoSubItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DaoSubItemColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static DaoSubItemCollection Top(int count, WhereDelegate<DaoSubItemColumns> where, OrderBy<DaoSubItemColumns> orderBy, Database database = null)
		{
			DaoSubItemColumns c = new DaoSubItemColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<DaoSubItem>();
			QuerySet query = GetQuerySet(db); 
			query.Top<DaoSubItem>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<DaoSubItemColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<DaoSubItemCollection>(0);
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
		public static DaoSubItemCollection Top(int count, QueryFilter where, OrderBy<DaoSubItemColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<DaoSubItem>();
			QuerySet query = GetQuerySet(db);
			query.Top<DaoSubItem>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<DaoSubItemColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<DaoSubItemCollection>(0);
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
		public static DaoSubItemCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<DaoSubItem>();
			QuerySet query = GetQuerySet(db);
			query.Top<DaoSubItem>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<DaoSubItemCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DaoSubItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DaoSubItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<DaoSubItemColumns> where, Database database = null)
		{
			DaoSubItemColumns c = new DaoSubItemColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<DaoSubItem>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<DaoSubItem>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static DaoSubItem CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<DaoSubItem>();			
			var dao = new DaoSubItem();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static DaoSubItem OneOrThrow(DaoSubItemCollection c)
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
