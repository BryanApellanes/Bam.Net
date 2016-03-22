/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data.Tests
{
    public class ListItemPagedQuery: PagedQuery<ListItemColumns, ListItem>
    { 
		public ListItemPagedQuery(ListItemColumns orderByColumn, ListItemQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}