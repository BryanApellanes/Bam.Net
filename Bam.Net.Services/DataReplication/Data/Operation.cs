/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Services.DataReplication.Data
{ 
    public abstract class Operation: AuditRepoData
    {
        public string TypeNamespace { get; set; }
        public string TypeName { get; set; }
        
        public abstract object Execute(IDistributedRepository repository);		

        public static TOperation For<TOperation>(Type type) where TOperation: Operation, new()
        {
            TOperation result = new TOperation()
            {
                TypeName = type.Name,
                TypeNamespace = type.Namespace
            };
            return result;
        }
	}
}
