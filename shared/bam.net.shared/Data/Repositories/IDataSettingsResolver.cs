using Bam.Net.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public interface IDataSettingsResolver
    {
        DefaultDataDirectoryProvider Resolve(IHttpContext context);
    }
}
