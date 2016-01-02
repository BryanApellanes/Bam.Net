/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data.Repositories.Tests
{
    public class SecondaryObjectPagedQuery: PagedQuery<SecondaryObjectColumns, SecondaryObject>
    { 
		public SecondaryObjectPagedQuery(SecondaryObjectColumns orderByColumn, SecondaryObjectQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}