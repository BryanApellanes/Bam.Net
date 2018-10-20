/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Common;

namespace Bam.Net.Data
{
    public interface IConnectionStringResolver
    {
        /// <summary>
        /// When implemented in a derived class returns the ConnectionStringSettings 
        /// for the specified connectionName or null if it cannot be resolved
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        ConnectionStringSettings Resolve(string connectionName);
        DbConnectionStringBuilder GetConnectionStringBuilder();
    }
}
