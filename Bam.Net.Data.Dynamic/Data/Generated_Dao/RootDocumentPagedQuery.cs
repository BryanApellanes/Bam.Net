/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data.Dynamic.Data.Dao
{
    public class RootDocumentPagedQuery: PagedQuery<RootDocumentColumns, RootDocument>
    { 
		public RootDocumentPagedQuery(RootDocumentColumns orderByColumn, RootDocumentQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}