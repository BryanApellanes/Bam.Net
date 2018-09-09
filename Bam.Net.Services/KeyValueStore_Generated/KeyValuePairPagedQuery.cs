/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Services.Data
{
    public class KeyValuePairPagedQuery: PagedQuery<KeyValuePairColumns, KeyValuePair>
    { 
		public KeyValuePairPagedQuery(KeyValuePairColumns orderByColumn, KeyValuePairQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}