using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.ServiceRegistration
{
    public interface ITypeResolver
    {
        Type ResolveType(string typeName);
    }
}
