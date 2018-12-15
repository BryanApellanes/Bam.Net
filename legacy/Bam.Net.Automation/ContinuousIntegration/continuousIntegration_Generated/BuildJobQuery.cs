/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Automation.ContinuousIntegration.Data
{
    public class BuildJobQuery: Query<BuildJobColumns, BuildJob>
    { 
		public BuildJobQuery(){}
		public BuildJobQuery(WhereDelegate<BuildJobColumns> where, OrderBy<BuildJobColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public BuildJobQuery(Func<BuildJobColumns, QueryFilter<BuildJobColumns>> where, OrderBy<BuildJobColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public BuildJobQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static BuildJobQuery Where(WhereDelegate<BuildJobColumns> where)
        {
            return Where(where, null, null);
        }

        public static BuildJobQuery Where(WhereDelegate<BuildJobColumns> where, OrderBy<BuildJobColumns> orderBy = null, Database db = null)
        {
            return new BuildJobQuery(where, orderBy, db);
        }

		public BuildJobCollection Execute()
		{
			return new BuildJobCollection(this, true);
		}
    }
}