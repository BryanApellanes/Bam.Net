using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;

namespace Bam.Net.CoreServices.ProtoBuf
{
    /// <summary>
    /// A class used to generate protobuf .proto files
    /// from clr types
    /// </summary>
    public class ProtoFileGenerator: Loggable
    {
        public ProtoFileGenerator() 
            : this(new InMemoryPropertyNumberer())
        { }

        public ProtoFileGenerator(IPropertyNumberer propertyNumberer) 
            : this(new TypeSchemaGenerator(), propertyNumberer)
        { }

        public ProtoFileGenerator(TypeSchemaGenerator typeSchemaGenerator, IPropertyNumberer propertyNumberer, Func<PropertyInfo, bool> propertyFilter = null)
        {
            Args.ThrowIfNull(typeSchemaGenerator);
            Args.ThrowIfNull(propertyNumberer, nameof(propertyNumberer));
            TypeSchemaGenerator = typeSchemaGenerator;
            PropertyNumberer = propertyNumberer;
            OutputDirectory = ".\\Generated_Protobuf";
            PropertyFilter = propertyFilter ?? ((p) => true);
        }

        [Verbosity(VerbosityLevel.Warning, MessageFormat = "ProtoFileDirectory:{ProtoFileDirectory}: {Message}")]
        public event EventHandler Warning;

        [Verbosity(VerbosityLevel.Information, MessageFormat = "{Message}")]
        public event EventHandler ProtoGenerationStarted;

        [Verbosity(VerbosityLevel.Information, MessageFormat = "{Message}")]
        public event EventHandler ProtoGenerationComplete;

        [Verbosity(VerbosityLevel.Error, MessageFormat = "{Message}")]
        public event EventHandler ProtoGenerationError;

        public string Message { get; set; }
        public string OutputDirectory { get; set; }
        public Func<PropertyInfo, bool> PropertyFilter { get; set; }
        public IPropertyNumberer PropertyNumberer { get; set; }
        public string TargetNamespace { get; set; }
        public void GenerateProtoFile(params Type[] clrTypes)
        {
            GenerateProtoFile(OutputDirectory, clrTypes);
        }

        public void GenerateProtoFile(string protoFileDirectory, params Type[] clrTypes)
        {
            GenerateProtoFile(clrTypes, protoFileDirectory);
        }
        public void GenerateProtoFile(IRepository repo, string protoFileDirectory = null)
        {
            GenerateProtoFile(repo.StorableTypes, protoFileDirectory);
        }
        public string GenerateProtoFile(IEnumerable<Type> clrTypes, string protoFileDirectory = null)
        {
            try
            {
                FireEvent(ProtoGenerationStarted);
                TypeSchema typeSchema = TypeSchemaGenerator.CreateTypeSchema(clrTypes);
                protoFileDirectory = protoFileDirectory ?? OutputDirectory;
                DirectoryInfo outputDir = new DirectoryInfo(protoFileDirectory);
                string nameSpace = GetNamespace(clrTypes);
                string targetNamespace = string.IsNullOrEmpty(TargetNamespace) ? $"{nameSpace}.Protobuf" : TargetNamespace;
                string filePath = Path.Combine(outputDir.FullName, $"{nameSpace}.proto");
                string moveExistingFileTo = new FileInfo(filePath).FullName.GetNextFileName();
                if (File.Exists(filePath))
                {
                    File.Move(filePath, moveExistingFileTo);
                }
                StringBuilder protoMessages = new StringBuilder();
                protoMessages.AppendLine("syntax = \"proto3\";");
                protoMessages.AppendLine($"package {targetNamespace};");
                foreach (Type type in typeSchema.Tables)
                {
                    ProtocolBufferType protoType = new ProtocolBufferType(type, PropertyNumberer, PropertyFilter);
                    StringBuilder properties = new StringBuilder();
                    foreach (ProtocolBufferProperty prop in protoType.Properties)
                    {
                        string propertyFormat = prop.IsRepeated ? ArrayPropertyFormat : PropertyFormat;
                        properties.Append(propertyFormat.NamedFormat(prop));
                    }
                    ProtocolBufferTypeModel model = new ProtocolBufferTypeModel { TypeName = protoType.TypeName, Properties = properties.ToString() };
                    protoMessages.Append(MessageFormat.NamedFormat(model));
                }
                protoMessages.ToString().SafeWriteToFile(filePath);
                FireEvent(ProtoGenerationComplete);
                return filePath;

            }
            catch (Exception ex)
            {
                Message = ex.Message;
                FireEvent(ProtoGenerationError);
                return string.Empty;
            }
        }
        
        protected TypeSchemaGenerator TypeSchemaGenerator
        {
            get;set;
        }

        private string GetNamespace(IEnumerable<Type> clrTypes)
        {
            string nameSpace = "";
            foreach(Type type in clrTypes)
            {
                if (string.IsNullOrEmpty(nameSpace))
                {
                    nameSpace = type.Namespace;
                }
                if (!nameSpace.Equals(type.Namespace))
                {
                    Message = $"Different namespaces found, will use ({nameSpace}) (JUST FYI): \r\n\t({nameSpace})\r\n\t({type.Namespace})";
                    FireEvent(Warning);
                }
            }
            return string.IsNullOrEmpty(nameSpace) ? "ProtoBuf": nameSpace;
        }

        private static string MessageFormat
        {
            get
            {
                return @"
message {TypeName}{{
{Properties}}}
";
            }
        }

        private static string PropertyFormat
        {
            get
            {
                return @"   {TypeName} {PropertyName} = {PropertyNumberId};
";
            }
        }

        private static string ArrayPropertyFormat
        {
            get
            {
                return @"   repeated {TypeName} {PropertyName} = {PropertyNumberId};
";
            }
        }
    }
}
