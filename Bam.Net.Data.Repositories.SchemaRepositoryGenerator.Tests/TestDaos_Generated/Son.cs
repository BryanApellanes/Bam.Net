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
	[Bam.Net.Data.Table("Son", "TestDaoSchema")]
	public partial class Son: Dao
	{
		public Son():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Son(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Son(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Son(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator Son(DataRow data)
		{
			return new Son(data);
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

	// property:Name, columnName:Name	
	[Bam.Net.Data.Column(Name="Name", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
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



	// start ParentId -> ParentId
	[Bam.Net.Data.ForeignKey(
        Table="Son",
		Name="ParentId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Parent",
		Suffix="1")]
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
				var colFilter = new SonColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the Son table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static SonCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Son>();
			Database db = database ?? Db.For<Son>();
			var results = new SonCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static async Task BatchAll(int batchSize, Action<IEnumerable<Son>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				SonColumns columns = new SonColumns();
				var orderBy = Order.By<SonColumns>(c => c.KeyColumn, SortOrder.Ascending);
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

		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<Son>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		public static async Task BatchQuery(int batchSize, WhereDelegate<SonColumns> where, Action<IEnumerable<Son>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				SonColumns columns = new SonColumns();
				var orderBy = Order.By<SonColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (SonColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static Son GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Son GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Son GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static Son GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		public static SonCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static SonCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<SonColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a SonColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between SonColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SonCollection Where(Func<SonColumns, QueryFilter<SonColumns>> where, OrderBy<SonColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<Son>();
			return new SonCollection(database.GetQuery<SonColumns, Son>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SonColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SonColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SonCollection Where(WhereDelegate<SonColumns> where, Database database = null)
		{		
			database = database ?? Db.For<Son>();
			var results = new SonCollection(database, database.GetQuery<SonColumns, Son>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SonColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SonColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static SonCollection Where(WhereDelegate<SonColumns> where, OrderBy<SonColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<Son>();
			var results = new SonCollection(database, database.GetQuery<SonColumns, Son>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;SonColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static SonCollection Where(QiQuery where, Database database = null)
		{
			var results = new SonCollection(database, Select<SonColumns>.From<Son>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static Son GetOneWhere(QueryFilter where, Database database = null)
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
		public static Son OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<SonColumns> whereDelegate = (c) => where;
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
		public static Son GetOneWhere(WhereDelegate<SonColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				SonColumns c = new SonColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Son instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SonColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SonColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Son OneWhere(WhereDelegate<SonColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<SonColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Son OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SonColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SonColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Son FirstOneWhere(WhereDelegate<SonColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a SonColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SonColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Son FirstOneWhere(WhereDelegate<SonColumns> where, OrderBy<SonColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a SonColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SonColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Son FirstOneWhere(QueryFilter where, OrderBy<SonColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<SonColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a SonColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SonColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static SonCollection Top(int count, WhereDelegate<SonColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a SonColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SonColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static SonCollection Top(int count, WhereDelegate<SonColumns> where, OrderBy<SonColumns> orderBy, Database database = null)
		{
			SonColumns c = new SonColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<Son>();
			QuerySet query = GetQuerySet(db); 
			query.Top<Son>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<SonColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<SonCollection>(0);
			results.Database = db;
			return results;
		}

		public static SonCollection Top(int count, QueryFilter where, Database database)
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
		public static SonCollection Top(int count, QueryFilter where, OrderBy<SonColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<Son>();
			QuerySet query = GetQuerySet(db);
			query.Top<Son>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<SonColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<SonCollection>(0);
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
		public static SonCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<Son>();
			QuerySet query = GetQuerySet(db);
			query.Top<Son>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<SonCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of Sons
		/// </summary>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<Son>();
            QuerySet query = GetQuerySet(db);
            query.Count<Son>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SonColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SonColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<SonColumns> where, Database database = null)
		{
			SonColumns c = new SonColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<Son>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Son>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static Son CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<Son>();			
			var dao = new Son();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static Son OneOrThrow(SonCollection c)
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
