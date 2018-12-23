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
	public class ScenarioContext
	{
		public ScenarioContext()
		{
			this.Scenarios = new Queue<Scenario>();
		}

		public Queue<Scenario> Scenarios { get; set; }

        //public ScenarioContext And(string and, Action andAction)
        //{
        //    return this;
        //}

        //public ScenarioContext When(string when, Action whenAction)
        //{
        //    return this;
        //}

        //public ScenarioContext Then(string then, Action thenAction)
        //{
        //    return this;
        //}

        //public ScenarioContext But(string but, Action butAction)
        //{
        //    return this;
        //}
    }
}
