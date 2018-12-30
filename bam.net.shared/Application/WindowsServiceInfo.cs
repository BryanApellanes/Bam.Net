using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    public class WindowsServiceInfo
    {
        public WindowsServiceInfo()
        {
            Host = string.Empty;
            Name = string.Empty;
            FileName = string.Empty;
            AppSettings = new Dictionary<string, string>();
        }

        public string Host { get; set; }
        /// <summary>
        /// Gets or sets the name of the service, used to name the destination folder.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the executable service file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the application settings, these values are written to the app.config file for the service
        /// after it has been copied into place and before the service is started.
        /// </summary>
        /// <value>
        /// The application settings.
        /// </value>
        public Dictionary<string, string> AppSettings { get; set; }
    }
}
