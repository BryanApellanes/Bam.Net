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
    public class ParentDaoQuery: Query<ParentDaoColumns, ParentDao>
    { 
		public ParentDaoQuery(){}
		public ParentDaoQuery(WhereDelegate<ParentDaoColumns> where, OrderBy<ParentDaoColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ParentDaoQuery(Func<ParentDaoColumns, QueryFilter<ParentDaoColumns>> where, OrderBy<ParentDaoColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ParentDaoQuery(Delegate where, Database db = null) : base(where, db) { }

		public ParentDaoCollection Execute()
		{
			return new ParentDaoCollection(this, true);
		}
    }
}