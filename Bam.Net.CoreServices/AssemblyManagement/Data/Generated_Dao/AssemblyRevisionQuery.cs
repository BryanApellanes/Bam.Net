/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.AssemblyManagement.Data.Dao
{
    public class AssemblyRevisionQuery: Query<AssemblyRevisionColumns, AssemblyRevision>
    { 
		public AssemblyRevisionQuery(){}
		public AssemblyRevisionQuery(WhereDelegate<AssemblyRevisionColumns> where, OrderBy<AssemblyRevisionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public AssemblyRevisionQuery(Func<AssemblyRevisionColumns, QueryFilter<AssemblyRevisionColumns>> where, OrderBy<AssemblyRevisionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public AssemblyRevisionQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static AssemblyRevisionQuery Where(WhereDelegate<AssemblyRevisionColumns> where)
        {
            return Where(where, null, null);
        }

        public static AssemblyRevisionQuery Where(WhereDelegate<AssemblyRevisionColumns> where, OrderBy<AssemblyRevisionColumns> orderBy = null, Database db = null)
        {
            return new AssemblyRevisionQuery(where, orderBy, db);
        }

		public AssemblyRevisionCollection Execute()
		{
			return new AssemblyRevisionCollection(this, true);
		}
    }
}