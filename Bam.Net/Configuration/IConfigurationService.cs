﻿using System.Collections.Generic;

namespace Bam.Net.Configuration
{
    public interface IConfigurationService
    {
        Dictionary<string, string> GetApplicationConfiguration(string applicationName, string configurationName = "");
        void SetApplicationConfiguration(string applicationName, Dictionary<string, string> configuration, string configurationName);
    }
}