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
    public class DeleteOperation : WriteOperation
    {
        public DeleteOperation()
        {
            Intent = OperationIntent.Delete;
        }

        public UniversalIdentifier UniversalIdentifier { get; set; }
        public string Identifier { get; set; }

        public override object Execute(IDistributedRepository repository)
        {
            repository.Delete(this);
            return base.Execute(repository);
        }

        public static DeleteOperation For(object toDelete, UniversalIdentifier identifier = UniversalIdentifier.Cuid)
        {
            DeleteOperation operation = For<DeleteOperation>(toDelete.GetType());
            operation.Identifier = toDelete.Property<string>(identifier.ToString());
            operation.UniversalIdentifier = identifier;
            return operation;
        }

    }
}
