using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Encryption
{
    public class CredentialInfo
    {
        public CredentialInfo()
        {
        }

        [JsonIgnore]
        public bool IsNull
        {
            get
            {
                return string.IsNullOrEmpty(Password);
            }
        }

        public string UserName { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the target service.  This will
        /// be either the name of an application that the
        /// credentials are used to access or the name
        /// of the service that uses the credentials.
        /// May be null.
        /// </summary>
        /// <value>
        /// The target service.
        /// </value>
        public string TargetService { get; set; }

        /// <summary>
        /// Gets or sets the name of the machine these credentials
        /// are intended for.  May be null.
        /// </summary>
        /// <value>
        /// The name of the machine.
        /// </value>
        public string MachineName { get; set; }
    }
}
