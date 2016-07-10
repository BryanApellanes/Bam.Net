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
    public class SonDaoQuery: Query<SonDaoColumns, SonDao>
    { 
		public SonDaoQuery(){}
		public SonDaoQuery(WhereDelegate<SonDaoColumns> where, OrderBy<SonDaoColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public SonDaoQuery(Func<SonDaoColumns, QueryFilter<SonDaoColumns>> where, OrderBy<SonDaoColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public SonDaoQuery(Delegate where, Database db = null) : base(where, db) { }

		public SonDaoCollection Execute()
		{
			return new SonDaoCollection(this, true);
		}
    }
}