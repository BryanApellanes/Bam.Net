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
    public class DaoSubItemQuery: Query<DaoSubItemColumns, DaoSubItem>
    { 
		public DaoSubItemQuery(){}
		public DaoSubItemQuery(WhereDelegate<DaoSubItemColumns> where, OrderBy<DaoSubItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public DaoSubItemQuery(Func<DaoSubItemColumns, QueryFilter<DaoSubItemColumns>> where, OrderBy<DaoSubItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public DaoSubItemQuery(Delegate where, Database db = null) : base(where, db) { }

		public DaoSubItemCollection Execute()
		{
			return new DaoSubItemCollection(this, true);
		}
    }
}