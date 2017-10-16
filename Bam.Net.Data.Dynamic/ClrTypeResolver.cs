using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Dynamic
{
    public class ClrTypeResolver : ITypeResolver
    {
        public ClrTypeResolver(DynamicTypeManager typeManager)
        {
            DynamicTypeManager = typeManager;
        }

        public DynamicTypeManager DynamicTypeManager { get; set; }
        public Type ResolveType(string typeName)
        {
            throw new NotImplementedException();
        }

        public Type ResolveType(string nameSpace, string typeName)
        {
            throw new NotImplementedException();
        }
    }
}
