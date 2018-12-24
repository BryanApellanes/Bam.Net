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
	public class Scenario: SpecificationTestAction<Scenario>
	{
        Queue<SetupAction> _setupActions;
        Queue<ThenAction> _thenActions;
        Queue<ButAction> _butActions;
        public Scenario(string scenario, Action setupScenario, ILogger logger = null)
		{
			this.Description = scenario;
			this.Action = setupScenario;
            _setupActions = new Queue<SetupAction>();
            _thenActions = new Queue<ThenAction>();
            _butActions = new Queue<ButAction>();
            Logger = logger;
        }
        
        internal FeatureContext FeatureContext { get; set; }
        public FeatureSetup CurrentFeature { get; set; }
        public ILogger Logger { get; set; }

        public WhenAction TestAction { get; set; }

        public override bool TryAction()
        {
            return TryAction((f, e) => { });
        }

        /// <summary>
        /// Tries to run the setup action
        /// </summary>
        /// <param name="exceptionHandler">The exception handler.</param>
        /// <returns></returns>
        public override bool TryAction(Action<Scenario, Exception> exceptionHandler)
        {
            return base.TryAction(this, exceptionHandler);
        }

        public Scenario Given(string given, Action givenAction)
        {
            _setupActions.Enqueue(new SetupAction { Description = given, Action = givenAction });
            return this;
        }

        public Scenario And(string and, Action andAction)
        {
            _setupActions.Enqueue(new SetupAction { Description = and, Action = andAction });
            return this;
        }

        public Scenario When(string when, Action whenAction)
        {
            if(TestAction != null)
            {
                throw new InvalidOperationException("Test method (When) has already been set for this scenario");
            }
            TestAction = new WhenAction { Description = when, Action = whenAction };
            return this;
        }

        public Scenario Then(string then, Action thenAction)
        {
            _thenActions.Enqueue(new ThenAction { Description = then, Action = thenAction });
            return this;
        }

        public Scenario But(string but, Action butAction)
        {
            _butActions.Enqueue(new ButAction { Description = but, Action = butAction });
            return this;
        }

        public bool Execute()
        {
            if (!TryAction())
            {
                Logger.AddEntry("Failed to setup scenario");
                return false;
            }
            while(_setupActions.Count > 0)
            {
                try
                {
                    SetupAction action = _setupActions.Dequeue();
                    Console.WriteLine(action.Description);
                    action.Action();
                }
                catch (Exception ex)
                {
                    Logger.AddEntry("Error setting up scenario ({0}): {1}", Description, ex.Message);
                    return false;
                }
            }
            try
            {
                TestAction.Action();
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Error running scenario ({0}): {1}", Description, ex.Message);
            }
            while (_thenActions.Count > 0)
            {
                try
                {
                    ThenAction action = _thenActions.Dequeue();
                    Console.WriteLine(action.Description);
                    action.Action();
                }
                catch (Exception ex)
                {
                    Logger.AddEntry("Error running assertions for scenario ({0}): {1}", Description, ex.Message);
                }
            }
            while(_butActions.Count > 0)
            {
                try
                {
                    ButAction action = _butActions.Dequeue();
                    Console.WriteLine(action.Description);
                    action.Action();
                }
                catch (Exception ex)
                {
                    Logger.AddEntry("Error running final assertions for scenario ({0}): {1}", Description, ex.Message);
                }
            }
            return true;
        }
    }
}
