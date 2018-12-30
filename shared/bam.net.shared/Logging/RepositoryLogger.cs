/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Logging
{
	public class RepositoryLogger: Logger
	{
		public RepositoryLogger(IRepository repository)
		{
			Repository = repository;
            Repository.AddType<LogEvent>();
		}

		public IRepository Repository { get; set; }
		public override void CommitLogEvent(LogEvent logEvent)
		{
			Repository.Create(logEvent);
		}
	}
}
