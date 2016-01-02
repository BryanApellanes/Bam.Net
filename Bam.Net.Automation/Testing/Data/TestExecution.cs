/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Testing.Repository.Data
{
	[Serializable]
	public class TestExecution: RepoData
	{
		public long TestDefinitionId { get; set; }
		public virtual TestDefinition TestDefinition { get; set; }

		public bool Pass { get; set; }
		public string Error { get; set; }

		public long TestSummaryId { get; set; }
		public virtual TestSummary TestSummary { get; set; }
	}
}
