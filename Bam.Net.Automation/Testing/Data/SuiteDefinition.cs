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
	public class SuiteDefinition: RepoData
	{
		public SuiteDefinition()
		{
		}
		public string Title { get; set; }
		public virtual TestDefinition[] TestDefinitions { get; set; }
	}
}
