using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Shop
{
    public class ShopShopItemColumns: QueryFilter<ShopShopItemColumns>, IFilterToken
    {
        public ShopShopItemColumns() { }
        public ShopShopItemColumns(string columnName)
            : base(columnName)
        { }
		
		public ShopShopItemColumns KeyColumn
		{
			get
			{
				return new ShopShopItemColumns("Id");
			}
		}	

				
        public ShopShopItemColumns Id
        {
            get
            {
                return new ShopShopItemColumns("Id");
            }
        }
        public ShopShopItemColumns Uuid
        {
            get
            {
                return new ShopShopItemColumns("Uuid");
            }
        }

        public ShopShopItemColumns ShopId
        {
            get
            {
                return new ShopShopItemColumns("ShopId");
            }
        }
        public ShopShopItemColumns ShopItemId
        {
            get
            {
                return new ShopShopItemColumns("ShopItemId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(ShopShopItem);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}