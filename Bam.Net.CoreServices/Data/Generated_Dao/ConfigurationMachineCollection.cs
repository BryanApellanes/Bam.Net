/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Data.Daos
{
    public class ConfigurationMachineCollection: DaoCollection<ConfigurationMachineColumns, ConfigurationMachine>
    { 
		public ConfigurationMachineCollection(){}
		public ConfigurationMachineCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ConfigurationMachineCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ConfigurationMachineCollection(Query<ConfigurationMachineColumns, ConfigurationMachine> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ConfigurationMachineCollection(Database db, Query<ConfigurationMachineColumns, ConfigurationMachine> q, bool load) : base(db, q, load) { }
		public ConfigurationMachineCollection(Query<ConfigurationMachineColumns, ConfigurationMachine> q, bool load) : base(q, load) { }
    }
}