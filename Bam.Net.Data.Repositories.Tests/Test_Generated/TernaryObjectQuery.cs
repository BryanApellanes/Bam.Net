/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data.Repositories.Tests
{
    public class TernaryObjectQuery: Query<TernaryObjectColumns, TernaryObject>
    { 
		public TernaryObjectQuery(){}
		public TernaryObjectQuery(WhereDelegate<TernaryObjectColumns> where, OrderBy<TernaryObjectColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public TernaryObjectQuery(Func<TernaryObjectColumns, QueryFilter<TernaryObjectColumns>> where, OrderBy<TernaryObjectColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public TernaryObjectQuery(Delegate where, Database db = null) : base(where, db) { }

		public TernaryObjectCollection Execute()
		{
			return new TernaryObjectCollection(this, true);
		}
    }
}