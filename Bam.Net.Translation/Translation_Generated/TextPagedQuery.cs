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
    public class TextPagedQuery: PagedQuery<TextColumns, Text>
    { 
		public TextPagedQuery(TextColumns orderByColumn, TextQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}