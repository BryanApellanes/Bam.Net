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
    public class DaughterQuery: Query<DaughterColumns, Daughter>
    { 
		public DaughterQuery(){}
		public DaughterQuery(WhereDelegate<DaughterColumns> where, OrderBy<DaughterColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public DaughterQuery(Func<DaughterColumns, QueryFilter<DaughterColumns>> where, OrderBy<DaughterColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public DaughterQuery(Delegate where, Database db = null) : base(where, db) { }

		public DaughterCollection Execute()
		{
			return new DaughterCollection(this, true);
		}
    }
}