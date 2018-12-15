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
		public StepContext(string description, Action step)
		{
			this.Description = description;
			this.Action = step;
		}

		public string Description { get; set; }
		public Action Action { get; set; }

		public StepContext And(string and, Action andAction)
		{
			return this;
		}

		public StepContext When(string when, Action whenAction)
		{
			return this;
		}
		public StepContext Then(string then, Action thenAction)
		{
			return this;
		}
		public StepContext But(string but, Action butAction)
		{
			return this;
		}
	}
}
