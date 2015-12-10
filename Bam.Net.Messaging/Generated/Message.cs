/*
	Copyright © Bryan Apellanes 2015  
*/
// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.Messaging.Data
{
	// schema = Messaging
	// connection Name = Messaging
	[Bam.Net.Data.Table("Message", "Messaging")]
	public partial class Message: Dao
	{
		public Message():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public Message(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Message(DataRow data)
		{
			return new Message(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("DirectMessage_MessageId", new DirectMessageCollection(new Query<DirectMessageColumns, DirectMessage>((c) => c.MessageId == this.Id), this, "MessageId"));							
		}

	// property:Id, columnName:Id	
	[Exclude]
	[Bam.Net.Data.KeyColumn(Name="Id", DbDataType="BigInt", MaxLength="8")]
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



				

	[Exclude]	
	public DirectMessageCollection DirectMessagesByMessageId
	{
		get
		{
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
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new MessageColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the Message table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static MessageCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Message>();
			Database db = database == null ? Db.For<Message>(): database;
			var results = new MessageCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a MessageColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between MessageColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static MessageCollection Where(Func<MessageColumns, QueryFilter<MessageColumns>> where, OrderBy<MessageColumns> orderBy = null)
		{
			return new MessageCollection(new Query<MessageColumns, Message>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a MessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between MessageColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static MessageCollection Where(WhereDelegate<MessageColumns> where, Database db = null)
		{
			var results = new MessageCollection(db, new Query<MessageColumns, Message>(where, db), true);
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
		/// <param name="db"></param>
		public static MessageCollection Where(WhereDelegate<MessageColumns> where, OrderBy<MessageColumns> orderBy = null, Database db = null)
		{
			var results = new MessageCollection(db, new Query<MessageColumns, Message>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<MessageColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static MessageCollection Where(QiQuery where, Database db = null)
		{
			var results = new MessageCollection(db, Select<MessageColumns>.From<Message>().Where(where, db));
			return results;
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
		/// <param name="db"></param>
		public static Message OneWhere(WhereDelegate<MessageColumns> where, Database db = null)
		{
			var results = new MessageCollection(db, Select<MessageColumns>.From<Message>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<MessageColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Message OneWhere(QiQuery where, Database db = null)
		{
			var results = new MessageCollection(db, Select<MessageColumns>.From<Message>().Where(where, db));
			return OneOrThrow(results);
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

		/// <summary>
		/// Execute a query and return the first result
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a MessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between MessageColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Message FirstOneWhere(WhereDelegate<MessageColumns> where, Database db = null)
		{
			var results = new MessageCollection(db, Select<MessageColumns>.From<Message>().Where(where, db));
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
		/// of values
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a MessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between MessageColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static MessageCollection Top(int count, WhereDelegate<MessageColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		/// <summary>
		/// Execute a query and return the specified count
		/// of values
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
		/// <param name="db"></param>
		public static MessageCollection Top(int count, WhereDelegate<MessageColumns> where, OrderBy<MessageColumns> orderBy, Database database = null)
		{
			MessageColumns c = new MessageColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<Message>(): database;
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

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a MessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between MessageColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<MessageColumns> where, Database database = null)
		{
			MessageColumns c = new MessageColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Message>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Message>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
