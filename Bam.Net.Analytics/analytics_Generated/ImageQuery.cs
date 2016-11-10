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
    public class ImageQuery: Query<ImageColumns, Image>
    { 
		public ImageQuery(){}
		public ImageQuery(WhereDelegate<ImageColumns> where, OrderBy<ImageColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ImageQuery(Func<ImageColumns, QueryFilter<ImageColumns>> where, OrderBy<ImageColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ImageQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ImageQuery Where(WhereDelegate<ImageColumns> where)
        {
            return Where(where, null, null);
        }

        public static ImageQuery Where(WhereDelegate<ImageColumns> where, OrderBy<ImageColumns> orderBy = null, Database db = null)
        {
            return new ImageQuery(where, orderBy, db);
        }

		public ImageCollection Execute()
		{
			return new ImageCollection(this, true);
		}
    }
}