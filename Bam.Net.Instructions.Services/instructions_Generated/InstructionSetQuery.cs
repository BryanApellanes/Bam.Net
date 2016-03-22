/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Instructions
{
    public class InstructionSetQuery: Query<InstructionSetColumns, InstructionSet>
    { 
		public InstructionSetQuery(){}
		public InstructionSetQuery(WhereDelegate<InstructionSetColumns> where, OrderBy<InstructionSetColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public InstructionSetQuery(Func<InstructionSetColumns, QueryFilter<InstructionSetColumns>> where, OrderBy<InstructionSetColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public InstructionSetQuery(Delegate where, Database db = null) : base(where, db) { }

		public InstructionSetCollection Execute()
		{
			return new InstructionSetCollection(this, true);
		}
    }
}