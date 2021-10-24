/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data.Repositories.Tests
{
    public class SecondaryObjectTernaryObjectQuery: Query<SecondaryObjectTernaryObjectColumns, SecondaryObjectTernaryObject>
    { 
		public SecondaryObjectTernaryObjectQuery(){}
		public SecondaryObjectTernaryObjectQuery(WhereDelegate<SecondaryObjectTernaryObjectColumns> where, OrderBy<SecondaryObjectTernaryObjectColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public SecondaryObjectTernaryObjectQuery(Func<SecondaryObjectTernaryObjectColumns, QueryFilter<SecondaryObjectTernaryObjectColumns>> where, OrderBy<SecondaryObjectTernaryObjectColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public SecondaryObjectTernaryObjectQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static SecondaryObjectTernaryObjectQuery Where(WhereDelegate<SecondaryObjectTernaryObjectColumns> where)
        {
            return Where(where, null, null);
        }

        public static SecondaryObjectTernaryObjectQuery Where(WhereDelegate<SecondaryObjectTernaryObjectColumns> where, OrderBy<SecondaryObjectTernaryObjectColumns> orderBy = null, Database db = null)
        {
            return new SecondaryObjectTernaryObjectQuery(where, orderBy, db);
        }

		public SecondaryObjectTernaryObjectCollection Execute()
		{
			return new SecondaryObjectTernaryObjectCollection(this, true);
		}
    }
}