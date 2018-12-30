using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy;
using Bam.Net.Configuration;

namespace Bam.Net.Server
{
    public class FileUploadEventArgs: EventArgs
    {
        public FileUploadEventArgs(IHttpContext context, HttpPostedFile file, string applicationName = null)
        {
            applicationName = string.IsNullOrEmpty(applicationName) ? DefaultConfiguration.DefaultApplicationName : applicationName;
            HttpContext = context;
            PostedFile = file;
            Continue = true;
            ApplicationName = applicationName;
        }
        public string ApplicationName { get; private set; }
        public IHttpContext HttpContext { get; private set; }
        public HttpPostedFile PostedFile { get; private set; }

        public string FilePath
        {
            get
            {
                return PostedFile.FullPath;
            }
        }
        public string UserName { get; set; }
        /// <summary>
        /// Continue the handling of the upload
        /// </summary>
        public bool Continue { get; set; }
    }
}
