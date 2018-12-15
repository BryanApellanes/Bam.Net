/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data
{
    public class ConnectionInfo
    {
        public ConnectionInfo()
        {
            this._settings = new Dictionary<string, string>();
        }

        public string ConnectionString
        {
            get;
            set;
        }

        public string ProviderName
        {
            get;
            set;
        }

        Dictionary<string, string> _settings;
        public string this[string key]
        {
            get
            {
                return _settings[key];
            }
            set
            {
                _settings[key] = value;
            }
        }
    }
}
