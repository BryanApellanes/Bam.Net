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
	public class FeatureAction: SpecificationTestAction<FeatureAction>
	{
		public FeatureAction(string featureDescription, Action action)
		{
			this.Description = featureDescription;
			this.Action = action;
		}
        
		public Queue<Scenario> Scenarios { get; set; }

        public override bool TryAction()
        {
            return TryAction((f,e)=> { });
        }

        public override bool TryAction(Action<FeatureAction, Exception> exceptionHandler)
        {
            return base.TryAction(this, exceptionHandler);
        }
    }
}
