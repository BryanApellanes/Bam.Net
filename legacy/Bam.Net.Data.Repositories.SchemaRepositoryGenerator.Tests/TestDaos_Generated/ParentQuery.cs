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
    public class ParentQuery: Query<ParentColumns, Parent>
    { 
		public ParentQuery(){}
		public ParentQuery(WhereDelegate<ParentColumns> where, OrderBy<ParentColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ParentQuery(Func<ParentColumns, QueryFilter<ParentColumns>> where, OrderBy<ParentColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ParentQuery(Delegate where, Database db = null) : base(where, db) { }

		public ParentCollection Execute()
		{
			return new ParentCollection(this, true);
		}
    }
}