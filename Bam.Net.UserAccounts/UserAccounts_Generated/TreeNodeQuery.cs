/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.UserAccounts.Data
{
    public class TreeNodeQuery: Query<TreeNodeColumns, TreeNode>
    { 
		public TreeNodeQuery(){}
		public TreeNodeQuery(WhereDelegate<TreeNodeColumns> where, OrderBy<TreeNodeColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public TreeNodeQuery(Func<TreeNodeColumns, QueryFilter<TreeNodeColumns>> where, OrderBy<TreeNodeColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public TreeNodeQuery(Delegate where, Database db = null) : base(where, db) { }

		public TreeNodeCollection Execute()
		{
			return new TreeNodeCollection(this, true);
		}
    }
}