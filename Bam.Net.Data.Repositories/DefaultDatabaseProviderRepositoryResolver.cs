using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Data.Repositories
{
    public class DefaultDatabaseProviderRepositoryResolver : RepositoryResolver, IDataSettingsResolver
    {
        public override IRepository GetRepository(IHttpContext context)
        {
            DefaultDataDirectoryProvider dataSettings = Resolve(context);
            return dataSettings.GetSysRepository();
        }

        public DefaultDataDirectoryProvider Resolve(IHttpContext context)
        {
            return new DefaultDataDirectoryProvider(ProcessModeResolver.Resolve(context.Request.Url));
        }
    }
}
