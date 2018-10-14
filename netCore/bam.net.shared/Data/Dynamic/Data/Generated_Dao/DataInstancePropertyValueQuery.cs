/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data.Dynamic.Data.Dao
{
    public class DataInstancePropertyValueQuery: Query<DataInstancePropertyValueColumns, DataInstancePropertyValue>
    { 
		public DataInstancePropertyValueQuery(){}
		public DataInstancePropertyValueQuery(WhereDelegate<DataInstancePropertyValueColumns> where, OrderBy<DataInstancePropertyValueColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public DataInstancePropertyValueQuery(Func<DataInstancePropertyValueColumns, QueryFilter<DataInstancePropertyValueColumns>> where, OrderBy<DataInstancePropertyValueColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public DataInstancePropertyValueQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static DataInstancePropertyValueQuery Where(WhereDelegate<DataInstancePropertyValueColumns> where)
        {
            return Where(where, null, null);
        }

        public static DataInstancePropertyValueQuery Where(WhereDelegate<DataInstancePropertyValueColumns> where, OrderBy<DataInstancePropertyValueColumns> orderBy = null, Database db = null)
        {
            return new DataInstancePropertyValueQuery(where, orderBy, db);
        }

		public DataInstancePropertyValueCollection Execute()
		{
			return new DataInstancePropertyValueCollection(this, true);
		}
    }
}