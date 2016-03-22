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
    public class ShopperPagedQuery: PagedQuery<ShopperColumns, Shopper>
    { 
		public ShopperPagedQuery(ShopperColumns orderByColumn, ShopperQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}