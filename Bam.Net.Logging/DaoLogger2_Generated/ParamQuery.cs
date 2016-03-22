/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Logging.Data
{
    public class ParamQuery: Query<ParamColumns, Param>
    { 
		public ParamQuery(){}
		public ParamQuery(WhereDelegate<ParamColumns> where, OrderBy<ParamColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ParamQuery(Func<ParamColumns, QueryFilter<ParamColumns>> where, OrderBy<ParamColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ParamQuery(Delegate where, Database db = null) : base(where, db) { }

		public ParamCollection Execute()
		{
			return new ParamCollection(this, true);
		}
    }
}