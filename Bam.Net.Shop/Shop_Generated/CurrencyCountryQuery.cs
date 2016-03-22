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
    public class CurrencyCountryQuery: Query<CurrencyCountryColumns, CurrencyCountry>
    { 
		public CurrencyCountryQuery(){}
		public CurrencyCountryQuery(WhereDelegate<CurrencyCountryColumns> where, OrderBy<CurrencyCountryColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public CurrencyCountryQuery(Func<CurrencyCountryColumns, QueryFilter<CurrencyCountryColumns>> where, OrderBy<CurrencyCountryColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public CurrencyCountryQuery(Delegate where, Database db = null) : base(where, db) { }

		public CurrencyCountryCollection Execute()
		{
			return new CurrencyCountryCollection(this, true);
		}
    }
}