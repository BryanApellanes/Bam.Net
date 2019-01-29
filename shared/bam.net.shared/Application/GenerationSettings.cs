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

        public GenerationConfig ToConfig()
        {
            return new GenerationConfig
            {
                TypeAssembly = Assembly.GetFilePath(),
                SchemaName = SchemaName,
                FromNameSpace = FromNameSpace,
                ToNameSpace = ToNameSpace,
                WriteSrc = WriteSourceTo,
                UseInheritanceSchema = UseInheritanceSchema,
                CheckForIds = true
            };
        }
    }
}
