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

namespace Bam.Net.ServiceProxy.Secure
{
	// schema = SecureServiceProxy
	// connection Name = SecureServiceProxy
	[Serializable]
	[Bam.Net.Data.Table("SecureSession", "SecureServiceProxy")]
	public partial class SecureSession: Bam.Net.Data.Dao
	{
		public SecureSession():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public SecureSession(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public SecureSession(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public SecureSession(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator SecureSession(DataRow data)
		{
			return new SecureSession(data);
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

	// property:Identifier, columnName:Identifier	
	[Bam.Net.Data.Column(Name="Identifier", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Identifier
	{
		get
		{
			return GetStringValue("Identifier");
		}
		set
		{
			SetValue("Identifier", value);
		}
	}

	// property:AsymmetricKey, columnName:AsymmetricKey	
	[Bam.Net.Data.Column(Name="AsymmetricKey", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string AsymmetricKey
	{
		get
		{
			return GetStringValue("AsymmetricKey");
		}
		set
		{
			SetValue("AsymmetricKey", value);
		}
	}

	// property:SymmetricKey, columnName:SymmetricKey	
	[Bam.Net.Data.Column(Name="SymmetricKey", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string SymmetricKey
	{
		get
		{
			return GetStringValue("SymmetricKey");
		}
		set
		{
			SetValue("SymmetricKey", value);
		}
	}

	// property:SymmetricIV, columnName:SymmetricIV	
	[Bam.Net.Data.Column(Name="SymmetricIV", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string SymmetricIV
	{
		get
		{
			return GetStringValue("SymmetricIV");
		}
		set
		{
			SetValue("SymmetricIV", value);
		}
	}

	// property:CreationDate, columnName:CreationDate	
	[Bam.Net.Data.Column(Name="CreationDate", DbDataType="DateTime", MaxLength="8", AllowNull=false)]
	public DateTime? CreationDate
	{
		get
		{
			return GetDateTimeValue("CreationDate");
		}
		set
		{
			SetValue("CreationDate", value);
		}
	}

	// property:TimeOffset, columnName:TimeOffset	
	[Bam.Net.Data.Column(Name="TimeOffset", DbDataType="Int", MaxLength="10", AllowNull=false)]
	public int? TimeOffset
	{
		get
		{
			return GetIntValue("TimeOffset");
		}
		set
		{
			SetValue("TimeOffset", value);
		}
	}

	// property:LastActivity, columnName:LastActivity	
	[Bam.Net.Data.Column(Name="LastActivity", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? LastActivity
	{
		get
		{
			return GetDateTimeValue("LastActivity");
		}
		set
		{
			SetValue("LastActivity", value);
		}
	}

	// property:IsActive, columnName:IsActive	
	[Bam.Net.Data.Column(Name="IsActive", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? IsActive
	{
		get
		{
			return GetBooleanValue("IsActive");
		}
		set
		{
			SetValue("IsActive", value);
		}
	}



	// start ApplicationId -> ApplicationId
	[Bam.Net.Data.ForeignKey(
        Table="SecureSession",
		Name="ApplicationId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Application",
		Suffix="1")]
	public ulong? ApplicationId
	{
		get
		{
			return GetULongValue("ApplicationId");
		}
		set
		{
			SetValue("ApplicationId", value);
		}
	}

	Application _applicationOfApplicationId;
	public Application ApplicationOfApplicationId
	{
		get
		{
			if(_applicationOfApplicationId == null)
			{
				_applicationOfApplicationId = Bam.Net.ServiceProxy.Secure.Application.OneWhere(c => c.KeyColumn == this.ApplicationId, this.Database);
			}
			return _applicationOfApplicationId;
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
				var colFilter = new SecureSessionColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the SecureSession table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static SecureSessionCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<SecureSession>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<SecureSession>();
			var results = new SecureSessionCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<SecureSession>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				SecureSessionColumns columns = new SecureSessionColumns();
				var orderBy = Bam.Net.Data.Order.By<SecureSessionColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<SecureSession>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<SecureSessionColumns> where, Action<IEnumerable<SecureSession>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				SecureSessionColumns columns = new SecureSessionColumns();
				var orderBy = Bam.Net.Data.Order.By<SecureSessionColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (SecureSessionColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<SecureSession>> batchProcessor, Bam.Net.Data.OrderBy<SecureSessionColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<SecureSessionColumns> where, Action<IEnumerable<SecureSession>> batchProcessor, Bam.Net.Data.OrderBy<SecureSessionColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				SecureSessionColumns columns = new SecureSessionColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (SecureSessionColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static SecureSession GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static SecureSession GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static SecureSession GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static SecureSession GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static SecureSession GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static SecureSession GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static SecureSessionCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static SecureSessionCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<SecureSessionColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a SecureSessionColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between SecureSessionColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static SecureSessionCollection Where(Func<SecureSessionColumns, QueryFilter<SecureSessionColumns>> where, OrderBy<SecureSessionColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<SecureSession>();
			return new SecureSessionCollection(database.GetQuery<SecureSessionColumns, SecureSession>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SecureSessionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SecureSessionColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static SecureSessionCollection Where(WhereDelegate<SecureSessionColumns> where, Database database = null)
		{		
			database = database ?? Db.For<SecureSession>();
			var results = new SecureSessionCollection(database, database.GetQuery<SecureSessionColumns, SecureSession>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SecureSessionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SecureSessionColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static SecureSessionCollection Where(WhereDelegate<SecureSessionColumns> where, OrderBy<SecureSessionColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<SecureSession>();
			var results = new SecureSessionCollection(database, database.GetQuery<SecureSessionColumns, SecureSession>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;SecureSessionColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static SecureSessionCollection Where(QiQuery where, Database database = null)
		{
			var results = new SecureSessionCollection(database, Select<SecureSessionColumns>.From<SecureSession>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static SecureSession GetOneWhere(QueryFilter where, Database database = null)
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
		public static SecureSession OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<SecureSessionColumns> whereDelegate = (c) => where;
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
		public static SecureSession GetOneWhere(WhereDelegate<SecureSessionColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				SecureSessionColumns c = new SecureSessionColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single SecureSession instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SecureSessionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SecureSessionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static SecureSession OneWhere(WhereDelegate<SecureSessionColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<SecureSessionColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static SecureSession OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SecureSessionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SecureSessionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static SecureSession FirstOneWhere(WhereDelegate<SecureSessionColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a SecureSessionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SecureSessionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static SecureSession FirstOneWhere(WhereDelegate<SecureSessionColumns> where, OrderBy<SecureSessionColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a SecureSessionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SecureSessionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static SecureSession FirstOneWhere(QueryFilter where, OrderBy<SecureSessionColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<SecureSessionColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a SecureSessionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SecureSessionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static SecureSessionCollection Top(int count, WhereDelegate<SecureSessionColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a SecureSessionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SecureSessionColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static SecureSessionCollection Top(int count, WhereDelegate<SecureSessionColumns> where, OrderBy<SecureSessionColumns> orderBy, Database database = null)
		{
			SecureSessionColumns c = new SecureSessionColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<SecureSession>();
			QuerySet query = GetQuerySet(db); 
			query.Top<SecureSession>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<SecureSessionColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<SecureSessionCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static SecureSessionCollection Top(int count, QueryFilter where, Database database)
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
		public static SecureSessionCollection Top(int count, QueryFilter where, OrderBy<SecureSessionColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<SecureSession>();
			QuerySet query = GetQuerySet(db);
			query.Top<SecureSession>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<SecureSessionColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<SecureSessionCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static SecureSessionCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<SecureSession>();
			QuerySet query = GetQuerySet(db);
			query.Top<SecureSession>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<SecureSessionCollection>(0);
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
		public static SecureSessionCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<SecureSession>();
			QuerySet query = GetQuerySet(db);
			query.Top<SecureSession>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<SecureSessionCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of SecureSessions
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<SecureSession>();
            QuerySet query = GetQuerySet(db);
            query.Count<SecureSession>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SecureSessionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SecureSessionColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<SecureSessionColumns> where, Database database = null)
		{
			SecureSessionColumns c = new SecureSessionColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<SecureSession>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<SecureSession>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<SecureSession>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<SecureSession>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static SecureSession CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<SecureSession>();			
			var dao = new SecureSession();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static SecureSession OneOrThrow(SecureSessionCollection c)
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
