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
    public class AssemblyReferenceDescriptorAssemblyDescriptorQuery: Query<AssemblyReferenceDescriptorAssemblyDescriptorColumns, AssemblyReferenceDescriptorAssemblyDescriptor>
    { 
		public AssemblyReferenceDescriptorAssemblyDescriptorQuery(){}
		public AssemblyReferenceDescriptorAssemblyDescriptorQuery(WhereDelegate<AssemblyReferenceDescriptorAssemblyDescriptorColumns> where, OrderBy<AssemblyReferenceDescriptorAssemblyDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public AssemblyReferenceDescriptorAssemblyDescriptorQuery(Func<AssemblyReferenceDescriptorAssemblyDescriptorColumns, QueryFilter<AssemblyReferenceDescriptorAssemblyDescriptorColumns>> where, OrderBy<AssemblyReferenceDescriptorAssemblyDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public AssemblyReferenceDescriptorAssemblyDescriptorQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static AssemblyReferenceDescriptorAssemblyDescriptorQuery Where(WhereDelegate<AssemblyReferenceDescriptorAssemblyDescriptorColumns> where)
        {
            return Where(where, null, null);
        }

        public static AssemblyReferenceDescriptorAssemblyDescriptorQuery Where(WhereDelegate<AssemblyReferenceDescriptorAssemblyDescriptorColumns> where, OrderBy<AssemblyReferenceDescriptorAssemblyDescriptorColumns> orderBy = null, Database db = null)
        {
            return new AssemblyReferenceDescriptorAssemblyDescriptorQuery(where, orderBy, db);
        }

		public AssemblyReferenceDescriptorAssemblyDescriptorCollection Execute()
		{
			return new AssemblyReferenceDescriptorAssemblyDescriptorCollection(this, true);
		}
    }
}