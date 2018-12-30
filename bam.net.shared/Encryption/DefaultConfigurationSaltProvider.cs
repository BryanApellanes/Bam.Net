using Bam.Net.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Encryption
{
    public class DefaultConfigurationSaltProvider : ISaltProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultConfigurationSaltProvider"/> class
        /// initializing the salt to the value in the configuration file for the 
        /// specified saltKey
        /// </summary>
        /// <param name="saltKey">The salt key.</param>
        public DefaultConfigurationSaltProvider(string saltKey)
        {
            _salt = DefaultConfiguration.GetAppSetting(saltKey, 2.RandomLetters());            
        }

        public DefaultConfigurationSaltProvider() : this("Salt")
        { }

        static ISaltProvider _instance;
        static object _instanceLock = new object();
        public static ISaltProvider Instance
        {
            get
            {
                return _instanceLock.DoubleCheckLock(ref _instance, () => new DefaultConfigurationSaltProvider());
            }
        }

        public int SaltLength
        {
            get { return _salt.Length; }
            set { throw new InvalidOperationException("This SaltProvider uses salt from the configuration file and cannot set the length directly."); }
        }

        string _salt;
        public string GetSalt()
        {
            return _salt;
        }
    }
}
