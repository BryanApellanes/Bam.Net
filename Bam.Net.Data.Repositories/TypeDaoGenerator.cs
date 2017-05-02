/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Bam.Net.Analytics;
using Bam.Net.CommandLine;
using Bam.Net.Configuration;
using Bam.Net.Data.Schema;
using Bam.Net.Logging;
using Newtonsoft.Json;

namespace Bam.Net.Data.Repositories
{
    /// <summary>
    /// A class used to generate data access objects from
    /// CLR types.
    /// </summary>
    public class TypeDaoGenerator : Loggable, IGeneratesDaoAssembly, IHasTypeSchemaTempPathProvider
    {
        DaoGenerator _daoGenerator;
        WrapperGenerator _wrapperGenerator;
        TypeSchemaGenerator _typeSchemaGenerator;
        HashSet<Assembly> _additonalReferenceAssemblies;

        /// <summary>
        /// Instantiate a new instance of TypeDaoGenerator
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="types"></param>
        public TypeDaoGenerator(ILogger logger = null, IEnumerable<Type> types = null)
        {
            _namespace = "TypeDaos";
            _daoGenerator = new DaoGenerator(DaoNamespace);
            _wrapperGenerator = new WrapperGenerator(WrapperNamespace, DaoNamespace);
            _typeSchemaGenerator = new TypeSchemaGenerator();
            _additonalReferenceAssemblies = new HashSet<Assembly>();

            TypeSchemaTempPathProvider = (schemaDef, typeSchema) => System.IO.Path.Combine(RuntimeSettings.AppDataFolder, "DaoTemp_{0}"._Format(schemaDef.Name));
            _types = new HashSet<Type>();
            if (logger != null)
            {
                Subscribe(logger);
            }
            if(types != null)
            {
                AddTypes(types);
            }
        }

        /// <summary>
        /// Instantiate a new instance of TypeDaoGenerator
        /// </summary>
        /// <param name="typeAssembly"></param>
        /// <param name="nameSpace"></param>
        /// <param name="logger"></param>
        public TypeDaoGenerator(Assembly typeAssembly, string nameSpace, ILogger logger = null)
            : this(logger)
        {
            BaseNamespace = nameSpace;
            Args.ThrowIfNull(typeAssembly, "typeAssembly");
            AddTypes(typeAssembly.GetTypes().Where(t => t.Namespace != null && t.Namespace.Equals(nameSpace)));
        }

        public TypeDaoGenerator(TypeSchemaGenerator typeSchemaGenerator) : this()
        {
            _typeSchemaGenerator = typeSchemaGenerator;
        }

        /// <summary>
        /// A filter function used to exclude anonymous types
        /// that were created by the use of lambda functions from 
        /// having dao types attempted to be generated
        /// </summary>
        public static Func<Type, bool> ClrDaoTypeFilter
        {
            get
            {
                return (t) => !t.IsAbstract && !t.HasCustomAttributeOfType<CompilerGeneratedAttribute>()
                && t.Attributes != (
                        TypeAttributes.NestedPrivate |
                        TypeAttributes.Sealed |
                        TypeAttributes.Serializable |
                        TypeAttributes.BeforeFieldInit
                    );
            }
        }

        public bool IncludeModifiedBy { get; set; }

        public bool CheckIdField { get; set; }

        string _namespace;
        /// <summary>
        /// The namespace to place generated classes into
        /// </summary>
        public string BaseNamespace
        {
            get
            {
                return _namespace;
            }
            set
            {
                _namespace = value;
                _daoGenerator.Namespace = DaoNamespace;
                _wrapperGenerator.WrapperNamespace = WrapperNamespace;
                _wrapperGenerator.DaoNamespace = DaoNamespace;
            }
        }

        string _daoNamespace;
        public string DaoNamespace
        {
            get { return _daoNamespace ?? $"{_namespace}.Dao"; }
            set
            {
                _daoNamespace = value;
                _daoGenerator.Namespace = _daoNamespace;
                _wrapperGenerator.DaoNamespace = _daoNamespace;
            }
        }

        string _wrapperNamespace;
        public string WrapperNamespace
        {
            get { return _wrapperNamespace ?? $"{_namespace}.Wrappers"; }
            set
            {
                _wrapperNamespace = value;
                _wrapperGenerator.WrapperNamespace = _wrapperNamespace;                
            }
        }

        string _schemaName;
        public string SchemaName
        {
            get
            {
                if (string.IsNullOrEmpty(_schemaName))
                {
                    _schemaName = string.Format("_{0}_Dao", _types.ToInfoHash());
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
            _typeSchemaGenerator.Subscribe(logger);
            base.Subscribe(logger);
        }

        HashSet<Type> _types;
        public Type[] Types
        {
            get
            {
                return _types.ToArray();
            }
        }

        public bool KeepSource { get; set; }

        public void AddTypes(IEnumerable<Type> types)
        {
            foreach (Type type in types)
            {
                AddType(type);
            }
        }

        public void AddType(Type type)
        {
            if (!ClrDaoTypeFilter(type))
                return;

            if (type.GetProperty("Id") == null &&
                !type.HasCustomAttributeOfType<KeyAttribute>() &&
                CheckIdField)
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
            GeneratedDaoAssemblyInfo info = GeneratedAssemblies.GetGeneratedAssemblyInfo(SchemaName) as GeneratedDaoAssemblyInfo;
            if (info == null)
            {
                TypeSchema typeSchema = SchemaDefinitionCreateResult.TypeSchema;
                SchemaDefinition schemaDef = SchemaDefinitionCreateResult.SchemaDefinition;
                string schemaName = schemaDef.Name;
                string schemaHash = typeSchema.Hash;
                info = new GeneratedDaoAssemblyInfo(schemaName, typeSchema, schemaDef);

                // check for the info file
                if (info.InfoFileExists && useExisting) // load it from file if it exists
                {
                    info = info.InfoFilePath.FromJsonFile<GeneratedDaoAssemblyInfo>();
                    if (info.TypeSchemaHash == null || !info.TypeSchemaHash.Equals(schemaHash)) // regenerate if the hashes don't match
                    {
                        ReportDiff(info, typeSchema);
                        GenerateOrThrow(schemaDef, typeSchema);
                    }
                    else
                    {
                        GeneratedAssemblies.SetAssemblyInfo(schemaName, info);
                    }
                }
                else
                {
                    GenerateOrThrow(schemaDef, typeSchema);
                }

                info = GeneratedAssemblies.GetGeneratedAssemblyInfo(SchemaName) as GeneratedDaoAssemblyInfo;
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

        public Func<SchemaDefinition, TypeSchema, string> TypeSchemaTempPathProvider { get; set; }

        [Verbosity(VerbosityLevel.Warning, MessageFormat = "TypeSchema difference detected\r\n {OldInfoString} \r\n *** \r\n {NewInfoString}")]
        public event EventHandler SchemaDifferenceDetected;
        public string OldInfoString { get; set; }
        public string NewInfoString { get; set; }
        public bool MissingColumns { get { return SchemaDefinitionCreateResult.MissingColumns; } }
        public SchemaWarnings Warnings { get { return SchemaDefinitionCreateResult.Warnings; } }

        public bool WarningsAsErrors
        {
            get; set;
        }
        [Verbosity(VerbosityLevel.Warning, MessageFormat = "Missing {PropertyType} property: {ClassName}.{PropertyName}")]
        public event EventHandler SchemaWarning;
        
        protected internal void EmitWarnings()
        {
            if (MissingColumns)
            {
                if (this.Warnings.MissingForeignKeyColumns.Length > 0)
                {
                    foreach (ForeignKeyColumn fk in this.Warnings.MissingForeignKeyColumns)
                    {
                        DaoRepositorySchemaWarningEventArgs drswea = GetEventArgs(fk);
                        FireEvent(SchemaWarning, drswea);
                    }
                }
                if (this.Warnings.MissingKeyColumns.Length > 0)
                {
                    foreach (KeyColumn keyColumn in this.Warnings.MissingKeyColumns)
                    {
                        DaoRepositorySchemaWarningEventArgs drswea = GetEventArgs(keyColumn);
                        FireEvent(SchemaWarning, drswea);
                    }
                }
            }
        }
        public void ThrowWarningsIfWarningsAsErrors()
        {
            if (MissingColumns && WarningsAsErrors)
            {
                if (this.Warnings.MissingForeignKeyColumns.Length > 0)
                {
                    List<string> missingColumns = new List<string>();
                    foreach (ForeignKeyColumn fk in this.Warnings.MissingForeignKeyColumns)
                    {
                        DaoRepositorySchemaWarningEventArgs drswea = GetEventArgs(fk);
                        missingColumns.Add("{ClassName}.{PropertyName}".NamedFormat(drswea));
                    }
                    throw new MissingForeignKeyPropertyException(missingColumns);
                }
                if (this.Warnings.MissingKeyColumns.Length > 0)
                {
                    List<string> classNames = new List<string>();
                    foreach (KeyColumn k in this.Warnings.MissingKeyColumns)
                    {
                        DaoRepositorySchemaWarningEventArgs drswea = GetEventArgs(k);
                        classNames.Add(k.TableClassName);
                    }
                    throw new NoIdPropertyException(classNames);
                }
            }
        }
        /// <summary>
        /// Create a SchemaDefintionCreateResult for the types currently
        /// added to the TypeDaoGenerator
        /// </summary>
        /// <param name="schemaName"></param>
        /// <returns></returns>
        protected internal SchemaDefinitionCreateResult CreateSchemaDefinition(string schemaName = null)
        {
            return _typeSchemaGenerator.CreateSchemaDefinition(_types, schemaName);
        }
        protected internal bool GenerateDaoAssembly(TypeSchema typeSchema, out CompilationException compilationEx)
        {
            try
            {
                compilationEx = null;
                SchemaDefinition schema = SchemaDefinitionCreateResult.SchemaDefinition;
                string assemblyName = "{0}.dll"._Format(schema.Name);

                string writeSourceTo = TypeSchemaTempPathProvider(schema, typeSchema);
                CompilerResults results = GenerateAndCompile(assemblyName, writeSourceTo);
                GeneratedDaoAssemblyInfo info = new GeneratedDaoAssemblyInfo(schema.Name, results);
                info.TypeSchema = typeSchema;
                info.SchemaDefinition = schema;
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
            GenerateSource(writeSourceTo);

            return Compile(assemblyNameToCreate, writeSourceTo);
        }

        /// <summary>
        /// Generate source code for the current set of types
        /// </summary>
        /// <param name="writeSourceTo"></param>
        public virtual void GenerateSource(string writeSourceTo)
        {
            EmitWarnings();
            ThrowWarningsIfWarningsAsErrors();
            GenerateDaos(SchemaDefinitionCreateResult.SchemaDefinition, writeSourceTo);
            GenerateWrappers(SchemaDefinitionCreateResult.TypeSchema, writeSourceTo);
        }

        protected internal void GenerateDaos(SchemaDefinition schema, string writeSourceTo)
        {
            _daoGenerator.Generate(schema, writeSourceTo);
        }

        protected internal void GenerateWrappers(TypeSchema schema, string writeSourceTo)
        {
            _wrapperGenerator.Generate(schema, writeSourceTo);
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
        private static DaoRepositorySchemaWarningEventArgs GetEventArgs(KeyColumn keyColumn)
        {
            string className = keyColumn.TableClassName;
            DaoRepositorySchemaWarningEventArgs drswea = new DaoRepositorySchemaWarningEventArgs { ClassName = className, PropertyName = "Id", PropertyType = "key column" };
            return drswea;
        }
        private static DaoRepositorySchemaWarningEventArgs GetEventArgs(ForeignKeyColumn fk)
        {
            string referencingClassName = fk.ReferencingClass.EndsWith("Dao") ? fk.ReferencingClass.Truncate(3) : fk.ReferencingClass;
            string propertyName = fk.PropertyName;
            DaoRepositorySchemaWarningEventArgs drswea = new DaoRepositorySchemaWarningEventArgs { ClassName = referencingClassName, PropertyName = propertyName, PropertyType = "foreign key" };
            return drswea;
        }
        private void GenerateOrThrow(SchemaDefinition schema, TypeSchema typeSchema)
        {
            string tempPath = TypeSchemaTempPathProvider(schema, typeSchema);
            if (Directory.Exists(tempPath))
            {
                string newPath = tempPath.GetNextDirectoryName();
                Directory.Move(tempPath, newPath);
            }
            CompilationException compilationException;
            if (!GenerateDaoAssembly(typeSchema, out compilationException))
            {
                throw new DaoGenerationException(SchemaName, typeSchema.Hash, Types.ToArray(), compilationException);
            }
        }

        protected internal bool TryDeleteDaoTemp(string writeSourceTo)
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

        private void ReportDiff(GeneratedDaoAssemblyInfo info, TypeSchema typeSchema)
        {
            OldInfoString = info.TypeSchemaInfo ?? string.Empty;
            NewInfoString = typeSchema.ToString();
            DiffReport diff = DiffReport.Create(OldInfoString, NewInfoString);
            ConsoleDiffReportFormatter diffFormatter = new ConsoleDiffReportFormatter(diff);
            diffFormatter.Format(); // outputs to console
            FireEvent(SchemaDifferenceDetected, new SchemaDifferenceEventArgs { GeneratedDaoAssemblyInfo = info, TypeSchema = typeSchema, DiffReport = diff });
        }

    }
}
