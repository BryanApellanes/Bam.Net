/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Testing.Report.Data
{
    /// <summary>
    /// Represents a logical set of TestDefinitions
    /// belonging to a common suite of tests
    /// </summary>
	[Serializable]
	public class SuiteDefinition: RepoData
	{
		public SuiteDefinition()
		{
		}
		public string Title { get; set; }
		public virtual TestDefinition[] TestDefinitions { get; set; }
	}
}
