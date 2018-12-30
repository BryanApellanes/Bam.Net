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
    public class UpdateOperation: WriteOperation
	{
        public UpdateOperation()
        {
            Intent = OperationIntent.Update;
        }

		public override object Execute(IDistributedRepository repository)
		{
            repository.Update(this);
            return base.Execute(repository);
		}

        public static UpdateOperation For(object toUpdate)
        {
            UpdateOperation operation = For<UpdateOperation>(toUpdate.GetType());
            operation.Properties = GetData(toUpdate);
            return operation;
        }        
    }
}
