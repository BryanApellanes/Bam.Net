/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class PortQuery: Query<PortColumns, Port>
    { 
		public PortQuery(){}
		public PortQuery(WhereDelegate<PortColumns> where, OrderBy<PortColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public PortQuery(Func<PortColumns, QueryFilter<PortColumns>> where, OrderBy<PortColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public PortQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static PortQuery Where(WhereDelegate<PortColumns> where)
        {
            return Where(where, null, null);
        }

        public static PortQuery Where(WhereDelegate<PortColumns> where, OrderBy<PortColumns> orderBy = null, Database db = null)
        {
            return new PortQuery(where, orderBy, db);
        }

		public PortCollection Execute()
		{
			return new PortCollection(this, true);
		}
    }
}