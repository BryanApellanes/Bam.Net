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
            DefaultDataSettingsProvider dataSettings = Resolve(context);
            return dataSettings.GetSysRepository();
        }

        public DefaultDataSettingsProvider Resolve(IHttpContext context)
        {
            return new DefaultDataSettingsProvider(ProcessModeResolver.Resolve(context.Request.Url));
        }
    }
}
