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

namespace Bam.Net.CoreServices.Data.Daos
{
	// schema = CoreRegistry
	// connection Name = CoreRegistry
	[Serializable]
	[Bam.Net.Data.Table("ExternalEventSubscription", "CoreRegistry")]
	public partial class ExternalEventSubscription: Dao
	{
		public ExternalEventSubscription():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ExternalEventSubscription(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ExternalEventSubscription(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ExternalEventSubscription(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator ExternalEventSubscription(DataRow data)
		{
			return new ExternalEventSubscription(data);
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

	// property:ClientName, columnName:ClientName	
	[Bam.Net.Data.Column(Name="ClientName", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string ClientName
	{
		get
		{
			return GetStringValue("ClientName");
		}
		set
		{
			SetValue("ClientName", value);
		}
	}

	// property:EventName, columnName:EventName	
	[Bam.Net.Data.Column(Name="EventName", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string EventName
	{
		get
		{
			return GetStringValue("EventName");
		}
		set
		{
			SetValue("EventName", value);
		}
	}

	// property:WebHookEndpoint, columnName:WebHookEndpoint	
	[Bam.Net.Data.Column(Name="WebHookEndpoint", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string WebHookEndpoint
	{
		get
		{
			return GetStringValue("WebHookEndpoint");
		}
		set
		{
			SetValue("WebHookEndpoint", value);
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
				return UniqueFilterProvider();
			}
			else
			{
				var colFilter = new ExternalEventSubscriptionColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the ExternalEventSubscription table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ExternalEventSubscriptionCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<ExternalEventSubscription>();
			Database db = database ?? Db.For<ExternalEventSubscription>();
			var results = new ExternalEventSubscriptionCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<ExternalEventSubscription>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				ExternalEventSubscriptionColumns columns = new ExternalEventSubscriptionColumns();
				var orderBy = Bam.Net.Data.Order.By<ExternalEventSubscriptionColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<ExternalEventSubscription>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<ExternalEventSubscriptionColumns> where, Action<IEnumerable<ExternalEventSubscription>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				ExternalEventSubscriptionColumns columns = new ExternalEventSubscriptionColumns();
				var orderBy = Bam.Net.Data.Order.By<ExternalEventSubscriptionColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (ExternalEventSubscriptionColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static ExternalEventSubscription GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static ExternalEventSubscription GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static ExternalEventSubscription GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static ExternalEventSubscription GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static ExternalEventSubscriptionCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static ExternalEventSubscriptionCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<ExternalEventSubscriptionColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ExternalEventSubscriptionColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ExternalEventSubscriptionColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ExternalEventSubscriptionCollection Where(Func<ExternalEventSubscriptionColumns, QueryFilter<ExternalEventSubscriptionColumns>> where, OrderBy<ExternalEventSubscriptionColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<ExternalEventSubscription>();
			return new ExternalEventSubscriptionCollection(database.GetQuery<ExternalEventSubscriptionColumns, ExternalEventSubscription>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ExternalEventSubscriptionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ExternalEventSubscriptionColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ExternalEventSubscriptionCollection Where(WhereDelegate<ExternalEventSubscriptionColumns> where, Database database = null)
		{		
			database = database ?? Db.For<ExternalEventSubscription>();
			var results = new ExternalEventSubscriptionCollection(database, database.GetQuery<ExternalEventSubscriptionColumns, ExternalEventSubscription>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ExternalEventSubscriptionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ExternalEventSubscriptionColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ExternalEventSubscriptionCollection Where(WhereDelegate<ExternalEventSubscriptionColumns> where, OrderBy<ExternalEventSubscriptionColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<ExternalEventSubscription>();
			var results = new ExternalEventSubscriptionCollection(database, database.GetQuery<ExternalEventSubscriptionColumns, ExternalEventSubscription>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;ExternalEventSubscriptionColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ExternalEventSubscriptionCollection Where(QiQuery where, Database database = null)
		{
			var results = new ExternalEventSubscriptionCollection(database, Select<ExternalEventSubscriptionColumns>.From<ExternalEventSubscription>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static ExternalEventSubscription GetOneWhere(QueryFilter where, Database database = null)
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
		public static ExternalEventSubscription OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<ExternalEventSubscriptionColumns> whereDelegate = (c) => where;
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
		public static ExternalEventSubscription GetOneWhere(WhereDelegate<ExternalEventSubscriptionColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				ExternalEventSubscriptionColumns c = new ExternalEventSubscriptionColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ExternalEventSubscription instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ExternalEventSubscriptionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ExternalEventSubscriptionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ExternalEventSubscription OneWhere(WhereDelegate<ExternalEventSubscriptionColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ExternalEventSubscriptionColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ExternalEventSubscription OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ExternalEventSubscriptionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ExternalEventSubscriptionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ExternalEventSubscription FirstOneWhere(WhereDelegate<ExternalEventSubscriptionColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ExternalEventSubscriptionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ExternalEventSubscriptionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ExternalEventSubscription FirstOneWhere(WhereDelegate<ExternalEventSubscriptionColumns> where, OrderBy<ExternalEventSubscriptionColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ExternalEventSubscriptionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ExternalEventSubscriptionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ExternalEventSubscription FirstOneWhere(QueryFilter where, OrderBy<ExternalEventSubscriptionColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<ExternalEventSubscriptionColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a ExternalEventSubscriptionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ExternalEventSubscriptionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ExternalEventSubscriptionCollection Top(int count, WhereDelegate<ExternalEventSubscriptionColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ExternalEventSubscriptionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ExternalEventSubscriptionColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ExternalEventSubscriptionCollection Top(int count, WhereDelegate<ExternalEventSubscriptionColumns> where, OrderBy<ExternalEventSubscriptionColumns> orderBy, Database database = null)
		{
			ExternalEventSubscriptionColumns c = new ExternalEventSubscriptionColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<ExternalEventSubscription>();
			QuerySet query = GetQuerySet(db); 
			query.Top<ExternalEventSubscription>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ExternalEventSubscriptionColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ExternalEventSubscriptionCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ExternalEventSubscriptionCollection Top(int count, QueryFilter where, Database database)
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
		[Bam.Net.Exclude]
		public static ExternalEventSubscriptionCollection Top(int count, QueryFilter where, OrderBy<ExternalEventSubscriptionColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<ExternalEventSubscription>();
			QuerySet query = GetQuerySet(db);
			query.Top<ExternalEventSubscription>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<ExternalEventSubscriptionColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ExternalEventSubscriptionCollection>(0);
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
		public static ExternalEventSubscriptionCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<ExternalEventSubscription>();
			QuerySet query = GetQuerySet(db);
			query.Top<ExternalEventSubscription>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<ExternalEventSubscriptionCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of ExternalEventSubscriptions
		/// </summary>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<ExternalEventSubscription>();
            QuerySet query = GetQuerySet(db);
            query.Count<ExternalEventSubscription>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ExternalEventSubscriptionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ExternalEventSubscriptionColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<ExternalEventSubscriptionColumns> where, Database database = null)
		{
			ExternalEventSubscriptionColumns c = new ExternalEventSubscriptionColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<ExternalEventSubscription>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<ExternalEventSubscription>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<ExternalEventSubscription>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<ExternalEventSubscription>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static ExternalEventSubscription CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<ExternalEventSubscription>();			
			var dao = new ExternalEventSubscription();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static ExternalEventSubscription OneOrThrow(ExternalEventSubscriptionCollection c)
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
