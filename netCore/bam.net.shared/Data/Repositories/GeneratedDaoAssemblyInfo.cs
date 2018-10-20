using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Schema;
using Bam.Net;
using Newtonsoft.Json;
using System.CodeDom.Compiler;

namespace Bam.Net.Data.Repositories
{
    public class GeneratedDaoAssemblyInfo: GeneratedAssemblyInfo
    {
        public GeneratedDaoAssemblyInfo() : base() { }
        public GeneratedDaoAssemblyInfo(string infoFileName, TypeSchema typeSchema, SchemaDefinition schemaDefintion) 
            : base(infoFileName)
        {
            TypeSchema = typeSchema;
            SchemaDefinition = schemaDefintion;
        }
        public GeneratedDaoAssemblyInfo(string infoFileName, CompilerResults results)
            : base(infoFileName, results)
        { }

        [Exclude]
        [JsonIgnore]
        public TypeSchema TypeSchema { get; set; }

        [Exclude]
        [JsonIgnore]
        public SchemaDefinition SchemaDefinition { get; set; }

        string _typeSchemaHash;
        public string TypeSchemaHash
        {
            get
            {
                if (string.IsNullOrEmpty(_typeSchemaHash))
                {
                    _typeSchemaHash = TypeSchema?.Hash;
                }

                return _typeSchemaHash;
            }
            set
            {
                _typeSchemaHash = value;
            }
        }
        string _typeSchemaInfo;
        public string TypeSchemaInfo
        {
            get
            {
                if (string.IsNullOrEmpty(_typeSchemaInfo))
                {
                    _typeSchemaInfo = TypeSchema?.ToString();
                }
                return _typeSchemaInfo;
            }
            set
            {
                _typeSchemaInfo = value;
            }
        }

        string _schemaName;
        public string SchemaName
        {
            get
            {
                if (string.IsNullOrEmpty(_schemaName) && SchemaDefinition != null)
                {
                    _schemaName = SchemaDefinition.Name;
                }
                return _schemaName;
            }
            set
            {
                _schemaName = value;
            }
        }
    }
}
