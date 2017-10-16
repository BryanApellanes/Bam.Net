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
    public class DataInstanceQuery: Query<DataInstanceColumns, DataInstance>
    { 
		public DataInstanceQuery(){}
		public DataInstanceQuery(WhereDelegate<DataInstanceColumns> where, OrderBy<DataInstanceColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public DataInstanceQuery(Func<DataInstanceColumns, QueryFilter<DataInstanceColumns>> where, OrderBy<DataInstanceColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public DataInstanceQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static DataInstanceQuery Where(WhereDelegate<DataInstanceColumns> where)
        {
            return Where(where, null, null);
        }

        public static DataInstanceQuery Where(WhereDelegate<DataInstanceColumns> where, OrderBy<DataInstanceColumns> orderBy = null, Database db = null)
        {
            return new DataInstanceQuery(where, orderBy, db);
        }

		public DataInstanceCollection Execute()
		{
			return new DataInstanceCollection(this, true);
		}
    }
}