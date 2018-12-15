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

namespace Bam.Net.Analytics
{
	// schema = Analytics
	// connection Name = Analytics
	[Serializable]
	[Bam.Net.Data.Table("Image", "Analytics")]
	public partial class Image: Bam.Net.Data.Dao
	{
		public Image():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Image(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Image(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Image(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator Image(DataRow data)
		{
			return new Image(data);
		}

		private void SetChildren()
		{

			if(_database != null)
			{
				this.ChildCollections.Add("ImageTag_ImageId", new ImageTagCollection(Database.GetQuery<ImageTagColumns, ImageTag>((c) => c.ImageId == GetULongValue("Id")), this, "ImageId"));				
			}			
            this.ChildCollections.Add("Image_ImageTag_Tag",  new XrefDaoCollection<ImageTag, Tag>(this, false));
							
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

	// property:Date, columnName:Date	
	[Bam.Net.Data.Column(Name="Date", DbDataType="DateTime", MaxLength="8", AllowNull=false)]
	public DateTime? Date
	{
		get
		{
			return GetDateTimeValue("Date");
		}
		set
		{
			SetValue("Date", value);
		}
	}



	// start UrlId -> UrlId
	[Bam.Net.Data.ForeignKey(
        Table="Image",
		Name="UrlId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Url",
		Suffix="1")]
	public ulong? UrlId
	{
		get
		{
			return GetULongValue("UrlId");
		}
		set
		{
			SetValue("UrlId", value);
		}
	}

	Url _urlOfUrlId;
	public Url UrlOfUrlId
	{
		get
		{
			if(_urlOfUrlId == null)
			{
				_urlOfUrlId = Bam.Net.Analytics.Url.OneWhere(c => c.KeyColumn == this.UrlId, this.Database);
			}
			return _urlOfUrlId;
		}
	}
	
	// start CrawlerId -> CrawlerId
	[Bam.Net.Data.ForeignKey(
        Table="Image",
		Name="CrawlerId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Crawler",
		Suffix="2")]
	public ulong? CrawlerId
	{
		get
		{
			return GetULongValue("CrawlerId");
		}
		set
		{
			SetValue("CrawlerId", value);
		}
	}

	Crawler _crawlerOfCrawlerId;
	public Crawler CrawlerOfCrawlerId
	{
		get
		{
			if(_crawlerOfCrawlerId == null)
			{
				_crawlerOfCrawlerId = Bam.Net.Analytics.Crawler.OneWhere(c => c.KeyColumn == this.CrawlerId, this.Database);
			}
			return _crawlerOfCrawlerId;
		}
	}
	
				

	[Bam.Net.Exclude]	
	public ImageTagCollection ImageTagsByImageId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("ImageTag_ImageId"))
			{
				SetChildren();
			}

			var c = (ImageTagCollection)this.ChildCollections["ImageTag_ImageId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			

		// Xref       
        public XrefDaoCollection<ImageTag, Tag> Tags
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("Image_ImageTag_Tag"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<ImageTag, Tag>)this.ChildCollections["Image_ImageTag_Tag"];
				if(!xref.Loaded)
				{
					xref.Load(Database);
				}

				return xref;
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
				var colFilter = new ImageColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the Image table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ImageCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<Image>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<Image>();
			var results = new ImageCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<Image>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ImageColumns columns = new ImageColumns();
				var orderBy = Bam.Net.Data.Order.By<ImageColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<Image>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<ImageColumns> where, Action<IEnumerable<Image>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ImageColumns columns = new ImageColumns();
				var orderBy = Bam.Net.Data.Order.By<ImageColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (ImageColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<Image>> batchProcessor, Bam.Net.Data.OrderBy<ImageColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<ImageColumns> where, Action<IEnumerable<Image>> batchProcessor, Bam.Net.Data.OrderBy<ImageColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ImageColumns columns = new ImageColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (ImageColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static Image GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static Image GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Image GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Image GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Image GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static Image GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static ImageCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static ImageCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<ImageColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ImageColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ImageColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ImageCollection Where(Func<ImageColumns, QueryFilter<ImageColumns>> where, OrderBy<ImageColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<Image>();
			return new ImageCollection(database.GetQuery<ImageColumns, Image>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ImageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ImageCollection Where(WhereDelegate<ImageColumns> where, Database database = null)
		{		
			database = database ?? Db.For<Image>();
			var results = new ImageCollection(database, database.GetQuery<ImageColumns, Image>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ImageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ImageCollection Where(WhereDelegate<ImageColumns> where, OrderBy<ImageColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<Image>();
			var results = new ImageCollection(database, database.GetQuery<ImageColumns, Image>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;ImageColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ImageCollection Where(QiQuery where, Database database = null)
		{
			var results = new ImageCollection(database, Select<ImageColumns>.From<Image>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static Image GetOneWhere(QueryFilter where, Database database = null)
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
		public static Image OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<ImageColumns> whereDelegate = (c) => where;
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
		public static Image GetOneWhere(WhereDelegate<ImageColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				ImageColumns c = new ImageColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Image instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ImageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Image OneWhere(WhereDelegate<ImageColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ImageColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Image OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ImageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Image FirstOneWhere(WhereDelegate<ImageColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ImageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Image FirstOneWhere(WhereDelegate<ImageColumns> where, OrderBy<ImageColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ImageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Image FirstOneWhere(QueryFilter where, OrderBy<ImageColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<ImageColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a ImageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ImageCollection Top(int count, WhereDelegate<ImageColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ImageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static ImageCollection Top(int count, WhereDelegate<ImageColumns> where, OrderBy<ImageColumns> orderBy, Database database = null)
		{
			ImageColumns c = new ImageColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<Image>();
			QuerySet query = GetQuerySet(db); 
			query.Top<Image>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ImageColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ImageCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ImageCollection Top(int count, QueryFilter where, Database database)
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
		public static ImageCollection Top(int count, QueryFilter where, OrderBy<ImageColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<Image>();
			QuerySet query = GetQuerySet(db);
			query.Top<Image>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<ImageColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ImageCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ImageCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<Image>();
			QuerySet query = GetQuerySet(db);
			query.Top<Image>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<ImageCollection>(0);
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
		public static ImageCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<Image>();
			QuerySet query = GetQuerySet(db);
			query.Top<Image>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<ImageCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of Images
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<Image>();
            QuerySet query = GetQuerySet(db);
            query.Count<Image>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ImageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<ImageColumns> where, Database database = null)
		{
			ImageColumns c = new ImageColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<Image>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Image>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<Image>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Image>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static Image CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<Image>();			
			var dao = new Image();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static Image OneOrThrow(ImageCollection c)
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
