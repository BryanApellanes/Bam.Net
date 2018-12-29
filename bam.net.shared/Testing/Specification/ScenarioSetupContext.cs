/*
	Copyright © Bryan Apellanes 2015  
*/
using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Testing.Specification
{
	public class ScenarioSetupContext: SpecTestContextSetup
	{
        Queue<ScenarioSetupAction> _setupActions;
        Queue<ThenAction> _assertionActions;
        public ScenarioSetupContext(string scenario, Action setupScenario, ILogger logger = null)
		{
			this.Description = scenario;
			this.SetupAction = setupScenario;
            _setupActions = new Queue<ScenarioSetupAction>();
            _assertionActions = new Queue<ThenAction>();
            Logger = logger;
        }
        
        public FeatureContext FeatureContext { get; set; }
        public FeatureContextSetup CurrentFeature { get; set; }
        public SpecTestContainer SpecTestContainer { get; set; }
        public ILogger Logger { get; set; }

        ItContext _assertionContext;
        object _assertionContextLock = new object();
        public ItContext AssertionContext
        {
            get
            {
                return _assertionContextLock.DoubleCheckLock(ref _assertionContext, () => new ItContext());
            }
        }

        public WhenAction TestAction { get; set; }

        public ScenarioSetupContext Given(string given, Action givenAction)
        {
            _setupActions.Enqueue(new ScenarioSetupAction { Description = given, Action = givenAction });
            return this;
        }

        public ScenarioSetupContext And(string and, Action andAction)
        {
            _setupActions.Enqueue(new ScenarioSetupAction { Description = and, Action = andAction });
            return this;
        }

        public ScenarioSetupContext When(string when, Action whenAction)
        {
            if(TestAction != null)
            {
                throw new InvalidOperationException("Test method (When) has already been set for this scenario");
            }
            TestAction = new WhenAction { Description = when, Action = whenAction };
            return this;
        }

        public ScenarioSetupContext Then(string then, Action<ThenDelegate> thenAction)
        {
            _assertionActions.Enqueue(new ThenAction { Description = then, Action = thenAction });
            return this;
        }

        public EventHandler ScenarioExecuting;
        public EventHandler ScenarioSetup;
        public EventHandler ScenarioSetupFailed;

        public EventHandler ScenarioSetupStep;
        public EventHandler ScenarioSetupStepFailed;

        public EventHandler ScenarioTest;
        public EventHandler ScenarioTestFailed;

        public EventHandler AssertionPassed;
        public EventHandler AssertionFailed;

        public EventHandler ScenarioExecuted;

        public bool Execute() 
        {
            FireEvent(ScenarioExecuting, new SpecTestEventArgs { ScenarioSetupContext = this, TestAction = TestAction });
            SpecTestReporter reporter = SpecTestContainer.GetReporter();
            if (!TrySetup())
            {
                reporter.AddWarningMessage("Failed to setup scenario: ({0})", Description);
                FireEvent(ScenarioSetupFailed, new SpecTestEventArgs { ScenarioSetupContext = this, TestAction = TestAction });
                return false;
            }
            reporter.AddMessage("Scenario: {0}", Description);
            FireEvent(ScenarioSetup, new SpecTestEventArgs { ScenarioSetupContext = this, TestAction = TestAction });
            while(_setupActions.Count > 0)
            {
                ScenarioSetupAction action = _setupActions.Dequeue();
                try
                {
                    LogMessage infoMessage = reporter.AddMessage(action.Description);                    
                    action.Action();
                    FireEvent(ScenarioSetupStep, new SpecTestEventArgs { ScenarioSetupContext = this, ScenarioSetupAction = action, TestAction = TestAction });
                }
                catch (Exception ex)
                {
                    reporter.AddErrorMessage("Error setting up scenario ({0})\r\n\t{1}: {2}", ex, Description, action.Description, ex.Message);
                    FireEvent(ScenarioSetupStepFailed, new SpecTestEventArgs { ScenarioSetupContext = this, TestAction = TestAction, Exception = ex });
                    return false;
                }
            }
            try
            {
                TestAction.Action();
                reporter.AddMessage("{0} executed", TestAction.Description);
                FireEvent(ScenarioTest, new SpecTestEventArgs { ScenarioSetupContext = this, TestAction = TestAction });
            }
            catch (Exception ex)
            {
                reporter.AddErrorMessage("Error running scenario ({0}): {1}", ex, Description, ex.Message);
                FireEvent(ScenarioTestFailed, new SpecTestEventArgs { ScenarioSetupContext = this, TestAction = TestAction });
                return false;
            }
            while (_assertionActions.Count > 0)
            {
                ThenAction action = _assertionActions.Dequeue();
                try
                {                    
                    reporter.AddMessage(action.Description);
                    action.Action(o => AssertionContext.It(o));
                    FireEvent(AssertionPassed, new SpecTestEventArgs { ScenarioSetupContext = this, TestAction = TestAction, AssertionAction = action });
                }
                catch (Exception ex)
                {
                    reporter.AddErrorMessage("Error running assertions for scenario ({0}): {1}", ex, Description, ex.Message);
                    FireEvent(AssertionFailed, new SpecTestEventArgs { ScenarioSetupContext = this, TestAction = TestAction, Exception = ex, AssertionAction = action });
                    return false;
                }
            }
            FireEvent(ScenarioExecuted, new SpecTestEventArgs { ScenarioSetupContext = this, TestAction = TestAction });
            return true;
        }

        
    }
}
