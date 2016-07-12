/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_
{
    public class HouseDaoQuery: Query<HouseDaoColumns, HouseDao>
    { 
		public HouseDaoQuery(){}
		public HouseDaoQuery(WhereDelegate<HouseDaoColumns> where, OrderBy<HouseDaoColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public HouseDaoQuery(Func<HouseDaoColumns, QueryFilter<HouseDaoColumns>> where, OrderBy<HouseDaoColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public HouseDaoQuery(Delegate where, Database db = null) : base(where, db) { }

		public HouseDaoCollection Execute()
		{
			return new HouseDaoCollection(this, true);
		}
    }
}