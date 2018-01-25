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
            DataSettings dataSettings = Resolve(context);
            return dataSettings.GetSysRepository();
        }

        public DataSettings Resolve(IHttpContext context)
        {
            return new DataSettings(ProcessModeResolver.Resolve(context.Request.Url));
        }
    }
}
