/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using Bam.Net.Data;
using Bam.Net.Logging;
using System.IO;
using System.Reflection;

namespace Bam.Net.Yaml
{
    /// <summary>
    /// A class used to generate Daos.  Internally
    /// uses a YamlSchemaGenerator to generate a YamlSchema, a 
    /// YamlTypeSchemaGenerator to transform the YamlSchema into 
    /// a TypeSchema and a TypeDaoGenerator to create a Dao Assembly
    /// from the TypeSchema.  NOTE: this is not well tested
    /// </summary>
    public partial class YamlDaoGenerator : TypeDaoGenerator
    {
        public Assembly GetDaoAssembly(string directoryPath, string schemaName, string fileExtension = "*.yaml")
        {
            return GetDaoAssembly(new DirectoryInfo(directoryPath), schemaName, fileExtension);
        }

        public Assembly GetDaoAssembly(DirectoryInfo rootDir, string schemaName, string fileExtension = "*.yaml")
        {
            YamlSchema yamlSchema = YamlSchemaGenerator.GenerateYamlSchema(rootDir, fileExtension);
            this.DeserializationFailures = yamlSchema.Failures;
            Subscribe(yamlSchema);
            DynamicYamlTypes = yamlSchema.GetDynamicTypes();
            AddTypes(DynamicYamlTypes.ToArray());
            return GetDaoAssembly();
        }
    }
}
