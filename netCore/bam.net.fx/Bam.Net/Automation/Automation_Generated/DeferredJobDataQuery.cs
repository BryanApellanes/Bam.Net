/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Automation.Data
{
    public class DeferredJobDataQuery: Query<DeferredJobDataColumns, DeferredJobData>
    { 
		public DeferredJobDataQuery(){}
		public DeferredJobDataQuery(WhereDelegate<DeferredJobDataColumns> where, OrderBy<DeferredJobDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public DeferredJobDataQuery(Func<DeferredJobDataColumns, QueryFilter<DeferredJobDataColumns>> where, OrderBy<DeferredJobDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public DeferredJobDataQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static DeferredJobDataQuery Where(WhereDelegate<DeferredJobDataColumns> where)
        {
            return Where(where, null, null);
        }

        public static DeferredJobDataQuery Where(WhereDelegate<DeferredJobDataColumns> where, OrderBy<DeferredJobDataColumns> orderBy = null, Database db = null)
        {
            return new DeferredJobDataQuery(where, orderBy, db);
        }

		public DeferredJobDataCollection Execute()
		{
			return new DeferredJobDataCollection(this, true);
		}
    }
}