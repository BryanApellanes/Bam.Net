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
    public class ProcessDescriptorQuery: Query<ProcessDescriptorColumns, ProcessDescriptor>
    { 
		public ProcessDescriptorQuery(){}
		public ProcessDescriptorQuery(WhereDelegate<ProcessDescriptorColumns> where, OrderBy<ProcessDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ProcessDescriptorQuery(Func<ProcessDescriptorColumns, QueryFilter<ProcessDescriptorColumns>> where, OrderBy<ProcessDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ProcessDescriptorQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ProcessDescriptorQuery Where(WhereDelegate<ProcessDescriptorColumns> where)
        {
            return Where(where, null, null);
        }

        public static ProcessDescriptorQuery Where(WhereDelegate<ProcessDescriptorColumns> where, OrderBy<ProcessDescriptorColumns> orderBy = null, Database db = null)
        {
            return new ProcessDescriptorQuery(where, orderBy, db);
        }

		public ProcessDescriptorCollection Execute()
		{
			return new ProcessDescriptorCollection(this, true);
		}
    }
}