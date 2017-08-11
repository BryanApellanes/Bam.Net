/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.CommandLine;
using System.Reflection;

namespace Bam.Net.Testing
{
	[Serializable]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class BeforeEachUnitTest : ConsoleActionAttribute
    {
	}
}
