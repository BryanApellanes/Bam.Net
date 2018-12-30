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
    public class WordPagedQuery: PagedQuery<WordColumns, Word>
    { 
		public WordPagedQuery(WordColumns orderByColumn, WordQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}