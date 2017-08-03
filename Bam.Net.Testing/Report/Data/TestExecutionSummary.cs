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
	[Serializable]
	public class TestExecutionSummary : RepoData
	{
        public long SuiteDefinitionId { get; set; }
        public virtual SuiteDefinition SuiteDefinition { get; set; }
		public virtual TestExecution[] TestExecutions { get; set; }
	}
}
