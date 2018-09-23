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
			this.Features = new Queue<Feature>();
		}
		public Queue<Feature> Features { get; set; }
	}
}
