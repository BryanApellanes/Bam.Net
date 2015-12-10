/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class ProtocolQuery: Query<ProtocolColumns, Protocol>
    { 
		public ProtocolQuery(){}
		public ProtocolQuery(WhereDelegate<ProtocolColumns> where, OrderBy<ProtocolColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ProtocolQuery(Func<ProtocolColumns, QueryFilter<ProtocolColumns>> where, OrderBy<ProtocolColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ProtocolQuery(Delegate where, Database db = null) : base(where, db) { }

		public ProtocolCollection Execute()
		{
			return new ProtocolCollection(this, true);
		}
    }
}