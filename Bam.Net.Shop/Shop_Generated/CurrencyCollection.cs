/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Shop
{
    public class CurrencyCollection: DaoCollection<CurrencyColumns, Currency>
    { 
		public CurrencyCollection(){}
		public CurrencyCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public CurrencyCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public CurrencyCollection(Query<CurrencyColumns, Currency> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public CurrencyCollection(Database db, Query<CurrencyColumns, Currency> q, bool load) : base(db, q, load) { }
		public CurrencyCollection(Query<CurrencyColumns, Currency> q, bool load) : base(q, load) { }
    }
}