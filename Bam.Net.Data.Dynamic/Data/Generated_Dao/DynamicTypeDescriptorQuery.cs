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
    public class DynamicTypeDescriptorQuery: Query<DynamicTypeDescriptorColumns, DynamicTypeDescriptor>
    { 
		public DynamicTypeDescriptorQuery(){}
		public DynamicTypeDescriptorQuery(WhereDelegate<DynamicTypeDescriptorColumns> where, OrderBy<DynamicTypeDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public DynamicTypeDescriptorQuery(Func<DynamicTypeDescriptorColumns, QueryFilter<DynamicTypeDescriptorColumns>> where, OrderBy<DynamicTypeDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public DynamicTypeDescriptorQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static DynamicTypeDescriptorQuery Where(WhereDelegate<DynamicTypeDescriptorColumns> where)
        {
            return Where(where, null, null);
        }

        public static DynamicTypeDescriptorQuery Where(WhereDelegate<DynamicTypeDescriptorColumns> where, OrderBy<DynamicTypeDescriptorColumns> orderBy = null, Database db = null)
        {
            return new DynamicTypeDescriptorQuery(where, orderBy, db);
        }

		public DynamicTypeDescriptorCollection Execute()
		{
			return new DynamicTypeDescriptorCollection(this, true);
		}
    }
}