using Bam.Net.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using System.IO;
using Bam.Net.CoreServices.OAuth.Data;
using System.Collections;

namespace Bam.Net.CoreServices.OAuth
{
    public class SupportedOAuthProviders: IEnumerable<OAuthProviderInfo>
    {
        List<OAuthProviderInfo> _oauthProviderSettings;
        public SupportedOAuthProviders()
        {
            _oauthProviderSettings = new List<OAuthProviderInfo>();
        }
        
        public void AddProvider(OAuthProviderInfo provider)
        {
            _oauthProviderSettings.Add(provider);
        }

        public void Save(string filePath)
        {
            _oauthProviderSettings.ToJsonFile(filePath);
        }

        public void Load(string filePath)
        {
            _oauthProviderSettings = filePath.FromJsonFile<List<OAuthProviderInfo>>();
        }

        public static SupportedOAuthProviders LoadFrom(string filePath)
        {
            SupportedOAuthProviders result = new SupportedOAuthProviders();
            result.Load(filePath);
            return result;
        }

        public static SupportedOAuthProviders Get(IApplicationNameProvider appNameProvider)
        {
            string filePath = GetSettingsPath(appNameProvider);
            return LoadFrom(filePath);
        }

        public static string GetSettingsPath(IApplicationNameProvider appNameProvider)
        {
            string appName = appNameProvider.GetApplicationName();
            string filePath = Path.Combine(DefaultDataDirectoryProvider.Current.AppDataDirectory, appName, $"{nameof(SupportedOAuthProviders)}.json");
            return filePath;
        }

        public OAuthProviderInfo this[string name]
        {
            get
            {
                return _oauthProviderSettings.FirstOrDefault(p => p.ProviderName.Equals(name));
            }
        }

        public IEnumerator<OAuthProviderInfo> GetEnumerator()
        {
            return _oauthProviderSettings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _oauthProviderSettings.GetEnumerator();
        }
    }
}
