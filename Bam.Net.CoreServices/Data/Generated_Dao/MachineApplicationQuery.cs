/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Data.Daos
{
    public class MachineApplicationQuery: Query<MachineApplicationColumns, MachineApplication>
    { 
		public MachineApplicationQuery(){}
		public MachineApplicationQuery(WhereDelegate<MachineApplicationColumns> where, OrderBy<MachineApplicationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public MachineApplicationQuery(Func<MachineApplicationColumns, QueryFilter<MachineApplicationColumns>> where, OrderBy<MachineApplicationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public MachineApplicationQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static MachineApplicationQuery Where(WhereDelegate<MachineApplicationColumns> where)
        {
            return Where(where, null, null);
        }

        public static MachineApplicationQuery Where(WhereDelegate<MachineApplicationColumns> where, OrderBy<MachineApplicationColumns> orderBy = null, Database db = null)
        {
            return new MachineApplicationQuery(where, orderBy, db);
        }

		public MachineApplicationCollection Execute()
		{
			return new MachineApplicationCollection(this, true);
		}
    }
}