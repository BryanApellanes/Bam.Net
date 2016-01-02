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
            return Create(applicationName, ApiKeyManager.Default);
        }

        public static ApplicationCreateResult Create(string applicationName, ApiKeyManager keyManager)
        {
            return keyManager.CreateApplication(applicationName);
        }
    }
}																								
