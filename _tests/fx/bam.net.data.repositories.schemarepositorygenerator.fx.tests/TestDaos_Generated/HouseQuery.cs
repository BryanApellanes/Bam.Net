/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data.Repositories.Tests.ClrTypes.Daos
{
    public class HouseQuery: Query<HouseColumns, House>
    { 
		public HouseQuery(){}
		public HouseQuery(WhereDelegate<HouseColumns> where, OrderBy<HouseColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public HouseQuery(Func<HouseColumns, QueryFilter<HouseColumns>> where, OrderBy<HouseColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public HouseQuery(Delegate where, Database db = null) : base(where, db) { }

		public HouseCollection Execute()
		{
			return new HouseCollection(this, true);
		}
    }
}