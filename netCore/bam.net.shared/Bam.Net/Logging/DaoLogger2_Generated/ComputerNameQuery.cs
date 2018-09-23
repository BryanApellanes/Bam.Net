/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Logging.Data
{
    public class ComputerNameQuery: Query<ComputerNameColumns, ComputerName>
    { 
		public ComputerNameQuery(){}
		public ComputerNameQuery(WhereDelegate<ComputerNameColumns> where, OrderBy<ComputerNameColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ComputerNameQuery(Func<ComputerNameColumns, QueryFilter<ComputerNameColumns>> where, OrderBy<ComputerNameColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ComputerNameQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ComputerNameQuery Where(WhereDelegate<ComputerNameColumns> where)
        {
            return Where(where, null, null);
        }

        public static ComputerNameQuery Where(WhereDelegate<ComputerNameColumns> where, OrderBy<ComputerNameColumns> orderBy = null, Database db = null)
        {
            return new ComputerNameQuery(where, orderBy, db);
        }

		public ComputerNameCollection Execute()
		{
			return new ComputerNameCollection(this, true);
		}
    }
}