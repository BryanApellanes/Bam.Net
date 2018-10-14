/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class MachineQuery: Query<MachineColumns, Machine>
    { 
		public MachineQuery(){}
		public MachineQuery(WhereDelegate<MachineColumns> where, OrderBy<MachineColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public MachineQuery(Func<MachineColumns, QueryFilter<MachineColumns>> where, OrderBy<MachineColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public MachineQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static MachineQuery Where(WhereDelegate<MachineColumns> where)
        {
            return Where(where, null, null);
        }

        public static MachineQuery Where(WhereDelegate<MachineColumns> where, OrderBy<MachineColumns> orderBy = null, Database db = null)
        {
            return new MachineQuery(where, orderBy, db);
        }

		public MachineCollection Execute()
		{
			return new MachineCollection(this, true);
		}
    }
}