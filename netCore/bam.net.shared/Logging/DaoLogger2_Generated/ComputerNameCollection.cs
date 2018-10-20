/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Logging.Data
{
    public class ComputerNameCollection: DaoCollection<ComputerNameColumns, ComputerName>
    { 
		public ComputerNameCollection(){}
		public ComputerNameCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ComputerNameCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ComputerNameCollection(Query<ComputerNameColumns, ComputerName> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ComputerNameCollection(Database db, Query<ComputerNameColumns, ComputerName> q, bool load) : base(db, q, load) { }
		public ComputerNameCollection(Query<ComputerNameColumns, ComputerName> q, bool load) : base(q, load) { }
    }
}