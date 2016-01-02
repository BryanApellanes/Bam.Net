/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Distributed
{
	public class HubNode: ComputeNode
	{
		public async Task<ReplicationResult> Replicate(Operation operation)
		{
			ReplicationResult result = new ReplicationResult();
			Parallel.ForEach<ComputeNode>(GetReplicationPartners(operation), compueNode =>
			{
				compueNode.RepositoryProvider.RecieveReplica(operation);
			});

			return result;
		}

		public List<ComputeNode> GetReplicationPartners(Operation operation)
		{
			throw new NotImplementedException();
		}
	}
}
