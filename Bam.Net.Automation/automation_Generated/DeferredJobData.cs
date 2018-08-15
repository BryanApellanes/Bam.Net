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

namespace Bam.Net.Automation.Data
{
	// schema = Automation
	// connection Name = Automation
	[Serializable]
	[Bam.Net.Data.Table("DeferredJobData", "Automation")]
	public partial class DeferredJobData: Bam.Net.Data.Dao
	{
		public DeferredJobData():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DeferredJobData(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DeferredJobData(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DeferredJobData(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator DeferredJobData(DataRow data)
		{
			return new DeferredJobData(data);
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

	// property:JobDirectory, columnName:JobDirectory	
	[Bam.Net.Data.Column(Name="JobDirectory", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string JobDirectory
	{
		get
		{
			return GetStringValue("JobDirectory");
		}
		set
		{
			SetValue("JobDirectory", value);
		}
	}

	// property:HostName, columnName:HostName	
	[Bam.Net.Data.Column(Name="HostName", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string HostName
	{
		get
		{
			return GetStringValue("HostName");
		}
		set
		{
			SetValue("HostName", value);
		}
	}

	// property:LastStepNumber, columnName:LastStepNumber	
	[Bam.Net.Data.Column(Name="LastStepNumber", DbDataType="BigInt", MaxLength="19", AllowNull=false)]
	public long? LastStepNumber
	{
		get
		{
			return GetLongValue("LastStepNumber");
		}
		set
		{
			SetValue("LastStepNumber", value);
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
				var colFilter = new DeferredJobDataColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the DeferredJobData table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static DeferredJobDataCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<DeferredJobData>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<DeferredJobData>();
			var results = new DeferredJobDataCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<DeferredJobData>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				DeferredJobDataColumns columns = new DeferredJobDataColumns();
				var orderBy = Bam.Net.Data.Order.By<DeferredJobDataColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<DeferredJobData>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<DeferredJobDataColumns> where, Action<IEnumerable<DeferredJobData>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				DeferredJobDataColumns columns = new DeferredJobDataColumns();
				var orderBy = Bam.Net.Data.Order.By<DeferredJobDataColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (DeferredJobDataColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<DeferredJobData>> batchProcessor, Bam.Net.Data.OrderBy<DeferredJobDataColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<DeferredJobDataColumns> where, Action<IEnumerable<DeferredJobData>> batchProcessor, Bam.Net.Data.OrderBy<DeferredJobDataColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				DeferredJobDataColumns columns = new DeferredJobDataColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (DeferredJobDataColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static DeferredJobData GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static DeferredJobData GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static DeferredJobData GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static DeferredJobData GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static DeferredJobData GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static DeferredJobData GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static DeferredJobDataCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static DeferredJobDataCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<DeferredJobDataColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a DeferredJobDataColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between DeferredJobDataColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static DeferredJobDataCollection Where(Func<DeferredJobDataColumns, QueryFilter<DeferredJobDataColumns>> where, OrderBy<DeferredJobDataColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<DeferredJobData>();
			return new DeferredJobDataCollection(database.GetQuery<DeferredJobDataColumns, DeferredJobData>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DeferredJobDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeferredJobDataColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static DeferredJobDataCollection Where(WhereDelegate<DeferredJobDataColumns> where, Database database = null)
		{		
			database = database ?? Db.For<DeferredJobData>();
			var results = new DeferredJobDataCollection(database, database.GetQuery<DeferredJobDataColumns, DeferredJobData>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DeferredJobDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeferredJobDataColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DeferredJobDataCollection Where(WhereDelegate<DeferredJobDataColumns> where, OrderBy<DeferredJobDataColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<DeferredJobData>();
			var results = new DeferredJobDataCollection(database, database.GetQuery<DeferredJobDataColumns, DeferredJobData>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;DeferredJobDataColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static DeferredJobDataCollection Where(QiQuery where, Database database = null)
		{
			var results = new DeferredJobDataCollection(database, Select<DeferredJobDataColumns>.From<DeferredJobData>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static DeferredJobData GetOneWhere(QueryFilter where, Database database = null)
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
		public static DeferredJobData OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<DeferredJobDataColumns> whereDelegate = (c) => where;
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
		public static DeferredJobData GetOneWhere(WhereDelegate<DeferredJobDataColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				DeferredJobDataColumns c = new DeferredJobDataColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single DeferredJobData instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DeferredJobDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeferredJobDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DeferredJobData OneWhere(WhereDelegate<DeferredJobDataColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<DeferredJobDataColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static DeferredJobData OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DeferredJobDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeferredJobDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DeferredJobData FirstOneWhere(WhereDelegate<DeferredJobDataColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DeferredJobDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeferredJobDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DeferredJobData FirstOneWhere(WhereDelegate<DeferredJobDataColumns> where, OrderBy<DeferredJobDataColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DeferredJobDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeferredJobDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DeferredJobData FirstOneWhere(QueryFilter where, OrderBy<DeferredJobDataColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<DeferredJobDataColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a DeferredJobDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeferredJobDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DeferredJobDataCollection Top(int count, WhereDelegate<DeferredJobDataColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DeferredJobDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeferredJobDataColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static DeferredJobDataCollection Top(int count, WhereDelegate<DeferredJobDataColumns> where, OrderBy<DeferredJobDataColumns> orderBy, Database database = null)
		{
			DeferredJobDataColumns c = new DeferredJobDataColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<DeferredJobData>();
			QuerySet query = GetQuerySet(db); 
			query.Top<DeferredJobData>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<DeferredJobDataColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<DeferredJobDataCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static DeferredJobDataCollection Top(int count, QueryFilter where, Database database)
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
		public static DeferredJobDataCollection Top(int count, QueryFilter where, OrderBy<DeferredJobDataColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<DeferredJobData>();
			QuerySet query = GetQuerySet(db);
			query.Top<DeferredJobData>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<DeferredJobDataColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<DeferredJobDataCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static DeferredJobDataCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<DeferredJobData>();
			QuerySet query = GetQuerySet(db);
			query.Top<DeferredJobData>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<DeferredJobDataCollection>(0);
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
		public static DeferredJobDataCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<DeferredJobData>();
			QuerySet query = GetQuerySet(db);
			query.Top<DeferredJobData>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<DeferredJobDataCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of DeferredJobDatas
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<DeferredJobData>();
            QuerySet query = GetQuerySet(db);
            query.Count<DeferredJobData>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DeferredJobDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeferredJobDataColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<DeferredJobDataColumns> where, Database database = null)
		{
			DeferredJobDataColumns c = new DeferredJobDataColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<DeferredJobData>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<DeferredJobData>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<DeferredJobData>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<DeferredJobData>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static DeferredJobData CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<DeferredJobData>();			
			var dao = new DeferredJobData();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static DeferredJobData OneOrThrow(DeferredJobDataCollection c)
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
