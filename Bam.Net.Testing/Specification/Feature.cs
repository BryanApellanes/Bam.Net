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
	public class Feature
	{
		public Feature(string feature, Action action)
		{
			this.Description = feature;
			this.Action = action;
		}

		public string Description { get; set; }
		public Action Action { get; set; }

		public Queue<Scenario> Scenarios { get; set; }
	}
}
