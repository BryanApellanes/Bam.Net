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
    public class LanguageDetectionQuery: Query<LanguageDetectionColumns, LanguageDetection>
    { 
		public LanguageDetectionQuery(){}
		public LanguageDetectionQuery(WhereDelegate<LanguageDetectionColumns> where, OrderBy<LanguageDetectionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public LanguageDetectionQuery(Func<LanguageDetectionColumns, QueryFilter<LanguageDetectionColumns>> where, OrderBy<LanguageDetectionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public LanguageDetectionQuery(Delegate where, Database db = null) : base(where, db) { }

		public LanguageDetectionCollection Execute()
		{
			return new LanguageDetectionCollection(this, true);
		}
    }
}