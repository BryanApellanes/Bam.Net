/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Services.OpenApi
{
    public class ObjectDescriptorQuery: Query<ObjectDescriptorColumns, ObjectDescriptor>
    { 
		public ObjectDescriptorQuery(){}
		public ObjectDescriptorQuery(WhereDelegate<ObjectDescriptorColumns> where, OrderBy<ObjectDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ObjectDescriptorQuery(Func<ObjectDescriptorColumns, QueryFilter<ObjectDescriptorColumns>> where, OrderBy<ObjectDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ObjectDescriptorQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ObjectDescriptorQuery Where(WhereDelegate<ObjectDescriptorColumns> where)
        {
            return Where(where, null, null);
        }

        public static ObjectDescriptorQuery Where(WhereDelegate<ObjectDescriptorColumns> where, OrderBy<ObjectDescriptorColumns> orderBy = null, Database db = null)
        {
            return new ObjectDescriptorQuery(where, orderBy, db);
        }

		public ObjectDescriptorCollection Execute()
		{
			return new ObjectDescriptorCollection(this, true);
		}
    }
}