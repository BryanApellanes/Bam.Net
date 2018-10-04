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
    public class CodePagedQuery: PagedQuery<CodeColumns, Code>
    { 
		public CodePagedQuery(CodeColumns orderByColumn, CodeQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}