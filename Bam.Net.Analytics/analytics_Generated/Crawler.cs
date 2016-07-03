/*
	This file was generated and should not be modified directly
*/
// Model is Table
using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.Analytics
{
	// schema = Analytics
	// connection Name = Analytics
	[Serializable]
	[Bam.Net.Data.Table("Crawler", "Analytics")]
	public partial class Crawler: Dao
	{
		public Crawler():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Crawler(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Crawler(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Crawler(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator Crawler(DataRow data)
		{
			return new Crawler(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("Image_CrawlerId", new ImageCollection(Database.GetQuery<ImageColumns, Image>((c) => c.CrawlerId == GetLongValue("Id")), this, "CrawlerId"));							
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

	// property:RootUrl, columnName:RootUrl	
	[Bam.Net.Data.Column(Name="RootUrl", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string RootUrl
	{
		get
		{
			return GetStringValue("RootUrl");
		}
		set
		{
			SetValue("RootUrl", value);
		}
	}



				

	[Exclude]	
	public ImageCollection ImagesByCrawlerId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("Image_CrawlerId"))
			{
				SetChildren();
			}

			var c = (ImageCollection)this.ChildCollections["Image_CrawlerId"];
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
		public override IQueryFilter GetUniqueFilter()
		{
			if(UniqueFilterProvider != null)
			{
				return UniqueFilterProvider();
			}
			else
			{
				var colFilter = new CrawlerColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the Crawler table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static CrawlerCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Crawler>();
			Database db = database ?? Db.For<Crawler>();
			var results = new CrawlerCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static async Task BatchAll(int batchSize, Func<CrawlerCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				CrawlerColumns columns = new CrawlerColumns();
				var orderBy = Order.By<CrawlerColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, (c) => c.KeyColumn > 0, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (c) => c.KeyColumn > topId, orderBy, database);
				}
			});			
		}	 

		public static async Task BatchQuery(int batchSize, QueryFilter filter, Func<CrawlerCollection, Task> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		public static async Task BatchQuery(int batchSize, WhereDelegate<CrawlerColumns> where, Func<CrawlerCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				CrawlerColumns columns = new CrawlerColumns();
				var orderBy = Order.By<CrawlerColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (CrawlerColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static Crawler GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Crawler GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Crawler GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static Crawler GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		public static CrawlerCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static CrawlerCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<CrawlerColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a CrawlerColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between CrawlerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CrawlerCollection Where(Func<CrawlerColumns, QueryFilter<CrawlerColumns>> where, OrderBy<CrawlerColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<Crawler>();
			return new CrawlerCollection(database.GetQuery<CrawlerColumns, Crawler>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CrawlerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CrawlerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CrawlerCollection Where(WhereDelegate<CrawlerColumns> where, Database database = null)
		{		
			database = database ?? Db.For<Crawler>();
			var results = new CrawlerCollection(database, database.GetQuery<CrawlerColumns, Crawler>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CrawlerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CrawlerColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static CrawlerCollection Where(WhereDelegate<CrawlerColumns> where, OrderBy<CrawlerColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<Crawler>();
			var results = new CrawlerCollection(database, database.GetQuery<CrawlerColumns, Crawler>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;CrawlerColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static CrawlerCollection Where(QiQuery where, Database database = null)
		{
			var results = new CrawlerCollection(database, Select<CrawlerColumns>.From<Crawler>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static Crawler GetOneWhere(QueryFilter where, Database database = null)
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
		public static Crawler OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<CrawlerColumns> whereDelegate = (c) => where;
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
		public static Crawler GetOneWhere(WhereDelegate<CrawlerColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				CrawlerColumns c = new CrawlerColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Crawler instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CrawlerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CrawlerColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Crawler OneWhere(WhereDelegate<CrawlerColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<CrawlerColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Crawler OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CrawlerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CrawlerColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Crawler FirstOneWhere(WhereDelegate<CrawlerColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a CrawlerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CrawlerColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Crawler FirstOneWhere(WhereDelegate<CrawlerColumns> where, OrderBy<CrawlerColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a CrawlerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CrawlerColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Crawler FirstOneWhere(QueryFilter where, OrderBy<CrawlerColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<CrawlerColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a CrawlerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CrawlerColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static CrawlerCollection Top(int count, WhereDelegate<CrawlerColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a CrawlerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CrawlerColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static CrawlerCollection Top(int count, WhereDelegate<CrawlerColumns> where, OrderBy<CrawlerColumns> orderBy, Database database = null)
		{
			CrawlerColumns c = new CrawlerColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<Crawler>();
			QuerySet query = GetQuerySet(db); 
			query.Top<Crawler>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<CrawlerColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<CrawlerCollection>(0);
			results.Database = db;
			return results;
		}

		public static CrawlerCollection Top(int count, QueryFilter where, Database database)
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
		/// <param name="db"></param>
		public static CrawlerCollection Top(int count, QueryFilter where, OrderBy<CrawlerColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<Crawler>();
			QuerySet query = GetQuerySet(db);
			query.Top<Crawler>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<CrawlerColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<CrawlerCollection>(0);
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
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static CrawlerCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<Crawler>();
			QuerySet query = GetQuerySet(db);
			query.Top<Crawler>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<CrawlerCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CrawlerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CrawlerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<CrawlerColumns> where, Database database = null)
		{
			CrawlerColumns c = new CrawlerColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<Crawler>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Crawler>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static Crawler CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<Crawler>();			
			var dao = new Crawler();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static Crawler OneOrThrow(CrawlerCollection c)
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
