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
    public class SonQuery: Query<SonColumns, Son>
    { 
		public SonQuery(){}
		public SonQuery(WhereDelegate<SonColumns> where, OrderBy<SonColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public SonQuery(Func<SonColumns, QueryFilter<SonColumns>> where, OrderBy<SonColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public SonQuery(Delegate where, Database db = null) : base(where, db) { }

		public SonCollection Execute()
		{
			return new SonCollection(this, true);
		}
    }
}