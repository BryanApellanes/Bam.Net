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
    public class MachineRegistriesQuery: Query<MachineRegistriesColumns, MachineRegistries>
    { 
		public MachineRegistriesQuery(){}
		public MachineRegistriesQuery(WhereDelegate<MachineRegistriesColumns> where, OrderBy<MachineRegistriesColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public MachineRegistriesQuery(Func<MachineRegistriesColumns, QueryFilter<MachineRegistriesColumns>> where, OrderBy<MachineRegistriesColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public MachineRegistriesQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static MachineRegistriesQuery Where(WhereDelegate<MachineRegistriesColumns> where)
        {
            return Where(where, null, null);
        }

        public static MachineRegistriesQuery Where(WhereDelegate<MachineRegistriesColumns> where, OrderBy<MachineRegistriesColumns> orderBy = null, Database db = null)
        {
            return new MachineRegistriesQuery(where, orderBy, db);
        }

		public MachineRegistriesCollection Execute()
		{
			return new MachineRegistriesCollection(this, true);
		}
    }
}