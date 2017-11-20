/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices;

namespace Bam.Net.Services.Distributed.Data
{ 
    [Serializable]
    public class ReplicateOperation: Operation
	{
        public ReplicationStatus Status { get; set; }
        public string SourceHost { get; set; }
        public int SourcePort { get; set; }
        public string DestinationHost { get; set; }
        public int DestinationPort { get; set; }

        public override object Execute(IDistributedRepository destination)
		{
            ProxyFactory _proxyFactory = new ProxyFactory();
            RepositoryService sourceRepo = _proxyFactory.GetProxy<RepositoryService>(SourceHost, SourcePort);
            // get types
            // for each type load all and save each
            sourceRepo.GetTypes().Each(type=>
            {

            });
            throw new NotImplementedException();
		}
	}
}
