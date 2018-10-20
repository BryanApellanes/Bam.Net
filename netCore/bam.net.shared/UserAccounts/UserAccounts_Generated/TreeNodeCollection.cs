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
    public class TreeNodeCollection: DaoCollection<TreeNodeColumns, TreeNode>
    { 
		public TreeNodeCollection(){}
		public TreeNodeCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public TreeNodeCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public TreeNodeCollection(Query<TreeNodeColumns, TreeNode> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public TreeNodeCollection(Database db, Query<TreeNodeColumns, TreeNode> q, bool load) : base(db, q, load) { }
		public TreeNodeCollection(Query<TreeNodeColumns, TreeNode> q, bool load) : base(q, load) { }
    }
}