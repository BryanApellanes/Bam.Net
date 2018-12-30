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

namespace Bam.Net.CoreServices.WebHooks.Data.Dao
{
	// schema = WebHooks
	// connection Name = WebHooks
	[Serializable]
	[Bam.Net.Data.Table("WebHookDescriptor", "WebHooks")]
	public partial class WebHookDescriptor: Bam.Net.Data.Dao
	{
		public WebHookDescriptor():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public WebHookDescriptor(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public WebHookDescriptor(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public WebHookDescriptor(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator WebHookDescriptor(DataRow data)
		{
			return new WebHookDescriptor(data);
		}

		private void SetChildren()
		{

			if(_database != null)
			{
				this.ChildCollections.Add("WebHookCall_WebHookDescriptorId", new WebHookCallCollection(Database.GetQuery<WebHookCallColumns, WebHookCall>((c) => c.WebHookDescriptorId == GetULongValue("Id")), this, "WebHookDescriptorId"));				
			}
			if(_database != null)
			{
				this.ChildCollections.Add("WebHookDescriptorWebHookSubscriber_WebHookDescriptorId", new WebHookDescriptorWebHookSubscriberCollection(Database.GetQuery<WebHookDescriptorWebHookSubscriberColumns, WebHookDescriptorWebHookSubscriber>((c) => c.WebHookDescriptorId == GetULongValue("Id")), this, "WebHookDescriptorId"));				
			}			
            this.ChildCollections.Add("WebHookDescriptor_WebHookDescriptorWebHookSubscriber_WebHookSubscriber",  new XrefDaoCollection<WebHookDescriptorWebHookSubscriber, WebHookSubscriber>(this, false));
							
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

	// property:WebHookName, columnName:WebHookName	
	[Bam.Net.Data.Column(Name="WebHookName", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string WebHookName
	{
		get
		{
			return GetStringValue("WebHookName");
		}
		set
		{
			SetValue("WebHookName", value);
		}
	}

	// property:Description, columnName:Description	
	[Bam.Net.Data.Column(Name="Description", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Description
	{
		get
		{
			return GetStringValue("Description");
		}
		set
		{
			SetValue("Description", value);
		}
	}

	// property:SharedSecret, columnName:SharedSecret	
	[Bam.Net.Data.Column(Name="SharedSecret", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string SharedSecret
	{
		get
		{
			return GetStringValue("SharedSecret");
		}
		set
		{
			SetValue("SharedSecret", value);
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



				

	[Bam.Net.Exclude]	
	public WebHookCallCollection WebHookCallsByWebHookDescriptorId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("WebHookCall_WebHookDescriptorId"))
			{
				SetChildren();
			}

			var c = (WebHookCallCollection)this.ChildCollections["WebHookCall_WebHookDescriptorId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Bam.Net.Exclude]	
	public WebHookDescriptorWebHookSubscriberCollection WebHookDescriptorWebHookSubscribersByWebHookDescriptorId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("WebHookDescriptorWebHookSubscriber_WebHookDescriptorId"))
			{
				SetChildren();
			}

			var c = (WebHookDescriptorWebHookSubscriberCollection)this.ChildCollections["WebHookDescriptorWebHookSubscriber_WebHookDescriptorId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			

		// Xref       
        public XrefDaoCollection<WebHookDescriptorWebHookSubscriber, WebHookSubscriber> WebHookSubscribers
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("WebHookDescriptor_WebHookDescriptorWebHookSubscriber_WebHookSubscriber"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<WebHookDescriptorWebHookSubscriber, WebHookSubscriber>)this.ChildCollections["WebHookDescriptor_WebHookDescriptorWebHookSubscriber_WebHookSubscriber"];
				if(!xref.Loaded)
				{
					xref.Load(Database);
				}

				return xref;
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
				var colFilter = new WebHookDescriptorColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the WebHookDescriptor table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static WebHookDescriptorCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<WebHookDescriptor>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<WebHookDescriptor>();
			var results = new WebHookDescriptorCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<WebHookDescriptor>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				WebHookDescriptorColumns columns = new WebHookDescriptorColumns();
				var orderBy = Bam.Net.Data.Order.By<WebHookDescriptorColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<WebHookDescriptor>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<WebHookDescriptorColumns> where, Action<IEnumerable<WebHookDescriptor>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				WebHookDescriptorColumns columns = new WebHookDescriptorColumns();
				var orderBy = Bam.Net.Data.Order.By<WebHookDescriptorColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (WebHookDescriptorColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<WebHookDescriptor>> batchProcessor, Bam.Net.Data.OrderBy<WebHookDescriptorColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<WebHookDescriptorColumns> where, Action<IEnumerable<WebHookDescriptor>> batchProcessor, Bam.Net.Data.OrderBy<WebHookDescriptorColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				WebHookDescriptorColumns columns = new WebHookDescriptorColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (WebHookDescriptorColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static WebHookDescriptor GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static WebHookDescriptor GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static WebHookDescriptor GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static WebHookDescriptor GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static WebHookDescriptor GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static WebHookDescriptor GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static WebHookDescriptorCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static WebHookDescriptorCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<WebHookDescriptorColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a WebHookDescriptorColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between WebHookDescriptorColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static WebHookDescriptorCollection Where(Func<WebHookDescriptorColumns, QueryFilter<WebHookDescriptorColumns>> where, OrderBy<WebHookDescriptorColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<WebHookDescriptor>();
			return new WebHookDescriptorCollection(database.GetQuery<WebHookDescriptorColumns, WebHookDescriptor>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a WebHookDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WebHookDescriptorColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static WebHookDescriptorCollection Where(WhereDelegate<WebHookDescriptorColumns> where, Database database = null)
		{		
			database = database ?? Db.For<WebHookDescriptor>();
			var results = new WebHookDescriptorCollection(database, database.GetQuery<WebHookDescriptorColumns, WebHookDescriptor>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a WebHookDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WebHookDescriptorColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static WebHookDescriptorCollection Where(WhereDelegate<WebHookDescriptorColumns> where, OrderBy<WebHookDescriptorColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<WebHookDescriptor>();
			var results = new WebHookDescriptorCollection(database, database.GetQuery<WebHookDescriptorColumns, WebHookDescriptor>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;WebHookDescriptorColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static WebHookDescriptorCollection Where(QiQuery where, Database database = null)
		{
			var results = new WebHookDescriptorCollection(database, Select<WebHookDescriptorColumns>.From<WebHookDescriptor>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static WebHookDescriptor GetOneWhere(QueryFilter where, Database database = null)
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
		public static WebHookDescriptor OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<WebHookDescriptorColumns> whereDelegate = (c) => where;
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
		public static WebHookDescriptor GetOneWhere(WhereDelegate<WebHookDescriptorColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				WebHookDescriptorColumns c = new WebHookDescriptorColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single WebHookDescriptor instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a WebHookDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WebHookDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static WebHookDescriptor OneWhere(WhereDelegate<WebHookDescriptorColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<WebHookDescriptorColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static WebHookDescriptor OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a WebHookDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WebHookDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static WebHookDescriptor FirstOneWhere(WhereDelegate<WebHookDescriptorColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a WebHookDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WebHookDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static WebHookDescriptor FirstOneWhere(WhereDelegate<WebHookDescriptorColumns> where, OrderBy<WebHookDescriptorColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a WebHookDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WebHookDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static WebHookDescriptor FirstOneWhere(QueryFilter where, OrderBy<WebHookDescriptorColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<WebHookDescriptorColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a WebHookDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WebHookDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static WebHookDescriptorCollection Top(int count, WhereDelegate<WebHookDescriptorColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a WebHookDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WebHookDescriptorColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static WebHookDescriptorCollection Top(int count, WhereDelegate<WebHookDescriptorColumns> where, OrderBy<WebHookDescriptorColumns> orderBy, Database database = null)
		{
			WebHookDescriptorColumns c = new WebHookDescriptorColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<WebHookDescriptor>();
			QuerySet query = GetQuerySet(db); 
			query.Top<WebHookDescriptor>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<WebHookDescriptorColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<WebHookDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static WebHookDescriptorCollection Top(int count, QueryFilter where, Database database)
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
		public static WebHookDescriptorCollection Top(int count, QueryFilter where, OrderBy<WebHookDescriptorColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<WebHookDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<WebHookDescriptor>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<WebHookDescriptorColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<WebHookDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static WebHookDescriptorCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<WebHookDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<WebHookDescriptor>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<WebHookDescriptorCollection>(0);
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
		public static WebHookDescriptorCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<WebHookDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<WebHookDescriptor>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<WebHookDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of WebHookDescriptors
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<WebHookDescriptor>();
            QuerySet query = GetQuerySet(db);
            query.Count<WebHookDescriptor>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a WebHookDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WebHookDescriptorColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<WebHookDescriptorColumns> where, Database database = null)
		{
			WebHookDescriptorColumns c = new WebHookDescriptorColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<WebHookDescriptor>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<WebHookDescriptor>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<WebHookDescriptor>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<WebHookDescriptor>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static WebHookDescriptor CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<WebHookDescriptor>();			
			var dao = new WebHookDescriptor();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static WebHookDescriptor OneOrThrow(WebHookDescriptorCollection c)
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
