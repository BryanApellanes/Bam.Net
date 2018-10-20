/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
namespace Bam.Net.ServiceProxy.Secure
{
    public interface IApiKeyProvider
    {
        ApiKeyInfo GetApiKeyInfo(IApplicationNameProvider nameProvider);
        string GetApplicationApiKey(string applicationClientId, int index);
        string GetApplicationClientId(IApplicationNameProvider nameProvider);
        string GetCurrentApiKey();
    }
}
