/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Configuration;
using Bam.Net.ServiceProxy;

namespace Bam.Net.ServiceProxy.Secure
{
    /// <summary>
    /// A class used to retrieve an applications Api Key and 
    /// client Id used in SecureServiceProxy sessions.
    /// Implementers of this class need only implement the
    /// GetApplicationClientId and GetApplicationApiKey methods, 
    /// retrieving each from an appropriate location.  For example,
    /// the DefaultConfigurationApiKeyProvider retrieves this
    /// information from the web.config or app.config file.
    /// </summary>
    public abstract class ApiKeyProvider : IApiKeyProvider
    {
        public ApiKeyInfo GetApiKeyInfo(IApplicationNameProvider nameProvider)
        {
            string clientId = GetApplicationClientId(nameProvider);
            ApiKeyInfo info = new ApiKeyInfo()
            {
                ApiKey = GetApplicationApiKey(clientId, 0),
                ApplicationClientId = clientId
            };
            return info;
        }

        public string GetCurrentApiKey()
        {
            return GetApplicationApiKey(GetApplicationClientId(ApplicationNameProvider.Default), 0);
        }

        public abstract string GetApplicationClientId(IApplicationNameProvider nameProvider);

        public abstract string GetApplicationApiKey(string applicationClientId, int index);

    }
}
