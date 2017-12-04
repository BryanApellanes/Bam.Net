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
    public class ApplicationMachineQuery: Query<ApplicationMachineColumns, ApplicationMachine>
    { 
		public ApplicationMachineQuery(){}
		public ApplicationMachineQuery(WhereDelegate<ApplicationMachineColumns> where, OrderBy<ApplicationMachineColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ApplicationMachineQuery(Func<ApplicationMachineColumns, QueryFilter<ApplicationMachineColumns>> where, OrderBy<ApplicationMachineColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ApplicationMachineQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ApplicationMachineQuery Where(WhereDelegate<ApplicationMachineColumns> where)
        {
            return Where(where, null, null);
        }

        public static ApplicationMachineQuery Where(WhereDelegate<ApplicationMachineColumns> where, OrderBy<ApplicationMachineColumns> orderBy = null, Database db = null)
        {
            return new ApplicationMachineQuery(where, orderBy, db);
        }

		public ApplicationMachineCollection Execute()
		{
			return new ApplicationMachineCollection(this, true);
		}
    }
}