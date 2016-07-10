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
    public class DaughterDaoQuery: Query<DaughterDaoColumns, DaughterDao>
    { 
		public DaughterDaoQuery(){}
		public DaughterDaoQuery(WhereDelegate<DaughterDaoColumns> where, OrderBy<DaughterDaoColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public DaughterDaoQuery(Func<DaughterDaoColumns, QueryFilter<DaughterDaoColumns>> where, OrderBy<DaughterDaoColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public DaughterDaoQuery(Delegate where, Database db = null) : base(where, db) { }

		public DaughterDaoCollection Execute()
		{
			return new DaughterDaoCollection(this, true);
		}
    }
}