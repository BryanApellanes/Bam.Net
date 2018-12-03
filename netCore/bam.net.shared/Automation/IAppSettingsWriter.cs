using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Automation
{
    public interface IAppSettingsWriter
    {
        void SetAppSettings(string host, string localPathOnRemote, Dictionary<string, string> appSettings);
    }
}
