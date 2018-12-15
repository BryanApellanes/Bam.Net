using System;
using System.Reflection;

namespace Bam.Net.Application
{
    [Serializable]
    public class GenerationInfo
    {
        public Assembly Assembly { get; set; }
        public string SchemaName { get; set; }
        public string FromNameSpace { get; set; }
        public string ToNameSpace { get; set; }
    }
}
