/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Translation
{
    public class TextQuery: Query<TextColumns, Text>
    { 
		public TextQuery(){}
		public TextQuery(WhereDelegate<TextColumns> where, OrderBy<TextColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public TextQuery(Func<TextColumns, QueryFilter<TextColumns>> where, OrderBy<TextColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public TextQuery(Delegate where, Database db = null) : base(where, db) { }

		public TextCollection Execute()
		{
			return new TextCollection(this, true);
		}
    }
}