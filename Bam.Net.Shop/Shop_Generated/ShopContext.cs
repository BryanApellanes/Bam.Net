/*
	This file was generated and should not be modified directly
*/
// model is SchemaDefinition
using System;
using System.Data;
using System.Data.Common;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.Shop
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


	public class CurrencyQueryContext
	{
			public CurrencyCollection Where(WhereDelegate<CurrencyColumns> where, Database db = null)
			{
				return Bam.Net.Shop.Currency.Where(where, db);
			}
		   
			public CurrencyCollection Where(WhereDelegate<CurrencyColumns> where, OrderBy<CurrencyColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Shop.Currency.Where(where, orderBy, db);
			}

			public Currency OneWhere(WhereDelegate<CurrencyColumns> where, Database db = null)
			{
				return Bam.Net.Shop.Currency.OneWhere(where, db);
			}
		
			public Currency FirstOneWhere(WhereDelegate<CurrencyColumns> where, Database db = null)
			{
				return Bam.Net.Shop.Currency.FirstOneWhere(where, db);
			}

			public CurrencyCollection Top(int count, WhereDelegate<CurrencyColumns> where, Database db = null)
			{
				return Bam.Net.Shop.Currency.Top(count, where, db);
			}

			public CurrencyCollection Top(int count, WhereDelegate<CurrencyColumns> where, OrderBy<CurrencyColumns> orderBy, Database db = null)
			{
				return Bam.Net.Shop.Currency.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<CurrencyColumns> where, Database db = null)
			{
				return Bam.Net.Shop.Currency.Count(where, db);
			}
	}

	static CurrencyQueryContext _currencies;
	static object _currenciesLock = new object();
	public static CurrencyQueryContext Currencies
	{
		get
		{
			return _currenciesLock.DoubleCheckLock<CurrencyQueryContext>(ref _currencies, () => new CurrencyQueryContext());
		}
	}
	public class CurrencyCountryQueryContext
	{
			public CurrencyCountryCollection Where(WhereDelegate<CurrencyCountryColumns> where, Database db = null)
			{
				return Bam.Net.Shop.CurrencyCountry.Where(where, db);
			}
		   
			public CurrencyCountryCollection Where(WhereDelegate<CurrencyCountryColumns> where, OrderBy<CurrencyCountryColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Shop.CurrencyCountry.Where(where, orderBy, db);
			}

			public CurrencyCountry OneWhere(WhereDelegate<CurrencyCountryColumns> where, Database db = null)
			{
				return Bam.Net.Shop.CurrencyCountry.OneWhere(where, db);
			}
		
			public CurrencyCountry FirstOneWhere(WhereDelegate<CurrencyCountryColumns> where, Database db = null)
			{
				return Bam.Net.Shop.CurrencyCountry.FirstOneWhere(where, db);
			}

			public CurrencyCountryCollection Top(int count, WhereDelegate<CurrencyCountryColumns> where, Database db = null)
			{
				return Bam.Net.Shop.CurrencyCountry.Top(count, where, db);
			}

			public CurrencyCountryCollection Top(int count, WhereDelegate<CurrencyCountryColumns> where, OrderBy<CurrencyCountryColumns> orderBy, Database db = null)
			{
				return Bam.Net.Shop.CurrencyCountry.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<CurrencyCountryColumns> where, Database db = null)
			{
				return Bam.Net.Shop.CurrencyCountry.Count(where, db);
			}
	}

	static CurrencyCountryQueryContext _currencyCountries;
	static object _currencyCountriesLock = new object();
	public static CurrencyCountryQueryContext CurrencyCountries
	{
		get
		{
			return _currencyCountriesLock.DoubleCheckLock<CurrencyCountryQueryContext>(ref _currencyCountries, () => new CurrencyCountryQueryContext());
		}
	}
	public class ShopQueryContext
	{
			public ShopCollection Where(WhereDelegate<ShopColumns> where, Database db = null)
			{
				return Bam.Net.Shop.Shop.Where(where, db);
			}
		   
			public ShopCollection Where(WhereDelegate<ShopColumns> where, OrderBy<ShopColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Shop.Shop.Where(where, orderBy, db);
			}

			public Shop OneWhere(WhereDelegate<ShopColumns> where, Database db = null)
			{
				return Bam.Net.Shop.Shop.OneWhere(where, db);
			}
		
			public Shop FirstOneWhere(WhereDelegate<ShopColumns> where, Database db = null)
			{
				return Bam.Net.Shop.Shop.FirstOneWhere(where, db);
			}

			public ShopCollection Top(int count, WhereDelegate<ShopColumns> where, Database db = null)
			{
				return Bam.Net.Shop.Shop.Top(count, where, db);
			}

			public ShopCollection Top(int count, WhereDelegate<ShopColumns> where, OrderBy<ShopColumns> orderBy, Database db = null)
			{
				return Bam.Net.Shop.Shop.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ShopColumns> where, Database db = null)
			{
				return Bam.Net.Shop.Shop.Count(where, db);
			}
	}

	static ShopQueryContext _shops;
	static object _shopsLock = new object();
	public static ShopQueryContext Shops
	{
		get
		{
			return _shopsLock.DoubleCheckLock<ShopQueryContext>(ref _shops, () => new ShopQueryContext());
		}
	}
	public class PromotionEffectsQueryContext
	{
			public PromotionEffectsCollection Where(WhereDelegate<PromotionEffectsColumns> where, Database db = null)
			{
				return Bam.Net.Shop.PromotionEffects.Where(where, db);
			}
		   
			public PromotionEffectsCollection Where(WhereDelegate<PromotionEffectsColumns> where, OrderBy<PromotionEffectsColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Shop.PromotionEffects.Where(where, orderBy, db);
			}

			public PromotionEffects OneWhere(WhereDelegate<PromotionEffectsColumns> where, Database db = null)
			{
				return Bam.Net.Shop.PromotionEffects.OneWhere(where, db);
			}
		
			public PromotionEffects FirstOneWhere(WhereDelegate<PromotionEffectsColumns> where, Database db = null)
			{
				return Bam.Net.Shop.PromotionEffects.FirstOneWhere(where, db);
			}

			public PromotionEffectsCollection Top(int count, WhereDelegate<PromotionEffectsColumns> where, Database db = null)
			{
				return Bam.Net.Shop.PromotionEffects.Top(count, where, db);
			}

			public PromotionEffectsCollection Top(int count, WhereDelegate<PromotionEffectsColumns> where, OrderBy<PromotionEffectsColumns> orderBy, Database db = null)
			{
				return Bam.Net.Shop.PromotionEffects.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PromotionEffectsColumns> where, Database db = null)
			{
				return Bam.Net.Shop.PromotionEffects.Count(where, db);
			}
	}

	static PromotionEffectsQueryContext _promotionEffectses;
	static object _promotionEffectsesLock = new object();
	public static PromotionEffectsQueryContext PromotionEffectses
	{
		get
		{
			return _promotionEffectsesLock.DoubleCheckLock<PromotionEffectsQueryContext>(ref _promotionEffectses, () => new PromotionEffectsQueryContext());
		}
	}
	public class PromotionQueryContext
	{
			public PromotionCollection Where(WhereDelegate<PromotionColumns> where, Database db = null)
			{
				return Bam.Net.Shop.Promotion.Where(where, db);
			}
		   
			public PromotionCollection Where(WhereDelegate<PromotionColumns> where, OrderBy<PromotionColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Shop.Promotion.Where(where, orderBy, db);
			}

			public Promotion OneWhere(WhereDelegate<PromotionColumns> where, Database db = null)
			{
				return Bam.Net.Shop.Promotion.OneWhere(where, db);
			}
		
			public Promotion FirstOneWhere(WhereDelegate<PromotionColumns> where, Database db = null)
			{
				return Bam.Net.Shop.Promotion.FirstOneWhere(where, db);
			}

			public PromotionCollection Top(int count, WhereDelegate<PromotionColumns> where, Database db = null)
			{
				return Bam.Net.Shop.Promotion.Top(count, where, db);
			}

			public PromotionCollection Top(int count, WhereDelegate<PromotionColumns> where, OrderBy<PromotionColumns> orderBy, Database db = null)
			{
				return Bam.Net.Shop.Promotion.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PromotionColumns> where, Database db = null)
			{
				return Bam.Net.Shop.Promotion.Count(where, db);
			}
	}

	static PromotionQueryContext _promotions;
	static object _promotionsLock = new object();
	public static PromotionQueryContext Promotions
	{
		get
		{
			return _promotionsLock.DoubleCheckLock<PromotionQueryContext>(ref _promotions, () => new PromotionQueryContext());
		}
	}
	public class PromotionEffectQueryContext
	{
			public PromotionEffectCollection Where(WhereDelegate<PromotionEffectColumns> where, Database db = null)
			{
				return Bam.Net.Shop.PromotionEffect.Where(where, db);
			}
		   
			public PromotionEffectCollection Where(WhereDelegate<PromotionEffectColumns> where, OrderBy<PromotionEffectColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Shop.PromotionEffect.Where(where, orderBy, db);
			}

			public PromotionEffect OneWhere(WhereDelegate<PromotionEffectColumns> where, Database db = null)
			{
				return Bam.Net.Shop.PromotionEffect.OneWhere(where, db);
			}
		
			public PromotionEffect FirstOneWhere(WhereDelegate<PromotionEffectColumns> where, Database db = null)
			{
				return Bam.Net.Shop.PromotionEffect.FirstOneWhere(where, db);
			}

			public PromotionEffectCollection Top(int count, WhereDelegate<PromotionEffectColumns> where, Database db = null)
			{
				return Bam.Net.Shop.PromotionEffect.Top(count, where, db);
			}

			public PromotionEffectCollection Top(int count, WhereDelegate<PromotionEffectColumns> where, OrderBy<PromotionEffectColumns> orderBy, Database db = null)
			{
				return Bam.Net.Shop.PromotionEffect.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PromotionEffectColumns> where, Database db = null)
			{
				return Bam.Net.Shop.PromotionEffect.Count(where, db);
			}
	}

	static PromotionEffectQueryContext _promotionEffects;
	static object _promotionEffectsLock = new object();
	public static PromotionEffectQueryContext PromotionEffects
	{
		get
		{
			return _promotionEffectsLock.DoubleCheckLock<PromotionEffectQueryContext>(ref _promotionEffects, () => new PromotionEffectQueryContext());
		}
	}
	public class PromotionConditionQueryContext
	{
			public PromotionConditionCollection Where(WhereDelegate<PromotionConditionColumns> where, Database db = null)
			{
				return Bam.Net.Shop.PromotionCondition.Where(where, db);
			}
		   
			public PromotionConditionCollection Where(WhereDelegate<PromotionConditionColumns> where, OrderBy<PromotionConditionColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Shop.PromotionCondition.Where(where, orderBy, db);
			}

			public PromotionCondition OneWhere(WhereDelegate<PromotionConditionColumns> where, Database db = null)
			{
				return Bam.Net.Shop.PromotionCondition.OneWhere(where, db);
			}
		
			public PromotionCondition FirstOneWhere(WhereDelegate<PromotionConditionColumns> where, Database db = null)
			{
				return Bam.Net.Shop.PromotionCondition.FirstOneWhere(where, db);
			}

			public PromotionConditionCollection Top(int count, WhereDelegate<PromotionConditionColumns> where, Database db = null)
			{
				return Bam.Net.Shop.PromotionCondition.Top(count, where, db);
			}

			public PromotionConditionCollection Top(int count, WhereDelegate<PromotionConditionColumns> where, OrderBy<PromotionConditionColumns> orderBy, Database db = null)
			{
				return Bam.Net.Shop.PromotionCondition.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PromotionConditionColumns> where, Database db = null)
			{
				return Bam.Net.Shop.PromotionCondition.Count(where, db);
			}
	}

	static PromotionConditionQueryContext _promotionConditions;
	static object _promotionConditionsLock = new object();
	public static PromotionConditionQueryContext PromotionConditions
	{
		get
		{
			return _promotionConditionsLock.DoubleCheckLock<PromotionConditionQueryContext>(ref _promotionConditions, () => new PromotionConditionQueryContext());
		}
	}
	public class PromotionCodeQueryContext
	{
			public PromotionCodeCollection Where(WhereDelegate<PromotionCodeColumns> where, Database db = null)
			{
				return Bam.Net.Shop.PromotionCode.Where(where, db);
			}
		   
			public PromotionCodeCollection Where(WhereDelegate<PromotionCodeColumns> where, OrderBy<PromotionCodeColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Shop.PromotionCode.Where(where, orderBy, db);
			}

			public PromotionCode OneWhere(WhereDelegate<PromotionCodeColumns> where, Database db = null)
			{
				return Bam.Net.Shop.PromotionCode.OneWhere(where, db);
			}
		
			public PromotionCode FirstOneWhere(WhereDelegate<PromotionCodeColumns> where, Database db = null)
			{
				return Bam.Net.Shop.PromotionCode.FirstOneWhere(where, db);
			}

			public PromotionCodeCollection Top(int count, WhereDelegate<PromotionCodeColumns> where, Database db = null)
			{
				return Bam.Net.Shop.PromotionCode.Top(count, where, db);
			}

			public PromotionCodeCollection Top(int count, WhereDelegate<PromotionCodeColumns> where, OrderBy<PromotionCodeColumns> orderBy, Database db = null)
			{
				return Bam.Net.Shop.PromotionCode.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PromotionCodeColumns> where, Database db = null)
			{
				return Bam.Net.Shop.PromotionCode.Count(where, db);
			}
	}

	static PromotionCodeQueryContext _promotionCodes;
	static object _promotionCodesLock = new object();
	public static PromotionCodeQueryContext PromotionCodes
	{
		get
		{
			return _promotionCodesLock.DoubleCheckLock<PromotionCodeQueryContext>(ref _promotionCodes, () => new PromotionCodeQueryContext());
		}
	}
	public class ShopperQueryContext
	{
			public ShopperCollection Where(WhereDelegate<ShopperColumns> where, Database db = null)
			{
				return Bam.Net.Shop.Shopper.Where(where, db);
			}
		   
			public ShopperCollection Where(WhereDelegate<ShopperColumns> where, OrderBy<ShopperColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Shop.Shopper.Where(where, orderBy, db);
			}

			public Shopper OneWhere(WhereDelegate<ShopperColumns> where, Database db = null)
			{
				return Bam.Net.Shop.Shopper.OneWhere(where, db);
			}
		
			public Shopper FirstOneWhere(WhereDelegate<ShopperColumns> where, Database db = null)
			{
				return Bam.Net.Shop.Shopper.FirstOneWhere(where, db);
			}

			public ShopperCollection Top(int count, WhereDelegate<ShopperColumns> where, Database db = null)
			{
				return Bam.Net.Shop.Shopper.Top(count, where, db);
			}

			public ShopperCollection Top(int count, WhereDelegate<ShopperColumns> where, OrderBy<ShopperColumns> orderBy, Database db = null)
			{
				return Bam.Net.Shop.Shopper.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ShopperColumns> where, Database db = null)
			{
				return Bam.Net.Shop.Shopper.Count(where, db);
			}
	}

	static ShopperQueryContext _shoppers;
	static object _shoppersLock = new object();
	public static ShopperQueryContext Shoppers
	{
		get
		{
			return _shoppersLock.DoubleCheckLock<ShopperQueryContext>(ref _shoppers, () => new ShopperQueryContext());
		}
	}
	public class ShoppingCartQueryContext
	{
			public ShoppingCartCollection Where(WhereDelegate<ShoppingCartColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShoppingCart.Where(where, db);
			}
		   
			public ShoppingCartCollection Where(WhereDelegate<ShoppingCartColumns> where, OrderBy<ShoppingCartColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Shop.ShoppingCart.Where(where, orderBy, db);
			}

			public ShoppingCart OneWhere(WhereDelegate<ShoppingCartColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShoppingCart.OneWhere(where, db);
			}
		
			public ShoppingCart FirstOneWhere(WhereDelegate<ShoppingCartColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShoppingCart.FirstOneWhere(where, db);
			}

			public ShoppingCartCollection Top(int count, WhereDelegate<ShoppingCartColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShoppingCart.Top(count, where, db);
			}

			public ShoppingCartCollection Top(int count, WhereDelegate<ShoppingCartColumns> where, OrderBy<ShoppingCartColumns> orderBy, Database db = null)
			{
				return Bam.Net.Shop.ShoppingCart.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ShoppingCartColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShoppingCart.Count(where, db);
			}
	}

	static ShoppingCartQueryContext _shoppingCarts;
	static object _shoppingCartsLock = new object();
	public static ShoppingCartQueryContext ShoppingCarts
	{
		get
		{
			return _shoppingCartsLock.DoubleCheckLock<ShoppingCartQueryContext>(ref _shoppingCarts, () => new ShoppingCartQueryContext());
		}
	}
	public class ShoppingCartItemQueryContext
	{
			public ShoppingCartItemCollection Where(WhereDelegate<ShoppingCartItemColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShoppingCartItem.Where(where, db);
			}
		   
			public ShoppingCartItemCollection Where(WhereDelegate<ShoppingCartItemColumns> where, OrderBy<ShoppingCartItemColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Shop.ShoppingCartItem.Where(where, orderBy, db);
			}

			public ShoppingCartItem OneWhere(WhereDelegate<ShoppingCartItemColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShoppingCartItem.OneWhere(where, db);
			}
		
			public ShoppingCartItem FirstOneWhere(WhereDelegate<ShoppingCartItemColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShoppingCartItem.FirstOneWhere(where, db);
			}

			public ShoppingCartItemCollection Top(int count, WhereDelegate<ShoppingCartItemColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShoppingCartItem.Top(count, where, db);
			}

			public ShoppingCartItemCollection Top(int count, WhereDelegate<ShoppingCartItemColumns> where, OrderBy<ShoppingCartItemColumns> orderBy, Database db = null)
			{
				return Bam.Net.Shop.ShoppingCartItem.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ShoppingCartItemColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShoppingCartItem.Count(where, db);
			}
	}

	static ShoppingCartItemQueryContext _shoppingCartItems;
	static object _shoppingCartItemsLock = new object();
	public static ShoppingCartItemQueryContext ShoppingCartItems
	{
		get
		{
			return _shoppingCartItemsLock.DoubleCheckLock<ShoppingCartItemQueryContext>(ref _shoppingCartItems, () => new ShoppingCartItemQueryContext());
		}
	}
	public class ShoppingListQueryContext
	{
			public ShoppingListCollection Where(WhereDelegate<ShoppingListColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShoppingList.Where(where, db);
			}
		   
			public ShoppingListCollection Where(WhereDelegate<ShoppingListColumns> where, OrderBy<ShoppingListColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Shop.ShoppingList.Where(where, orderBy, db);
			}

			public ShoppingList OneWhere(WhereDelegate<ShoppingListColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShoppingList.OneWhere(where, db);
			}
		
			public ShoppingList FirstOneWhere(WhereDelegate<ShoppingListColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShoppingList.FirstOneWhere(where, db);
			}

			public ShoppingListCollection Top(int count, WhereDelegate<ShoppingListColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShoppingList.Top(count, where, db);
			}

			public ShoppingListCollection Top(int count, WhereDelegate<ShoppingListColumns> where, OrderBy<ShoppingListColumns> orderBy, Database db = null)
			{
				return Bam.Net.Shop.ShoppingList.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ShoppingListColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShoppingList.Count(where, db);
			}
	}

	static ShoppingListQueryContext _shoppingLists;
	static object _shoppingListsLock = new object();
	public static ShoppingListQueryContext ShoppingLists
	{
		get
		{
			return _shoppingListsLock.DoubleCheckLock<ShoppingListQueryContext>(ref _shoppingLists, () => new ShoppingListQueryContext());
		}
	}
	public class ShopItemQueryContext
	{
			public ShopItemCollection Where(WhereDelegate<ShopItemColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopItem.Where(where, db);
			}
		   
			public ShopItemCollection Where(WhereDelegate<ShopItemColumns> where, OrderBy<ShopItemColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Shop.ShopItem.Where(where, orderBy, db);
			}

			public ShopItem OneWhere(WhereDelegate<ShopItemColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopItem.OneWhere(where, db);
			}
		
			public ShopItem FirstOneWhere(WhereDelegate<ShopItemColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopItem.FirstOneWhere(where, db);
			}

			public ShopItemCollection Top(int count, WhereDelegate<ShopItemColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopItem.Top(count, where, db);
			}

			public ShopItemCollection Top(int count, WhereDelegate<ShopItemColumns> where, OrderBy<ShopItemColumns> orderBy, Database db = null)
			{
				return Bam.Net.Shop.ShopItem.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ShopItemColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopItem.Count(where, db);
			}
	}

	static ShopItemQueryContext _shopItems;
	static object _shopItemsLock = new object();
	public static ShopItemQueryContext ShopItems
	{
		get
		{
			return _shopItemsLock.DoubleCheckLock<ShopItemQueryContext>(ref _shopItems, () => new ShopItemQueryContext());
		}
	}
	public class PriceQueryContext
	{
			public PriceCollection Where(WhereDelegate<PriceColumns> where, Database db = null)
			{
				return Bam.Net.Shop.Price.Where(where, db);
			}
		   
			public PriceCollection Where(WhereDelegate<PriceColumns> where, OrderBy<PriceColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Shop.Price.Where(where, orderBy, db);
			}

			public Price OneWhere(WhereDelegate<PriceColumns> where, Database db = null)
			{
				return Bam.Net.Shop.Price.OneWhere(where, db);
			}
		
			public Price FirstOneWhere(WhereDelegate<PriceColumns> where, Database db = null)
			{
				return Bam.Net.Shop.Price.FirstOneWhere(where, db);
			}

			public PriceCollection Top(int count, WhereDelegate<PriceColumns> where, Database db = null)
			{
				return Bam.Net.Shop.Price.Top(count, where, db);
			}

			public PriceCollection Top(int count, WhereDelegate<PriceColumns> where, OrderBy<PriceColumns> orderBy, Database db = null)
			{
				return Bam.Net.Shop.Price.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PriceColumns> where, Database db = null)
			{
				return Bam.Net.Shop.Price.Count(where, db);
			}
	}

	static PriceQueryContext _prices;
	static object _pricesLock = new object();
	public static PriceQueryContext Prices
	{
		get
		{
			return _pricesLock.DoubleCheckLock<PriceQueryContext>(ref _prices, () => new PriceQueryContext());
		}
	}
	public class ShopItemAttributeQueryContext
	{
			public ShopItemAttributeCollection Where(WhereDelegate<ShopItemAttributeColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopItemAttribute.Where(where, db);
			}
		   
			public ShopItemAttributeCollection Where(WhereDelegate<ShopItemAttributeColumns> where, OrderBy<ShopItemAttributeColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Shop.ShopItemAttribute.Where(where, orderBy, db);
			}

			public ShopItemAttribute OneWhere(WhereDelegate<ShopItemAttributeColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopItemAttribute.OneWhere(where, db);
			}
		
			public ShopItemAttribute FirstOneWhere(WhereDelegate<ShopItemAttributeColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopItemAttribute.FirstOneWhere(where, db);
			}

			public ShopItemAttributeCollection Top(int count, WhereDelegate<ShopItemAttributeColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopItemAttribute.Top(count, where, db);
			}

			public ShopItemAttributeCollection Top(int count, WhereDelegate<ShopItemAttributeColumns> where, OrderBy<ShopItemAttributeColumns> orderBy, Database db = null)
			{
				return Bam.Net.Shop.ShopItemAttribute.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ShopItemAttributeColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopItemAttribute.Count(where, db);
			}
	}

	static ShopItemAttributeQueryContext _shopItemAttributes;
	static object _shopItemAttributesLock = new object();
	public static ShopItemAttributeQueryContext ShopItemAttributes
	{
		get
		{
			return _shopItemAttributesLock.DoubleCheckLock<ShopItemAttributeQueryContext>(ref _shopItemAttributes, () => new ShopItemAttributeQueryContext());
		}
	}
	public class ShopItemAttributeValueQueryContext
	{
			public ShopItemAttributeValueCollection Where(WhereDelegate<ShopItemAttributeValueColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopItemAttributeValue.Where(where, db);
			}
		   
			public ShopItemAttributeValueCollection Where(WhereDelegate<ShopItemAttributeValueColumns> where, OrderBy<ShopItemAttributeValueColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Shop.ShopItemAttributeValue.Where(where, orderBy, db);
			}

			public ShopItemAttributeValue OneWhere(WhereDelegate<ShopItemAttributeValueColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopItemAttributeValue.OneWhere(where, db);
			}
		
			public ShopItemAttributeValue FirstOneWhere(WhereDelegate<ShopItemAttributeValueColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopItemAttributeValue.FirstOneWhere(where, db);
			}

			public ShopItemAttributeValueCollection Top(int count, WhereDelegate<ShopItemAttributeValueColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopItemAttributeValue.Top(count, where, db);
			}

			public ShopItemAttributeValueCollection Top(int count, WhereDelegate<ShopItemAttributeValueColumns> where, OrderBy<ShopItemAttributeValueColumns> orderBy, Database db = null)
			{
				return Bam.Net.Shop.ShopItemAttributeValue.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ShopItemAttributeValueColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopItemAttributeValue.Count(where, db);
			}
	}

	static ShopItemAttributeValueQueryContext _shopItemAttributeValues;
	static object _shopItemAttributeValuesLock = new object();
	public static ShopItemAttributeValueQueryContext ShopItemAttributeValues
	{
		get
		{
			return _shopItemAttributeValuesLock.DoubleCheckLock<ShopItemAttributeValueQueryContext>(ref _shopItemAttributeValues, () => new ShopItemAttributeValueQueryContext());
		}
	}
	public class ShoppingListShopItemQueryContext
	{
			public ShoppingListShopItemCollection Where(WhereDelegate<ShoppingListShopItemColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShoppingListShopItem.Where(where, db);
			}
		   
			public ShoppingListShopItemCollection Where(WhereDelegate<ShoppingListShopItemColumns> where, OrderBy<ShoppingListShopItemColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Shop.ShoppingListShopItem.Where(where, orderBy, db);
			}

			public ShoppingListShopItem OneWhere(WhereDelegate<ShoppingListShopItemColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShoppingListShopItem.OneWhere(where, db);
			}
		
			public ShoppingListShopItem FirstOneWhere(WhereDelegate<ShoppingListShopItemColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShoppingListShopItem.FirstOneWhere(where, db);
			}

			public ShoppingListShopItemCollection Top(int count, WhereDelegate<ShoppingListShopItemColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShoppingListShopItem.Top(count, where, db);
			}

			public ShoppingListShopItemCollection Top(int count, WhereDelegate<ShoppingListShopItemColumns> where, OrderBy<ShoppingListShopItemColumns> orderBy, Database db = null)
			{
				return Bam.Net.Shop.ShoppingListShopItem.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ShoppingListShopItemColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShoppingListShopItem.Count(where, db);
			}
	}

	static ShoppingListShopItemQueryContext _shoppingListShopItems;
	static object _shoppingListShopItemsLock = new object();
	public static ShoppingListShopItemQueryContext ShoppingListShopItems
	{
		get
		{
			return _shoppingListShopItemsLock.DoubleCheckLock<ShoppingListShopItemQueryContext>(ref _shoppingListShopItems, () => new ShoppingListShopItemQueryContext());
		}
	}
	public class ShopShopItemQueryContext
	{
			public ShopShopItemCollection Where(WhereDelegate<ShopShopItemColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopShopItem.Where(where, db);
			}
		   
			public ShopShopItemCollection Where(WhereDelegate<ShopShopItemColumns> where, OrderBy<ShopShopItemColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Shop.ShopShopItem.Where(where, orderBy, db);
			}

			public ShopShopItem OneWhere(WhereDelegate<ShopShopItemColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopShopItem.OneWhere(where, db);
			}
		
			public ShopShopItem FirstOneWhere(WhereDelegate<ShopShopItemColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopShopItem.FirstOneWhere(where, db);
			}

			public ShopShopItemCollection Top(int count, WhereDelegate<ShopShopItemColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopShopItem.Top(count, where, db);
			}

			public ShopShopItemCollection Top(int count, WhereDelegate<ShopShopItemColumns> where, OrderBy<ShopShopItemColumns> orderBy, Database db = null)
			{
				return Bam.Net.Shop.ShopShopItem.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ShopShopItemColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopShopItem.Count(where, db);
			}
	}

	static ShopShopItemQueryContext _shopShopItems;
	static object _shopShopItemsLock = new object();
	public static ShopShopItemQueryContext ShopShopItems
	{
		get
		{
			return _shopShopItemsLock.DoubleCheckLock<ShopShopItemQueryContext>(ref _shopShopItems, () => new ShopShopItemQueryContext());
		}
	}
	public class ShopItemShopItemAttributeQueryContext
	{
			public ShopItemShopItemAttributeCollection Where(WhereDelegate<ShopItemShopItemAttributeColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopItemShopItemAttribute.Where(where, db);
			}
		   
			public ShopItemShopItemAttributeCollection Where(WhereDelegate<ShopItemShopItemAttributeColumns> where, OrderBy<ShopItemShopItemAttributeColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Shop.ShopItemShopItemAttribute.Where(where, orderBy, db);
			}

			public ShopItemShopItemAttribute OneWhere(WhereDelegate<ShopItemShopItemAttributeColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopItemShopItemAttribute.OneWhere(where, db);
			}
		
			public ShopItemShopItemAttribute FirstOneWhere(WhereDelegate<ShopItemShopItemAttributeColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopItemShopItemAttribute.FirstOneWhere(where, db);
			}

			public ShopItemShopItemAttributeCollection Top(int count, WhereDelegate<ShopItemShopItemAttributeColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopItemShopItemAttribute.Top(count, where, db);
			}

			public ShopItemShopItemAttributeCollection Top(int count, WhereDelegate<ShopItemShopItemAttributeColumns> where, OrderBy<ShopItemShopItemAttributeColumns> orderBy, Database db = null)
			{
				return Bam.Net.Shop.ShopItemShopItemAttribute.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ShopItemShopItemAttributeColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopItemShopItemAttribute.Count(where, db);
			}
	}

	static ShopItemShopItemAttributeQueryContext _shopItemShopItemAttributes;
	static object _shopItemShopItemAttributesLock = new object();
	public static ShopItemShopItemAttributeQueryContext ShopItemShopItemAttributes
	{
		get
		{
			return _shopItemShopItemAttributesLock.DoubleCheckLock<ShopItemShopItemAttributeQueryContext>(ref _shopItemShopItemAttributes, () => new ShopItemShopItemAttributeQueryContext());
		}
	}
	public class ShopPromotionQueryContext
	{
			public ShopPromotionCollection Where(WhereDelegate<ShopPromotionColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopPromotion.Where(where, db);
			}
		   
			public ShopPromotionCollection Where(WhereDelegate<ShopPromotionColumns> where, OrderBy<ShopPromotionColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Shop.ShopPromotion.Where(where, orderBy, db);
			}

			public ShopPromotion OneWhere(WhereDelegate<ShopPromotionColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopPromotion.OneWhere(where, db);
			}
		
			public ShopPromotion FirstOneWhere(WhereDelegate<ShopPromotionColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopPromotion.FirstOneWhere(where, db);
			}

			public ShopPromotionCollection Top(int count, WhereDelegate<ShopPromotionColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopPromotion.Top(count, where, db);
			}

			public ShopPromotionCollection Top(int count, WhereDelegate<ShopPromotionColumns> where, OrderBy<ShopPromotionColumns> orderBy, Database db = null)
			{
				return Bam.Net.Shop.ShopPromotion.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ShopPromotionColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopPromotion.Count(where, db);
			}
	}

	static ShopPromotionQueryContext _shopPromotions;
	static object _shopPromotionsLock = new object();
	public static ShopPromotionQueryContext ShopPromotions
	{
		get
		{
			return _shopPromotionsLock.DoubleCheckLock<ShopPromotionQueryContext>(ref _shopPromotions, () => new ShopPromotionQueryContext());
		}
	}
	public class ShopItemPromotionQueryContext
	{
			public ShopItemPromotionCollection Where(WhereDelegate<ShopItemPromotionColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopItemPromotion.Where(where, db);
			}
		   
			public ShopItemPromotionCollection Where(WhereDelegate<ShopItemPromotionColumns> where, OrderBy<ShopItemPromotionColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Shop.ShopItemPromotion.Where(where, orderBy, db);
			}

			public ShopItemPromotion OneWhere(WhereDelegate<ShopItemPromotionColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopItemPromotion.OneWhere(where, db);
			}
		
			public ShopItemPromotion FirstOneWhere(WhereDelegate<ShopItemPromotionColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopItemPromotion.FirstOneWhere(where, db);
			}

			public ShopItemPromotionCollection Top(int count, WhereDelegate<ShopItemPromotionColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopItemPromotion.Top(count, where, db);
			}

			public ShopItemPromotionCollection Top(int count, WhereDelegate<ShopItemPromotionColumns> where, OrderBy<ShopItemPromotionColumns> orderBy, Database db = null)
			{
				return Bam.Net.Shop.ShopItemPromotion.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ShopItemPromotionColumns> where, Database db = null)
			{
				return Bam.Net.Shop.ShopItemPromotion.Count(where, db);
			}
	}

	static ShopItemPromotionQueryContext _shopItemPromotions;
	static object _shopItemPromotionsLock = new object();
	public static ShopItemPromotionQueryContext ShopItemPromotions
	{
		get
		{
			return _shopItemPromotionsLock.DoubleCheckLock<ShopItemPromotionQueryContext>(ref _shopItemPromotions, () => new ShopItemPromotionQueryContext());
		}
	}    }
}																								
