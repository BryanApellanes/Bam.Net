/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Shop
{
    public class CurrencyColumns: QueryFilter<CurrencyColumns>, IFilterToken
    {
        public CurrencyColumns() { }
        public CurrencyColumns(string columnName)
            : base(columnName)
        { }
		
		public CurrencyColumns KeyColumn
		{
			get
			{
				return new CurrencyColumns("Id");
			}
		}	
				
﻿        public CurrencyColumns Id
        {
            get
            {
                return new CurrencyColumns("Id");
            }
        }
﻿        public CurrencyColumns Uuid
        {
            get
            {
                return new CurrencyColumns("Uuid");
            }
        }
﻿        public CurrencyColumns Symbol
        {
            get
            {
                return new CurrencyColumns("Symbol");
            }
        }
﻿        public CurrencyColumns Name
        {
            get
            {
                return new CurrencyColumns("Name");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Currency);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}