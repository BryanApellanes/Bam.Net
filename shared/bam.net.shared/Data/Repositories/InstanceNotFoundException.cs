using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    [Serializable]
    public class InstanceNotFoundException: Exception
    {
        public InstanceNotFoundException(Type type, long id) : base($"Instance of type {type.Name} with Id {id} was not found")
        { }
        public InstanceNotFoundException(Type type, ulong id) : base($"Instance of type {type.Name} with Id {id} was not found")
        { }
    }
}
