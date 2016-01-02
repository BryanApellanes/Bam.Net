/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Shop
{
    public class ShoppingCartColumns: QueryFilter<ShoppingCartColumns>, IFilterToken
    {
        public ShoppingCartColumns() { }
        public ShoppingCartColumns(string columnName)
            : base(columnName)
        { }
		
		public ShoppingCartColumns KeyColumn
		{
			get
			{
				return new ShoppingCartColumns("Id");
			}
		}	
				
﻿        public ShoppingCartColumns Id
        {
            get
            {
                return new ShoppingCartColumns("Id");
            }
        }
﻿        public ShoppingCartColumns Uuid
        {
            get
            {
                return new ShoppingCartColumns("Uuid");
            }
        }
﻿        public ShoppingCartColumns Name
        {
            get
            {
                return new ShoppingCartColumns("Name");
            }
        }

﻿        public ShoppingCartColumns ShopperId
        {
            get
            {
                return new ShoppingCartColumns("ShopperId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(ShoppingCart);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}