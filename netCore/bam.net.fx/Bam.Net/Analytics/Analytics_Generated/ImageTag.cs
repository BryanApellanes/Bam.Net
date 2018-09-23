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
	[Bam.Net.Data.Table("ImageTag", "Analytics")]
	public partial class ImageTag: Bam.Net.Data.Dao
	{
		public ImageTag():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ImageTag(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ImageTag(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ImageTag(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator ImageTag(DataRow data)
		{
			return new ImageTag(data);
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



	// start ImageId -> ImageId
	[Bam.Net.Data.ForeignKey(
        Table="ImageTag",
		Name="ImageId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Image",
		Suffix="1")]
	public ulong? ImageId
	{
		get
		{
			return GetULongValue("ImageId");
		}
		set
		{
			SetValue("ImageId", value);
		}
	}

	Image _imageOfImageId;
	public Image ImageOfImageId
	{
		get
		{
			if(_imageOfImageId == null)
			{
				_imageOfImageId = Bam.Net.Analytics.Image.OneWhere(c => c.KeyColumn == this.ImageId, this.Database);
			}
			return _imageOfImageId;
		}
	}
	
	// start TagId -> TagId
	[Bam.Net.Data.ForeignKey(
        Table="ImageTag",
		Name="TagId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Tag",
		Suffix="2")]
	public ulong? TagId
	{
		get
		{
			return GetULongValue("TagId");
		}
		set
		{
			SetValue("TagId", value);
		}
	}

	Tag _tagOfTagId;
	public Tag TagOfTagId
	{
		get
		{
			if(_tagOfTagId == null)
			{
				_tagOfTagId = Bam.Net.Analytics.Tag.OneWhere(c => c.KeyColumn == this.TagId, this.Database);
			}
			return _tagOfTagId;
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
				var colFilter = new ImageTagColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the ImageTag table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ImageTagCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<ImageTag>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<ImageTag>();
			var results = new ImageTagCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<ImageTag>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ImageTagColumns columns = new ImageTagColumns();
				var orderBy = Bam.Net.Data.Order.By<ImageTagColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<ImageTag>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<ImageTagColumns> where, Action<IEnumerable<ImageTag>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ImageTagColumns columns = new ImageTagColumns();
				var orderBy = Bam.Net.Data.Order.By<ImageTagColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (ImageTagColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<ImageTag>> batchProcessor, Bam.Net.Data.OrderBy<ImageTagColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<ImageTagColumns> where, Action<IEnumerable<ImageTag>> batchProcessor, Bam.Net.Data.OrderBy<ImageTagColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ImageTagColumns columns = new ImageTagColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (ImageTagColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static ImageTag GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static ImageTag GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static ImageTag GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static ImageTag GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static ImageTag GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static ImageTag GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static ImageTagCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static ImageTagCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<ImageTagColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ImageTagColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ImageTagColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ImageTagCollection Where(Func<ImageTagColumns, QueryFilter<ImageTagColumns>> where, OrderBy<ImageTagColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<ImageTag>();
			return new ImageTagCollection(database.GetQuery<ImageTagColumns, ImageTag>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ImageTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageTagColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ImageTagCollection Where(WhereDelegate<ImageTagColumns> where, Database database = null)
		{		
			database = database ?? Db.For<ImageTag>();
			var results = new ImageTagCollection(database, database.GetQuery<ImageTagColumns, ImageTag>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ImageTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageTagColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ImageTagCollection Where(WhereDelegate<ImageTagColumns> where, OrderBy<ImageTagColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<ImageTag>();
			var results = new ImageTagCollection(database, database.GetQuery<ImageTagColumns, ImageTag>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;ImageTagColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ImageTagCollection Where(QiQuery where, Database database = null)
		{
			var results = new ImageTagCollection(database, Select<ImageTagColumns>.From<ImageTag>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static ImageTag GetOneWhere(QueryFilter where, Database database = null)
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
		public static ImageTag OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<ImageTagColumns> whereDelegate = (c) => where;
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
		public static ImageTag GetOneWhere(WhereDelegate<ImageTagColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				ImageTagColumns c = new ImageTagColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ImageTag instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ImageTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageTagColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ImageTag OneWhere(WhereDelegate<ImageTagColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ImageTagColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ImageTag OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ImageTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageTagColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ImageTag FirstOneWhere(WhereDelegate<ImageTagColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ImageTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageTagColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ImageTag FirstOneWhere(WhereDelegate<ImageTagColumns> where, OrderBy<ImageTagColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ImageTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageTagColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ImageTag FirstOneWhere(QueryFilter where, OrderBy<ImageTagColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<ImageTagColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a ImageTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageTagColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ImageTagCollection Top(int count, WhereDelegate<ImageTagColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ImageTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageTagColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static ImageTagCollection Top(int count, WhereDelegate<ImageTagColumns> where, OrderBy<ImageTagColumns> orderBy, Database database = null)
		{
			ImageTagColumns c = new ImageTagColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<ImageTag>();
			QuerySet query = GetQuerySet(db); 
			query.Top<ImageTag>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ImageTagColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ImageTagCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ImageTagCollection Top(int count, QueryFilter where, Database database)
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
		public static ImageTagCollection Top(int count, QueryFilter where, OrderBy<ImageTagColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<ImageTag>();
			QuerySet query = GetQuerySet(db);
			query.Top<ImageTag>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<ImageTagColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ImageTagCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ImageTagCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<ImageTag>();
			QuerySet query = GetQuerySet(db);
			query.Top<ImageTag>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<ImageTagCollection>(0);
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
		public static ImageTagCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<ImageTag>();
			QuerySet query = GetQuerySet(db);
			query.Top<ImageTag>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<ImageTagCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of ImageTags
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<ImageTag>();
            QuerySet query = GetQuerySet(db);
            query.Count<ImageTag>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ImageTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageTagColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<ImageTagColumns> where, Database database = null)
		{
			ImageTagColumns c = new ImageTagColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<ImageTag>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<ImageTag>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<ImageTag>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<ImageTag>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static ImageTag CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<ImageTag>();			
			var dao = new ImageTag();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static ImageTag OneOrThrow(ImageTagCollection c)
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
