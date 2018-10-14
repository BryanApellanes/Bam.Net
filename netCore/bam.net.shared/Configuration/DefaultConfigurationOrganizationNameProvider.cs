using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Bam.Net.IOrganizationNameProvider" />
    public class DefaultConfigurationOrganizationNameProvider : IOrganizationNameProvider
    {
        static IOrganizationNameProvider _instance;
        static object _lock = new object();
        public static IOrganizationNameProvider Instance
        {
            get
            {
                return _lock.DoubleCheckLock(ref _instance, () => new DefaultConfigurationOrganizationNameProvider());
            }
        }
        public string GetOrganizationName()
        {
            return DefaultConfiguration.GetAppSetting("OrganizationName", ApplicationDiagnosticInfo.PublicOrganization);
        }
    }
}
