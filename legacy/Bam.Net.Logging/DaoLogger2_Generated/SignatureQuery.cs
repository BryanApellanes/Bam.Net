/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Logging.Data
{
    public class SignatureQuery: Query<SignatureColumns, Signature>
    { 
		public SignatureQuery(){}
		public SignatureQuery(WhereDelegate<SignatureColumns> where, OrderBy<SignatureColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public SignatureQuery(Func<SignatureColumns, QueryFilter<SignatureColumns>> where, OrderBy<SignatureColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public SignatureQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static SignatureQuery Where(WhereDelegate<SignatureColumns> where)
        {
            return Where(where, null, null);
        }

        public static SignatureQuery Where(WhereDelegate<SignatureColumns> where, OrderBy<SignatureColumns> orderBy = null, Database db = null)
        {
            return new SignatureQuery(where, orderBy, db);
        }

		public SignatureCollection Execute()
		{
			return new SignatureCollection(this, true);
		}
    }
}