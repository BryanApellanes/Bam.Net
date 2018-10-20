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
    public class JobDataQuery: Query<JobDataColumns, JobData>
    { 
		public JobDataQuery(){}
		public JobDataQuery(WhereDelegate<JobDataColumns> where, OrderBy<JobDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public JobDataQuery(Func<JobDataColumns, QueryFilter<JobDataColumns>> where, OrderBy<JobDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public JobDataQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static JobDataQuery Where(WhereDelegate<JobDataColumns> where)
        {
            return Where(where, null, null);
        }

        public static JobDataQuery Where(WhereDelegate<JobDataColumns> where, OrderBy<JobDataColumns> orderBy = null, Database db = null)
        {
            return new JobDataQuery(where, orderBy, db);
        }

		public JobDataCollection Execute()
		{
			return new JobDataCollection(this, true);
		}
    }
}