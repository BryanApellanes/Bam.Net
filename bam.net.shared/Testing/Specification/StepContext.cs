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
	public class StepContext
	{
        Queue<AndAction> _andActions;
        Queue<WhenAction> _whenActions;
        Queue<ThenAction> _thenActions;
        Queue<ButAction> _butActions;
        public StepContext()
        {
            _andActions = new Queue<AndAction>();
            _whenActions = new Queue<WhenAction>();
            _thenActions = new Queue<ThenAction>();
            _butActions = new Queue<ButAction>();
        }

        public StepContext(string description, Action setup)
		{
			this.Description = description;
			this.Action = setup;
        }

		public string Description { get; set; }
		public Action Action { get; set; }

		public StepContext And(string and, Action andAction)
		{
            _andActions.Enqueue(new AndAction { Description = and, Action = andAction });
			return this;
		}

		public StepContext When(string when, Action whenAction)
		{
            _whenActions.Enqueue(new WhenAction { Description = when, Action = whenAction });
			return this;
		}

		public StepContext Then(string then, Action thenAction)
		{
            _thenActions.Enqueue(new ThenAction { Description = then, Action = thenAction });
			return this;
		}

		public StepContext But(string but, Action butAction)
		{
            _butActions.Enqueue(new ButAction { Description = but, Action = butAction });
			return this;
		}
	}
}
