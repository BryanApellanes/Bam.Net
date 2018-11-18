/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Bam.Net
{
    public static class MsSqlExtensions
    {
        public static string DatabaseName(this string connectionString)
        {
            SqlConnectionStringBuilder destinationConnBuilder = new SqlConnectionStringBuilder(connectionString);
            string databaseName = destinationConnBuilder["Initial Catalog"] as string;

            if (string.IsNullOrEmpty(databaseName))
                databaseName = destinationConnBuilder["Database"] as string;

            if (string.IsNullOrEmpty(databaseName))
                throw new InvalidOperationException("Unable to determine database name from the connection string");
            return databaseName;
        }
    }
}
