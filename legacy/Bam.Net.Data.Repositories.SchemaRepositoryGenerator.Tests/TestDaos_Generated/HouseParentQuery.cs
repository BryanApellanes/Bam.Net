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
    public class HouseParentQuery: Query<HouseParentColumns, HouseParent>
    { 
		public HouseParentQuery(){}
		public HouseParentQuery(WhereDelegate<HouseParentColumns> where, OrderBy<HouseParentColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public HouseParentQuery(Func<HouseParentColumns, QueryFilter<HouseParentColumns>> where, OrderBy<HouseParentColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public HouseParentQuery(Delegate where, Database db = null) : base(where, db) { }

		public HouseParentCollection Execute()
		{
			return new HouseParentCollection(this, true);
		}
    }
}