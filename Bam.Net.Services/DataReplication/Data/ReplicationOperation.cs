/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices;
using System.Reflection;
using Bam.Net.Logging;

namespace Bam.Net.Services.DataReplication.Data
{ 
    [Serializable]
    public class ReplicationOperation: Operation
	{
        public ReplicationOperation()
        {
            FromCuid = "0";
        }
        public ReplicationStatus Status { get; set; }
        public string SourceHost { get; set; }
        public int SourcePort { get; set; }
        public string DestinationHost { get; set; }
        public int DestinationPort { get; set; }

        public int BatchSize { get; set; }

        /// <summary>
        /// Used to batch operations in order by Cuid.
        /// </summary>
        /// <value>
        /// From cuid.
        /// </value>
        public string FromCuid { get; set; }
        
        public override object Execute(IDistributedRepository destination)
		{
            ProxyFactory _proxyFactory = new ProxyFactory();
            RepositoryService sourceRepo = _proxyFactory.GetProxy<RepositoryService>(SourceHost, SourcePort, Log.Default);            
            // get types
            // for each type load all and save each
            string lastCuid = FromCuid;
            foreach(string type in sourceRepo.GetTypes())
            {
                ReplicationOperation op = this.CopyAs<ReplicationOperation>();                
                op.TypeName = type;
                op.FromCuid = lastCuid;
                List<object> values = sourceRepo.NextSet(op).ToList();
                Parallel.ForEach(values, (value) => destination.Save(SaveOperation.For(value)));
                values.Sort((x, y) => x.Property("Cuid").ToString().CompareTo(y.Property("Cuid")));
                lastCuid = values[values.Count - 1].Property("Cuid").ToString();
            };

            FromCuid = lastCuid;
            Status = ReplicationStatus.Success;
            return this;
		}
	}
}
