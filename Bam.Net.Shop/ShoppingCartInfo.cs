/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Shop
{
	public class ShoppingCartInfo
	{
		public ShoppingCartInfo()
		{

		}

		public ShoppingCartInfo(ShoppingCart cart)
		{
			if (cart == null)
			{
				throw new ArgumentNullException("cart");
			}

			this.Id = cart.Id.Value;
			this.ShopperId = cart.ShopperId.Value;
			this.ShopperName = cart.ShopperOfShopperId.Name;
			this.ItemIds = cart.ShoppingCartItemsByShoppingCartId.Select(ci => ci.ShopItemId.Value).ToArray();
		}

		public ShoppingCart ToShoppingCart()
		{
			return ShoppingCart.OneWhere(c => c.Id == Id);
		}

		public long Id { get; set; }
		public long ShopperId { get; set; }
		public string ShopperName { get; set; }
		public long[] ItemIds { get; set; }

		public ShopItemInfo[] GetItems()
		{
			ShopItemCollection items = ShopItem.Where(c => c.Id.In(ItemIds));
			return items.Select(i => new ShopItemInfo(i)).ToArray();
		}
	}
}
