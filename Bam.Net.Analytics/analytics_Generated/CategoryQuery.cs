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
    public class CategoryQuery: Query<CategoryColumns, Category>
    { 
		public CategoryQuery(){}
		public CategoryQuery(WhereDelegate<CategoryColumns> where, OrderBy<CategoryColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public CategoryQuery(Func<CategoryColumns, QueryFilter<CategoryColumns>> where, OrderBy<CategoryColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public CategoryQuery(Delegate where, Database db = null) : base(where, db) { }

		public CategoryCollection Execute()
		{
			return new CategoryCollection(this, true);
		}
    }
}