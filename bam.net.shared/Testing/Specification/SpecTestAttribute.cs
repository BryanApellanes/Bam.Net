/*
	Copyright © Bryan Apellanes 2015  
*/
using Bam.Net.CommandLine;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Testing.Specification
{

	[AttributeUsage(AttributeTargets.Method)]
	public class SpecTestAttribute: ConsoleActionAttribute
    {
	}
}
