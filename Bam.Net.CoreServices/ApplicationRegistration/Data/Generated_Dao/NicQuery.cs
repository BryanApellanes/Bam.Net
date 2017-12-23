/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class NicQuery: Query<NicColumns, Nic>
    { 
		public NicQuery(){}
		public NicQuery(WhereDelegate<NicColumns> where, OrderBy<NicColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public NicQuery(Func<NicColumns, QueryFilter<NicColumns>> where, OrderBy<NicColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public NicQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static NicQuery Where(WhereDelegate<NicColumns> where)
        {
            return Where(where, null, null);
        }

        public static NicQuery Where(WhereDelegate<NicColumns> where, OrderBy<NicColumns> orderBy = null, Database db = null)
        {
            return new NicQuery(where, orderBy, db);
        }

		public NicCollection Execute()
		{
			return new NicCollection(this, true);
		}
    }
}