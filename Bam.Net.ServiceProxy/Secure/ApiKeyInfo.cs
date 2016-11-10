/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Configuration;

namespace Bam.Net.ServiceProxy.Secure
{
    public class ApiKeyInfo
    {
        public ApiKeyInfo()
        {
            this.ApplicationNameProvider = new DefaultConfigurationApplicationNameProvider();
        }
        public ApiKeyInfo(IApplicationNameProvider nameProvider)
        {
            ApplicationNameProvider = nameProvider;
        }
        protected internal IApplicationNameProvider ApplicationNameProvider
        {
            get;
            set;
        }
        string _appName;
        public string ApplicationName
        {
            get
            {
                if (string.IsNullOrEmpty(_appName))
                {
                    _appName = ApplicationNameProvider.GetApplicationName();
                }
                return _appName;
            }
            set
            {
                _appName = value;
            }
        }

        public string ApplicationClientId
        {
            get;
            set;
        }

        public string ApiKey
        {
            get;
            set;
        }
    }
}
