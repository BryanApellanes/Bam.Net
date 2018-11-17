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
            throw new PlatformNotSupportedException("GetTypeId(KeyHashAuditRepoData instance, out object dynamicInstance, out Type dynamicType) is not supported on this platform, use GetTypeId(Type, out string) instead.");
        }
    }
}
