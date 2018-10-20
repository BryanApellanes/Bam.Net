/*
	Copyright © Bryan Apellanes 2015  
*/
// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.ServiceProxy.Secure
{
	public partial class Application
	{
        /// <summary>
        /// Creates an Application using ApiKeyManager.Default.CreateApplication
        /// </summary>
        /// <param name="applicationName"></param>
        /// <returns></returns>
        public static ApplicationCreateResult Create(string applicationName)
        {
            return Create(applicationName, LocalApiKeyManager.Default);
        }

        public static ApplicationCreateResult Create(string applicationName, LocalApiKeyManager keyManager)
        {
            return keyManager.CreateApplication(applicationName);
        }
        static object _defaultLock = new object();
        static Application _defaultApplication;
        public static Application Unknown
        {
            get { return _defaultLock.DoubleCheckLock(ref _defaultApplication, () => new Application { Name = ApplicationDiagnosticInfo.UnknownApplication }); }
        }
    }
}																								
