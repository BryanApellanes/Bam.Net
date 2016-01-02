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
	public class ShopItemInfo
	{
		public ShopItemInfo()
		{

		}

		public ShopItemInfo(long id)
		{
			this.Id = id;
			ShopItem item = ToShopItem();
			if (item != null)
			{
				SetProperties(item);
			}
		}

		public ShopItemInfo(ShopItem item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}

			this.Id = item.Id.Value;
			this.SetProperties(item);
		}

		private void SetProperties(ShopItem item)
		{
			this.Name = item.Name;
			this.ImageSrc = item.ImageSrc;
			//this.Price = item.Price.Value;
			this.DetailUrl = item.DetailUrl;
		}

		public ShopItem ToShopItem()
		{
			return ShopItem.OneWhere(c => c.Id == Id);
		}

		public long Id { get; set; }
		public string Name { get; set; }
		public string ImageSrc { get; set; }

		public decimal Price { get; set; }

		public string DetailUrl { get; set; }
	}
}
