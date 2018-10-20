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
    public class CategoryPagedQuery: PagedQuery<CategoryColumns, Category>
    { 
		public CategoryPagedQuery(CategoryColumns orderByColumn, CategoryQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}