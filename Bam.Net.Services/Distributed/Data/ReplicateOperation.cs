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
    public class ReplicateOperation: Operation
	{
        public string SourceHost { get; set; }
        public int SourcePort { get; set; }
		public override object Execute(IDistributedRepository reciever)
		{
            ProxyFactory _proxyFactory = new ProxyFactory();
            RepositoryService svc = _proxyFactory.GetProxy<RepositoryService>(SourceHost, SourcePort);
            // get types
            // for each type load all and save each
            svc.GetTypes().Each(type=>
            {

            });
            throw new NotImplementedException();
		}
	}
}
