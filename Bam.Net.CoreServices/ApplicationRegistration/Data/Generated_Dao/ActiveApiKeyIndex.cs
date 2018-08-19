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

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
	// schema = ApplicationRegistration
	// connection Name = ApplicationRegistration
	[Serializable]
	[Bam.Net.Data.Table("ActiveApiKeyIndex", "ApplicationRegistration")]
	public partial class ActiveApiKeyIndex: Bam.Net.Data.Dao
	{
		public ActiveApiKeyIndex():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ActiveApiKeyIndex(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ActiveApiKeyIndex(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ActiveApiKeyIndex(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator ActiveApiKeyIndex(DataRow data)
		{
			return new ActiveApiKeyIndex(data);
		}

		private void SetChildren()
		{
						
		}

	// property:Id, columnName:Id	
	[Bam.Net.Exclude]
	[Bam.Net.Data.KeyColumn(Name="Id", DbDataType="BigInt", MaxLength="19")]
	public ulong? Id
	{
		get
		{
			return GetULongValue("Id");
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

	// property:Cuid, columnName:Cuid	
	[Bam.Net.Data.Column(Name="Cuid", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Cuid
	{
		get
		{
			return GetStringValue("Cuid");
		}
		set
		{
			SetValue("Cuid", value);
		}
	}

	// property:ApplicationCuid, columnName:ApplicationCuid	
	[Bam.Net.Data.Column(Name="ApplicationCuid", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string ApplicationCuid
	{
		get
		{
			return GetStringValue("ApplicationCuid");
		}
		set
		{
			SetValue("ApplicationCuid", value);
		}
	}

	// property:Value, columnName:Value	
	[Bam.Net.Data.Column(Name="Value", DbDataType="Int", MaxLength="10", AllowNull=true)]
	public int? Value
	{
		get
		{
			return GetIntValue("Value");
		}
		set
		{
			SetValue("Value", value);
		}
	}

	// property:CreatedBy, columnName:CreatedBy	
	[Bam.Net.Data.Column(Name="CreatedBy", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string CreatedBy
	{
		get
		{
			return GetStringValue("CreatedBy");
		}
		set
		{
			SetValue("CreatedBy", value);
		}
	}

	// property:ModifiedBy, columnName:ModifiedBy	
	[Bam.Net.Data.Column(Name="ModifiedBy", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string ModifiedBy
	{
		get
		{
			return GetStringValue("ModifiedBy");
		}
		set
		{
			SetValue("ModifiedBy", value);
		}
	}

	// property:Modified, columnName:Modified	
	[Bam.Net.Data.Column(Name="Modified", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? Modified
	{
		get
		{
			return GetDateTimeValue("Modified");
		}
		set
		{
			SetValue("Modified", value);
		}
	}

	// property:Deleted, columnName:Deleted	
	[Bam.Net.Data.Column(Name="Deleted", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? Deleted
	{
		get
		{
			return GetDateTimeValue("Deleted");
		}
		set
		{
			SetValue("Deleted", value);
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



				
		

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance's.
		/// </summary>
		[Bam.Net.Exclude] 
		public override IQueryFilter GetUniqueFilter()
		{
			if(UniqueFilterProvider != null)
			{
				return UniqueFilterProvider(this);
			}
			else
			{
				var colFilter = new ActiveApiKeyIndexColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the ActiveApiKeyIndex table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ActiveApiKeyIndexCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<ActiveApiKeyIndex>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<ActiveApiKeyIndex>();
			var results = new ActiveApiKeyIndexCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<ActiveApiKeyIndex>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ActiveApiKeyIndexColumns columns = new ActiveApiKeyIndexColumns();
				var orderBy = Bam.Net.Data.Order.By<ActiveApiKeyIndexColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, (c) => c.KeyColumn > 0, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (c) => c.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<ActiveApiKeyIndex>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<ActiveApiKeyIndexColumns> where, Action<IEnumerable<ActiveApiKeyIndex>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ActiveApiKeyIndexColumns columns = new ActiveApiKeyIndexColumns();
				var orderBy = Bam.Net.Data.Order.By<ActiveApiKeyIndexColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (ActiveApiKeyIndexColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<ActiveApiKeyIndex>> batchProcessor, Bam.Net.Data.OrderBy<ActiveApiKeyIndexColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<ActiveApiKeyIndexColumns> where, Action<IEnumerable<ActiveApiKeyIndex>> batchProcessor, Bam.Net.Data.OrderBy<ActiveApiKeyIndexColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ActiveApiKeyIndexColumns columns = new ActiveApiKeyIndexColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (ActiveApiKeyIndexColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static ActiveApiKeyIndex GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static ActiveApiKeyIndex GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static ActiveApiKeyIndex GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static ActiveApiKeyIndex GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static ActiveApiKeyIndex GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static ActiveApiKeyIndex GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static ActiveApiKeyIndexCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static ActiveApiKeyIndexCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<ActiveApiKeyIndexColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ActiveApiKeyIndexColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ActiveApiKeyIndexColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ActiveApiKeyIndexCollection Where(Func<ActiveApiKeyIndexColumns, QueryFilter<ActiveApiKeyIndexColumns>> where, OrderBy<ActiveApiKeyIndexColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<ActiveApiKeyIndex>();
			return new ActiveApiKeyIndexCollection(database.GetQuery<ActiveApiKeyIndexColumns, ActiveApiKeyIndex>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ActiveApiKeyIndexColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ActiveApiKeyIndexColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ActiveApiKeyIndexCollection Where(WhereDelegate<ActiveApiKeyIndexColumns> where, Database database = null)
		{		
			database = database ?? Db.For<ActiveApiKeyIndex>();
			var results = new ActiveApiKeyIndexCollection(database, database.GetQuery<ActiveApiKeyIndexColumns, ActiveApiKeyIndex>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ActiveApiKeyIndexColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ActiveApiKeyIndexColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ActiveApiKeyIndexCollection Where(WhereDelegate<ActiveApiKeyIndexColumns> where, OrderBy<ActiveApiKeyIndexColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<ActiveApiKeyIndex>();
			var results = new ActiveApiKeyIndexCollection(database, database.GetQuery<ActiveApiKeyIndexColumns, ActiveApiKeyIndex>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;ActiveApiKeyIndexColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ActiveApiKeyIndexCollection Where(QiQuery where, Database database = null)
		{
			var results = new ActiveApiKeyIndexCollection(database, Select<ActiveApiKeyIndexColumns>.From<ActiveApiKeyIndex>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static ActiveApiKeyIndex GetOneWhere(QueryFilter where, Database database = null)
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
		[Bam.Net.Exclude]
		public static ActiveApiKeyIndex OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<ActiveApiKeyIndexColumns> whereDelegate = (c) => where;
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
		[Bam.Net.Exclude]
		public static ActiveApiKeyIndex GetOneWhere(WhereDelegate<ActiveApiKeyIndexColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				ActiveApiKeyIndexColumns c = new ActiveApiKeyIndexColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ActiveApiKeyIndex instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ActiveApiKeyIndexColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ActiveApiKeyIndexColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ActiveApiKeyIndex OneWhere(WhereDelegate<ActiveApiKeyIndexColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ActiveApiKeyIndexColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ActiveApiKeyIndex OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ActiveApiKeyIndexColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ActiveApiKeyIndexColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ActiveApiKeyIndex FirstOneWhere(WhereDelegate<ActiveApiKeyIndexColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ActiveApiKeyIndexColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ActiveApiKeyIndexColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ActiveApiKeyIndex FirstOneWhere(WhereDelegate<ActiveApiKeyIndexColumns> where, OrderBy<ActiveApiKeyIndexColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ActiveApiKeyIndexColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ActiveApiKeyIndexColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ActiveApiKeyIndex FirstOneWhere(QueryFilter where, OrderBy<ActiveApiKeyIndexColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<ActiveApiKeyIndexColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a ActiveApiKeyIndexColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ActiveApiKeyIndexColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ActiveApiKeyIndexCollection Top(int count, WhereDelegate<ActiveApiKeyIndexColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ActiveApiKeyIndexColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ActiveApiKeyIndexColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static ActiveApiKeyIndexCollection Top(int count, WhereDelegate<ActiveApiKeyIndexColumns> where, OrderBy<ActiveApiKeyIndexColumns> orderBy, Database database = null)
		{
			ActiveApiKeyIndexColumns c = new ActiveApiKeyIndexColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<ActiveApiKeyIndex>();
			QuerySet query = GetQuerySet(db); 
			query.Top<ActiveApiKeyIndex>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ActiveApiKeyIndexColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ActiveApiKeyIndexCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ActiveApiKeyIndexCollection Top(int count, QueryFilter where, Database database)
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
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static ActiveApiKeyIndexCollection Top(int count, QueryFilter where, OrderBy<ActiveApiKeyIndexColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<ActiveApiKeyIndex>();
			QuerySet query = GetQuerySet(db);
			query.Top<ActiveApiKeyIndex>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<ActiveApiKeyIndexColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ActiveApiKeyIndexCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ActiveApiKeyIndexCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<ActiveApiKeyIndex>();
			QuerySet query = GetQuerySet(db);
			query.Top<ActiveApiKeyIndex>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<ActiveApiKeyIndexCollection>(0);
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
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static ActiveApiKeyIndexCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<ActiveApiKeyIndex>();
			QuerySet query = GetQuerySet(db);
			query.Top<ActiveApiKeyIndex>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<ActiveApiKeyIndexCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of ActiveApiKeyIndexs
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<ActiveApiKeyIndex>();
            QuerySet query = GetQuerySet(db);
            query.Count<ActiveApiKeyIndex>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ActiveApiKeyIndexColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ActiveApiKeyIndexColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<ActiveApiKeyIndexColumns> where, Database database = null)
		{
			ActiveApiKeyIndexColumns c = new ActiveApiKeyIndexColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<ActiveApiKeyIndex>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<ActiveApiKeyIndex>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<ActiveApiKeyIndex>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<ActiveApiKeyIndex>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static ActiveApiKeyIndex CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<ActiveApiKeyIndex>();			
			var dao = new ActiveApiKeyIndex();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static ActiveApiKeyIndex OneOrThrow(ActiveApiKeyIndexCollection c)
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
