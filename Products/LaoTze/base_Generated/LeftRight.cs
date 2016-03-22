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

namespace Bam.Net.DaoRef
{
	// schema = DaoRef
	// connection Name = DaoRef
	[Serializable]
	[Bam.Net.Data.Table("LeftRight", "DaoRef")]
	public partial class LeftRight: Dao
	{
		public LeftRight():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public LeftRight(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public LeftRight(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public LeftRight(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator LeftRight(DataRow data)
		{
			return new LeftRight(data);
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



	// start LeftId -> LeftId
	[Bam.Net.Data.ForeignKey(
        Table="LeftRight",
		Name="LeftId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Left",
		Suffix="1")]
	public long? LeftId
	{
		get
		{
			return GetLongValue("LeftId");
		}
		set
		{
			SetValue("LeftId", value);
		}
	}

	Left _leftOfLeftId;
	public Left LeftOfLeftId
	{
		get
		{
			if(_leftOfLeftId == null)
			{
				_leftOfLeftId = Bam.Net.DaoRef.Left.OneWhere(c => c.KeyColumn == this.LeftId, this.Database);
			}
			return _leftOfLeftId;
		}
	}
	
	// start RightId -> RightId
	[Bam.Net.Data.ForeignKey(
        Table="LeftRight",
		Name="RightId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Right",
		Suffix="2")]
	public long? RightId
	{
		get
		{
			return GetLongValue("RightId");
		}
		set
		{
			SetValue("RightId", value);
		}
	}

	Right _rightOfRightId;
	public Right RightOfRightId
	{
		get
		{
			if(_rightOfRightId == null)
			{
				_rightOfRightId = Bam.Net.DaoRef.Right.OneWhere(c => c.KeyColumn == this.RightId, this.Database);
			}
			return _rightOfRightId;
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
				var colFilter = new LeftRightColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the LeftRight table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static LeftRightCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<LeftRight>();
			Database db = database ?? Db.For<LeftRight>();
			var results = new LeftRightCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static async Task BatchAll(int batchSize, Func<LeftRightCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				LeftRightColumns columns = new LeftRightColumns();
				var orderBy = Order.By<LeftRightColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, (c) => c.KeyColumn > 0, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (c) => c.KeyColumn > topId, orderBy, database);
				}
			});			
		}	 

		public static async Task BatchQuery(int batchSize, QueryFilter filter, Func<LeftRightCollection, Task> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		public static async Task BatchQuery(int batchSize, WhereDelegate<LeftRightColumns> where, Func<LeftRightCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				LeftRightColumns columns = new LeftRightColumns();
				var orderBy = Order.By<LeftRightColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (LeftRightColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static LeftRight GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static LeftRight GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static LeftRight GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static LeftRight GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		public static LeftRightCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static LeftRightCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<LeftRightColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a LeftRightColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between LeftRightColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static LeftRightCollection Where(Func<LeftRightColumns, QueryFilter<LeftRightColumns>> where, OrderBy<LeftRightColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<LeftRight>();
			return new LeftRightCollection(database.GetQuery<LeftRightColumns, LeftRight>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LeftRightColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LeftRightColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static LeftRightCollection Where(WhereDelegate<LeftRightColumns> where, Database database = null)
		{		
			database = database ?? Db.For<LeftRight>();
			var results = new LeftRightCollection(database, database.GetQuery<LeftRightColumns, LeftRight>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LeftRightColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LeftRightColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static LeftRightCollection Where(WhereDelegate<LeftRightColumns> where, OrderBy<LeftRightColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<LeftRight>();
			var results = new LeftRightCollection(database, database.GetQuery<LeftRightColumns, LeftRight>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;LeftRightColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static LeftRightCollection Where(QiQuery where, Database database = null)
		{
			var results = new LeftRightCollection(database, Select<LeftRightColumns>.From<LeftRight>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static LeftRight GetOneWhere(QueryFilter where, Database database = null)
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
		public static LeftRight OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<LeftRightColumns> whereDelegate = (c) => where;
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
		public static LeftRight GetOneWhere(WhereDelegate<LeftRightColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				LeftRightColumns c = new LeftRightColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single LeftRight instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LeftRightColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LeftRightColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static LeftRight OneWhere(WhereDelegate<LeftRightColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;LeftRightColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static LeftRight OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LeftRightColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LeftRightColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static LeftRight FirstOneWhere(WhereDelegate<LeftRightColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a LeftRightColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LeftRightColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static LeftRight FirstOneWhere(WhereDelegate<LeftRightColumns> where, OrderBy<LeftRightColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a LeftRightColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LeftRightColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static LeftRight FirstOneWhere(QueryFilter where, OrderBy<LeftRightColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<LeftRightColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a LeftRightColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LeftRightColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static LeftRightCollection Top(int count, WhereDelegate<LeftRightColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a LeftRightColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LeftRightColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static LeftRightCollection Top(int count, WhereDelegate<LeftRightColumns> where, OrderBy<LeftRightColumns> orderBy, Database database = null)
		{
			LeftRightColumns c = new LeftRightColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<LeftRight>();
			QuerySet query = GetQuerySet(db); 
			query.Top<LeftRight>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<LeftRightColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<LeftRightCollection>(0);
			results.Database = db;
			return results;
		}

		public static LeftRightCollection Top(int count, QueryFilter where, Database database)
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
		public static LeftRightCollection Top(int count, QueryFilter where, OrderBy<LeftRightColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<LeftRight>();
			QuerySet query = GetQuerySet(db);
			query.Top<LeftRight>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<LeftRightColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<LeftRightCollection>(0);
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
		public static LeftRightCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<LeftRight>();
			QuerySet query = GetQuerySet(db);
			query.Top<LeftRight>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<LeftRightCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LeftRightColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LeftRightColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<LeftRightColumns> where, Database database = null)
		{
			LeftRightColumns c = new LeftRightColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<LeftRight>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<LeftRight>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static LeftRight CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<LeftRight>();			
			var dao = new LeftRight();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static LeftRight OneOrThrow(LeftRightCollection c)
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
