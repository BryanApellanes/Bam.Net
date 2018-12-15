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
    public class DataRelationshipQuery: Query<DataRelationshipColumns, DataRelationship>
    { 
		public DataRelationshipQuery(){}
		public DataRelationshipQuery(WhereDelegate<DataRelationshipColumns> where, OrderBy<DataRelationshipColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public DataRelationshipQuery(Func<DataRelationshipColumns, QueryFilter<DataRelationshipColumns>> where, OrderBy<DataRelationshipColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public DataRelationshipQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static DataRelationshipQuery Where(WhereDelegate<DataRelationshipColumns> where)
        {
            return Where(where, null, null);
        }

        public static DataRelationshipQuery Where(WhereDelegate<DataRelationshipColumns> where, OrderBy<DataRelationshipColumns> orderBy = null, Database db = null)
        {
            return new DataRelationshipQuery(where, orderBy, db);
        }

		public DataRelationshipCollection Execute()
		{
			return new DataRelationshipCollection(this, true);
		}
    }
}