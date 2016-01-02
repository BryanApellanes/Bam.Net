/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Naizari.Extensions;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
//using Naizari.Testing;
using System.IO;
//using Naizari.Data;
//using Naizari.Helpers;
using Bam.Net.Data;
using Bam.Net;
using Bam.Net.Testing;

namespace Bam.Net.Data.Tests
{
    public class ColumnAttributeTestClass
    {
        [Column]
        public string ColumnOne
        {
            get;
            set;
        }

        [Column(Name = "two", Table = "myTable", ExtractedType = "SelectDataType")]
        public string ColumnTwo
        {
            get;
            set;
        }

        public string Excluded { get; set; }
    }

    public class ColumnAttributeTests : CommandLineTestInterface
    {
        [UnitTest]
        public static void GetColumnsShouldReturnAllColumnAttributeFromType()
        {
            ColumnAttribute[] attrs = ColumnAttribute.GetColumns(typeof(ColumnAttributeTestClass));
            Expect.AreEqual(2, attrs.Length);
            Expect.AreEqual("two", attrs[1].Name);
            Expect.AreEqual("myTable", attrs[1].Table);
            Expect.AreEqual("SelectDataType", attrs[1].ExtractedType);
        }
    }
}
