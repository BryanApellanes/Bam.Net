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
	[Bam.Net.Data.Table("LeftOfManyItemRightOfManyItem", "DaoTestData")]
	public partial class LeftOfManyItemRightOfManyItem: Dao
	{
		public LeftOfManyItemRightOfManyItem():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public LeftOfManyItemRightOfManyItem(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public LeftOfManyItemRightOfManyItem(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public LeftOfManyItemRightOfManyItem(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator LeftOfManyItemRightOfManyItem(DataRow data)
		{
			return new LeftOfManyItemRightOfManyItem(data);
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



	// start LeftOfManyItemId -> LeftOfManyItemId
	[Bam.Net.Data.ForeignKey(
        Table="LeftOfManyItemRightOfManyItem",
		Name="LeftOfManyItemId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="LeftOfManyItem",
		Suffix="1")]
	public long? LeftOfManyItemId
	{
		get
		{
			return GetLongValue("LeftOfManyItemId");
		}
		set
		{
			SetValue("LeftOfManyItemId", value);
		}
	}

	LeftOfManyItem _leftOfManyItemOfLeftOfManyItemId;
	public LeftOfManyItem LeftOfManyItemOfLeftOfManyItemId
	{
		get
		{
			if(_leftOfManyItemOfLeftOfManyItemId == null)
			{
				_leftOfManyItemOfLeftOfManyItemId = Bam.Net.Data.LeftOfManyItem.OneWhere(c => c.KeyColumn == this.LeftOfManyItemId, this.Database);
			}
			return _leftOfManyItemOfLeftOfManyItemId;
		}
	}
	
	// start RightOfManyItemId -> RightOfManyItemId
	[Bam.Net.Data.ForeignKey(
        Table="LeftOfManyItemRightOfManyItem",
		Name="RightOfManyItemId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="RightOfManyItem",
		Suffix="2")]
	public long? RightOfManyItemId
	{
		get
		{
			return GetLongValue("RightOfManyItemId");
		}
		set
		{
			SetValue("RightOfManyItemId", value);
		}
	}

	RightOfManyItem _rightOfManyItemOfRightOfManyItemId;
	public RightOfManyItem RightOfManyItemOfRightOfManyItemId
	{
		get
		{
			if(_rightOfManyItemOfRightOfManyItemId == null)
			{
				_rightOfManyItemOfRightOfManyItemId = Bam.Net.Data.RightOfManyItem.OneWhere(c => c.KeyColumn == this.RightOfManyItemId, this.Database);
			}
			return _rightOfManyItemOfRightOfManyItemId;
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
				var colFilter = new LeftOfManyItemRightOfManyItemColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the LeftOfManyItemRightOfManyItem table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static LeftOfManyItemRightOfManyItemCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<LeftOfManyItemRightOfManyItem>();
			Database db = database ?? Db.For<LeftOfManyItemRightOfManyItem>();
			var results = new LeftOfManyItemRightOfManyItemCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static async Task BatchAll(int batchSize, Func<LeftOfManyItemRightOfManyItemCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				LeftOfManyItemRightOfManyItemColumns columns = new LeftOfManyItemRightOfManyItemColumns();
				var orderBy = Order.By<LeftOfManyItemRightOfManyItemColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, (c) => c.KeyColumn > 0, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (c) => c.KeyColumn > topId, orderBy, database);
				}
			});			
		}	 

		public static async Task BatchQuery(int batchSize, QueryFilter filter, Func<LeftOfManyItemRightOfManyItemCollection, Task> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		public static async Task BatchQuery(int batchSize, WhereDelegate<LeftOfManyItemRightOfManyItemColumns> where, Func<LeftOfManyItemRightOfManyItemCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				LeftOfManyItemRightOfManyItemColumns columns = new LeftOfManyItemRightOfManyItemColumns();
				var orderBy = Order.By<LeftOfManyItemRightOfManyItemColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (LeftOfManyItemRightOfManyItemColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static LeftOfManyItemRightOfManyItem GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static LeftOfManyItemRightOfManyItem GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static LeftOfManyItemRightOfManyItem GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static LeftOfManyItemRightOfManyItem GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		public static LeftOfManyItemRightOfManyItemCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static LeftOfManyItemRightOfManyItemCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<LeftOfManyItemRightOfManyItemColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a LeftOfManyItemRightOfManyItemColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between LeftOfManyItemRightOfManyItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static LeftOfManyItemRightOfManyItemCollection Where(Func<LeftOfManyItemRightOfManyItemColumns, QueryFilter<LeftOfManyItemRightOfManyItemColumns>> where, OrderBy<LeftOfManyItemRightOfManyItemColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<LeftOfManyItemRightOfManyItem>();
			return new LeftOfManyItemRightOfManyItemCollection(database.GetQuery<LeftOfManyItemRightOfManyItemColumns, LeftOfManyItemRightOfManyItem>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LeftOfManyItemRightOfManyItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LeftOfManyItemRightOfManyItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static LeftOfManyItemRightOfManyItemCollection Where(WhereDelegate<LeftOfManyItemRightOfManyItemColumns> where, Database database = null)
		{		
			database = database ?? Db.For<LeftOfManyItemRightOfManyItem>();
			var results = new LeftOfManyItemRightOfManyItemCollection(database, database.GetQuery<LeftOfManyItemRightOfManyItemColumns, LeftOfManyItemRightOfManyItem>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LeftOfManyItemRightOfManyItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LeftOfManyItemRightOfManyItemColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static LeftOfManyItemRightOfManyItemCollection Where(WhereDelegate<LeftOfManyItemRightOfManyItemColumns> where, OrderBy<LeftOfManyItemRightOfManyItemColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<LeftOfManyItemRightOfManyItem>();
			var results = new LeftOfManyItemRightOfManyItemCollection(database, database.GetQuery<LeftOfManyItemRightOfManyItemColumns, LeftOfManyItemRightOfManyItem>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;LeftOfManyItemRightOfManyItemColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static LeftOfManyItemRightOfManyItemCollection Where(QiQuery where, Database database = null)
		{
			var results = new LeftOfManyItemRightOfManyItemCollection(database, Select<LeftOfManyItemRightOfManyItemColumns>.From<LeftOfManyItemRightOfManyItem>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static LeftOfManyItemRightOfManyItem GetOneWhere(QueryFilter where, Database database = null)
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
		public static LeftOfManyItemRightOfManyItem OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<LeftOfManyItemRightOfManyItemColumns> whereDelegate = (c) => where;
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
		public static LeftOfManyItemRightOfManyItem GetOneWhere(WhereDelegate<LeftOfManyItemRightOfManyItemColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				LeftOfManyItemRightOfManyItemColumns c = new LeftOfManyItemRightOfManyItemColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single LeftOfManyItemRightOfManyItem instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LeftOfManyItemRightOfManyItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LeftOfManyItemRightOfManyItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static LeftOfManyItemRightOfManyItem OneWhere(WhereDelegate<LeftOfManyItemRightOfManyItemColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;LeftOfManyItemRightOfManyItemColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static LeftOfManyItemRightOfManyItem OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LeftOfManyItemRightOfManyItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LeftOfManyItemRightOfManyItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static LeftOfManyItemRightOfManyItem FirstOneWhere(WhereDelegate<LeftOfManyItemRightOfManyItemColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a LeftOfManyItemRightOfManyItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LeftOfManyItemRightOfManyItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static LeftOfManyItemRightOfManyItem FirstOneWhere(WhereDelegate<LeftOfManyItemRightOfManyItemColumns> where, OrderBy<LeftOfManyItemRightOfManyItemColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a LeftOfManyItemRightOfManyItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LeftOfManyItemRightOfManyItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static LeftOfManyItemRightOfManyItem FirstOneWhere(QueryFilter where, OrderBy<LeftOfManyItemRightOfManyItemColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<LeftOfManyItemRightOfManyItemColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a LeftOfManyItemRightOfManyItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LeftOfManyItemRightOfManyItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static LeftOfManyItemRightOfManyItemCollection Top(int count, WhereDelegate<LeftOfManyItemRightOfManyItemColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a LeftOfManyItemRightOfManyItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LeftOfManyItemRightOfManyItemColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static LeftOfManyItemRightOfManyItemCollection Top(int count, WhereDelegate<LeftOfManyItemRightOfManyItemColumns> where, OrderBy<LeftOfManyItemRightOfManyItemColumns> orderBy, Database database = null)
		{
			LeftOfManyItemRightOfManyItemColumns c = new LeftOfManyItemRightOfManyItemColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<LeftOfManyItemRightOfManyItem>();
			QuerySet query = GetQuerySet(db); 
			query.Top<LeftOfManyItemRightOfManyItem>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<LeftOfManyItemRightOfManyItemColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<LeftOfManyItemRightOfManyItemCollection>(0);
			results.Database = db;
			return results;
		}

		public static LeftOfManyItemRightOfManyItemCollection Top(int count, QueryFilter where, Database database)
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
		public static LeftOfManyItemRightOfManyItemCollection Top(int count, QueryFilter where, OrderBy<LeftOfManyItemRightOfManyItemColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<LeftOfManyItemRightOfManyItem>();
			QuerySet query = GetQuerySet(db);
			query.Top<LeftOfManyItemRightOfManyItem>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<LeftOfManyItemRightOfManyItemColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<LeftOfManyItemRightOfManyItemCollection>(0);
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
		public static LeftOfManyItemRightOfManyItemCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<LeftOfManyItemRightOfManyItem>();
			QuerySet query = GetQuerySet(db);
			query.Top<LeftOfManyItemRightOfManyItem>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<LeftOfManyItemRightOfManyItemCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LeftOfManyItemRightOfManyItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LeftOfManyItemRightOfManyItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<LeftOfManyItemRightOfManyItemColumns> where, Database database = null)
		{
			LeftOfManyItemRightOfManyItemColumns c = new LeftOfManyItemRightOfManyItemColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<LeftOfManyItemRightOfManyItem>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<LeftOfManyItemRightOfManyItem>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static LeftOfManyItemRightOfManyItem CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<LeftOfManyItemRightOfManyItem>();			
			var dao = new LeftOfManyItemRightOfManyItem();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static LeftOfManyItemRightOfManyItem OneOrThrow(LeftOfManyItemRightOfManyItemCollection c)
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