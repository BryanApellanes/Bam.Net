
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using System.IO;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Incubation;
using Bam.Net.Logging;
using Yahoo.Yui.Compressor;
using System.Threading;

namespace Bam.Net.Data
{
    public partial class DaoProxyRegistration //fx
    {
        /// <summary>
        /// Initialize the inner registration container and 
        /// registers the mvc route for query interface (qi.js; pronounced "chi") 
        /// calls.
        /// </summary>
        internal static void Initialize()
        {
            _registrations = new Dictionary<string, DaoProxyRegistration>();
        }
    }
}
