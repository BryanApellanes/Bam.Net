using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Shop
{
    public class ShopPromotionColumns: QueryFilter<ShopPromotionColumns>, IFilterToken
    {
        public ShopPromotionColumns() { }
        public ShopPromotionColumns(string columnName)
            : base(columnName)
        { }
		
		public ShopPromotionColumns KeyColumn
		{
			get
			{
				return new ShopPromotionColumns("Id");
			}
		}	

				
        public ShopPromotionColumns Id
        {
            get
            {
                return new ShopPromotionColumns("Id");
            }
        }
        public ShopPromotionColumns Uuid
        {
            get
            {
                return new ShopPromotionColumns("Uuid");
            }
        }

        public ShopPromotionColumns ShopId
        {
            get
            {
                return new ShopPromotionColumns("ShopId");
            }
        }
        public ShopPromotionColumns PromotionId
        {
            get
            {
                return new ShopPromotionColumns("PromotionId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(ShopPromotion);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}