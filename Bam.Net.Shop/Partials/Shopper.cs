/*
	Copyright © Bryan Apellanes 2015  
*/
// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;
using Bam.Net.ServiceProxy;
using Bam.Net.Logging;

namespace Bam.Net.Shop
{
	[Proxy]
	public partial class Shopper: IRequiresHttpContext
	{
		public override void OnInitialize()
		{
			this.UserResolver = new DefaultUserResolver();
		}

		static Shopper _anonymousShopper;
		static object _shopperLock = new object();
		public static Shopper AnonymousShopper
		{
			get
			{
				return _shopperLock.DoubleCheckLock(ref _anonymousShopper, () => { return new Shopper { Name = "Anonymous" }; });
			}
		}

		public IUserResolver UserResolver { get; set; }
		/// <summary>
		/// Get a Shopper instance representing the current user
		/// </summary>
		/// <returns></returns>
		protected internal Shopper GetCurrent()
		{
			string userName = UserResolver.GetCurrentUser();
			Shopper result = null;
			if(!string.IsNullOrEmpty(userName))
			{
				result = Shopper.OneWhere(c => c.Name == userName);
				if (result == null)
				{
					result = new Shopper
					{
						Name = userName
					};
					result.Save();
				}
			}

			return result ?? AnonymousShopper;
		}
		
		/// <summary>
		/// Add the specified item to the current user's Cart
		/// and return the Cart
		/// </summary>
		/// <param name="itemId"></param>
		/// <returns></returns>
		public ShoppingCartInfo AddToShoppingCart(params long[] itemIds)
		{
			ShoppingCart cart = GetShoppingCart();
			itemIds.Each(id =>
			{
				ShoppingCartItem cartItem = cart.ShoppingCartItemsByShoppingCartId.AddNew();
				cartItem.Quantity = 1;
				cartItem.ShopItemId = id;
			});

			cart.Save();

			return new ShoppingCartInfo(cart);
		}

		public ShoppingCartInfo GetShoppingCartInfo()
		{
			return new ShoppingCartInfo(GetShoppingCart());
		}

		protected internal ShoppingCart GetShoppingCart()
		{
			ShoppingCart cart = ShoppingCart.OneWhere(sc => sc.ShopperId == this.Id);
			if (cart == null)
			{
				cart = new ShoppingCart();
				cart.ShopperId = this.Id;
				cart.Save();
			}
			return cart;
		}

		public bool RemoveFromShoppingCart(long itemId)
		{
			ShoppingCart cart = GetShoppingCart();
			ShoppingCartItem toRemove = ShoppingCartItem.OneWhere(c => c.ShopItemId == itemId && c.ShoppingCartId == cart.Id);
			bool result = false;
			if (toRemove != null)
			{
				try
				{
					toRemove.Delete();
					result = true;
				}
				catch (Exception ex)
				{
					Log.AddEntry("An exception occurred removing item ({0}) from cart: {1}", ex, itemId.ToString(), ex.Message);
				}
			}

			return result;
		}

		#region IRequiresHttpContext Members

		public IHttpContext HttpContext
		{
			get
			{
				return UserResolver.HttpContext;
			}
			set
			{
				UserResolver.HttpContext = value;
			}
		}

        public object Clone()
        {
            Shopper clone = new Shopper();
            clone.CopyProperties(this);
            return clone;
        }

        #endregion
    }
}																								
