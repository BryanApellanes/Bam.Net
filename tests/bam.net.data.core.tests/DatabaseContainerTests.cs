/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using Bam.Net.Data;
using Bam.Net;
using Bam.Net.Testing;
using System.Configuration;
using MySql.Data.MySqlClient;
using Bam.Net.Incubation;
using Bam.Net.CommandLine;
using System.Data.OleDb;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Data.Tests
{
    [Table(ConnectionName = "Test")]
    public class TestDao : Dao
    {
        public override IQueryFilter GetUniqueFilter()
        {
            return null;
        }

        public override void Delete(Database db = null)
        {
            throw new NotImplementedException();
        }
    }

    public class DatabaseContainerTests : CommandLineTool
    {
        [UnitTest]
        public static void ShouldGetStaticReadOnlyInstanceField()
        {
            FieldInfo field = typeof(SqlClientFactory).GetField("Instance");
            object inst = field.GetValue(null);
            Expect.IsNotNull(inst);
        }
    }
}
