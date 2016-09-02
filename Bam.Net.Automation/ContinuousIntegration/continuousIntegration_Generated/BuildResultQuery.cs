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
    public class BuildResultQuery: Query<BuildResultColumns, BuildResult>
    { 
		public BuildResultQuery(){}
		public BuildResultQuery(WhereDelegate<BuildResultColumns> where, OrderBy<BuildResultColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public BuildResultQuery(Func<BuildResultColumns, QueryFilter<BuildResultColumns>> where, OrderBy<BuildResultColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public BuildResultQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static BuildResultQuery Where(WhereDelegate<BuildResultColumns> where)
        {
            return Where(where, null, null);
        }

        public static BuildResultQuery Where(WhereDelegate<BuildResultColumns> where, OrderBy<BuildResultColumns> orderBy = null, Database db = null)
        {
            return new BuildResultQuery(where, orderBy, db);
        }

		public BuildResultCollection Execute()
		{
			return new BuildResultCollection(this, true);
		}
    }
}