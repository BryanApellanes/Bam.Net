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

namespace Bam.Net.Services.DataReplication.Data.Dao
{
	// schema = DataReplication
	// connection Name = DataReplication
	[Serializable]
	[Bam.Net.Data.Table("ReplicationOperation", "DataReplication")]
	public partial class ReplicationOperation: Bam.Net.Data.Dao
	{
		public ReplicationOperation():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ReplicationOperation(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ReplicationOperation(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ReplicationOperation(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator ReplicationOperation(DataRow data)
		{
			return new ReplicationOperation(data);
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

	// property:SourceHost, columnName:SourceHost	
	[Bam.Net.Data.Column(Name="SourceHost", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string SourceHost
	{
		get
		{
			return GetStringValue("SourceHost");
		}
		set
		{
			SetValue("SourceHost", value);
		}
	}

	// property:SourcePort, columnName:SourcePort	
	[Bam.Net.Data.Column(Name="SourcePort", DbDataType="Int", MaxLength="10", AllowNull=true)]
	public int? SourcePort
	{
		get
		{
			return GetIntValue("SourcePort");
		}
		set
		{
			SetValue("SourcePort", value);
		}
	}

	// property:DestinationHost, columnName:DestinationHost	
	[Bam.Net.Data.Column(Name="DestinationHost", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string DestinationHost
	{
		get
		{
			return GetStringValue("DestinationHost");
		}
		set
		{
			SetValue("DestinationHost", value);
		}
	}

	// property:DestinationPort, columnName:DestinationPort	
	[Bam.Net.Data.Column(Name="DestinationPort", DbDataType="Int", MaxLength="10", AllowNull=true)]
	public int? DestinationPort
	{
		get
		{
			return GetIntValue("DestinationPort");
		}
		set
		{
			SetValue("DestinationPort", value);
		}
	}

	// property:BatchSize, columnName:BatchSize	
	[Bam.Net.Data.Column(Name="BatchSize", DbDataType="Int", MaxLength="10", AllowNull=true)]
	public int? BatchSize
	{
		get
		{
			return GetIntValue("BatchSize");
		}
		set
		{
			SetValue("BatchSize", value);
		}
	}

	// property:FromCuid, columnName:FromCuid	
	[Bam.Net.Data.Column(Name="FromCuid", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string FromCuid
	{
		get
		{
			return GetStringValue("FromCuid");
		}
		set
		{
			SetValue("FromCuid", value);
		}
	}

	// property:TypeNamespace, columnName:TypeNamespace	
	[Bam.Net.Data.Column(Name="TypeNamespace", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string TypeNamespace
	{
		get
		{
			return GetStringValue("TypeNamespace");
		}
		set
		{
			SetValue("TypeNamespace", value);
		}
	}

	// property:TypeName, columnName:TypeName	
	[Bam.Net.Data.Column(Name="TypeName", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string TypeName
	{
		get
		{
			return GetStringValue("TypeName");
		}
		set
		{
			SetValue("TypeName", value);
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
				var colFilter = new ReplicationOperationColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the ReplicationOperation table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ReplicationOperationCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<ReplicationOperation>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<ReplicationOperation>();
			var results = new ReplicationOperationCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<ReplicationOperation>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ReplicationOperationColumns columns = new ReplicationOperationColumns();
				var orderBy = Bam.Net.Data.Order.By<ReplicationOperationColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<ReplicationOperation>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<ReplicationOperationColumns> where, Action<IEnumerable<ReplicationOperation>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ReplicationOperationColumns columns = new ReplicationOperationColumns();
				var orderBy = Bam.Net.Data.Order.By<ReplicationOperationColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (ReplicationOperationColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<ReplicationOperation>> batchProcessor, Bam.Net.Data.OrderBy<ReplicationOperationColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<ReplicationOperationColumns> where, Action<IEnumerable<ReplicationOperation>> batchProcessor, Bam.Net.Data.OrderBy<ReplicationOperationColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ReplicationOperationColumns columns = new ReplicationOperationColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (ReplicationOperationColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static ReplicationOperation GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static ReplicationOperation GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static ReplicationOperation GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static ReplicationOperation GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static ReplicationOperationCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static ReplicationOperationCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<ReplicationOperationColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ReplicationOperationColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ReplicationOperationColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ReplicationOperationCollection Where(Func<ReplicationOperationColumns, QueryFilter<ReplicationOperationColumns>> where, OrderBy<ReplicationOperationColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<ReplicationOperation>();
			return new ReplicationOperationCollection(database.GetQuery<ReplicationOperationColumns, ReplicationOperation>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ReplicationOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ReplicationOperationColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ReplicationOperationCollection Where(WhereDelegate<ReplicationOperationColumns> where, Database database = null)
		{		
			database = database ?? Db.For<ReplicationOperation>();
			var results = new ReplicationOperationCollection(database, database.GetQuery<ReplicationOperationColumns, ReplicationOperation>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ReplicationOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ReplicationOperationColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ReplicationOperationCollection Where(WhereDelegate<ReplicationOperationColumns> where, OrderBy<ReplicationOperationColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<ReplicationOperation>();
			var results = new ReplicationOperationCollection(database, database.GetQuery<ReplicationOperationColumns, ReplicationOperation>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;ReplicationOperationColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ReplicationOperationCollection Where(QiQuery where, Database database = null)
		{
			var results = new ReplicationOperationCollection(database, Select<ReplicationOperationColumns>.From<ReplicationOperation>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static ReplicationOperation GetOneWhere(QueryFilter where, Database database = null)
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
		public static ReplicationOperation OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<ReplicationOperationColumns> whereDelegate = (c) => where;
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
		public static ReplicationOperation GetOneWhere(WhereDelegate<ReplicationOperationColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				ReplicationOperationColumns c = new ReplicationOperationColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ReplicationOperation instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ReplicationOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ReplicationOperationColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ReplicationOperation OneWhere(WhereDelegate<ReplicationOperationColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ReplicationOperationColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ReplicationOperation OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ReplicationOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ReplicationOperationColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ReplicationOperation FirstOneWhere(WhereDelegate<ReplicationOperationColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ReplicationOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ReplicationOperationColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ReplicationOperation FirstOneWhere(WhereDelegate<ReplicationOperationColumns> where, OrderBy<ReplicationOperationColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ReplicationOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ReplicationOperationColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ReplicationOperation FirstOneWhere(QueryFilter where, OrderBy<ReplicationOperationColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<ReplicationOperationColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a ReplicationOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ReplicationOperationColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ReplicationOperationCollection Top(int count, WhereDelegate<ReplicationOperationColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ReplicationOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ReplicationOperationColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static ReplicationOperationCollection Top(int count, WhereDelegate<ReplicationOperationColumns> where, OrderBy<ReplicationOperationColumns> orderBy, Database database = null)
		{
			ReplicationOperationColumns c = new ReplicationOperationColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<ReplicationOperation>();
			QuerySet query = GetQuerySet(db); 
			query.Top<ReplicationOperation>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ReplicationOperationColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ReplicationOperationCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ReplicationOperationCollection Top(int count, QueryFilter where, Database database)
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
		public static ReplicationOperationCollection Top(int count, QueryFilter where, OrderBy<ReplicationOperationColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<ReplicationOperation>();
			QuerySet query = GetQuerySet(db);
			query.Top<ReplicationOperation>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<ReplicationOperationColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ReplicationOperationCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ReplicationOperationCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<ReplicationOperation>();
			QuerySet query = GetQuerySet(db);
			query.Top<ReplicationOperation>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<ReplicationOperationCollection>(0);
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
		public static ReplicationOperationCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<ReplicationOperation>();
			QuerySet query = GetQuerySet(db);
			query.Top<ReplicationOperation>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<ReplicationOperationCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of ReplicationOperations
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<ReplicationOperation>();
            QuerySet query = GetQuerySet(db);
            query.Count<ReplicationOperation>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ReplicationOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ReplicationOperationColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<ReplicationOperationColumns> where, Database database = null)
		{
			ReplicationOperationColumns c = new ReplicationOperationColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<ReplicationOperation>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<ReplicationOperation>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<ReplicationOperation>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<ReplicationOperation>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static ReplicationOperation CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<ReplicationOperation>();			
			var dao = new ReplicationOperation();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static ReplicationOperation OneOrThrow(ReplicationOperationCollection c)
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
