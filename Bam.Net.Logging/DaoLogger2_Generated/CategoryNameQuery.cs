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
    public class CategoryNameQuery: Query<CategoryNameColumns, CategoryName>
    { 
		public CategoryNameQuery(){}
		public CategoryNameQuery(WhereDelegate<CategoryNameColumns> where, OrderBy<CategoryNameColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public CategoryNameQuery(Func<CategoryNameColumns, QueryFilter<CategoryNameColumns>> where, OrderBy<CategoryNameColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public CategoryNameQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static CategoryNameQuery Where(WhereDelegate<CategoryNameColumns> where)
        {
            return Where(where, null, null);
        }

        public static CategoryNameQuery Where(WhereDelegate<CategoryNameColumns> where, OrderBy<CategoryNameColumns> orderBy = null, Database db = null)
        {
            return new CategoryNameQuery(where, orderBy, db);
        }

		public CategoryNameCollection Execute()
		{
			return new CategoryNameCollection(this, true);
		}
    }
}