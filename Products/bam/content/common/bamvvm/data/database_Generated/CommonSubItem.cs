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

namespace Bryan.Common.Data
{
	// schema = BryanCommon
	// connection Name = BryanCommon
	[Serializable]
	[Bam.Net.Data.Table("CommonSubItem", "BryanCommon")]
	public partial class CommonSubItem: Dao
	{
		public CommonSubItem():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public CommonSubItem(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public CommonSubItem(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public CommonSubItem(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator CommonSubItem(DataRow data)
		{
			return new CommonSubItem(data);
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



	// start CommonItemId -> CommonItemId
	[Bam.Net.Data.ForeignKey(
        Table="CommonSubItem",
		Name="CommonItemId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="CommonItem",
		Suffix="1")]
	public long? CommonItemId
	{
		get
		{
			return GetLongValue("CommonItemId");
		}
		set
		{
			SetValue("CommonItemId", value);
		}
	}

	CommonItem _commonItemOfCommonItemId;
	public CommonItem CommonItemOfCommonItemId
	{
		get
		{
			if(_commonItemOfCommonItemId == null)
			{
				_commonItemOfCommonItemId = Bryan.Common.Data.CommonItem.OneWhere(c => c.KeyColumn == this.CommonItemId, this.Database);
			}
			return _commonItemOfCommonItemId;
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
				var colFilter = new CommonSubItemColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the CommonSubItem table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static CommonSubItemCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<CommonSubItem>();
			Database db = database ?? Db.For<CommonSubItem>();
			var results = new CommonSubItemCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static async Task BatchAll(int batchSize, Func<CommonSubItemCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				CommonSubItemColumns columns = new CommonSubItemColumns();
				var orderBy = Order.By<CommonSubItemColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, (c) => c.KeyColumn > 0, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (c) => c.KeyColumn > topId, orderBy, database);
				}
			});			
		}	 

		public static async Task BatchQuery(int batchSize, QueryFilter filter, Func<CommonSubItemCollection, Task> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		public static async Task BatchQuery(int batchSize, WhereDelegate<CommonSubItemColumns> where, Func<CommonSubItemCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				CommonSubItemColumns columns = new CommonSubItemColumns();
				var orderBy = Order.By<CommonSubItemColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (CommonSubItemColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static CommonSubItem GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static CommonSubItem GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static CommonSubItem GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static CommonSubItem GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		public static CommonSubItemCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static CommonSubItemCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<CommonSubItemColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a CommonSubItemColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between CommonSubItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CommonSubItemCollection Where(Func<CommonSubItemColumns, QueryFilter<CommonSubItemColumns>> where, OrderBy<CommonSubItemColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<CommonSubItem>();
			return new CommonSubItemCollection(database.GetQuery<CommonSubItemColumns, CommonSubItem>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CommonSubItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CommonSubItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CommonSubItemCollection Where(WhereDelegate<CommonSubItemColumns> where, Database database = null)
		{		
			database = database ?? Db.For<CommonSubItem>();
			var results = new CommonSubItemCollection(database, database.GetQuery<CommonSubItemColumns, CommonSubItem>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CommonSubItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CommonSubItemColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static CommonSubItemCollection Where(WhereDelegate<CommonSubItemColumns> where, OrderBy<CommonSubItemColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<CommonSubItem>();
			var results = new CommonSubItemCollection(database, database.GetQuery<CommonSubItemColumns, CommonSubItem>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;CommonSubItemColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static CommonSubItemCollection Where(QiQuery where, Database database = null)
		{
			var results = new CommonSubItemCollection(database, Select<CommonSubItemColumns>.From<CommonSubItem>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static CommonSubItem GetOneWhere(QueryFilter where, Database database = null)
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
		public static CommonSubItem OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<CommonSubItemColumns> whereDelegate = (c) => where;
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
		public static CommonSubItem GetOneWhere(WhereDelegate<CommonSubItemColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				CommonSubItemColumns c = new CommonSubItemColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single CommonSubItem instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CommonSubItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CommonSubItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static CommonSubItem OneWhere(WhereDelegate<CommonSubItemColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;CommonSubItemColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static CommonSubItem OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CommonSubItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CommonSubItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static CommonSubItem FirstOneWhere(WhereDelegate<CommonSubItemColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a CommonSubItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CommonSubItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static CommonSubItem FirstOneWhere(WhereDelegate<CommonSubItemColumns> where, OrderBy<CommonSubItemColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a CommonSubItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CommonSubItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static CommonSubItem FirstOneWhere(QueryFilter where, OrderBy<CommonSubItemColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<CommonSubItemColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a CommonSubItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CommonSubItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static CommonSubItemCollection Top(int count, WhereDelegate<CommonSubItemColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a CommonSubItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CommonSubItemColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static CommonSubItemCollection Top(int count, WhereDelegate<CommonSubItemColumns> where, OrderBy<CommonSubItemColumns> orderBy, Database database = null)
		{
			CommonSubItemColumns c = new CommonSubItemColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<CommonSubItem>();
			QuerySet query = GetQuerySet(db); 
			query.Top<CommonSubItem>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<CommonSubItemColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<CommonSubItemCollection>(0);
			results.Database = db;
			return results;
		}

		public static CommonSubItemCollection Top(int count, QueryFilter where, Database database)
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
		public static CommonSubItemCollection Top(int count, QueryFilter where, OrderBy<CommonSubItemColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<CommonSubItem>();
			QuerySet query = GetQuerySet(db);
			query.Top<CommonSubItem>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<CommonSubItemColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<CommonSubItemCollection>(0);
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
		public static CommonSubItemCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<CommonSubItem>();
			QuerySet query = GetQuerySet(db);
			query.Top<CommonSubItem>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<CommonSubItemCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CommonSubItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CommonSubItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<CommonSubItemColumns> where, Database database = null)
		{
			CommonSubItemColumns c = new CommonSubItemColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<CommonSubItem>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<CommonSubItem>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static CommonSubItem CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<CommonSubItem>();			
			var dao = new CommonSubItem();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static CommonSubItem OneOrThrow(CommonSubItemCollection c)
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
