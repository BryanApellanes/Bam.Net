using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.ProtoBuf
{
    /// <summary>
    /// A ProtocoloBufferAssemblyGenerator that will
    /// only include properties addorned with the 
    /// custom attribute ColumnAttribute
    /// </summary>
    public class DaoProtocolBuffersAssemblyGenerator : ProtocolBuffersAssemblyGenerator
    {
        public DaoProtocolBuffersAssemblyGenerator() 
            : base(new DaoProtoFileGenerator(new InMemoryPropertyNumberer()))
        { }

        public DaoProtocolBuffersAssemblyGenerator(IPropertyNumberer propertyNumberer) 
            : base(new DaoProtoFileGenerator(propertyNumberer))
        { }
    }
}
