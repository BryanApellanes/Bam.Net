/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.ServiceProxy.Secure
{
    public class ConfigurationQuery: Query<ConfigurationColumns, Configuration>
    { 
		public ConfigurationQuery(){}
		public ConfigurationQuery(WhereDelegate<ConfigurationColumns> where, OrderBy<ConfigurationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ConfigurationQuery(Func<ConfigurationColumns, QueryFilter<ConfigurationColumns>> where, OrderBy<ConfigurationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ConfigurationQuery(Delegate where, Database db = null) : base(where, db) { }

		public ConfigurationCollection Execute()
		{
			return new ConfigurationCollection(this, true);
		}
    }
}