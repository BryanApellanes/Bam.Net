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
    public class ConfigurationPagedQuery: PagedQuery<ConfigurationColumns, Configuration>
    { 
		public ConfigurationPagedQuery(ConfigurationColumns orderByColumn, ConfigurationQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}