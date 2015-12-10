/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class TagQuery: Query<TagColumns, Tag>
    { 
		public TagQuery(){}
		public TagQuery(WhereDelegate<TagColumns> where, OrderBy<TagColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public TagQuery(Func<TagColumns, QueryFilter<TagColumns>> where, OrderBy<TagColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public TagQuery(Delegate where, Database db = null) : base(where, db) { }

		public TagCollection Execute()
		{
			return new TagCollection(this, true);
		}
    }
}