/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Analytics.EnglishDictionary
{
    public class WordCollection: DaoCollection<WordColumns, Word>
    { 
		public WordCollection(){}
		public WordCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public WordCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public WordCollection(Query<WordColumns, Word> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public WordCollection(Database db, Query<WordColumns, Word> q, bool load) : base(db, q, load) { }
		public WordCollection(Query<WordColumns, Word> q, bool load) : base(q, load) { }
    }
}