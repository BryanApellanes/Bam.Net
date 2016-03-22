/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data
{
    public class DaoBaseItemQuery: Query<DaoBaseItemColumns, DaoBaseItem>
    { 
		public DaoBaseItemQuery(){}
		public DaoBaseItemQuery(WhereDelegate<DaoBaseItemColumns> where, OrderBy<DaoBaseItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public DaoBaseItemQuery(Func<DaoBaseItemColumns, QueryFilter<DaoBaseItemColumns>> where, OrderBy<DaoBaseItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public DaoBaseItemQuery(Delegate where, Database db = null) : base(where, db) { }

		public DaoBaseItemCollection Execute()
		{
			return new DaoBaseItemCollection(this, true);
		}
    }
}