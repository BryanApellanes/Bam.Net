/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data.Tests
{
    public class CartColumns: QueryFilter<CartColumns>, IFilterToken
    {
        public CartColumns() { }
        public CartColumns(string columnName)
            : base(columnName)
        { }
		
		public CartColumns KeyColumn
		{
			get
			{
				return new CartColumns("Id");
			}
		}	
				
        public CartColumns Id
        {
            get
            {
                return new CartColumns("Id");
            }
        }
        public CartColumns Uuid
        {
            get
            {
                return new CartColumns("Uuid");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Cart);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}