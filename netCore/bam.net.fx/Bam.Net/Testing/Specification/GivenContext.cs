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
	public class GivenContext
	{
		public GivenContext(string given, Action givenAction)
		{
			this.Given = given;
			this.Action = givenAction;
		}

		public Action Action { get; set; }
		public string Given { get; set; }
		public GivenContext And(string and, Action andAction)
		{
			return this;
		}

		public StepContext When(string when, Action whenAction)
		{
			return new StepContext(when, whenAction);
		}
	}
}
