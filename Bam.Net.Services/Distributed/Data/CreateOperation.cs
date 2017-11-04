/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Distributed.Data
{
    [Serializable]
    public class CreateOperation : WriteOperation
    {
        public override object Execute(IDistributedRepository repository)
        {
            repository.Create(this);
            return base.Execute(repository);
        }

        public static CreateOperation For(object toCreate)
        {
            List<DataProperty> data = GetData(toCreate);
            CreateOperation result = For<CreateOperation>(toCreate.GetType());
            result.Properties = data;            
            return result;
        }

    }
}
