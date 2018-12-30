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

namespace Bam.Net.Logging.Data
{
	// schema = DaoLogger2
	// connection Name = DaoLogger2
	[Serializable]
	[Bam.Net.Data.Table("Param", "DaoLogger2")]
	public partial class Param: Bam.Net.Data.Dao
	{
		public Param():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Param(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Param(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Param(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator Param(DataRow data)
		{
			return new Param(data);
		}

		private void SetChildren()
		{

			if(_database != null)
			{
				this.ChildCollections.Add("EventParam_ParamId", new EventParamCollection(Database.GetQuery<EventParamColumns, EventParam>((c) => c.ParamId == GetULongValue("Id")), this, "ParamId"));				
			}						
            this.ChildCollections.Add("Param_EventParam_Event",  new XrefDaoCollection<EventParam, Event>(this, false));
				
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

	// property:Position, columnName:Position	
	[Bam.Net.Data.Column(Name="Position", DbDataType="Int", MaxLength="10", AllowNull=true)]
	public int? Position
	{
		get
		{
			return GetIntValue("Position");
		}
		set
		{
			SetValue("Position", value);
		}
	}

	// property:Value, columnName:Value	
	[Bam.Net.Data.Column(Name="Value", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Value
	{
		get
		{
			return GetStringValue("Value");
		}
		set
		{
			SetValue("Value", value);
		}
	}



				

	[Bam.Net.Exclude]	
	public EventParamCollection EventParamsByParamId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("EventParam_ParamId"))
			{
				SetChildren();
			}

			var c = (EventParamCollection)this.ChildCollections["EventParam_ParamId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			


		// Xref       
        public XrefDaoCollection<EventParam, Event> Events
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("Param_EventParam_Event"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<EventParam, Event>)this.ChildCollections["Param_EventParam_Event"];
				if(!xref.Loaded)
				{
					xref.Load(Database);
				}

				return xref;
            }
        }		/// <summary>
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
				var colFilter = new ParamColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the Param table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ParamCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<Param>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<Param>();
			var results = new ParamCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<Param>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ParamColumns columns = new ParamColumns();
				var orderBy = Bam.Net.Data.Order.By<ParamColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<Param>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<ParamColumns> where, Action<IEnumerable<Param>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ParamColumns columns = new ParamColumns();
				var orderBy = Bam.Net.Data.Order.By<ParamColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (ParamColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<Param>> batchProcessor, Bam.Net.Data.OrderBy<ParamColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<ParamColumns> where, Action<IEnumerable<Param>> batchProcessor, Bam.Net.Data.OrderBy<ParamColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ParamColumns columns = new ParamColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (ParamColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static Param GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static Param GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Param GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Param GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Param GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static Param GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static ParamCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static ParamCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<ParamColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ParamColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ParamColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ParamCollection Where(Func<ParamColumns, QueryFilter<ParamColumns>> where, OrderBy<ParamColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<Param>();
			return new ParamCollection(database.GetQuery<ParamColumns, Param>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ParamColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ParamColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ParamCollection Where(WhereDelegate<ParamColumns> where, Database database = null)
		{		
			database = database ?? Db.For<Param>();
			var results = new ParamCollection(database, database.GetQuery<ParamColumns, Param>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ParamColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ParamColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ParamCollection Where(WhereDelegate<ParamColumns> where, OrderBy<ParamColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<Param>();
			var results = new ParamCollection(database, database.GetQuery<ParamColumns, Param>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;ParamColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ParamCollection Where(QiQuery where, Database database = null)
		{
			var results = new ParamCollection(database, Select<ParamColumns>.From<Param>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static Param GetOneWhere(QueryFilter where, Database database = null)
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
		public static Param OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<ParamColumns> whereDelegate = (c) => where;
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
		public static Param GetOneWhere(WhereDelegate<ParamColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				ParamColumns c = new ParamColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Param instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ParamColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ParamColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Param OneWhere(WhereDelegate<ParamColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ParamColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Param OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ParamColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ParamColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Param FirstOneWhere(WhereDelegate<ParamColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ParamColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ParamColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Param FirstOneWhere(WhereDelegate<ParamColumns> where, OrderBy<ParamColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ParamColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ParamColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Param FirstOneWhere(QueryFilter where, OrderBy<ParamColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<ParamColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a ParamColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ParamColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ParamCollection Top(int count, WhereDelegate<ParamColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ParamColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ParamColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static ParamCollection Top(int count, WhereDelegate<ParamColumns> where, OrderBy<ParamColumns> orderBy, Database database = null)
		{
			ParamColumns c = new ParamColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<Param>();
			QuerySet query = GetQuerySet(db); 
			query.Top<Param>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ParamColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ParamCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ParamCollection Top(int count, QueryFilter where, Database database)
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
		public static ParamCollection Top(int count, QueryFilter where, OrderBy<ParamColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<Param>();
			QuerySet query = GetQuerySet(db);
			query.Top<Param>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<ParamColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ParamCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ParamCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<Param>();
			QuerySet query = GetQuerySet(db);
			query.Top<Param>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<ParamCollection>(0);
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
		public static ParamCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<Param>();
			QuerySet query = GetQuerySet(db);
			query.Top<Param>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<ParamCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of Params
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<Param>();
            QuerySet query = GetQuerySet(db);
            query.Count<Param>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ParamColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ParamColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<ParamColumns> where, Database database = null)
		{
			ParamColumns c = new ParamColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<Param>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Param>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<Param>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Param>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static Param CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<Param>();			
			var dao = new Param();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static Param OneOrThrow(ParamCollection c)
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
