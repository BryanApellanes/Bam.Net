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
    public class TranslationQuery: Query<TranslationColumns, Translation>
    { 
		public TranslationQuery(){}
		public TranslationQuery(WhereDelegate<TranslationColumns> where, OrderBy<TranslationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public TranslationQuery(Func<TranslationColumns, QueryFilter<TranslationColumns>> where, OrderBy<TranslationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public TranslationQuery(Delegate where, Database db = null) : base(where, db) { }

		public TranslationCollection Execute()
		{
			return new TranslationCollection(this, true);
		}
    }
}