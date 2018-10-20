using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices.ProtoBuf
{
    /// <summary>
    /// A ProtoFileGenerator that will only
    /// include properties addorned with the 
    /// custom attribute ColumnAttribute
    /// </summary>
    public class DaoProtoFileGenerator : ProtoFileGenerator
    {
        public DaoProtoFileGenerator(IPropertyNumberer propertyNumberer) 
            : this(new TypeSchemaGenerator(), propertyNumberer)
        { }

        public DaoProtoFileGenerator(TypeSchemaGenerator typeSchemaGenerator, IPropertyNumberer propertyNumberer) 
            : base(typeSchemaGenerator, propertyNumberer, (pi)=>pi.HasCustomAttributeOfType<ColumnAttribute>())
        { }
    }
}
