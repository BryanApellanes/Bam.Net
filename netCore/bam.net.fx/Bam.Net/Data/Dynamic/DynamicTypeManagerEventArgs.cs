using Bam.Net.Data.Dynamic.Data;
using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Dynamic
{
    [Serializable]
    public class DynamicTypeManagerEventArgs: EventArgs
    {
        public DynamicTypeDescriptor[] DynamicTypeDescriptors { get; set; }
        public DynamicTypePropertyDescriptor[] DynamicTypePropertyDescriptors { get; set; }
        public string TypeName { get; set; }
        public string FoundTypes { get; set; }
    }
}
