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
	public class IntegrationTestAttribute: Attribute
	{
		public IntegrationTestAttribute()
		{
		}

		public IntegrationTestAttribute(string description)
		{
			this.Description = description;
		}

		public string Description
		{
			get;
			private set;
		}
	}
}
