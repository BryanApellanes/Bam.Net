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
    public class HouseDaoParentDaoQuery: Query<HouseDaoParentDaoColumns, HouseDaoParentDao>
    { 
		public HouseDaoParentDaoQuery(){}
		public HouseDaoParentDaoQuery(WhereDelegate<HouseDaoParentDaoColumns> where, OrderBy<HouseDaoParentDaoColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public HouseDaoParentDaoQuery(Func<HouseDaoParentDaoColumns, QueryFilter<HouseDaoParentDaoColumns>> where, OrderBy<HouseDaoParentDaoColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public HouseDaoParentDaoQuery(Delegate where, Database db = null) : base(where, db) { }

		public HouseDaoParentDaoCollection Execute()
		{
			return new HouseDaoParentDaoCollection(this, true);
		}
    }
}