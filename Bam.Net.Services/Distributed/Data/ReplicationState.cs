/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Services.Distributed.Data
{
    public enum ReplicationStatuses
    {
        Invalid,
        InProgress,
        Failed,
        Complete
    }

	public class ReplicationState: AuditRepoData
	{
        public ReplicationStatuses Status { get; set; }
        public string SourceHost { get; set; }
        public int SourcePort { get; set; }
        public string DestinationHost { get; set; }
        public int DestinationPort { get; set; }
        public string Message { get; set; }
	}
}
