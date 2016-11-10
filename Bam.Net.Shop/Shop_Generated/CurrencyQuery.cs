/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Shop
{
    public class CurrencyQuery: Query<CurrencyColumns, Currency>
    { 
		public CurrencyQuery(){}
		public CurrencyQuery(WhereDelegate<CurrencyColumns> where, OrderBy<CurrencyColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public CurrencyQuery(Func<CurrencyColumns, QueryFilter<CurrencyColumns>> where, OrderBy<CurrencyColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public CurrencyQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static CurrencyQuery Where(WhereDelegate<CurrencyColumns> where)
        {
            return Where(where, null, null);
        }

        public static CurrencyQuery Where(WhereDelegate<CurrencyColumns> where, OrderBy<CurrencyColumns> orderBy = null, Database db = null)
        {
            return new CurrencyQuery(where, orderBy, db);
        }

		public CurrencyCollection Execute()
		{
			return new CurrencyCollection(this, true);
		}
    }
}