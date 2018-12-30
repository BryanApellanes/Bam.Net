/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
	public class GenerateDaoAssemblyEventArgs: EventArgs
	{
		public GenerateDaoAssemblyEventArgs(GeneratedAssemblyInfo generatedAssemblyInfo)
		{
			this.GeneratedAssemblyInfo = generatedAssemblyInfo;
		}

		public GeneratedAssemblyInfo GeneratedAssemblyInfo { get; set; }

	}
}
