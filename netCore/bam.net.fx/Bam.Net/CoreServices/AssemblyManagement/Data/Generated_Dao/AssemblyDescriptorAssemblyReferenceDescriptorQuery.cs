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
    public class AssemblyDescriptorAssemblyReferenceDescriptorQuery: Query<AssemblyDescriptorAssemblyReferenceDescriptorColumns, AssemblyDescriptorAssemblyReferenceDescriptor>
    { 
		public AssemblyDescriptorAssemblyReferenceDescriptorQuery(){}
		public AssemblyDescriptorAssemblyReferenceDescriptorQuery(WhereDelegate<AssemblyDescriptorAssemblyReferenceDescriptorColumns> where, OrderBy<AssemblyDescriptorAssemblyReferenceDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public AssemblyDescriptorAssemblyReferenceDescriptorQuery(Func<AssemblyDescriptorAssemblyReferenceDescriptorColumns, QueryFilter<AssemblyDescriptorAssemblyReferenceDescriptorColumns>> where, OrderBy<AssemblyDescriptorAssemblyReferenceDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public AssemblyDescriptorAssemblyReferenceDescriptorQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static AssemblyDescriptorAssemblyReferenceDescriptorQuery Where(WhereDelegate<AssemblyDescriptorAssemblyReferenceDescriptorColumns> where)
        {
            return Where(where, null, null);
        }

        public static AssemblyDescriptorAssemblyReferenceDescriptorQuery Where(WhereDelegate<AssemblyDescriptorAssemblyReferenceDescriptorColumns> where, OrderBy<AssemblyDescriptorAssemblyReferenceDescriptorColumns> orderBy = null, Database db = null)
        {
            return new AssemblyDescriptorAssemblyReferenceDescriptorQuery(where, orderBy, db);
        }

		public AssemblyDescriptorAssemblyReferenceDescriptorCollection Execute()
		{
			return new AssemblyDescriptorAssemblyReferenceDescriptorCollection(this, true);
		}
    }
}