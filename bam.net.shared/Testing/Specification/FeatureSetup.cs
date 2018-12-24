/*
	Copyright © Bryan Apellanes 2015  
*/
using Bam.Net.CoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Testing.Specification
{
	public class FeatureSetup: SpecificationTestAction<FeatureSetup>
	{
		public FeatureSetup(string featureDescription, Action action)
		{
			this.Description = featureDescription;
			this.Action = action;
		}
        
        public override bool TryAction()
        {
            return TryAction((f,e)=> { });
        }

        public override bool TryAction(Action<FeatureSetup, Exception> exceptionHandler)
        {
            return base.TryAction(this, exceptionHandler);
        }
    }
}
