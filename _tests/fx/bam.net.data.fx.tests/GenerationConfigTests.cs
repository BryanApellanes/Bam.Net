using Bam.Net.Application;
using Bam.Net.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Tests
{
    [Serializable]
    public class GenerationConfigTests
    {
        [ConsoleAction]
        public void CanSerializeAndDeserializeYamlConfig()
        {
            GenerationConfig config = new GenerationConfig()
            {
                TypeAssembly = "path to assembly",
                SchemaName = "TestSchemaName",
                FromNameSpace = "From.Name.Space",
                ToNameSpace = "To.Name.Space",
                WriteSrc = "C:\\bam\\src\\_gen",
                CheckForIds = true,
                UseInheritanceSchema = false
            };
            string path = ".\\generationConfig.yaml";
            config.ToYamlFile(path);
            $"notepad {path}".Run();
        }
    }
}
