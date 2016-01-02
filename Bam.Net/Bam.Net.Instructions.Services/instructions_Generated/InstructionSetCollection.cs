/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Instructions
{
    public class InstructionSetCollection: DaoCollection<InstructionSetColumns, InstructionSet>
    { 
		public InstructionSetCollection(){}
		public InstructionSetCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public InstructionSetCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public InstructionSetCollection(Query<InstructionSetColumns, InstructionSet> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public InstructionSetCollection(Database db, Query<InstructionSetColumns, InstructionSet> q, bool load) : base(db, q, load) { }
		public InstructionSetCollection(Query<InstructionSetColumns, InstructionSet> q, bool load) : base(q, load) { }
    }
}