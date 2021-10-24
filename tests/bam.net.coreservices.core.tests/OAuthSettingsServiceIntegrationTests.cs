using Bam.Net.CommandLine;
using Bam.Net.Configuration;
using Bam.Net.Services.Clients;
using Bam.Net.Testing;
using Bam.Net.Testing.Integration;
using System;

namespace Bam.Net.Tests
{
    [Serializable]
    class OAuthSettingsServiceIntegrationTests : CommandLineTool
    {
        [ConsoleAction]
        [IntegrationTest]
        public void WillSaveSupportedOAuthSettings()
        {
            StaticApplicationNameProvider appNameProvider = new StaticApplicationNameProvider(nameof(WillSaveSupportedOAuthSettings));
            //CoreClient client = new CoreClient()
            // TODO: finish implementing this
            throw new NotImplementedException();
        }
    }
}
