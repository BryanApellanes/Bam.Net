/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.OleDb;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data.OleDb
{
	public class OleDbConnectionStringResolver: IConnectionStringResolver
	{
		public OleDbConnectionStringResolver(string provider, string dataSource, string name = null)
		{
            Provider = provider;
            DataSource = dataSource;
            ConnectionName = name.Or(8.RandomLetters());
		}
        public string ConnectionName { get; set; }
		public string Provider { get; set; }
        public string DataSource { get; set; }
        
		#region IConnectionStringResolver Members

		public ConnectionStringSettings Resolve(string connectionName)
        {
            ConnectionStringSettings s = new ConnectionStringSettings();
            s.Name = connectionName;
            s.ProviderName = typeof(OleDbFactory).AssemblyQualifiedName;

            DbConnectionStringBuilder connectionStringBuilder = GetConnectionStringBuilder();

            s.ConnectionString = connectionStringBuilder.ConnectionString;

            return s;
        }

        public DbConnectionStringBuilder GetConnectionStringBuilder()
        {
            DbConnectionStringBuilder connectionStringBuilder = OleDbFactory.Instance.CreateConnectionStringBuilder();
            connectionStringBuilder.Add("Data Source", DataSource);
            connectionStringBuilder.Add("Provider", Provider);
            return connectionStringBuilder;
        }

        #endregion
    }
}
