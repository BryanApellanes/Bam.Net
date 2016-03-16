/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Bam.Net.Data.Schema;
using Bam.Net.Logging;
using Newtonsoft.Json;

namespace Bam.Net.Data.Repositories
{
    /// <summary>
    /// A class used to generate data access objects from
    /// CLR types.
    /// </summary>
    public class TypeDaoGenerator : Loggable, IGeneratesDaoAssembly
    {
        DaoGenerator _daoGenerator;
        PocoGenerator _pocoGenerator;
        TypeSchemaGenerator _schemaGenerator;
        HashSet<Assembly> _additonalReferenceAssemblies;
        public TypeDaoGenerator(ILogger logger = null)
        {
            _namespace = "TypeDaos";
            _daoGenerator = new DaoGenerator(_namespace);
            _pocoGenerator = new PocoGenerator(_namespace);
            _schemaGenerator = new TypeSchemaGenerator();
            _additonalReferenceAssemblies = new HashSet<Assembly>();

            TempPathProvider = (schemaName) => Path.Combine("".GetAppDataFolder(), "DaoTemp_{0}"._Format(schemaName));
            _types = new List<Type>();
            if (logger != null)
            {
                Subscribe(logger);
            }
        }
        
        public bool AddAuditFields { get; set; }

        public bool IncludeModifiedBy { get; set; }

        public bool AddIdField { get; set; }

        string _namespace;
        /// <summary>
        /// The namespace to place generated Dao objects into
        /// </summary>
        public string Namespace
        {
            get
            {
                return _namespace;
            }
            set
            {
                _namespace = value;
                _daoGenerator.Namespace = value;
                _pocoGenerator.Namespace = value;
            }
        }

        string _schemaName;
        public string SchemaName
        {
            get
            {
                if (string.IsNullOrEmpty(_schemaName))
                {
                    _schemaName = string.Format("_{0}_Dao", _types.Select(t => t.AssemblyQualifiedName).ToArray().ToDelimited(s => s, ", ").Md5());
                }

                return _schemaName;
            }
            set
            {
                _schemaName = value;

            }
        }

        public override void Subscribe(ILogger logger)
        {
            _schemaGenerator.Subscribe(logger);
            base.Subscribe(logger);
        }

        List<Type> _types;
        public Type[] Types
        {
            get
            {
                return _types.ToArray();
            }
        }

        public bool KeepSource { get; set; }

        public void AddTypes(Type[] types)
        {
            foreach (Type type in types)
            {
                AddType(type);
            }
        }

        public void AddType(Type type)
        {
            if (type.GetProperty("Id") == null &&
                !type.HasCustomAttributeOfType<KeyAttribute>() &&
                !AddIdField)
            {
                throw new NoIdPropertyException(type);
            }

            // TODO: Investigate if this is necessary.  Can a type have children that are 
            // of the same type as itself
            if (type.HasEnumerableOfMe(type))
            {
                throw new NotSupportedException("Storable types cannot have enumerable properties that are of the same type as themselves.");
            }

            _additonalReferenceAssemblies.Add(type.Assembly);
            _types.Add(type);
        }

        public Assembly GetDaoAssembly(bool useExisting = true)
        {
            GeneratedAssemblyInfo info = GeneratedAssemblies.GetGeneratedAssemblyInfo(SchemaName);
            if (info == null)
            {
                SchemaDefinition schema = SchemaDefinitionCreateResult.SchemaDefinition;
                info = new GeneratedAssemblyInfo(schema.Name);

                // check for the info file
                if (info.InfoFileExists && useExisting) // load it from file if it exists
                {
                    info = info.InfoFilePath.FromJsonFile<GeneratedAssemblyInfo>();
                    if (!info.InfoFileName.Equals(schema.Name)) // regenerate if the names don't match
                    {
                        GenerateOrThrow();
                    }
                    else
                    {
                        GeneratedAssemblies.SetAssemblyInfo(schema.Name, info);
                    }
                }
                else
                {
                    GenerateOrThrow();
                }

                info = GeneratedAssemblies.GetGeneratedAssemblyInfo(SchemaName);
            }

            return info.GetAssembly();
        }

        SchemaDefinitionCreateResult _schemaDefinitionCreateResult;
        object _schemaDefinitionCreateResultLock = new object();
        public SchemaDefinitionCreateResult SchemaDefinitionCreateResult
        {
            get
            {
                return _schemaDefinitionCreateResultLock.DoubleCheckLock(ref _schemaDefinitionCreateResult, () => CreateSchemaDefinition(SchemaName));
            }
        }

        [Verbosity(VerbosityLevel.Error, MessageFormat = "Failed to generate DaoAssembly for {SchemaName}:\r\n {Message}")]
        public event EventHandler GenerateDaoAssemblyFailed;

        [Verbosity(VerbosityLevel.Information, MessageFormat = "{Message}")]
        public event EventHandler GenerateDaoAssemblySucceeded;

        public string TempPath { get; set; }

        public string Message { get; set; }

        [Verbosity(VerbosityLevel.Warning, MessageFormat = "Couldn't delete folder {TempPath}:\r\nMessage: {Message}")]
        public event EventHandler DeleteDaoTempFailed;

        public Func<string, string> TempPathProvider { get; set; }
        
        /// <summary>
        /// Create a SchemaDefintionCreateResult for the types currently
        /// added to the TypeDaoGenerator
        /// </summary>
        /// <param name="schemaName"></param>
        /// <returns></returns>
        protected internal SchemaDefinitionCreateResult CreateSchemaDefinition(string schemaName = null)
        {
            return _schemaGenerator.CreateSchemaDefinition(_types, schemaName);
        }
        protected internal bool GenerateDaoAssembly(out CompilationException compilationEx)
        {
            try
            {
                compilationEx = null;
                SchemaDefinition schema = SchemaDefinitionCreateResult.SchemaDefinition;
                string assemblyName = "{0}.dll"._Format(schema.Name);

                string writeSourceTo = TempPathProvider(schema.Name);
                CompilerResults results = GenerateAndCompile(assemblyName, writeSourceTo);
                GeneratedAssemblyInfo info = new GeneratedAssemblyInfo(schema.Name, results);
                info.Save();

                GeneratedAssemblies.SetAssemblyInfo(schema.Name, info);

                Message = "Type Dao Generation completed successfully";
                FireEvent(GenerateDaoAssemblySucceeded, new GenerateDaoAssemblyEventArgs(info));

                TryDeleteDaoTemp(writeSourceTo);

                return true;
            }
            catch (CompilationException ex)
            {
                Message = ex.Message;
                if (!string.IsNullOrEmpty(ex.StackTrace))
                {
                    Message = "{0}:\r\nStackTrace: {1}"._Format(Message, ex.StackTrace);
                }
                compilationEx = ex;
                FireEvent(GenerateDaoAssemblyFailed, EventArgs.Empty);
                return false;
            }
        }

        protected internal CompilerResults GenerateAndCompile(string assemblyNameToCreate, string writeSourceTo)
        {
            TryDeleteDaoTemp(writeSourceTo);

            GenerateDaos(SchemaDefinitionCreateResult.SchemaDefinition, writeSourceTo);
            GeneratePocos(SchemaDefinitionCreateResult.TypeSchema, writeSourceTo);

            return Compile(assemblyNameToCreate, writeSourceTo);
        }

        protected internal void GenerateDaos(SchemaDefinition schema, string writeSourceTo)
        {
            _daoGenerator.Generate(schema, writeSourceTo);
        }

        protected internal void GeneratePocos(TypeSchema schema, string writeSourceTo)
        {
            _pocoGenerator.Generate(schema, writeSourceTo);
        }

        protected internal CompilerResults Compile(string assemblyNameToCreate, string writeSourceTo)
        {
            HashSet<string> references = new HashSet<string>(DaoGenerator.DefaultReferenceAssemblies.ToArray());
            references.Add(typeof(JsonIgnoreAttribute).Assembly.GetFileInfo().FullName);
            _additonalReferenceAssemblies.Each(asm =>
            {
                references.Add(asm.GetFilePath());
            });
            SchemaDefinitionCreateResult.TypeSchema.Tables.Each(type => references.Add(type.Assembly.GetFileInfo().FullName));
            references.Add(typeof(DaoRepository).Assembly.GetFileInfo().FullName);
            CompilerResults results = _daoGenerator.Compile(new DirectoryInfo(writeSourceTo), assemblyNameToCreate, references.ToArray(), false);
            return results;
        }

        private void GenerateOrThrow()
        {
            string tempPath = TempPathProvider(SchemaName);
            if (Directory.Exists(tempPath))
            {
                Directory.Move(tempPath, $"{tempPath}_{DateTime.UtcNow.ToJulianDate().ToString()}");
            }
            CompilationException compilationException;
            if (!GenerateDaoAssembly(out compilationException))
            {
                throw new DaoGenerationException(SchemaName, Types.ToArray(), compilationException);
            }
        }

        private bool TryDeleteDaoTemp(string writeSourceTo)
        {
            if (!KeepSource)
            {
                try
                {
                    TempPath = writeSourceTo;
                    if (Directory.Exists(writeSourceTo))
                    {
                        Directory.Delete(writeSourceTo, true);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                    if (!string.IsNullOrEmpty(ex.StackTrace))
                    {
                        Message = string.Format("{0}\r\nStackTrace: {1}", Message, ex.StackTrace);
                    }
                    FireEvent(DeleteDaoTempFailed, EventArgs.Empty);
                    return false;
                }
            }
            return false;
        }
    }
}
