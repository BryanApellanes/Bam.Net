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
    public class LanguageDetectionCollection: DaoCollection<LanguageDetectionColumns, LanguageDetection>
    { 
		public LanguageDetectionCollection(){}
		public LanguageDetectionCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public LanguageDetectionCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public LanguageDetectionCollection(Query<LanguageDetectionColumns, LanguageDetection> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public LanguageDetectionCollection(Database db, Query<LanguageDetectionColumns, LanguageDetection> q, bool load) : base(db, q, load) { }
		public LanguageDetectionCollection(Query<LanguageDetectionColumns, LanguageDetection> q, bool load) : base(q, load) { }
    }
}