using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication.Data
{
    [Serializable]
    public class SaveOperation : WriteOperation
    {        
        public override object Execute(IDistributedRepository repository)
        {
            return repository.Save(this);
        }

        public static SaveOperation For(object toCreate)
        {
            List<DataProperty> data = GetData(toCreate);
            SaveOperation result = For<SaveOperation>(toCreate.GetType());
            result.Properties = data;
            return result;
        }

    }
}
