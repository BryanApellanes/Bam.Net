/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Translation
{
    public class OtherNamePagedQuery: PagedQuery<OtherNameColumns, OtherName>
    { 
		public OtherNamePagedQuery(OtherNameColumns orderByColumn, OtherNameQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}