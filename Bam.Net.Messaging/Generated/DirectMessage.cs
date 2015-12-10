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
	[Bam.Net.Data.Table("DirectMessage", "Messaging")]
	public partial class DirectMessage: Dao
	{
		public DirectMessage():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public DirectMessage(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator DirectMessage(DataRow data)
		{
			return new DirectMessage(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("EmailMessage_DirectMessageId", new EmailMessageCollection(new Query<EmailMessageColumns, EmailMessage>((c) => c.DirectMessageId == this.Id), this, "DirectMessageId"));							
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

	// property:To, columnName:To	
	[Bam.Net.Data.Column(Name="To", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string To
	{
		get
		{
			return GetStringValue("To");
		}
		set
		{
			SetValue("To", value);
		}
	}

	// property:ToEmail, columnName:ToEmail	
	[Bam.Net.Data.Column(Name="ToEmail", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string ToEmail
	{
		get
		{
			return GetStringValue("ToEmail");
		}
		set
		{
			SetValue("ToEmail", value);
		}
	}



	// start MessageId -> MessageId
	[Bam.Net.Data.ForeignKey(
        Table="DirectMessage",
		Name="MessageId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Message",
		Suffix="1")]
	public long? MessageId
	{
		get
		{
			return GetLongValue("MessageId");
		}
		set
		{
			SetValue("MessageId", value);
		}
	}

	Message _messageOfMessageId;
	public Message MessageOfMessageId
	{
		get
		{
			if(_messageOfMessageId == null)
			{
				_messageOfMessageId = Bam.Net.Messaging.Data.Message.OneWhere(f => f.Id == this.MessageId);
			}
			return _messageOfMessageId;
		}
	}
	
				

	[Exclude]	
	public EmailMessageCollection EmailMessagesByDirectMessageId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("EmailMessage_DirectMessageId"))
			{
				SetChildren();
			}

			var c = (EmailMessageCollection)this.ChildCollections["EmailMessage_DirectMessageId"];
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
			var colFilter = new DirectMessageColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the DirectMessage table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static DirectMessageCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<DirectMessage>();
			Database db = database == null ? Db.For<DirectMessage>(): database;
			var results = new DirectMessageCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a DirectMessageColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between DirectMessageColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static DirectMessageCollection Where(Func<DirectMessageColumns, QueryFilter<DirectMessageColumns>> where, OrderBy<DirectMessageColumns> orderBy = null)
		{
			return new DirectMessageCollection(new Query<DirectMessageColumns, DirectMessage>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DirectMessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DirectMessageColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static DirectMessageCollection Where(WhereDelegate<DirectMessageColumns> where, Database db = null)
		{
			var results = new DirectMessageCollection(db, new Query<DirectMessageColumns, DirectMessage>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DirectMessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DirectMessageColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static DirectMessageCollection Where(WhereDelegate<DirectMessageColumns> where, OrderBy<DirectMessageColumns> orderBy = null, Database db = null)
		{
			var results = new DirectMessageCollection(db, new Query<DirectMessageColumns, DirectMessage>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<DirectMessageColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static DirectMessageCollection Where(QiQuery where, Database db = null)
		{
			var results = new DirectMessageCollection(db, Select<DirectMessageColumns>.From<DirectMessage>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single DirectMessage instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DirectMessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DirectMessageColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static DirectMessage OneWhere(WhereDelegate<DirectMessageColumns> where, Database db = null)
		{
			var results = new DirectMessageCollection(db, Select<DirectMessageColumns>.From<DirectMessage>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<DirectMessageColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static DirectMessage OneWhere(QiQuery where, Database db = null)
		{
			var results = new DirectMessageCollection(db, Select<DirectMessageColumns>.From<DirectMessage>().Where(where, db));
			return OneOrThrow(results);
		}

		private static DirectMessage OneOrThrow(DirectMessageCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a DirectMessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DirectMessageColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static DirectMessage FirstOneWhere(WhereDelegate<DirectMessageColumns> where, Database db = null)
		{
			var results = new DirectMessageCollection(db, Select<DirectMessageColumns>.From<DirectMessage>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a DirectMessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DirectMessageColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static DirectMessageCollection Top(int count, WhereDelegate<DirectMessageColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a DirectMessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DirectMessageColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static DirectMessageCollection Top(int count, WhereDelegate<DirectMessageColumns> where, OrderBy<DirectMessageColumns> orderBy, Database database = null)
		{
			DirectMessageColumns c = new DirectMessageColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<DirectMessage>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<DirectMessage>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<DirectMessageColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<DirectMessageCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DirectMessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DirectMessageColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<DirectMessageColumns> where, Database database = null)
		{
			DirectMessageColumns c = new DirectMessageColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<DirectMessage>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<DirectMessage>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
