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
    public class DynamicTypePropertyDescriptorQuery: Query<DynamicTypePropertyDescriptorColumns, DynamicTypePropertyDescriptor>
    { 
		public DynamicTypePropertyDescriptorQuery(){}
		public DynamicTypePropertyDescriptorQuery(WhereDelegate<DynamicTypePropertyDescriptorColumns> where, OrderBy<DynamicTypePropertyDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public DynamicTypePropertyDescriptorQuery(Func<DynamicTypePropertyDescriptorColumns, QueryFilter<DynamicTypePropertyDescriptorColumns>> where, OrderBy<DynamicTypePropertyDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public DynamicTypePropertyDescriptorQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static DynamicTypePropertyDescriptorQuery Where(WhereDelegate<DynamicTypePropertyDescriptorColumns> where)
        {
            return Where(where, null, null);
        }

        public static DynamicTypePropertyDescriptorQuery Where(WhereDelegate<DynamicTypePropertyDescriptorColumns> where, OrderBy<DynamicTypePropertyDescriptorColumns> orderBy = null, Database db = null)
        {
            return new DynamicTypePropertyDescriptorQuery(where, orderBy, db);
        }

		public DynamicTypePropertyDescriptorCollection Execute()
		{
			return new DynamicTypePropertyDescriptorCollection(this, true);
		}
    }
}