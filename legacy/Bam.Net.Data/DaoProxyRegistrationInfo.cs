using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data
{
    public class DaoProxyRegistrationInfo: DatabaseInfo
    {
        public DaoProxyRegistrationInfo(DaoProxyRegistration proxyRegInfo)
            : base(proxyRegInfo.Database)
        {
            this.ContextName = proxyRegInfo.ContextName;
            this.DaoClassNames = proxyRegInfo.ServiceProvider.ClassNames;
        }

        /// <summary>
        /// The context name, typically the same as the connection name
        /// </summary>
        public string ContextName { get; set; }
        public string[] DaoClassNames { get; set; }
    }
}
