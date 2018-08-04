using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Data.Repositories
{
    public class DataSettingsResolver : RepositoryResolver, IDataSettingsResolver
    {
        public override IRepository GetRepository(IHttpContext context)
        {
            DefaultDatabaseProvider dataSettings = Resolve(context);
            return dataSettings.GetSysRepository();
        }

        public DefaultDatabaseProvider Resolve(IHttpContext context)
        {
            return new DefaultDatabaseProvider(ProcessModeResolver.Resolve(context.Request.Url));
        }
    }
}
