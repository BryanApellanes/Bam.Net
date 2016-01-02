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
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Testing;
using Bam.Net.Encryption;

namespace Bam.Net.Automation.Tests
{
    [Serializable]
    class Program : CommandLineTestInterface
    {
        static void Main(string[] args)
        {
			AddValidArgument("t", true, "run all tests");
			DefaultMethod = typeof(Program).GetMethod("Start");

			Initialize(args);
		}

		public static void Start()
		{
			if (Arguments.Contains("t"))
			{
				RunAllTests(typeof(Program).Assembly);
			}
			else
			{
				Interactive();
			}
		}
    }
}
