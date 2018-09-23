/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Configuration;

namespace Bam.Net.Configuration
{
    /// <summary>
    /// An ApplicationNameProvider that retrieves the ApplicationName
    /// value from the appSettings section of the default configuration file
    /// </summary>
    public class DefaultConfigurationApplicationNameProvider: IApplicationNameProvider
    {
        static IApplicationNameProvider _instance;
        static object _lock = new object();
        public static IApplicationNameProvider Instance
        {
            get
            {
                return _lock.DoubleCheckLock(ref _instance, () => new DefaultConfigurationApplicationNameProvider());
            }
        }
        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        /// <returns></returns>
        public string GetApplicationName()
        {
            return DefaultConfiguration.GetAppSetting("ApplicationName", ApplicationDiagnosticInfo.UnknownApplication);
        }
    }
}
