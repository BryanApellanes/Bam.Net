/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Shop
{
    public class ShopItemColumns: QueryFilter<ShopItemColumns>, IFilterToken
    {
        public ShopItemColumns() { }
        public ShopItemColumns(string columnName)
            : base(columnName)
        { }
		
		public ShopItemColumns KeyColumn
		{
			get
			{
				return new ShopItemColumns("Id");
			}
		}	
				
﻿        public ShopItemColumns Id
        {
            get
            {
                return new ShopItemColumns("Id");
            }
        }
﻿        public ShopItemColumns Uuid
        {
            get
            {
                return new ShopItemColumns("Uuid");
            }
        }
﻿        public ShopItemColumns Name
        {
            get
            {
                return new ShopItemColumns("Name");
            }
        }
﻿        public ShopItemColumns Source
        {
            get
            {
                return new ShopItemColumns("Source");
            }
        }
﻿        public ShopItemColumns SourceId
        {
            get
            {
                return new ShopItemColumns("SourceId");
            }
        }
﻿        public ShopItemColumns DetailUrl
        {
            get
            {
                return new ShopItemColumns("DetailUrl");
            }
        }
﻿        public ShopItemColumns ImageSrc
        {
            get
            {
                return new ShopItemColumns("ImageSrc");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(ShopItem);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}