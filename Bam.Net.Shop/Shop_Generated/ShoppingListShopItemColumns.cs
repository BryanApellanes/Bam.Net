/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Shop
{
    public class ShoppingListShopItemColumns: QueryFilter<ShoppingListShopItemColumns>, IFilterToken
    {
        public ShoppingListShopItemColumns() { }
        public ShoppingListShopItemColumns(string columnName)
            : base(columnName)
        { }
		
		public ShoppingListShopItemColumns KeyColumn
		{
			get
			{
				return new ShoppingListShopItemColumns("Id");
			}
		}	
				
﻿        public ShoppingListShopItemColumns Id
        {
            get
            {
                return new ShoppingListShopItemColumns("Id");
            }
        }
﻿        public ShoppingListShopItemColumns Uuid
        {
            get
            {
                return new ShoppingListShopItemColumns("Uuid");
            }
        }

﻿        public ShoppingListShopItemColumns ShoppingListId
        {
            get
            {
                return new ShoppingListShopItemColumns("ShoppingListId");
            }
        }
﻿        public ShoppingListShopItemColumns ShopItemId
        {
            get
            {
                return new ShoppingListShopItemColumns("ShopItemId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(ShoppingListShopItem);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}