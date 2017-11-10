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

namespace Bam.Net.Services.Distributed.Data.Dao
{
	// schema = DistributedData
	// connection Name = DistributedData
	[Serializable]
	[Bam.Net.Data.Table("UpdateOperation", "DistributedData")]
	public partial class UpdateOperation: Bam.Net.Data.Dao
	{
		public UpdateOperation():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public UpdateOperation(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public UpdateOperation(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public UpdateOperation(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator UpdateOperation(DataRow data)
		{
			return new UpdateOperation(data);
		}

		private void SetChildren()
		{
			if(_database != null)
			{
				this.ChildCollections.Add("DataProperty_UpdateOperationId", new DataPropertyCollection(Database.GetQuery<DataPropertyColumns, DataProperty>((c) => c.UpdateOperationId == GetLongValue("Id")), this, "UpdateOperationId"));				
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

	// property:TypeName, columnName:TypeName	
	[Bam.Net.Data.Column(Name="TypeName", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string TypeName
	{
		get
		{
			return GetStringValue("TypeName");
		}
		set
		{
			SetValue("TypeName", value);
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
	public DataPropertyCollection DataPropertiesByUpdateOperationId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("DataProperty_UpdateOperationId"))
			{
				SetChildren();
			}

			var c = (DataPropertyCollection)this.ChildCollections["DataProperty_UpdateOperationId"];
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
				return UniqueFilterProvider();
			}
			else
			{
				var colFilter = new UpdateOperationColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the UpdateOperation table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static UpdateOperationCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<UpdateOperation>();
			Database db = database ?? Db.For<UpdateOperation>();
			var results = new UpdateOperationCollection(db, sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<UpdateOperation>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				UpdateOperationColumns columns = new UpdateOperationColumns();
				var orderBy = Bam.Net.Data.Order.By<UpdateOperationColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, (c) => c.KeyColumn > 0, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<UpdateOperation>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<UpdateOperationColumns> where, Action<IEnumerable<UpdateOperation>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				UpdateOperationColumns columns = new UpdateOperationColumns();
				var orderBy = Bam.Net.Data.Order.By<UpdateOperationColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (UpdateOperationColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<UpdateOperation>> batchProcessor, Bam.Net.Data.OrderBy<UpdateOperationColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<UpdateOperationColumns> where, Action<IEnumerable<UpdateOperation>> batchProcessor, Bam.Net.Data.OrderBy<UpdateOperationColumns> orderBy, Database database = null)
		{
			await Task.Run(async ()=>
			{
				UpdateOperationColumns columns = new UpdateOperationColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (UpdateOperationColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static UpdateOperation GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static UpdateOperation GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static UpdateOperation GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static UpdateOperation GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static UpdateOperationCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static UpdateOperationCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<UpdateOperationColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a UpdateOperationColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between UpdateOperationColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static UpdateOperationCollection Where(Func<UpdateOperationColumns, QueryFilter<UpdateOperationColumns>> where, OrderBy<UpdateOperationColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<UpdateOperation>();
			return new UpdateOperationCollection(database.GetQuery<UpdateOperationColumns, UpdateOperation>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UpdateOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UpdateOperationColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static UpdateOperationCollection Where(WhereDelegate<UpdateOperationColumns> where, Database database = null)
		{		
			database = database ?? Db.For<UpdateOperation>();
			var results = new UpdateOperationCollection(database, database.GetQuery<UpdateOperationColumns, UpdateOperation>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UpdateOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UpdateOperationColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static UpdateOperationCollection Where(WhereDelegate<UpdateOperationColumns> where, OrderBy<UpdateOperationColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<UpdateOperation>();
			var results = new UpdateOperationCollection(database, database.GetQuery<UpdateOperationColumns, UpdateOperation>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;UpdateOperationColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static UpdateOperationCollection Where(QiQuery where, Database database = null)
		{
			var results = new UpdateOperationCollection(database, Select<UpdateOperationColumns>.From<UpdateOperation>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static UpdateOperation GetOneWhere(QueryFilter where, Database database = null)
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
		public static UpdateOperation OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<UpdateOperationColumns> whereDelegate = (c) => where;
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
		public static UpdateOperation GetOneWhere(WhereDelegate<UpdateOperationColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				UpdateOperationColumns c = new UpdateOperationColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single UpdateOperation instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UpdateOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UpdateOperationColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static UpdateOperation OneWhere(WhereDelegate<UpdateOperationColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<UpdateOperationColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static UpdateOperation OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UpdateOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UpdateOperationColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static UpdateOperation FirstOneWhere(WhereDelegate<UpdateOperationColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a UpdateOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UpdateOperationColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static UpdateOperation FirstOneWhere(WhereDelegate<UpdateOperationColumns> where, OrderBy<UpdateOperationColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a UpdateOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UpdateOperationColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static UpdateOperation FirstOneWhere(QueryFilter where, OrderBy<UpdateOperationColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<UpdateOperationColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a UpdateOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UpdateOperationColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static UpdateOperationCollection Top(int count, WhereDelegate<UpdateOperationColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a UpdateOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UpdateOperationColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static UpdateOperationCollection Top(int count, WhereDelegate<UpdateOperationColumns> where, OrderBy<UpdateOperationColumns> orderBy, Database database = null)
		{
			UpdateOperationColumns c = new UpdateOperationColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<UpdateOperation>();
			QuerySet query = GetQuerySet(db); 
			query.Top<UpdateOperation>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<UpdateOperationColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<UpdateOperationCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static UpdateOperationCollection Top(int count, QueryFilter where, Database database)
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
		public static UpdateOperationCollection Top(int count, QueryFilter where, OrderBy<UpdateOperationColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<UpdateOperation>();
			QuerySet query = GetQuerySet(db);
			query.Top<UpdateOperation>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<UpdateOperationColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<UpdateOperationCollection>(0);
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
		public static UpdateOperationCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<UpdateOperation>();
			QuerySet query = GetQuerySet(db);
			query.Top<UpdateOperation>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<UpdateOperationCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of UpdateOperations
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<UpdateOperation>();
            QuerySet query = GetQuerySet(db);
            query.Count<UpdateOperation>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UpdateOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UpdateOperationColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<UpdateOperationColumns> where, Database database = null)
		{
			UpdateOperationColumns c = new UpdateOperationColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<UpdateOperation>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<UpdateOperation>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<UpdateOperation>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<UpdateOperation>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static UpdateOperation CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<UpdateOperation>();			
			var dao = new UpdateOperation();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static UpdateOperation OneOrThrow(UpdateOperationCollection c)
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
