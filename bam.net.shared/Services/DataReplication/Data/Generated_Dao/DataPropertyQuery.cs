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
    public class DataPropertyQuery: Query<DataPropertyColumns, DataProperty>
    { 
		public DataPropertyQuery(){}
		public DataPropertyQuery(WhereDelegate<DataPropertyColumns> where, OrderBy<DataPropertyColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public DataPropertyQuery(Func<DataPropertyColumns, QueryFilter<DataPropertyColumns>> where, OrderBy<DataPropertyColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public DataPropertyQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static DataPropertyQuery Where(WhereDelegate<DataPropertyColumns> where)
        {
            return Where(where, null, null);
        }

        public static DataPropertyQuery Where(WhereDelegate<DataPropertyColumns> where, OrderBy<DataPropertyColumns> orderBy = null, Database db = null)
        {
            return new DataPropertyQuery(where, orderBy, db);
        }

		public DataPropertyCollection Execute()
		{
			return new DataPropertyCollection(this, true);
		}
    }
}