/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Presentation.Unicode
{
    public class EmojiPagedQuery: PagedQuery<EmojiColumns, Emoji>
    { 
		public EmojiPagedQuery(EmojiColumns orderByColumn, EmojiQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}