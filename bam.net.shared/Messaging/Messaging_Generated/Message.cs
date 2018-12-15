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

namespace Bam.Net.Messaging.Data
{
	// schema = Messaging
	// connection Name = Messaging
	[Serializable]
	[Bam.Net.Data.Table("Message", "Messaging")]
	public partial class Message: Bam.Net.Data.Dao
	{
		public Message():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Message(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Message(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Message(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator Message(DataRow data)
		{
			return new Message(data);
		}

		private void SetChildren()
		{

			if(_database != null)
			{
				this.ChildCollections.Add("DirectMessage_MessageId", new DirectMessageCollection(Database.GetQuery<DirectMessageColumns, DirectMessage>((c) => c.MessageId == GetULongValue("Id")), this, "MessageId"));				
			}						
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

	// property:CreatedDate, columnName:CreatedDate	
	[Bam.Net.Data.Column(Name="CreatedDate", DbDataType="DateTime", MaxLength="8", AllowNull=false)]
	public DateTime? CreatedDate
	{
		get
		{
			return GetDateTimeValue("CreatedDate");
		}
		set
		{
			SetValue("CreatedDate", value);
		}
	}

	// property:From, columnName:From	
	[Bam.Net.Data.Column(Name="From", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string From
	{
		get
		{
			return GetStringValue("From");
		}
		set
		{
			SetValue("From", value);
		}
	}

	// property:FromEmail, columnName:FromEmail	
	[Bam.Net.Data.Column(Name="FromEmail", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string FromEmail
	{
		get
		{
			return GetStringValue("FromEmail");
		}
		set
		{
			SetValue("FromEmail", value);
		}
	}

	// property:Subject, columnName:Subject	
	[Bam.Net.Data.Column(Name="Subject", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Subject
	{
		get
		{
			return GetStringValue("Subject");
		}
		set
		{
			SetValue("Subject", value);
		}
	}

	// property:Body, columnName:Body	
	[Bam.Net.Data.Column(Name="Body", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Body
	{
		get
		{
			return GetStringValue("Body");
		}
		set
		{
			SetValue("Body", value);
		}
	}



				

	[Bam.Net.Exclude]	
	public DirectMessageCollection DirectMessagesByMessageId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("DirectMessage_MessageId"))
			{
				SetChildren();
			}

			var c = (DirectMessageCollection)this.ChildCollections["DirectMessage_MessageId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
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
				var colFilter = new MessageColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the Message table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static MessageCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<Message>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<Message>();
			var results = new MessageCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<Message>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				MessageColumns columns = new MessageColumns();
				var orderBy = Bam.Net.Data.Order.By<MessageColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<Message>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<MessageColumns> where, Action<IEnumerable<Message>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				MessageColumns columns = new MessageColumns();
				var orderBy = Bam.Net.Data.Order.By<MessageColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (MessageColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<Message>> batchProcessor, Bam.Net.Data.OrderBy<MessageColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<MessageColumns> where, Action<IEnumerable<Message>> batchProcessor, Bam.Net.Data.OrderBy<MessageColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				MessageColumns columns = new MessageColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (MessageColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static Message GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static Message GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Message GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Message GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Message GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static Message GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static MessageCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static MessageCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<MessageColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a MessageColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between MessageColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static MessageCollection Where(Func<MessageColumns, QueryFilter<MessageColumns>> where, OrderBy<MessageColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<Message>();
			return new MessageCollection(database.GetQuery<MessageColumns, Message>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a MessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between MessageColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static MessageCollection Where(WhereDelegate<MessageColumns> where, Database database = null)
		{		
			database = database ?? Db.For<Message>();
			var results = new MessageCollection(database, database.GetQuery<MessageColumns, Message>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a MessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between MessageColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static MessageCollection Where(WhereDelegate<MessageColumns> where, OrderBy<MessageColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<Message>();
			var results = new MessageCollection(database, database.GetQuery<MessageColumns, Message>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;MessageColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static MessageCollection Where(QiQuery where, Database database = null)
		{
			var results = new MessageCollection(database, Select<MessageColumns>.From<Message>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static Message GetOneWhere(QueryFilter where, Database database = null)
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
		public static Message OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<MessageColumns> whereDelegate = (c) => where;
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
		public static Message GetOneWhere(WhereDelegate<MessageColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				MessageColumns c = new MessageColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Message instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a MessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between MessageColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Message OneWhere(WhereDelegate<MessageColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<MessageColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Message OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a MessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between MessageColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Message FirstOneWhere(WhereDelegate<MessageColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a MessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between MessageColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Message FirstOneWhere(WhereDelegate<MessageColumns> where, OrderBy<MessageColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a MessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between MessageColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Message FirstOneWhere(QueryFilter where, OrderBy<MessageColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<MessageColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a MessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between MessageColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static MessageCollection Top(int count, WhereDelegate<MessageColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a MessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between MessageColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static MessageCollection Top(int count, WhereDelegate<MessageColumns> where, OrderBy<MessageColumns> orderBy, Database database = null)
		{
			MessageColumns c = new MessageColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<Message>();
			QuerySet query = GetQuerySet(db); 
			query.Top<Message>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<MessageColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<MessageCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static MessageCollection Top(int count, QueryFilter where, Database database)
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
		public static MessageCollection Top(int count, QueryFilter where, OrderBy<MessageColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<Message>();
			QuerySet query = GetQuerySet(db);
			query.Top<Message>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<MessageColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<MessageCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static MessageCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<Message>();
			QuerySet query = GetQuerySet(db);
			query.Top<Message>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<MessageCollection>(0);
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
		public static MessageCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<Message>();
			QuerySet query = GetQuerySet(db);
			query.Top<Message>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<MessageCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of Messages
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<Message>();
            QuerySet query = GetQuerySet(db);
            query.Count<Message>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a MessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between MessageColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<MessageColumns> where, Database database = null)
		{
			MessageColumns c = new MessageColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<Message>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Message>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<Message>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Message>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static Message CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<Message>();			
			var dao = new Message();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static Message OneOrThrow(MessageCollection c)
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
