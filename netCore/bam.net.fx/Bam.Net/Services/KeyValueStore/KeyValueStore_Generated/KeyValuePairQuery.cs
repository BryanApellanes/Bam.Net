/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Services.Data
{
    public class KeyValuePairQuery: Query<KeyValuePairColumns, KeyValuePair>
    { 
		public KeyValuePairQuery(){}
		public KeyValuePairQuery(WhereDelegate<KeyValuePairColumns> where, OrderBy<KeyValuePairColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public KeyValuePairQuery(Func<KeyValuePairColumns, QueryFilter<KeyValuePairColumns>> where, OrderBy<KeyValuePairColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public KeyValuePairQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static KeyValuePairQuery Where(WhereDelegate<KeyValuePairColumns> where)
        {
            return Where(where, null, null);
        }

        public static KeyValuePairQuery Where(WhereDelegate<KeyValuePairColumns> where, OrderBy<KeyValuePairColumns> orderBy = null, Database db = null)
        {
            return new KeyValuePairQuery(where, orderBy, db);
        }

		public KeyValuePairCollection Execute()
		{
			return new KeyValuePairCollection(this, true);
		}
    }
}