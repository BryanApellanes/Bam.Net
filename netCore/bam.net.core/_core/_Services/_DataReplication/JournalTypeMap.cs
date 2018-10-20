using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public partial class JournalTypeMap
    {
        public static long GetTypeId(KeyHashAuditRepoData instance, out object dynamicInstance, out Type dynamicType)
        {
            Log.AddEntry("GetTypeId(KeyHashAuditRepoData instance, out object dynamicInstance, out Type dynamicType) is not supported on this platform and dynamicInstance will be a bald object.");
            dynamicInstance = new object();
            dynamicType = null;
            return GetTypeId(instance.GetType(), out string ignore);
        }
    }
}
