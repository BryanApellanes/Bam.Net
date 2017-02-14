/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Instructions
{
    public class StepQuery: Query<StepColumns, Step>
    { 
		public StepQuery(){}
		public StepQuery(WhereDelegate<StepColumns> where, OrderBy<StepColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public StepQuery(Func<StepColumns, QueryFilter<StepColumns>> where, OrderBy<StepColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public StepQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static StepQuery Where(WhereDelegate<StepColumns> where)
        {
            return Where(where, null, null);
        }

        public static StepQuery Where(WhereDelegate<StepColumns> where, OrderBy<StepColumns> orderBy = null, Database db = null)
        {
            return new StepQuery(where, orderBy, db);
        }

		public StepCollection Execute()
		{
			return new StepCollection(this, true);
		}
    }
}