using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Shop
{
    public class ShopItemAttributeColumns: QueryFilter<ShopItemAttributeColumns>, IFilterToken
    {
        public ShopItemAttributeColumns() { }
        public ShopItemAttributeColumns(string columnName)
            : base(columnName)
        { }
		
		public ShopItemAttributeColumns KeyColumn
		{
			get
			{
				return new ShopItemAttributeColumns("Id");
			}
		}	

				
        public ShopItemAttributeColumns Id
        {
            get
            {
                return new ShopItemAttributeColumns("Id");
            }
        }
        public ShopItemAttributeColumns Uuid
        {
            get
            {
                return new ShopItemAttributeColumns("Uuid");
            }
        }
        public ShopItemAttributeColumns Cuid
        {
            get
            {
                return new ShopItemAttributeColumns("Cuid");
            }
        }
        public ShopItemAttributeColumns Name
        {
            get
            {
                return new ShopItemAttributeColumns("Name");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(ShopItemAttribute);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}