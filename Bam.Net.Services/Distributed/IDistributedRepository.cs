/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Services.Distributed.Data;

namespace Bam.Net.Services.Distributed
{
    public interface IDistributedRepository
    {
        object Create(CreateOperation value);
        object Retrieve(RetrieveOperation value);
        object Update(UpdateOperation value);
        bool Delete(DeleteOperation value);
        IEnumerable<object> Query(QueryOperation query);

        ReplicateOperation Replicate(ReplicateOperation operation);

        IEnumerable<object> BatchAll(string type, int batchSize);
    }
}
