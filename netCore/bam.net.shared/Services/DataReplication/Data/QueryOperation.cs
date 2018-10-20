/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication.Data
{
    [Serializable]
	public class QueryOperation: Operation
	{
        public List<DataProperty> Properties { get; set; }
		public override object Execute(IDistributedRepository repository)
		{
            return repository.Query(this);
		}

        public static QueryOperation For(Type type, dynamic queryProperties)
        {
            return For(type, ((object)queryProperties).ToDictionary());
        }

        public static QueryOperation For(Type type, Dictionary<string, object> properties)
        {
            QueryOperation operation = For<QueryOperation>(type);
            operation.Properties = new List<DataProperty>();
            properties.Keys.Each(key =>
            {
                operation.Properties.Add(new DataProperty { Name = key, Value = properties[key] });
            });
            return operation;
        }
	}
}
