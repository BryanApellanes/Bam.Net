using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Shop
{
    public class ShopColumns: QueryFilter<ShopColumns>, IFilterToken
    {
        public ShopColumns() { }
        public ShopColumns(string columnName)
            : base(columnName)
        { }
		
		public ShopColumns KeyColumn
		{
			get
			{
				return new ShopColumns("Id");
			}
		}	

				
        public ShopColumns Id
        {
            get
            {
                return new ShopColumns("Id");
            }
        }
        public ShopColumns Uuid
        {
            get
            {
                return new ShopColumns("Uuid");
            }
        }
        public ShopColumns Cuid
        {
            get
            {
                return new ShopColumns("Cuid");
            }
        }
        public ShopColumns Name
        {
            get
            {
                return new ShopColumns("Name");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Shop);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}