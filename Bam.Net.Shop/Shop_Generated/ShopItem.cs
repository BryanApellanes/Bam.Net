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

namespace Bam.Net.Shop
{
	// schema = Shop
	// connection Name = Shop
	[Serializable]
	[Bam.Net.Data.Table("ShopItem", "Shop")]
	public partial class ShopItem: Dao
	{
		public ShopItem():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ShopItem(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ShopItem(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ShopItem(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator ShopItem(DataRow data)
		{
			return new ShopItem(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("ShoppingCartItem_ShopItemId", new ShoppingCartItemCollection(Database.GetQuery<ShoppingCartItemColumns, ShoppingCartItem>((c) => c.ShopItemId == GetLongValue("Id")), this, "ShopItemId"));	
            this.ChildCollections.Add("Price_ShopItemId", new PriceCollection(Database.GetQuery<PriceColumns, Price>((c) => c.ShopItemId == GetLongValue("Id")), this, "ShopItemId"));	
            this.ChildCollections.Add("ShoppingListShopItem_ShopItemId", new ShoppingListShopItemCollection(Database.GetQuery<ShoppingListShopItemColumns, ShoppingListShopItem>((c) => c.ShopItemId == GetLongValue("Id")), this, "ShopItemId"));	
            this.ChildCollections.Add("ShopShopItem_ShopItemId", new ShopShopItemCollection(Database.GetQuery<ShopShopItemColumns, ShopShopItem>((c) => c.ShopItemId == GetLongValue("Id")), this, "ShopItemId"));	
            this.ChildCollections.Add("ShopItemShopItemAttribute_ShopItemId", new ShopItemShopItemAttributeCollection(Database.GetQuery<ShopItemShopItemAttributeColumns, ShopItemShopItemAttribute>((c) => c.ShopItemId == GetLongValue("Id")), this, "ShopItemId"));	
            this.ChildCollections.Add("ShopItemPromotion_ShopItemId", new ShopItemPromotionCollection(Database.GetQuery<ShopItemPromotionColumns, ShopItemPromotion>((c) => c.ShopItemId == GetLongValue("Id")), this, "ShopItemId"));				
            this.ChildCollections.Add("ShopItem_ShopItemShopItemAttribute_ShopItemAttribute",  new XrefDaoCollection<ShopItemShopItemAttribute, ShopItemAttribute>(this, false));
				
            this.ChildCollections.Add("ShopItem_ShopItemPromotion_Promotion",  new XrefDaoCollection<ShopItemPromotion, Promotion>(this, false));
							
            this.ChildCollections.Add("ShopItem_ShoppingListShopItem_ShoppingList",  new XrefDaoCollection<ShoppingListShopItem, ShoppingList>(this, false));
				
            this.ChildCollections.Add("ShopItem_ShopShopItem_Shop",  new XrefDaoCollection<ShopShopItem, Shop>(this, false));
				
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

	// property:Source, columnName:Source	
	[Bam.Net.Data.Column(Name="Source", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Source
	{
		get
		{
			return GetStringValue("Source");
		}
		set
		{
			SetValue("Source", value);
		}
	}

	// property:SourceId, columnName:SourceId	
	[Bam.Net.Data.Column(Name="SourceId", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string SourceId
	{
		get
		{
			return GetStringValue("SourceId");
		}
		set
		{
			SetValue("SourceId", value);
		}
	}

	// property:DetailUrl, columnName:DetailUrl	
	[Bam.Net.Data.Column(Name="DetailUrl", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string DetailUrl
	{
		get
		{
			return GetStringValue("DetailUrl");
		}
		set
		{
			SetValue("DetailUrl", value);
		}
	}

	// property:ImageSrc, columnName:ImageSrc	
	[Bam.Net.Data.Column(Name="ImageSrc", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string ImageSrc
	{
		get
		{
			return GetStringValue("ImageSrc");
		}
		set
		{
			SetValue("ImageSrc", value);
		}
	}



				

	[Bam.Net.Exclude]	
	public ShoppingCartItemCollection ShoppingCartItemsByShopItemId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("ShoppingCartItem_ShopItemId"))
			{
				SetChildren();
			}

			var c = (ShoppingCartItemCollection)this.ChildCollections["ShoppingCartItem_ShopItemId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Bam.Net.Exclude]	
	public PriceCollection PricesByShopItemId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("Price_ShopItemId"))
			{
				SetChildren();
			}

			var c = (PriceCollection)this.ChildCollections["Price_ShopItemId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Bam.Net.Exclude]	
	public ShoppingListShopItemCollection ShoppingListShopItemsByShopItemId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("ShoppingListShopItem_ShopItemId"))
			{
				SetChildren();
			}

			var c = (ShoppingListShopItemCollection)this.ChildCollections["ShoppingListShopItem_ShopItemId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Bam.Net.Exclude]	
	public ShopShopItemCollection ShopShopItemsByShopItemId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("ShopShopItem_ShopItemId"))
			{
				SetChildren();
			}

			var c = (ShopShopItemCollection)this.ChildCollections["ShopShopItem_ShopItemId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Bam.Net.Exclude]	
	public ShopItemShopItemAttributeCollection ShopItemShopItemAttributesByShopItemId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("ShopItemShopItemAttribute_ShopItemId"))
			{
				SetChildren();
			}

			var c = (ShopItemShopItemAttributeCollection)this.ChildCollections["ShopItemShopItemAttribute_ShopItemId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Bam.Net.Exclude]	
	public ShopItemPromotionCollection ShopItemPromotionsByShopItemId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("ShopItemPromotion_ShopItemId"))
			{
				SetChildren();
			}

			var c = (ShopItemPromotionCollection)this.ChildCollections["ShopItemPromotion_ShopItemId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			

		// Xref       
        public XrefDaoCollection<ShopItemShopItemAttribute, ShopItemAttribute> ShopItemAttributes
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("ShopItem_ShopItemShopItemAttribute_ShopItemAttribute"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<ShopItemShopItemAttribute, ShopItemAttribute>)this.ChildCollections["ShopItem_ShopItemShopItemAttribute_ShopItemAttribute"];
				if(!xref.Loaded)
				{
					xref.Load(Database);
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<ShopItemPromotion, Promotion> Promotions
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("ShopItem_ShopItemPromotion_Promotion"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<ShopItemPromotion, Promotion>)this.ChildCollections["ShopItem_ShopItemPromotion_Promotion"];
				if(!xref.Loaded)
				{
					xref.Load(Database);
				}

				return xref;
            }
        }

		// Xref       
        public XrefDaoCollection<ShoppingListShopItem, ShoppingList> ShoppingLists
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("ShopItem_ShoppingListShopItem_ShoppingList"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<ShoppingListShopItem, ShoppingList>)this.ChildCollections["ShopItem_ShoppingListShopItem_ShoppingList"];
				if(!xref.Loaded)
				{
					xref.Load(Database);
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<ShopShopItem, Shop> Shops
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("ShopItem_ShopShopItem_Shop"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<ShopShopItem, Shop>)this.ChildCollections["ShopItem_ShopShopItem_Shop"];
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
				return UniqueFilterProvider();
			}
			else
			{
				var colFilter = new ShopItemColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the ShopItem table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ShopItemCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<ShopItem>();
			Database db = database ?? Db.For<ShopItem>();
			var results = new ShopItemCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<ShopItem>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				ShopItemColumns columns = new ShopItemColumns();
				var orderBy = Bam.Net.Data.Order.By<ShopItemColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<ShopItem>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<ShopItemColumns> where, Action<IEnumerable<ShopItem>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				ShopItemColumns columns = new ShopItemColumns();
				var orderBy = Bam.Net.Data.Order.By<ShopItemColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (ShopItemColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static ShopItem GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static ShopItem GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static ShopItem GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static ShopItem GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static ShopItemCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static ShopItemCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<ShopItemColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ShopItemColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ShopItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ShopItemCollection Where(Func<ShopItemColumns, QueryFilter<ShopItemColumns>> where, OrderBy<ShopItemColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<ShopItem>();
			return new ShopItemCollection(database.GetQuery<ShopItemColumns, ShopItem>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ShopItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ShopItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ShopItemCollection Where(WhereDelegate<ShopItemColumns> where, Database database = null)
		{		
			database = database ?? Db.For<ShopItem>();
			var results = new ShopItemCollection(database, database.GetQuery<ShopItemColumns, ShopItem>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ShopItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ShopItemColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ShopItemCollection Where(WhereDelegate<ShopItemColumns> where, OrderBy<ShopItemColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<ShopItem>();
			var results = new ShopItemCollection(database, database.GetQuery<ShopItemColumns, ShopItem>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;ShopItemColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ShopItemCollection Where(QiQuery where, Database database = null)
		{
			var results = new ShopItemCollection(database, Select<ShopItemColumns>.From<ShopItem>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static ShopItem GetOneWhere(QueryFilter where, Database database = null)
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
		public static ShopItem OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<ShopItemColumns> whereDelegate = (c) => where;
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
		public static ShopItem GetOneWhere(WhereDelegate<ShopItemColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				ShopItemColumns c = new ShopItemColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ShopItem instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ShopItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ShopItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ShopItem OneWhere(WhereDelegate<ShopItemColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ShopItemColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ShopItem OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ShopItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ShopItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ShopItem FirstOneWhere(WhereDelegate<ShopItemColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ShopItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ShopItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ShopItem FirstOneWhere(WhereDelegate<ShopItemColumns> where, OrderBy<ShopItemColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ShopItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ShopItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ShopItem FirstOneWhere(QueryFilter where, OrderBy<ShopItemColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<ShopItemColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a ShopItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ShopItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ShopItemCollection Top(int count, WhereDelegate<ShopItemColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ShopItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ShopItemColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ShopItemCollection Top(int count, WhereDelegate<ShopItemColumns> where, OrderBy<ShopItemColumns> orderBy, Database database = null)
		{
			ShopItemColumns c = new ShopItemColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<ShopItem>();
			QuerySet query = GetQuerySet(db); 
			query.Top<ShopItem>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ShopItemColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ShopItemCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ShopItemCollection Top(int count, QueryFilter where, Database database)
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
		[Bam.Net.Exclude]
		public static ShopItemCollection Top(int count, QueryFilter where, OrderBy<ShopItemColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<ShopItem>();
			QuerySet query = GetQuerySet(db);
			query.Top<ShopItem>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<ShopItemColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ShopItemCollection>(0);
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
		public static ShopItemCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<ShopItem>();
			QuerySet query = GetQuerySet(db);
			query.Top<ShopItem>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<ShopItemCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of ShopItems
		/// </summary>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<ShopItem>();
            QuerySet query = GetQuerySet(db);
            query.Count<ShopItem>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ShopItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ShopItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<ShopItemColumns> where, Database database = null)
		{
			ShopItemColumns c = new ShopItemColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<ShopItem>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<ShopItem>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<ShopItem>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<ShopItem>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static ShopItem CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<ShopItem>();			
			var dao = new ShopItem();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static ShopItem OneOrThrow(ShopItemCollection c)
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
