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
    public class TextCollection: DaoCollection<TextColumns, Text>
    { 
		public TextCollection(){}
		public TextCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public TextCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public TextCollection(Query<TextColumns, Text> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public TextCollection(Database db, Query<TextColumns, Text> q, bool load) : base(db, q, load) { }
		public TextCollection(Query<TextColumns, Text> q, bool load) : base(q, load) { }
    }
}