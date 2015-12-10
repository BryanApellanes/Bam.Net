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
	public class Scenario
	{
		public Scenario(string scenario, Action scenarioAction)
		{
			this.Description = scenario;
			this.Action = scenarioAction;
		}

		public string Description { get; set; }
		public Action Action { get; set; }
	}
}
