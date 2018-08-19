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

namespace Bam.Net.Presentation.Unicode
{
	// schema = Emojis
	// connection Name = Emojis
	[Serializable]
	[Bam.Net.Data.Table("Category", "Emojis")]
	public partial class Category: Bam.Net.Data.Dao
	{
		public Category():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Category(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Category(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Category(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator Category(DataRow data)
		{
			return new Category(data);
		}

		private void SetChildren()
		{

			if(_database != null)
			{
				this.ChildCollections.Add("Emoji_CategoryId", new EmojiCollection(Database.GetQuery<EmojiColumns, Emoji>((c) => c.CategoryId == GetULongValue("Id")), this, "CategoryId"));				
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

	// property:Name, columnName:Name	
	[Bam.Net.Data.Column(Name="Name", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Name
	{
		get
		{
			return GetStringValue("Name");
		}
		set
		{
			SetValue("Name", value);
		}
	}



				

	[Bam.Net.Exclude]	
	public EmojiCollection EmojisByCategoryId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("Emoji_CategoryId"))
			{
				SetChildren();
			}

			var c = (EmojiCollection)this.ChildCollections["Emoji_CategoryId"];
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
				var colFilter = new CategoryColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the Category table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static CategoryCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<Category>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<Category>();
			var results = new CategoryCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<Category>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				CategoryColumns columns = new CategoryColumns();
				var orderBy = Bam.Net.Data.Order.By<CategoryColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<Category>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<CategoryColumns> where, Action<IEnumerable<Category>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				CategoryColumns columns = new CategoryColumns();
				var orderBy = Bam.Net.Data.Order.By<CategoryColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (CategoryColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<Category>> batchProcessor, Bam.Net.Data.OrderBy<CategoryColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<CategoryColumns> where, Action<IEnumerable<Category>> batchProcessor, Bam.Net.Data.OrderBy<CategoryColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				CategoryColumns columns = new CategoryColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (CategoryColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static Category GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static Category GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Category GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Category GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Category GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static Category GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static CategoryCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static CategoryCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<CategoryColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a CategoryColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between CategoryColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static CategoryCollection Where(Func<CategoryColumns, QueryFilter<CategoryColumns>> where, OrderBy<CategoryColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<Category>();
			return new CategoryCollection(database.GetQuery<CategoryColumns, Category>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CategoryColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static CategoryCollection Where(WhereDelegate<CategoryColumns> where, Database database = null)
		{		
			database = database ?? Db.For<Category>();
			var results = new CategoryCollection(database, database.GetQuery<CategoryColumns, Category>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CategoryColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static CategoryCollection Where(WhereDelegate<CategoryColumns> where, OrderBy<CategoryColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<Category>();
			var results = new CategoryCollection(database, database.GetQuery<CategoryColumns, Category>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;CategoryColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static CategoryCollection Where(QiQuery where, Database database = null)
		{
			var results = new CategoryCollection(database, Select<CategoryColumns>.From<Category>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static Category GetOneWhere(QueryFilter where, Database database = null)
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
		public static Category OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<CategoryColumns> whereDelegate = (c) => where;
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
		public static Category GetOneWhere(WhereDelegate<CategoryColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				CategoryColumns c = new CategoryColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Category instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CategoryColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Category OneWhere(WhereDelegate<CategoryColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<CategoryColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Category OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CategoryColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Category FirstOneWhere(WhereDelegate<CategoryColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a CategoryColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Category FirstOneWhere(WhereDelegate<CategoryColumns> where, OrderBy<CategoryColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a CategoryColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Category FirstOneWhere(QueryFilter where, OrderBy<CategoryColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<CategoryColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a CategoryColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static CategoryCollection Top(int count, WhereDelegate<CategoryColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a CategoryColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static CategoryCollection Top(int count, WhereDelegate<CategoryColumns> where, OrderBy<CategoryColumns> orderBy, Database database = null)
		{
			CategoryColumns c = new CategoryColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<Category>();
			QuerySet query = GetQuerySet(db); 
			query.Top<Category>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<CategoryColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<CategoryCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static CategoryCollection Top(int count, QueryFilter where, Database database)
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
		public static CategoryCollection Top(int count, QueryFilter where, OrderBy<CategoryColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<Category>();
			QuerySet query = GetQuerySet(db);
			query.Top<Category>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<CategoryColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<CategoryCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static CategoryCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<Category>();
			QuerySet query = GetQuerySet(db);
			query.Top<Category>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<CategoryCollection>(0);
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
		public static CategoryCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<Category>();
			QuerySet query = GetQuerySet(db);
			query.Top<Category>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<CategoryCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of Categories
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<Category>();
            QuerySet query = GetQuerySet(db);
            query.Count<Category>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CategoryColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<CategoryColumns> where, Database database = null)
		{
			CategoryColumns c = new CategoryColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<Category>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Category>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<Category>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Category>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static Category CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<Category>();			
			var dao = new Category();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static Category OneOrThrow(CategoryCollection c)
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
