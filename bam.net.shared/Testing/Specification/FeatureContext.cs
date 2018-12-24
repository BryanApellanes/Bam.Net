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
	internal class FeatureContext
	{
		public FeatureContext()
		{
			this.Features = new Queue<FeatureSetup>();
		}
		public Queue<FeatureSetup> Features { get; set; }
	}
}
