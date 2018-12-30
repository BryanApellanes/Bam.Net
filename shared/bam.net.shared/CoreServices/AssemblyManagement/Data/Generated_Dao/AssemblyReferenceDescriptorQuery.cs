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
    public class AssemblyReferenceDescriptorQuery: Query<AssemblyReferenceDescriptorColumns, AssemblyReferenceDescriptor>
    { 
		public AssemblyReferenceDescriptorQuery(){}
		public AssemblyReferenceDescriptorQuery(WhereDelegate<AssemblyReferenceDescriptorColumns> where, OrderBy<AssemblyReferenceDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public AssemblyReferenceDescriptorQuery(Func<AssemblyReferenceDescriptorColumns, QueryFilter<AssemblyReferenceDescriptorColumns>> where, OrderBy<AssemblyReferenceDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public AssemblyReferenceDescriptorQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static AssemblyReferenceDescriptorQuery Where(WhereDelegate<AssemblyReferenceDescriptorColumns> where)
        {
            return Where(where, null, null);
        }

        public static AssemblyReferenceDescriptorQuery Where(WhereDelegate<AssemblyReferenceDescriptorColumns> where, OrderBy<AssemblyReferenceDescriptorColumns> orderBy = null, Database db = null)
        {
            return new AssemblyReferenceDescriptorQuery(where, orderBy, db);
        }

		public AssemblyReferenceDescriptorCollection Execute()
		{
			return new AssemblyReferenceDescriptorCollection(this, true);
		}
    }
}