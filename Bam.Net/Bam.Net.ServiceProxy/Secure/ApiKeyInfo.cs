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
        protected internal IApplicationNameProvider ApplicationNameProvider
        {
            get;
            set;
        }
        public string ApplicationName
        {
            get
            {
                return ApplicationNameProvider.GetApplicationName();
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
