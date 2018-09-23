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
    public class ProcessRuntimeDescriptorQuery: Query<ProcessRuntimeDescriptorColumns, ProcessRuntimeDescriptor>
    { 
		public ProcessRuntimeDescriptorQuery(){}
		public ProcessRuntimeDescriptorQuery(WhereDelegate<ProcessRuntimeDescriptorColumns> where, OrderBy<ProcessRuntimeDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ProcessRuntimeDescriptorQuery(Func<ProcessRuntimeDescriptorColumns, QueryFilter<ProcessRuntimeDescriptorColumns>> where, OrderBy<ProcessRuntimeDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ProcessRuntimeDescriptorQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ProcessRuntimeDescriptorQuery Where(WhereDelegate<ProcessRuntimeDescriptorColumns> where)
        {
            return Where(where, null, null);
        }

        public static ProcessRuntimeDescriptorQuery Where(WhereDelegate<ProcessRuntimeDescriptorColumns> where, OrderBy<ProcessRuntimeDescriptorColumns> orderBy = null, Database db = null)
        {
            return new ProcessRuntimeDescriptorQuery(where, orderBy, db);
        }

		public ProcessRuntimeDescriptorCollection Execute()
		{
			return new ProcessRuntimeDescriptorCollection(this, true);
		}
    }
}