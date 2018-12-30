/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.AccessControl.Data.Dao
{
    public class ResourceQuery: Query<ResourceColumns, Resource>
    { 
		public ResourceQuery(){}
		public ResourceQuery(WhereDelegate<ResourceColumns> where, OrderBy<ResourceColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ResourceQuery(Func<ResourceColumns, QueryFilter<ResourceColumns>> where, OrderBy<ResourceColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ResourceQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ResourceQuery Where(WhereDelegate<ResourceColumns> where)
        {
            return Where(where, null, null);
        }

        public static ResourceQuery Where(WhereDelegate<ResourceColumns> where, OrderBy<ResourceColumns> orderBy = null, Database db = null)
        {
            return new ResourceQuery(where, orderBy, db);
        }

		public ResourceCollection Execute()
		{
			return new ResourceCollection(this, true);
		}
    }
}