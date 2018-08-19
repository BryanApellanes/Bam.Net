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
	[Bam.Net.Data.Table("Tag", "Analytics")]
	public partial class Tag: Bam.Net.Data.Dao
	{
		public Tag():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Tag(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Tag(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Tag(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator Tag(DataRow data)
		{
			return new Tag(data);
		}

		private void SetChildren()
		{

			if(_database != null)
			{
				this.ChildCollections.Add("UrlTag_TagId", new UrlTagCollection(Database.GetQuery<UrlTagColumns, UrlTag>((c) => c.TagId == GetULongValue("Id")), this, "TagId"));				
			}
			if(_database != null)
			{
				this.ChildCollections.Add("ImageTag_TagId", new ImageTagCollection(Database.GetQuery<ImageTagColumns, ImageTag>((c) => c.TagId == GetULongValue("Id")), this, "TagId"));				
			}						
            this.ChildCollections.Add("Tag_UrlTag_Url",  new XrefDaoCollection<UrlTag, Url>(this, false));
				
            this.ChildCollections.Add("Tag_ImageTag_Image",  new XrefDaoCollection<ImageTag, Image>(this, false));
				
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

	// property:Value, columnName:Value	
	[Bam.Net.Data.Column(Name="Value", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
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
	public UrlTagCollection UrlTagsByTagId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("UrlTag_TagId"))
			{
				SetChildren();
			}

			var c = (UrlTagCollection)this.ChildCollections["UrlTag_TagId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Bam.Net.Exclude]	
	public ImageTagCollection ImageTagsByTagId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("ImageTag_TagId"))
			{
				SetChildren();
			}

			var c = (ImageTagCollection)this.ChildCollections["ImageTag_TagId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			


		// Xref       
        public XrefDaoCollection<UrlTag, Url> Urls
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("Tag_UrlTag_Url"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<UrlTag, Url>)this.ChildCollections["Tag_UrlTag_Url"];
				if(!xref.Loaded)
				{
					xref.Load(Database);
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<ImageTag, Image> Images
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("Tag_ImageTag_Image"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<ImageTag, Image>)this.ChildCollections["Tag_ImageTag_Image"];
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
				var colFilter = new TagColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the Tag table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static TagCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<Tag>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<Tag>();
			var results = new TagCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<Tag>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				TagColumns columns = new TagColumns();
				var orderBy = Bam.Net.Data.Order.By<TagColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<Tag>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<TagColumns> where, Action<IEnumerable<Tag>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				TagColumns columns = new TagColumns();
				var orderBy = Bam.Net.Data.Order.By<TagColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (TagColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<Tag>> batchProcessor, Bam.Net.Data.OrderBy<TagColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<TagColumns> where, Action<IEnumerable<Tag>> batchProcessor, Bam.Net.Data.OrderBy<TagColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				TagColumns columns = new TagColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (TagColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static Tag GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static Tag GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Tag GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Tag GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Tag GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static Tag GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static TagCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static TagCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<TagColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a TagColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between TagColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static TagCollection Where(Func<TagColumns, QueryFilter<TagColumns>> where, OrderBy<TagColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<Tag>();
			return new TagCollection(database.GetQuery<TagColumns, Tag>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TagColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static TagCollection Where(WhereDelegate<TagColumns> where, Database database = null)
		{		
			database = database ?? Db.For<Tag>();
			var results = new TagCollection(database, database.GetQuery<TagColumns, Tag>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TagColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static TagCollection Where(WhereDelegate<TagColumns> where, OrderBy<TagColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<Tag>();
			var results = new TagCollection(database, database.GetQuery<TagColumns, Tag>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;TagColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static TagCollection Where(QiQuery where, Database database = null)
		{
			var results = new TagCollection(database, Select<TagColumns>.From<Tag>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static Tag GetOneWhere(QueryFilter where, Database database = null)
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
		public static Tag OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<TagColumns> whereDelegate = (c) => where;
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
		public static Tag GetOneWhere(WhereDelegate<TagColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				TagColumns c = new TagColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Tag instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TagColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Tag OneWhere(WhereDelegate<TagColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<TagColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Tag OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TagColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Tag FirstOneWhere(WhereDelegate<TagColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a TagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TagColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Tag FirstOneWhere(WhereDelegate<TagColumns> where, OrderBy<TagColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a TagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TagColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Tag FirstOneWhere(QueryFilter where, OrderBy<TagColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<TagColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a TagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TagColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static TagCollection Top(int count, WhereDelegate<TagColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a TagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TagColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static TagCollection Top(int count, WhereDelegate<TagColumns> where, OrderBy<TagColumns> orderBy, Database database = null)
		{
			TagColumns c = new TagColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<Tag>();
			QuerySet query = GetQuerySet(db); 
			query.Top<Tag>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<TagColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<TagCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static TagCollection Top(int count, QueryFilter where, Database database)
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
		public static TagCollection Top(int count, QueryFilter where, OrderBy<TagColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<Tag>();
			QuerySet query = GetQuerySet(db);
			query.Top<Tag>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<TagColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<TagCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static TagCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<Tag>();
			QuerySet query = GetQuerySet(db);
			query.Top<Tag>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<TagCollection>(0);
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
		public static TagCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<Tag>();
			QuerySet query = GetQuerySet(db);
			query.Top<Tag>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<TagCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of Tags
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<Tag>();
            QuerySet query = GetQuerySet(db);
            query.Count<Tag>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TagColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<TagColumns> where, Database database = null)
		{
			TagColumns c = new TagColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<Tag>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Tag>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<Tag>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Tag>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static Tag CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<Tag>();			
			var dao = new Tag();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static Tag OneOrThrow(TagCollection c)
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
