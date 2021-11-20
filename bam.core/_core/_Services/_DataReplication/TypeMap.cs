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
    public partial class TypeMap
    {
        public static ulong GetTypeId(CompositeKeyAuditRepoData instance, out object dynamicInstance, out Type dynamicType)
        {
            dynamicInstance = instance.ToDictionary();
            Type type = instance.GetType();
            dynamicType = type;
            return GetTypeId(type);
        }
    }
}
