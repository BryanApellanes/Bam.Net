/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Encryption
{
    public class VaultItemPagedQuery: PagedQuery<VaultItemColumns, VaultItem>
    { 
		public VaultItemPagedQuery(VaultItemColumns orderByColumn, VaultItemQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}