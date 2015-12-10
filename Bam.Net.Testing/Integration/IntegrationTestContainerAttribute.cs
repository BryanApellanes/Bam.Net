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
	[AttributeUsage(AttributeTargets.Class)]
	public class IntegrationTestContainerAttribute : Attribute
	{
		public IntegrationTestContainerAttribute() { }
		public IntegrationTestContainerAttribute(string description)
		{
			this.Description = description;
		}
		public string Description { get; set; }
	}
}
