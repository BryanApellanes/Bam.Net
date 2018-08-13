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
	[Bam.Net.Data.Table("DeleteEvent", "DataReplication")]
	public partial class DeleteEvent: Bam.Net.Data.Dao
	{
		public DeleteEvent():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DeleteEvent(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DeleteEvent(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DeleteEvent(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator DeleteEvent(DataRow data)
		{
			return new DeleteEvent(data);
		}

		private void SetChildren()
		{

			if(_database != null)
			{
				this.ChildCollections.Add("DataProperty_DeleteEventId", new DataPropertyCollection(Database.GetQuery<DataPropertyColumns, DataProperty>((c) => c.DeleteEventId == GetLongValue("Id")), this, "DeleteEventId"));				
			}						
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

	// property:Type, columnName:Type	
	[Bam.Net.Data.Column(Name="Type", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Type
	{
		get
		{
			return GetStringValue("Type");
		}
		set
		{
			SetValue("Type", value);
		}
	}

	// property:InstanceCuid, columnName:InstanceCuid	
	[Bam.Net.Data.Column(Name="InstanceCuid", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string InstanceCuid
	{
		get
		{
			return GetStringValue("InstanceCuid");
		}
		set
		{
			SetValue("InstanceCuid", value);
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
	public DataPropertyCollection DataPropertiesByDeleteEventId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("DataProperty_DeleteEventId"))
			{
				SetChildren();
			}

			var c = (DataPropertyCollection)this.ChildCollections["DataProperty_DeleteEventId"];
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
				var colFilter = new DeleteEventColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the DeleteEvent table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static DeleteEventCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<DeleteEvent>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<DeleteEvent>();
			var results = new DeleteEventCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<DeleteEvent>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				DeleteEventColumns columns = new DeleteEventColumns();
				var orderBy = Bam.Net.Data.Order.By<DeleteEventColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<DeleteEvent>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<DeleteEventColumns> where, Action<IEnumerable<DeleteEvent>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				DeleteEventColumns columns = new DeleteEventColumns();
				var orderBy = Bam.Net.Data.Order.By<DeleteEventColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (DeleteEventColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<DeleteEvent>> batchProcessor, Bam.Net.Data.OrderBy<DeleteEventColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<DeleteEventColumns> where, Action<IEnumerable<DeleteEvent>> batchProcessor, Bam.Net.Data.OrderBy<DeleteEventColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				DeleteEventColumns columns = new DeleteEventColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (DeleteEventColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static DeleteEvent GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static DeleteEvent GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static DeleteEvent GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static DeleteEvent GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static DeleteEventCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static DeleteEventCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<DeleteEventColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a DeleteEventColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between DeleteEventColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static DeleteEventCollection Where(Func<DeleteEventColumns, QueryFilter<DeleteEventColumns>> where, OrderBy<DeleteEventColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<DeleteEvent>();
			return new DeleteEventCollection(database.GetQuery<DeleteEventColumns, DeleteEvent>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DeleteEventColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeleteEventColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static DeleteEventCollection Where(WhereDelegate<DeleteEventColumns> where, Database database = null)
		{		
			database = database ?? Db.For<DeleteEvent>();
			var results = new DeleteEventCollection(database, database.GetQuery<DeleteEventColumns, DeleteEvent>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DeleteEventColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeleteEventColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DeleteEventCollection Where(WhereDelegate<DeleteEventColumns> where, OrderBy<DeleteEventColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<DeleteEvent>();
			var results = new DeleteEventCollection(database, database.GetQuery<DeleteEventColumns, DeleteEvent>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;DeleteEventColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static DeleteEventCollection Where(QiQuery where, Database database = null)
		{
			var results = new DeleteEventCollection(database, Select<DeleteEventColumns>.From<DeleteEvent>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static DeleteEvent GetOneWhere(QueryFilter where, Database database = null)
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
		public static DeleteEvent OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<DeleteEventColumns> whereDelegate = (c) => where;
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
		public static DeleteEvent GetOneWhere(WhereDelegate<DeleteEventColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				DeleteEventColumns c = new DeleteEventColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single DeleteEvent instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DeleteEventColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeleteEventColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DeleteEvent OneWhere(WhereDelegate<DeleteEventColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<DeleteEventColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static DeleteEvent OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DeleteEventColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeleteEventColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DeleteEvent FirstOneWhere(WhereDelegate<DeleteEventColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DeleteEventColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeleteEventColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DeleteEvent FirstOneWhere(WhereDelegate<DeleteEventColumns> where, OrderBy<DeleteEventColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DeleteEventColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeleteEventColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DeleteEvent FirstOneWhere(QueryFilter where, OrderBy<DeleteEventColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<DeleteEventColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a DeleteEventColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeleteEventColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DeleteEventCollection Top(int count, WhereDelegate<DeleteEventColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DeleteEventColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeleteEventColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static DeleteEventCollection Top(int count, WhereDelegate<DeleteEventColumns> where, OrderBy<DeleteEventColumns> orderBy, Database database = null)
		{
			DeleteEventColumns c = new DeleteEventColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<DeleteEvent>();
			QuerySet query = GetQuerySet(db); 
			query.Top<DeleteEvent>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<DeleteEventColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<DeleteEventCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static DeleteEventCollection Top(int count, QueryFilter where, Database database)
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
		public static DeleteEventCollection Top(int count, QueryFilter where, OrderBy<DeleteEventColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<DeleteEvent>();
			QuerySet query = GetQuerySet(db);
			query.Top<DeleteEvent>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<DeleteEventColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<DeleteEventCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static DeleteEventCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<DeleteEvent>();
			QuerySet query = GetQuerySet(db);
			query.Top<DeleteEvent>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<DeleteEventCollection>(0);
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
		public static DeleteEventCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<DeleteEvent>();
			QuerySet query = GetQuerySet(db);
			query.Top<DeleteEvent>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<DeleteEventCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of DeleteEvents
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<DeleteEvent>();
            QuerySet query = GetQuerySet(db);
            query.Count<DeleteEvent>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DeleteEventColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeleteEventColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<DeleteEventColumns> where, Database database = null)
		{
			DeleteEventColumns c = new DeleteEventColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<DeleteEvent>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<DeleteEvent>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<DeleteEvent>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<DeleteEvent>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static DeleteEvent CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<DeleteEvent>();			
			var dao = new DeleteEvent();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static DeleteEvent OneOrThrow(DeleteEventCollection c)
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
