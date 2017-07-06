using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.Data.Dao.Repository;

namespace Bam.Net.CoreServices
{
    public abstract class CoreProxyableService: ProxyableService
    {
        public CoreRegistryRepository CoreRegistryRepository { get; set; }
        public IApplicationNameProvider ApplicationNameProvider { get; set; }
        public override string ApplicationName
        {
            get
            {
                if(ApplicationNameProvider != null && ApplicationNameProvider != this)
                {
                    return ApplicationNameProvider.GetApplicationName();
                }
                return base.ApplicationName;
            }
        }

        public string ClientSpecifiedApplicationName
        {
            get
            {
                return base.ApplicationName;
            }
        }

        /// <summary>
        /// Returns true if the ApplicationNameProvider returns the same 
        /// ApplicationName as is specified by the request headers
        /// </summary>
        /// <returns></returns>
        public bool IsRequestForCurrentApplication()
        {
            if(ApplicationNameProvider == null)
            {
                Logger.Warning("{0} was null, '{1}' will always return true in this case", nameof(ApplicationNameProvider), nameof(IsRequestForCurrentApplication));
            }
            return ClientSpecifiedApplicationName.Equals(ApplicationName);
        }
    }
}
