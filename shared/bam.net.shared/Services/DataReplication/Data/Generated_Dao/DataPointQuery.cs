/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Services.DataReplication.Data.Dao
{
    public class DataPointQuery: Query<DataPointColumns, DataPoint>
    { 
		public DataPointQuery(){}
		public DataPointQuery(WhereDelegate<DataPointColumns> where, OrderBy<DataPointColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public DataPointQuery(Func<DataPointColumns, QueryFilter<DataPointColumns>> where, OrderBy<DataPointColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public DataPointQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static DataPointQuery Where(WhereDelegate<DataPointColumns> where)
        {
            return Where(where, null, null);
        }

        public static DataPointQuery Where(WhereDelegate<DataPointColumns> where, OrderBy<DataPointColumns> orderBy = null, Database db = null)
        {
            return new DataPointQuery(where, orderBy, db);
        }

		public DataPointCollection Execute()
		{
			return new DataPointCollection(this, true);
		}
    }
}