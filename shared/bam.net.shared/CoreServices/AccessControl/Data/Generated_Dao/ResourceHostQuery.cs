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
    public class ResourceHostQuery: Query<ResourceHostColumns, ResourceHost>
    { 
		public ResourceHostQuery(){}
		public ResourceHostQuery(WhereDelegate<ResourceHostColumns> where, OrderBy<ResourceHostColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ResourceHostQuery(Func<ResourceHostColumns, QueryFilter<ResourceHostColumns>> where, OrderBy<ResourceHostColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ResourceHostQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ResourceHostQuery Where(WhereDelegate<ResourceHostColumns> where)
        {
            return Where(where, null, null);
        }

        public static ResourceHostQuery Where(WhereDelegate<ResourceHostColumns> where, OrderBy<ResourceHostColumns> orderBy = null, Database db = null)
        {
            return new ResourceHostQuery(where, orderBy, db);
        }

		public ResourceHostCollection Execute()
		{
			return new ResourceHostCollection(this, true);
		}
    }
}