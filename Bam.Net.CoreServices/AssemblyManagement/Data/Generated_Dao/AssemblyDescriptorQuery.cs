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
    public class AssemblyDescriptorQuery: Query<AssemblyDescriptorColumns, AssemblyDescriptor>
    { 
		public AssemblyDescriptorQuery(){}
		public AssemblyDescriptorQuery(WhereDelegate<AssemblyDescriptorColumns> where, OrderBy<AssemblyDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public AssemblyDescriptorQuery(Func<AssemblyDescriptorColumns, QueryFilter<AssemblyDescriptorColumns>> where, OrderBy<AssemblyDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public AssemblyDescriptorQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static AssemblyDescriptorQuery Where(WhereDelegate<AssemblyDescriptorColumns> where)
        {
            return Where(where, null, null);
        }

        public static AssemblyDescriptorQuery Where(WhereDelegate<AssemblyDescriptorColumns> where, OrderBy<AssemblyDescriptorColumns> orderBy = null, Database db = null)
        {
            return new AssemblyDescriptorQuery(where, orderBy, db);
        }

		public AssemblyDescriptorCollection Execute()
		{
			return new AssemblyDescriptorCollection(this, true);
		}
    }
}