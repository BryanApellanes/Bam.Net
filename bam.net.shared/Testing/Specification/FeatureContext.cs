/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Testing.Specification
{
	public class FeatureContext
	{
		public FeatureContext()
		{
			this.Features = new Queue<FeatureContextSetup>();
		}
		public Queue<FeatureContextSetup> Features { get; set; }
	}
}
