using Bam.Net.Data;
using Bam.Net.Data.Repositories;
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
            dynamicInstance = instance.ToDynamic(p => p.PropertyType.IsValueType || p.PropertyType == typeof(string), out dynamicType);
            return GetTypeId(dynamicType, out string ignore);
        }

    }
}
