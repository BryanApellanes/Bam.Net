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
    public class JobRunDataQuery: Query<JobRunDataColumns, JobRunData>
    { 
		public JobRunDataQuery(){}
		public JobRunDataQuery(WhereDelegate<JobRunDataColumns> where, OrderBy<JobRunDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public JobRunDataQuery(Func<JobRunDataColumns, QueryFilter<JobRunDataColumns>> where, OrderBy<JobRunDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public JobRunDataQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static JobRunDataQuery Where(WhereDelegate<JobRunDataColumns> where)
        {
            return Where(where, null, null);
        }

        public static JobRunDataQuery Where(WhereDelegate<JobRunDataColumns> where, OrderBy<JobRunDataColumns> orderBy = null, Database db = null)
        {
            return new JobRunDataQuery(where, orderBy, db);
        }

		public JobRunDataCollection Execute()
		{
			return new JobRunDataCollection(this, true);
		}
    }
}