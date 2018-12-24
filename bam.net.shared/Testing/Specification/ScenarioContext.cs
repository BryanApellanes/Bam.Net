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
            Scenarios = new Queue<Scenario>();
        }
        public Queue<Scenario> Scenarios { get; set; }
        public Scenario CurrentScenario { get; set; }

        public Scenario AddScenario(string scenarioDescription, Action scenarioSetup)
        {
            Scenario scenario = new Scenario(scenarioDescription, scenarioSetup);
            Scenarios.Enqueue(scenario);
            CurrentScenario = scenario;
            return scenario;
        }
	}
}
