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
    public class LanguagePagedQuery: PagedQuery<LanguageColumns, Language>
    { 
		public LanguagePagedQuery(LanguageColumns orderByColumn, LanguageQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}