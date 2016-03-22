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
    public class ItemPagedQuery: PagedQuery<ItemColumns, Item>
    { 
		public ItemPagedQuery(ItemColumns orderByColumn, ItemQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}