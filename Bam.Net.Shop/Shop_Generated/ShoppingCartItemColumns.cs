using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Shop
{
    public class ShoppingCartItemColumns: QueryFilter<ShoppingCartItemColumns>, IFilterToken
    {
        public ShoppingCartItemColumns() { }
        public ShoppingCartItemColumns(string columnName)
            : base(columnName)
        { }
		
		public ShoppingCartItemColumns KeyColumn
		{
			get
			{
				return new ShoppingCartItemColumns("Id");
			}
		}	

				
        public ShoppingCartItemColumns Id
        {
            get
            {
                return new ShoppingCartItemColumns("Id");
            }
        }
        public ShoppingCartItemColumns Uuid
        {
            get
            {
                return new ShoppingCartItemColumns("Uuid");
            }
        }
        public ShoppingCartItemColumns Quantity
        {
            get
            {
                return new ShoppingCartItemColumns("Quantity");
            }
        }

        public ShoppingCartItemColumns ShoppingCartId
        {
            get
            {
                return new ShoppingCartItemColumns("ShoppingCartId");
            }
        }
        public ShoppingCartItemColumns ShopItemId
        {
            get
            {
                return new ShoppingCartItemColumns("ShopItemId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(ShoppingCartItem);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}