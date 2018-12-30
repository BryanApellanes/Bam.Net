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
    public class SecondaryObjectQuery: Query<SecondaryObjectColumns, SecondaryObject>
    { 
		public SecondaryObjectQuery(){}
		public SecondaryObjectQuery(WhereDelegate<SecondaryObjectColumns> where, OrderBy<SecondaryObjectColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public SecondaryObjectQuery(Func<SecondaryObjectColumns, QueryFilter<SecondaryObjectColumns>> where, OrderBy<SecondaryObjectColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public SecondaryObjectQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static SecondaryObjectQuery Where(WhereDelegate<SecondaryObjectColumns> where)
        {
            return Where(where, null, null);
        }

        public static SecondaryObjectQuery Where(WhereDelegate<SecondaryObjectColumns> where, OrderBy<SecondaryObjectColumns> orderBy = null, Database db = null)
        {
            return new SecondaryObjectQuery(where, orderBy, db);
        }

		public SecondaryObjectCollection Execute()
		{
			return new SecondaryObjectCollection(this, true);
		}
    }
}