/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data.Tests
{
    public class CartItemColumns: QueryFilter<CartItemColumns>, IFilterToken
    {
        public CartItemColumns() { }
        public CartItemColumns(string columnName)
            : base(columnName)
        { }
		
		public CartItemColumns KeyColumn
		{
			get
			{
				return new CartItemColumns("Id");
			}
		}	
				
﻿        public CartItemColumns Id
        {
            get
            {
                return new CartItemColumns("Id");
            }
        }
﻿        public CartItemColumns Uuid
        {
            get
            {
                return new CartItemColumns("Uuid");
            }
        }
﻿        public CartItemColumns Quantity
        {
            get
            {
                return new CartItemColumns("Quantity");
            }
        }

﻿        public CartItemColumns CartId
        {
            get
            {
                return new CartItemColumns("CartId");
            }
        }
﻿        public CartItemColumns ItemId
        {
            get
            {
                return new CartItemColumns("ItemId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(CartItem);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}