/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.DistributedHashTable.Data;
using Bam.Net.CoreServices.DistributedHashTable.Data;

namespace Bam.Net.CoreServices.DistributedHashTable
{
    public interface IDistributedRepository
    {
        object Create(CreateOperation value);
        object Retrieve(RetrieveOperation value);
        object Update(UpdateOperation value);
        bool Delete(DeleteOperation value);
        IEnumerable<object> Query(QueryOperation query);

		Task<ReplicationResult> RecieveReplica(ReplicateOperation operation);
    }
}
