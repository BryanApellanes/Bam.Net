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
    public class UrlTagQuery: Query<UrlTagColumns, UrlTag>
    { 
		public UrlTagQuery(){}
		public UrlTagQuery(WhereDelegate<UrlTagColumns> where, OrderBy<UrlTagColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public UrlTagQuery(Func<UrlTagColumns, QueryFilter<UrlTagColumns>> where, OrderBy<UrlTagColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public UrlTagQuery(Delegate where, Database db = null) : base(where, db) { }

		public UrlTagCollection Execute()
		{
			return new UrlTagCollection(this, true);
		}
    }
}