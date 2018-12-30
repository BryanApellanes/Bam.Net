/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Presentation.Unicode
{
    public class EmojiCollection: DaoCollection<EmojiColumns, Emoji>
    { 
		public EmojiCollection(){}
		public EmojiCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public EmojiCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public EmojiCollection(Query<EmojiColumns, Emoji> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public EmojiCollection(Database db, Query<EmojiColumns, Emoji> q, bool load) : base(db, q, load) { }
		public EmojiCollection(Query<EmojiColumns, Emoji> q, bool load) : base(q, load) { }
    }
}