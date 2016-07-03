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

namespace Bam.Net.Shop
{
	// schema = Shop
	// connection Name = Shop
	[Serializable]
	[Bam.Net.Data.Table("ShopItemAttribute", "Shop")]
	public partial class ShopItemAttribute: Dao
	{
		public ShopItemAttribute():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ShopItemAttribute(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ShopItemAttribute(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ShopItemAttribute(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator ShopItemAttribute(DataRow data)
		{
			return new ShopItemAttribute(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("ShopItemAttributeValue_ShopItemAttributeId", new ShopItemAttributeValueCollection(Database.GetQuery<ShopItemAttributeValueColumns, ShopItemAttributeValue>((c) => c.ShopItemAttributeId == GetLongValue("Id")), this, "ShopItemAttributeId"));	
            this.ChildCollections.Add("ShopItemShopItemAttribute_ShopItemAttributeId", new ShopItemShopItemAttributeCollection(Database.GetQuery<ShopItemShopItemAttributeColumns, ShopItemShopItemAttribute>((c) => c.ShopItemAttributeId == GetLongValue("Id")), this, "ShopItemAttributeId"));							
            this.ChildCollections.Add("ShopItemAttribute_ShopItemShopItemAttribute_ShopItem",  new XrefDaoCollection<ShopItemShopItemAttribute, ShopItem>(this, false));
				
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



				

	[Exclude]	
	public ShopItemAttributeValueCollection ShopItemAttributeValuesByShopItemAttributeId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("ShopItemAttributeValue_ShopItemAttributeId"))
			{
				SetChildren();
			}

			var c = (ShopItemAttributeValueCollection)this.ChildCollections["ShopItemAttributeValue_ShopItemAttributeId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public ShopItemShopItemAttributeCollection ShopItemShopItemAttributesByShopItemAttributeId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("ShopItemShopItemAttribute_ShopItemAttributeId"))
			{
				SetChildren();
			}

			var c = (ShopItemShopItemAttributeCollection)this.ChildCollections["ShopItemShopItemAttribute_ShopItemAttributeId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			


		// Xref       
        public XrefDaoCollection<ShopItemShopItemAttribute, ShopItem> ShopItems
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("ShopItemAttribute_ShopItemShopItemAttribute_ShopItem"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<ShopItemShopItemAttribute, ShopItem>)this.ChildCollections["ShopItemAttribute_ShopItemShopItemAttribute_ShopItem"];
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
		public override IQueryFilter GetUniqueFilter()
		{
			if(UniqueFilterProvider != null)
			{
				return UniqueFilterProvider();
			}
			else
			{
				var colFilter = new ShopItemAttributeColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the ShopItemAttribute table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ShopItemAttributeCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<ShopItemAttribute>();
			Database db = database ?? Db.For<ShopItemAttribute>();
			var results = new ShopItemAttributeCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static async Task BatchAll(int batchSize, Func<ShopItemAttributeCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				ShopItemAttributeColumns columns = new ShopItemAttributeColumns();
				var orderBy = Order.By<ShopItemAttributeColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, (c) => c.KeyColumn > 0, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (c) => c.KeyColumn > topId, orderBy, database);
				}
			});			
		}	 

		public static async Task BatchQuery(int batchSize, QueryFilter filter, Func<ShopItemAttributeCollection, Task> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		public static async Task BatchQuery(int batchSize, WhereDelegate<ShopItemAttributeColumns> where, Func<ShopItemAttributeCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				ShopItemAttributeColumns columns = new ShopItemAttributeColumns();
				var orderBy = Order.By<ShopItemAttributeColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (ShopItemAttributeColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static ShopItemAttribute GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static ShopItemAttribute GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static ShopItemAttribute GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static ShopItemAttribute GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		public static ShopItemAttributeCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static ShopItemAttributeCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<ShopItemAttributeColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ShopItemAttributeColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ShopItemAttributeColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ShopItemAttributeCollection Where(Func<ShopItemAttributeColumns, QueryFilter<ShopItemAttributeColumns>> where, OrderBy<ShopItemAttributeColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<ShopItemAttribute>();
			return new ShopItemAttributeCollection(database.GetQuery<ShopItemAttributeColumns, ShopItemAttribute>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ShopItemAttributeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ShopItemAttributeColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ShopItemAttributeCollection Where(WhereDelegate<ShopItemAttributeColumns> where, Database database = null)
		{		
			database = database ?? Db.For<ShopItemAttribute>();
			var results = new ShopItemAttributeCollection(database, database.GetQuery<ShopItemAttributeColumns, ShopItemAttribute>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ShopItemAttributeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ShopItemAttributeColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static ShopItemAttributeCollection Where(WhereDelegate<ShopItemAttributeColumns> where, OrderBy<ShopItemAttributeColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<ShopItemAttribute>();
			var results = new ShopItemAttributeCollection(database, database.GetQuery<ShopItemAttributeColumns, ShopItemAttribute>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;ShopItemAttributeColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ShopItemAttributeCollection Where(QiQuery where, Database database = null)
		{
			var results = new ShopItemAttributeCollection(database, Select<ShopItemAttributeColumns>.From<ShopItemAttribute>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static ShopItemAttribute GetOneWhere(QueryFilter where, Database database = null)
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
		public static ShopItemAttribute OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<ShopItemAttributeColumns> whereDelegate = (c) => where;
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
		public static ShopItemAttribute GetOneWhere(WhereDelegate<ShopItemAttributeColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				ShopItemAttributeColumns c = new ShopItemAttributeColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ShopItemAttribute instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ShopItemAttributeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ShopItemAttributeColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static ShopItemAttribute OneWhere(WhereDelegate<ShopItemAttributeColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ShopItemAttributeColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ShopItemAttribute OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ShopItemAttributeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ShopItemAttributeColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static ShopItemAttribute FirstOneWhere(WhereDelegate<ShopItemAttributeColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ShopItemAttributeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ShopItemAttributeColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static ShopItemAttribute FirstOneWhere(WhereDelegate<ShopItemAttributeColumns> where, OrderBy<ShopItemAttributeColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ShopItemAttributeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ShopItemAttributeColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static ShopItemAttribute FirstOneWhere(QueryFilter where, OrderBy<ShopItemAttributeColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<ShopItemAttributeColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a ShopItemAttributeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ShopItemAttributeColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static ShopItemAttributeCollection Top(int count, WhereDelegate<ShopItemAttributeColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ShopItemAttributeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ShopItemAttributeColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static ShopItemAttributeCollection Top(int count, WhereDelegate<ShopItemAttributeColumns> where, OrderBy<ShopItemAttributeColumns> orderBy, Database database = null)
		{
			ShopItemAttributeColumns c = new ShopItemAttributeColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<ShopItemAttribute>();
			QuerySet query = GetQuerySet(db); 
			query.Top<ShopItemAttribute>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ShopItemAttributeColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ShopItemAttributeCollection>(0);
			results.Database = db;
			return results;
		}

		public static ShopItemAttributeCollection Top(int count, QueryFilter where, Database database)
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
		public static ShopItemAttributeCollection Top(int count, QueryFilter where, OrderBy<ShopItemAttributeColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<ShopItemAttribute>();
			QuerySet query = GetQuerySet(db);
			query.Top<ShopItemAttribute>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<ShopItemAttributeColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ShopItemAttributeCollection>(0);
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
		public static ShopItemAttributeCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<ShopItemAttribute>();
			QuerySet query = GetQuerySet(db);
			query.Top<ShopItemAttribute>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<ShopItemAttributeCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ShopItemAttributeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ShopItemAttributeColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<ShopItemAttributeColumns> where, Database database = null)
		{
			ShopItemAttributeColumns c = new ShopItemAttributeColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<ShopItemAttribute>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<ShopItemAttribute>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static ShopItemAttribute CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<ShopItemAttribute>();			
			var dao = new ShopItemAttribute();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static ShopItemAttribute OneOrThrow(ShopItemAttributeCollection c)
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
