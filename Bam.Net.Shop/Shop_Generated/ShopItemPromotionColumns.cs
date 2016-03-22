using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Shop
{
    public class ShopItemPromotionColumns: QueryFilter<ShopItemPromotionColumns>, IFilterToken
    {
        public ShopItemPromotionColumns() { }
        public ShopItemPromotionColumns(string columnName)
            : base(columnName)
        { }
		
		public ShopItemPromotionColumns KeyColumn
		{
			get
			{
				return new ShopItemPromotionColumns("Id");
			}
		}	

				
        public ShopItemPromotionColumns Id
        {
            get
            {
                return new ShopItemPromotionColumns("Id");
            }
        }
        public ShopItemPromotionColumns Uuid
        {
            get
            {
                return new ShopItemPromotionColumns("Uuid");
            }
        }

        public ShopItemPromotionColumns ShopItemId
        {
            get
            {
                return new ShopItemPromotionColumns("ShopItemId");
            }
        }
        public ShopItemPromotionColumns PromotionId
        {
            get
            {
                return new ShopItemPromotionColumns("PromotionId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(ShopItemPromotion);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}