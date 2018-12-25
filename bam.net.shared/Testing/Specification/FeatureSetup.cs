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
	public class FeatureSetup: SpecTestContextSetupAction<FeatureSetup>
	{
		public FeatureSetup(string featureDescription, Action action)
		{
			this.Description = featureDescription;
			this.SetupAction = action;
		}
        
        public override bool TrySetup()
        {
            return TrySetup((f,e)=> { });
        }

        public override bool TrySetup(Action<FeatureSetup, Exception> exceptionHandler)
        {
            return base.TrySetup(this, exceptionHandler);
        }
    }
}
