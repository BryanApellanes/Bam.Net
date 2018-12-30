/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Analytics.EnglishDictionary
{
    public class DefinitionQuery: Query<DefinitionColumns, Definition>
    { 
		public DefinitionQuery(){}
		public DefinitionQuery(WhereDelegate<DefinitionColumns> where, OrderBy<DefinitionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public DefinitionQuery(Func<DefinitionColumns, QueryFilter<DefinitionColumns>> where, OrderBy<DefinitionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public DefinitionQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static DefinitionQuery Where(WhereDelegate<DefinitionColumns> where)
        {
            return Where(where, null, null);
        }

        public static DefinitionQuery Where(WhereDelegate<DefinitionColumns> where, OrderBy<DefinitionColumns> orderBy = null, Database db = null)
        {
            return new DefinitionQuery(where, orderBy, db);
        }

		public DefinitionCollection Execute()
		{
			return new DefinitionCollection(this, true);
		}
    }
}