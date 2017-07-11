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
    public class AssemblyDescriptorProcessRuntimeDescriptorQuery: Query<AssemblyDescriptorProcessRuntimeDescriptorColumns, AssemblyDescriptorProcessRuntimeDescriptor>
    { 
		public AssemblyDescriptorProcessRuntimeDescriptorQuery(){}
		public AssemblyDescriptorProcessRuntimeDescriptorQuery(WhereDelegate<AssemblyDescriptorProcessRuntimeDescriptorColumns> where, OrderBy<AssemblyDescriptorProcessRuntimeDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public AssemblyDescriptorProcessRuntimeDescriptorQuery(Func<AssemblyDescriptorProcessRuntimeDescriptorColumns, QueryFilter<AssemblyDescriptorProcessRuntimeDescriptorColumns>> where, OrderBy<AssemblyDescriptorProcessRuntimeDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public AssemblyDescriptorProcessRuntimeDescriptorQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static AssemblyDescriptorProcessRuntimeDescriptorQuery Where(WhereDelegate<AssemblyDescriptorProcessRuntimeDescriptorColumns> where)
        {
            return Where(where, null, null);
        }

        public static AssemblyDescriptorProcessRuntimeDescriptorQuery Where(WhereDelegate<AssemblyDescriptorProcessRuntimeDescriptorColumns> where, OrderBy<AssemblyDescriptorProcessRuntimeDescriptorColumns> orderBy = null, Database db = null)
        {
            return new AssemblyDescriptorProcessRuntimeDescriptorQuery(where, orderBy, db);
        }

		public AssemblyDescriptorProcessRuntimeDescriptorCollection Execute()
		{
			return new AssemblyDescriptorProcessRuntimeDescriptorCollection(this, true);
		}
    }
}