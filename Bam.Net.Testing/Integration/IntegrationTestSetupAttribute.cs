/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Testing.Integration
{
	[AttributeUsage(AttributeTargets.Method)]
	public class IntegrationTestSetupAttribute: Attribute
	{
	}
}
