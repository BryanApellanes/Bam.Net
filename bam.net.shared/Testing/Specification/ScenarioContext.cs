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
            Scenarios = new Queue<ScenarioContextSetup>();
        }
        public Queue<ScenarioContextSetup> Scenarios { get; set; }
        public ScenarioContextSetup CurrentScenario { get; set; }

        public ScenarioContextSetup AddScenario(string scenarioDescription, Action scenarioSetup)
        {
            ScenarioContextSetup scenario = new ScenarioContextSetup(scenarioDescription, scenarioSetup);
            Scenarios.Enqueue(scenario);
            CurrentScenario = scenario;
            return scenario;
        }
	}
}
