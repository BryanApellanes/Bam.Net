/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data.Dynamic.Data.Dao
{
    public class DynamicNamespaceDescriptorQuery: Query<DynamicNamespaceDescriptorColumns, DynamicNamespaceDescriptor>
    { 
		public DynamicNamespaceDescriptorQuery(){}
		public DynamicNamespaceDescriptorQuery(WhereDelegate<DynamicNamespaceDescriptorColumns> where, OrderBy<DynamicNamespaceDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public DynamicNamespaceDescriptorQuery(Func<DynamicNamespaceDescriptorColumns, QueryFilter<DynamicNamespaceDescriptorColumns>> where, OrderBy<DynamicNamespaceDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public DynamicNamespaceDescriptorQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static DynamicNamespaceDescriptorQuery Where(WhereDelegate<DynamicNamespaceDescriptorColumns> where)
        {
            return Where(where, null, null);
        }

        public static DynamicNamespaceDescriptorQuery Where(WhereDelegate<DynamicNamespaceDescriptorColumns> where, OrderBy<DynamicNamespaceDescriptorColumns> orderBy = null, Database db = null)
        {
            return new DynamicNamespaceDescriptorQuery(where, orderBy, db);
        }

		public DynamicNamespaceDescriptorCollection Execute()
		{
			return new DynamicNamespaceDescriptorCollection(this, true);
		}
    }
}