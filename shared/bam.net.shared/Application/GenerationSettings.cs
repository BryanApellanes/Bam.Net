using System;
using System.Reflection;

namespace Bam.Net.Application
{
    [Serializable]
    public class GenerationSettings
    {
        public Assembly Assembly { get; set; }
        public string SchemaName { get; set; }
        public string FromNameSpace { get; set; }
        public string ToNameSpace { get; set; }

        public bool UseInheritanceSchema { get; set; }
        public string WriteSourceTo { get; set; }
    }
}
