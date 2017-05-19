/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DistributedService.Data
{
    public class CreateOperation : WriteOperation
    {
        public override object Execute(IDistributedRepository repository)
        {
            return repository.Create(this);
        }

        public static CreateOperation For(object toCreate)
        {
            List<DataProperty> data = GetData(toCreate);
            CreateOperation result = For<CreateOperation>(toCreate.GetType());
            result.Properties = data;            
            return result;
        }

        protected override void Commit(IDistributedRepository repo, WriteEvent writeEvent)
        {
            throw new NotImplementedException();
        }
    }
}
