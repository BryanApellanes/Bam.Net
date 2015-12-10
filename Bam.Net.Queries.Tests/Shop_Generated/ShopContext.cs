/*
	Copyright © Bryan Apellanes 2015  
*/
// model is SchemaDefinition
using System;
using System.Data;
using System.Data.Common;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.Data.Tests
{
	// schema = Shop 
    public static class ShopContext
    {
		public static string ConnectionName
		{
			get
			{
				return "Shop";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}

﻿
	public class CartQueryContext
	{
			public CartCollection Where(WhereDelegate<CartColumns> where, Database db = null)
			{
				return Bam.Net.Data.Tests.Cart.Where(where, db);
			}
		   
			public CartCollection Where(WhereDelegate<CartColumns> where, OrderBy<CartColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.Tests.Cart.Where(where, orderBy, db);
			}

			public Cart OneWhere(WhereDelegate<CartColumns> where, Database db = null)
			{
				return Bam.Net.Data.Tests.Cart.OneWhere(where, db);
			}
		
			public Cart FirstOneWhere(WhereDelegate<CartColumns> where, Database db = null)
			{
				return Bam.Net.Data.Tests.Cart.FirstOneWhere(where, db);
			}

			public CartCollection Top(int count, WhereDelegate<CartColumns> where, Database db = null)
			{
				return Bam.Net.Data.Tests.Cart.Top(count, where, db);
			}

			public CartCollection Top(int count, WhereDelegate<CartColumns> where, OrderBy<CartColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.Tests.Cart.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<CartColumns> where, Database db = null)
			{
				return Bam.Net.Data.Tests.Cart.Count(where, db);
			}
	}

	static CartQueryContext _carts;
	static object _cartsLock = new object();
	public static CartQueryContext Carts
	{
		get
		{
			return _cartsLock.DoubleCheckLock<CartQueryContext>(ref _carts, () => new CartQueryContext());
		}
	}﻿
	public class CartItemQueryContext
	{
			public CartItemCollection Where(WhereDelegate<CartItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.Tests.CartItem.Where(where, db);
			}
		   
			public CartItemCollection Where(WhereDelegate<CartItemColumns> where, OrderBy<CartItemColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.Tests.CartItem.Where(where, orderBy, db);
			}

			public CartItem OneWhere(WhereDelegate<CartItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.Tests.CartItem.OneWhere(where, db);
			}
		
			public CartItem FirstOneWhere(WhereDelegate<CartItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.Tests.CartItem.FirstOneWhere(where, db);
			}

			public CartItemCollection Top(int count, WhereDelegate<CartItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.Tests.CartItem.Top(count, where, db);
			}

			public CartItemCollection Top(int count, WhereDelegate<CartItemColumns> where, OrderBy<CartItemColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.Tests.CartItem.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<CartItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.Tests.CartItem.Count(where, db);
			}
	}

	static CartItemQueryContext _cartItems;
	static object _cartItemsLock = new object();
	public static CartItemQueryContext CartItems
	{
		get
		{
			return _cartItemsLock.DoubleCheckLock<CartItemQueryContext>(ref _cartItems, () => new CartItemQueryContext());
		}
	}﻿
	public class ListQueryContext
	{
			public ListCollection Where(WhereDelegate<ListColumns> where, Database db = null)
			{
				return Bam.Net.Data.Tests.List.Where(where, db);
			}
		   
			public ListCollection Where(WhereDelegate<ListColumns> where, OrderBy<ListColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.Tests.List.Where(where, orderBy, db);
			}

			public List OneWhere(WhereDelegate<ListColumns> where, Database db = null)
			{
				return Bam.Net.Data.Tests.List.OneWhere(where, db);
			}
		
			public List FirstOneWhere(WhereDelegate<ListColumns> where, Database db = null)
			{
				return Bam.Net.Data.Tests.List.FirstOneWhere(where, db);
			}

			public ListCollection Top(int count, WhereDelegate<ListColumns> where, Database db = null)
			{
				return Bam.Net.Data.Tests.List.Top(count, where, db);
			}

			public ListCollection Top(int count, WhereDelegate<ListColumns> where, OrderBy<ListColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.Tests.List.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ListColumns> where, Database db = null)
			{
				return Bam.Net.Data.Tests.List.Count(where, db);
			}
	}

	static ListQueryContext _lists;
	static object _listsLock = new object();
	public static ListQueryContext Lists
	{
		get
		{
			return _listsLock.DoubleCheckLock<ListQueryContext>(ref _lists, () => new ListQueryContext());
		}
	}﻿
	public class ItemQueryContext
	{
			public ItemCollection Where(WhereDelegate<ItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.Tests.Item.Where(where, db);
			}
		   
			public ItemCollection Where(WhereDelegate<ItemColumns> where, OrderBy<ItemColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.Tests.Item.Where(where, orderBy, db);
			}

			public Item OneWhere(WhereDelegate<ItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.Tests.Item.OneWhere(where, db);
			}
		
			public Item FirstOneWhere(WhereDelegate<ItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.Tests.Item.FirstOneWhere(where, db);
			}

			public ItemCollection Top(int count, WhereDelegate<ItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.Tests.Item.Top(count, where, db);
			}

			public ItemCollection Top(int count, WhereDelegate<ItemColumns> where, OrderBy<ItemColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.Tests.Item.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.Tests.Item.Count(where, db);
			}
	}

	static ItemQueryContext _items;
	static object _itemsLock = new object();
	public static ItemQueryContext Items
	{
		get
		{
			return _itemsLock.DoubleCheckLock<ItemQueryContext>(ref _items, () => new ItemQueryContext());
		}
	}﻿
	public class ListItemQueryContext
	{
			public ListItemCollection Where(WhereDelegate<ListItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.Tests.ListItem.Where(where, db);
			}
		   
			public ListItemCollection Where(WhereDelegate<ListItemColumns> where, OrderBy<ListItemColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.Tests.ListItem.Where(where, orderBy, db);
			}

			public ListItem OneWhere(WhereDelegate<ListItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.Tests.ListItem.OneWhere(where, db);
			}
		
			public ListItem FirstOneWhere(WhereDelegate<ListItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.Tests.ListItem.FirstOneWhere(where, db);
			}

			public ListItemCollection Top(int count, WhereDelegate<ListItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.Tests.ListItem.Top(count, where, db);
			}

			public ListItemCollection Top(int count, WhereDelegate<ListItemColumns> where, OrderBy<ListItemColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.Tests.ListItem.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ListItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.Tests.ListItem.Count(where, db);
			}
	}

	static ListItemQueryContext _listItems;
	static object _listItemsLock = new object();
	public static ListItemQueryContext ListItems
	{
		get
		{
			return _listItemsLock.DoubleCheckLock<ListItemQueryContext>(ref _listItems, () => new ListItemQueryContext());
		}
	}    }
}																								
