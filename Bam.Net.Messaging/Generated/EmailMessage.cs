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
	[Bam.Net.Data.Table("EmailMessage", "Messaging")]
	public partial class EmailMessage: Dao
	{
		public EmailMessage():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public EmailMessage(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator EmailMessage(DataRow data)
		{
			return new EmailMessage(data);
		}

		private void SetChildren()
		{
						
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

	// property:Sent, columnName:Sent	
	[Bam.Net.Data.Column(Name="Sent", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? Sent
	{
		get
		{
			return GetBooleanValue("Sent");
		}
		set
		{
			SetValue("Sent", value);
		}
	}

	// property:TemplateName, columnName:TemplateName	
	[Bam.Net.Data.Column(Name="TemplateName", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string TemplateName
	{
		get
		{
			return GetStringValue("TemplateName");
		}
		set
		{
			SetValue("TemplateName", value);
		}
	}

	// property:TemplateJsonData, columnName:TemplateJsonData	
	[Bam.Net.Data.Column(Name="TemplateJsonData", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string TemplateJsonData
	{
		get
		{
			return GetStringValue("TemplateJsonData");
		}
		set
		{
			SetValue("TemplateJsonData", value);
		}
	}



	// start DirectMessageId -> DirectMessageId
	[Bam.Net.Data.ForeignKey(
        Table="EmailMessage",
		Name="DirectMessageId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="DirectMessage",
		Suffix="1")]
	public long? DirectMessageId
	{
		get
		{
			return GetLongValue("DirectMessageId");
		}
		set
		{
			SetValue("DirectMessageId", value);
		}
	}

	DirectMessage _directMessageOfDirectMessageId;
	public DirectMessage DirectMessageOfDirectMessageId
	{
		get
		{
			if(_directMessageOfDirectMessageId == null)
			{
				_directMessageOfDirectMessageId = Bam.Net.Messaging.Data.DirectMessage.OneWhere(f => f.Id == this.DirectMessageId);
			}
			return _directMessageOfDirectMessageId;
		}
	}
	
				
		

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new EmailMessageColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the EmailMessage table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static EmailMessageCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<EmailMessage>();
			Database db = database == null ? Db.For<EmailMessage>(): database;
			var results = new EmailMessageCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a EmailMessageColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between EmailMessageColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static EmailMessageCollection Where(Func<EmailMessageColumns, QueryFilter<EmailMessageColumns>> where, OrderBy<EmailMessageColumns> orderBy = null)
		{
			return new EmailMessageCollection(new Query<EmailMessageColumns, EmailMessage>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EmailMessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EmailMessageColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static EmailMessageCollection Where(WhereDelegate<EmailMessageColumns> where, Database db = null)
		{
			var results = new EmailMessageCollection(db, new Query<EmailMessageColumns, EmailMessage>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EmailMessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EmailMessageColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static EmailMessageCollection Where(WhereDelegate<EmailMessageColumns> where, OrderBy<EmailMessageColumns> orderBy = null, Database db = null)
		{
			var results = new EmailMessageCollection(db, new Query<EmailMessageColumns, EmailMessage>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<EmailMessageColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static EmailMessageCollection Where(QiQuery where, Database db = null)
		{
			var results = new EmailMessageCollection(db, Select<EmailMessageColumns>.From<EmailMessage>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single EmailMessage instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EmailMessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EmailMessageColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static EmailMessage OneWhere(WhereDelegate<EmailMessageColumns> where, Database db = null)
		{
			var results = new EmailMessageCollection(db, Select<EmailMessageColumns>.From<EmailMessage>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<EmailMessageColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static EmailMessage OneWhere(QiQuery where, Database db = null)
		{
			var results = new EmailMessageCollection(db, Select<EmailMessageColumns>.From<EmailMessage>().Where(where, db));
			return OneOrThrow(results);
		}

		private static EmailMessage OneOrThrow(EmailMessageCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a EmailMessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EmailMessageColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static EmailMessage FirstOneWhere(WhereDelegate<EmailMessageColumns> where, Database db = null)
		{
			var results = new EmailMessageCollection(db, Select<EmailMessageColumns>.From<EmailMessage>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a EmailMessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EmailMessageColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static EmailMessageCollection Top(int count, WhereDelegate<EmailMessageColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a EmailMessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EmailMessageColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static EmailMessageCollection Top(int count, WhereDelegate<EmailMessageColumns> where, OrderBy<EmailMessageColumns> orderBy, Database database = null)
		{
			EmailMessageColumns c = new EmailMessageColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<EmailMessage>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<EmailMessage>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<EmailMessageColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<EmailMessageCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EmailMessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EmailMessageColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<EmailMessageColumns> where, Database database = null)
		{
			EmailMessageColumns c = new EmailMessageColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<EmailMessage>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<EmailMessage>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
