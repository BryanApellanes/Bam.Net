/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Services.DataReplication.Data;

namespace Bam.Net.Services.DataReplication
{
    public interface IDistributedRepository
    {
        object Save(SaveOperation value);
        object Create(CreateOperation value);
        object Retrieve(RetrieveOperation value);
        object Update(UpdateOperation value);
        bool Delete(DeleteOperation value);
        IEnumerable<object> Query(QueryOperation query);

        ReplicationOperation Replicate(ReplicationOperation operation);

        IEnumerable<object> NextSet(ReplicationOperation operation);
    }
}
