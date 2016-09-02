/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class ImageTagQuery: Query<ImageTagColumns, ImageTag>
    { 
		public ImageTagQuery(){}
		public ImageTagQuery(WhereDelegate<ImageTagColumns> where, OrderBy<ImageTagColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ImageTagQuery(Func<ImageTagColumns, QueryFilter<ImageTagColumns>> where, OrderBy<ImageTagColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ImageTagQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ImageTagQuery Where(WhereDelegate<ImageTagColumns> where)
        {
            return Where(where, null, null);
        }

        public static ImageTagQuery Where(WhereDelegate<ImageTagColumns> where, OrderBy<ImageTagColumns> orderBy = null, Database db = null)
        {
            return new ImageTagQuery(where, orderBy, db);
        }

		public ImageTagCollection Execute()
		{
			return new ImageTagCollection(this, true);
		}
    }
}