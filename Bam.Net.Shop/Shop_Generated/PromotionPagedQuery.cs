/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Shop
{
    public class PromotionPagedQuery: PagedQuery<PromotionColumns, Promotion>
    { 
		public PromotionPagedQuery(PromotionColumns orderByColumn, PromotionQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}