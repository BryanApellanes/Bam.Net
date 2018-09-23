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
	[Bam.Net.Data.Table("EmailMessage", "Messaging")]
	public partial class EmailMessage: Bam.Net.Data.Dao
	{
		public EmailMessage():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public EmailMessage(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public EmailMessage(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public EmailMessage(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator EmailMessage(DataRow data)
		{
			return new EmailMessage(data);
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
	public ulong? DirectMessageId
	{
		get
		{
			return GetULongValue("DirectMessageId");
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
				_directMessageOfDirectMessageId = Bam.Net.Messaging.Data.DirectMessage.OneWhere(c => c.KeyColumn == this.DirectMessageId, this.Database);
			}
			return _directMessageOfDirectMessageId;
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
				var colFilter = new EmailMessageColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the EmailMessage table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static EmailMessageCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<EmailMessage>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<EmailMessage>();
			var results = new EmailMessageCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<EmailMessage>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				EmailMessageColumns columns = new EmailMessageColumns();
				var orderBy = Bam.Net.Data.Order.By<EmailMessageColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<EmailMessage>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<EmailMessageColumns> where, Action<IEnumerable<EmailMessage>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				EmailMessageColumns columns = new EmailMessageColumns();
				var orderBy = Bam.Net.Data.Order.By<EmailMessageColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (EmailMessageColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<EmailMessage>> batchProcessor, Bam.Net.Data.OrderBy<EmailMessageColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<EmailMessageColumns> where, Action<IEnumerable<EmailMessage>> batchProcessor, Bam.Net.Data.OrderBy<EmailMessageColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				EmailMessageColumns columns = new EmailMessageColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (EmailMessageColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static EmailMessage GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static EmailMessage GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static EmailMessage GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static EmailMessage GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static EmailMessage GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static EmailMessage GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static EmailMessageCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static EmailMessageCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<EmailMessageColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a EmailMessageColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between EmailMessageColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static EmailMessageCollection Where(Func<EmailMessageColumns, QueryFilter<EmailMessageColumns>> where, OrderBy<EmailMessageColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<EmailMessage>();
			return new EmailMessageCollection(database.GetQuery<EmailMessageColumns, EmailMessage>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EmailMessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EmailMessageColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static EmailMessageCollection Where(WhereDelegate<EmailMessageColumns> where, Database database = null)
		{		
			database = database ?? Db.For<EmailMessage>();
			var results = new EmailMessageCollection(database, database.GetQuery<EmailMessageColumns, EmailMessage>(where), true);
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
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static EmailMessageCollection Where(WhereDelegate<EmailMessageColumns> where, OrderBy<EmailMessageColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<EmailMessage>();
			var results = new EmailMessageCollection(database, database.GetQuery<EmailMessageColumns, EmailMessage>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;EmailMessageColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static EmailMessageCollection Where(QiQuery where, Database database = null)
		{
			var results = new EmailMessageCollection(database, Select<EmailMessageColumns>.From<EmailMessage>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static EmailMessage GetOneWhere(QueryFilter where, Database database = null)
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
		public static EmailMessage OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<EmailMessageColumns> whereDelegate = (c) => where;
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
		public static EmailMessage GetOneWhere(WhereDelegate<EmailMessageColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				EmailMessageColumns c = new EmailMessageColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
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
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static EmailMessage OneWhere(WhereDelegate<EmailMessageColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<EmailMessageColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static EmailMessage OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EmailMessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EmailMessageColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static EmailMessage FirstOneWhere(WhereDelegate<EmailMessageColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a EmailMessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EmailMessageColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static EmailMessage FirstOneWhere(WhereDelegate<EmailMessageColumns> where, OrderBy<EmailMessageColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a EmailMessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EmailMessageColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static EmailMessage FirstOneWhere(QueryFilter where, OrderBy<EmailMessageColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<EmailMessageColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a EmailMessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EmailMessageColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static EmailMessageCollection Top(int count, WhereDelegate<EmailMessageColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a EmailMessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EmailMessageColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static EmailMessageCollection Top(int count, WhereDelegate<EmailMessageColumns> where, OrderBy<EmailMessageColumns> orderBy, Database database = null)
		{
			EmailMessageColumns c = new EmailMessageColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<EmailMessage>();
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

		[Bam.Net.Exclude]
		public static EmailMessageCollection Top(int count, QueryFilter where, Database database)
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
		public static EmailMessageCollection Top(int count, QueryFilter where, OrderBy<EmailMessageColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<EmailMessage>();
			QuerySet query = GetQuerySet(db);
			query.Top<EmailMessage>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<EmailMessageColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<EmailMessageCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static EmailMessageCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<EmailMessage>();
			QuerySet query = GetQuerySet(db);
			query.Top<EmailMessage>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<EmailMessageCollection>(0);
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
		public static EmailMessageCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<EmailMessage>();
			QuerySet query = GetQuerySet(db);
			query.Top<EmailMessage>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<EmailMessageCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of EmailMessages
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<EmailMessage>();
            QuerySet query = GetQuerySet(db);
            query.Count<EmailMessage>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EmailMessageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EmailMessageColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<EmailMessageColumns> where, Database database = null)
		{
			EmailMessageColumns c = new EmailMessageColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<EmailMessage>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<EmailMessage>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<EmailMessage>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<EmailMessage>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static EmailMessage CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<EmailMessage>();			
			var dao = new EmailMessage();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
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

	}
}																								
