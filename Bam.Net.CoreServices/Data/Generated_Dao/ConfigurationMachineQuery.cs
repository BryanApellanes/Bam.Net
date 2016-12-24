/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Data.Daos
{
    public class ConfigurationMachineQuery: Query<ConfigurationMachineColumns, ConfigurationMachine>
    { 
		public ConfigurationMachineQuery(){}
		public ConfigurationMachineQuery(WhereDelegate<ConfigurationMachineColumns> where, OrderBy<ConfigurationMachineColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ConfigurationMachineQuery(Func<ConfigurationMachineColumns, QueryFilter<ConfigurationMachineColumns>> where, OrderBy<ConfigurationMachineColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ConfigurationMachineQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ConfigurationMachineQuery Where(WhereDelegate<ConfigurationMachineColumns> where)
        {
            return Where(where, null, null);
        }

        public static ConfigurationMachineQuery Where(WhereDelegate<ConfigurationMachineColumns> where, OrderBy<ConfigurationMachineColumns> orderBy = null, Database db = null)
        {
            return new ConfigurationMachineQuery(where, orderBy, db);
        }

		public ConfigurationMachineCollection Execute()
		{
			return new ConfigurationMachineCollection(this, true);
		}
    }
}