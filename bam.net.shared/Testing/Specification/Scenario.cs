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
	public class Scenario: SpecificationTestAction<Scenario>
	{
        StepContext _steps;
		public Scenario(string scenario, Action scenarioAction)
		{
			this.Description = scenario;
			this.Action = scenarioAction;
            _steps = new StepContext();
		}
        
        public override bool TryAction()
        {
            return TryAction((f, e) => { });
        }

        public override bool TryAction(Action<Scenario, Exception> exceptionHandler)
        {
            return base.TryAction(this, exceptionHandler);
        }

        public StepContext And(string and, Action andAction)
        {
            return _steps.And(and, andAction);            
        }

        public StepContext When(string when, Action whenAction)
        {
            return _steps.When(when, whenAction);
        }

        public StepContext Then(string then, Action thenAction)
        {
            return _steps.Then(then, thenAction);
        }
    }
}
