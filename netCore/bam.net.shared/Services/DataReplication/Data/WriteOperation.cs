using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication.Data
{
    public abstract class WriteOperation: Operation
    {  
        public OperationIntent Intent { get; set; }

        /// <summary>
        /// The properties that were written
        /// </summary>
        public List<DataProperty> Properties { get; set; }

        public override object Execute(IDistributedRepository repo)
        {
            WriteEvent writeEvent = this.CopyAs<WriteEvent>();            
            Commit(repo, writeEvent);
            Any?.Invoke(this, new OpertionEventArgs { WriteEvent = writeEvent });
            return writeEvent;
        }

        protected void Commit(IDistributedRepository repo, WriteEvent writeEvent)
        {
            repo.Save(SaveOperation.For(writeEvent));
        }

        public static event EventHandler Any;

        protected static List<DataProperty> GetData(object instance)
        {
            List<DataProperty> data = new List<DataProperty>();
            Type type = instance.GetType();
            type.GetValueProperties().Each(prop =>
            {
                data.Add(new Data.DataProperty { Name = prop.Name, Value = prop.GetValue(instance) });
            });
            return data;
        }
    }
}
