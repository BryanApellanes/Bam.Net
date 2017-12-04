/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ServiceRegistration.Data.Dao
{
    public class MachineServicesQuery: Query<MachineServicesColumns, MachineServices>
    { 
		public MachineServicesQuery(){}
		public MachineServicesQuery(WhereDelegate<MachineServicesColumns> where, OrderBy<MachineServicesColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public MachineServicesQuery(Func<MachineServicesColumns, QueryFilter<MachineServicesColumns>> where, OrderBy<MachineServicesColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public MachineServicesQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static MachineServicesQuery Where(WhereDelegate<MachineServicesColumns> where)
        {
            return Where(where, null, null);
        }

        public static MachineServicesQuery Where(WhereDelegate<MachineServicesColumns> where, OrderBy<MachineServicesColumns> orderBy = null, Database db = null)
        {
            return new MachineServicesQuery(where, orderBy, db);
        }

		public MachineServicesCollection Execute()
		{
			return new MachineServicesCollection(this, true);
		}
    }
}