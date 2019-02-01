using Bam.Net.Data.Dynamic.Data;
using Bam.Net.Data.Dynamic.Data.Dao.Repository;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Yaml;
using YamlDotNet.Serialization;

namespace Bam.Net.Data.Dynamic
{
    /// <summary>
    /// Creates type definitions for json and yaml strings.
    /// </summary>
    public class DynamicTypeManager: Loggable
    {
        public DynamicTypeManager() : this(new DynamicTypeDataRepository(), DefaultDataDirectoryProvider.Current)
        { }

        public DynamicTypeManager(DynamicTypeDataRepository descriptorRepository, IDataDirectoryProvider settings) 
        {
            DataSettings = settings;
            JsonDirectory = settings.GetRootDataDirectory(nameof(DynamicTypeManager), "json");
            if (!JsonDirectory.Exists)
            {
                JsonDirectory.Create();
            }
            YamlDirectory = settings.GetRootDataDirectory(nameof(DynamicTypeManager), "yaml");
            if (!YamlDirectory.Exists)
            {
                YamlDirectory.Create();
            }
            
            descriptorRepository.EnsureDaoAssemblyAndSchema();
            DynamicTypeNameResolver = new DynamicTypeNameResolver();
            DynamicTypeDataRepository = descriptorRepository;
            JsonFileProcessor = new BackgroundThreadQueue<DataFile>()
            {
                Process = (df) =>
                {
                    ProcessJsonFile(df.TypeName, df.FileInfo);
                }
            };
            YamlFileProcessor = new BackgroundThreadQueue<DataFile>()
            {
                Process = (df) =>
                {
                    ProcessYamlFile(df.TypeName, df.FileInfo);
                }
            };
        }

        public IDataDirectoryProvider DataSettings { get; set; }
        public DynamicTypeNameResolver DynamicTypeNameResolver { get; set; }
        public DynamicTypeDataRepository DynamicTypeDataRepository { get; set; }
        public DirectoryInfo JsonDirectory { get; set; }
        public DirectoryInfo YamlDirectory { get; set; }
        public BackgroundThreadQueue<DataFile> JsonFileProcessor { get; }
        public BackgroundThreadQueue<DataFile> YamlFileProcessor { get; }

        /// <summary>
        /// Write source code to the specified appData folder and return
        /// the source as well.
        /// </summary>
        /// <param name="appData">The application data.</param>
        /// <param name="nameSpace">The name space.</param>
        /// <returns></returns>
        public string GenerateSource(DirectoryInfo appData, string nameSpace = null)
        {
            nameSpace = nameSpace ?? DynamicNamespaceDescriptor.DefaultNamespace;
            List<DynamicDataSaveResult>  results = ProcessDataFiles(appData, nameSpace);

            string source = GenerateSource(nameSpace);
            WriteSource(appData, source);

            return source;
        }

        /// <summary>
        /// Generates classes to represent data found in AppData/json and AppData/yaml placing the assembly 
        /// fil into the _gen/bin directory of the speicifed appData folder.
        /// </summary>
        /// <param name="appData">The application data.</param>
        /// <param name="nameSpace">The name space.</param>
        /// <returns></returns>
        public Assembly GenerateAssembly(DirectoryInfo appData, string nameSpace = null)
        {
            nameSpace = nameSpace ?? DynamicNamespaceDescriptor.DefaultNamespace;
            ProcessDataFiles(appData, nameSpace);

            Assembly assembly = GenerateAssembly(out string src, nameSpace);
            FileInfo assemblyFile = assembly.GetFileInfo();
            WriteSource(appData, src);

            WriteAssembly(appData, assemblyFile);

            return assembly;
        }

        private List<DynamicDataSaveResult> ProcessDataFiles(DirectoryInfo appData, string nameSpace)
        {
            List<DynamicDataSaveResult> results = new List<DynamicDataSaveResult>();
            results.AddRange(ProcessJson(appData, nameSpace));
            results.AddRange(ProcessYaml(appData, nameSpace));
            return results;
        }

        private static void WriteAssembly(DirectoryInfo appData, FileInfo assemblyFile)
        {
            DirectoryInfo binDir = new DirectoryInfo(Path.Combine(appData.FullName, "_gen", "bin"));
            if (!binDir.Exists)
            {
                binDir.Create();
            }
            string dllFilePath = Path.Combine(binDir.FullName, assemblyFile.Name);
            if (File.Exists(dllFilePath))
            {
                File.Move(dllFilePath, dllFilePath.GetNextFileName());
            }
            assemblyFile.MoveTo(dllFilePath);
        }

        private static void WriteSource(DirectoryInfo appData, string src)
        {
            DirectoryInfo srcDir = new DirectoryInfo(Path.Combine(appData.FullName, "_gen", "src"));
            if (!srcDir.Exists)
            {
                srcDir.Create();
            }
            FileInfo sourceFile = new FileInfo(Path.Combine(srcDir.FullName, $"{src.Sha256()}.cs"));
            src.SafeWriteToFile(sourceFile.FullName, true);
        }

        public List<DynamicDataSaveResult> ProcessJson(DirectoryInfo appData, string nameSpace = null)
        {
            DirectoryInfo jsonDirectory = new DirectoryInfo(Path.Combine(appData.FullName, "json"));
            List<DynamicDataSaveResult> results = new List<DynamicDataSaveResult>();
            foreach(FileInfo jsonFile in jsonDirectory.GetFiles("*.json"))
            {
                string typeName = DynamicTypeNameResolver.ResolveJsonTypeName(jsonFile.ReadAllText());
                results.Add(ProcessJsonFile(typeName, jsonFile, nameSpace ?? DynamicNamespaceDescriptor.DefaultNamespace));
            }
            return results;
        }

        public List<DynamicDataSaveResult> ProcessYaml(DirectoryInfo appData, string nameSpace = null)
        {
            DirectoryInfo yamlDirectory = new DirectoryInfo(Path.Combine(appData.FullName, "yaml"));
            List<DynamicDataSaveResult> results = new List<DynamicDataSaveResult>();
            foreach (FileInfo yamlFile in yamlDirectory.GetFiles("*.yaml"))
            {
                string typeName = DynamicTypeNameResolver.ResolveYamlTypeName(yamlFile.ReadAllText());
                results.Add(ProcessYamlFile(typeName, yamlFile, nameSpace ?? DynamicNamespaceDescriptor.DefaultNamespace));
            }
            return results;
        }

        public void ProcessYaml(string yaml)
        {
            ProcessYaml(DynamicTypeNameResolver.ResolveYamlTypeName(yaml), yaml);
        }

        public void ProcessJson(string json)
        {
            ProcessJson(DynamicTypeNameResolver.ResolveJsonTypeName(json), json);
        }

        public void ProcessJson(string typeName, string json)
        {
            string filePath = WriteJsonFile(json);
            JsonFileProcessor.Enqueue(new DataFile { FileInfo = new FileInfo(filePath), TypeName = typeName });
        }

        protected string WriteJsonFile(string json)
        {
            string filePath = Path.Combine(JsonDirectory.FullName, $"{json.Sha512()}.json").GetNextFileName();
            json.SafeWriteToFile(filePath);
            return filePath;
        }

        public void ProcessYaml(string typeName, string yaml)
        {
            string filePath = Path.Combine(YamlDirectory.FullName, $"{yaml.Sha512()}.yaml").GetNextFileName();
            yaml.SafeWriteToFile(filePath);
            YamlFileProcessor.Enqueue(new DataFile { FileInfo = new FileInfo(filePath), TypeName = typeName });
        }

        public DynamicTypeDescriptor AddType(string typeName)
        {
            return AddType(typeName, null);
        }

        public DynamicTypeDescriptor AddType(string typeName, string nameSpace)
        {
            return AddType(typeName, nameSpace, out DynamicNamespaceDescriptor ignore);
        }

        public DynamicTypeDescriptor AddType(string typeName, string nameSpace, out DynamicNamespaceDescriptor dynamicNamespaceDescriptor)
        {
            nameSpace = string.IsNullOrEmpty(nameSpace) ? DynamicNamespaceDescriptor.DefaultNamespace : nameSpace;
            return EnsureType(typeName, nameSpace, out dynamicNamespaceDescriptor);
        }

        public DynamicTypePropertyDescriptor AddProperty(string typeName, string propertyName, string propertyType, string nameSpace = null)
        {
            Type type = Type.GetType(propertyType);
            if(type == null)
            {
                type = Type.GetType($"System.{propertyType}");
            }
            Args.ThrowIfNull(type, "propertyType");
            nameSpace = nameSpace ?? DynamicNamespaceDescriptor.DefaultNamespace;
            DynamicNamespaceDescriptor nameSpaceDescriptor = EnsureNamespace(nameSpace);
            DynamicTypeDescriptor typeDescriptor = EnsureType(typeName, nameSpaceDescriptor);
            return SetDynamicTypePropertyDescriptor(new DynamicTypePropertyDescriptor
            {
                DynamicTypeDescriptorId = typeDescriptor.Id,
                ParentTypeName = typeDescriptor.TypeName,
                PropertyType = propertyType,
                PropertyName = propertyName
            });
        }

        [Verbosity(VerbosityLevel.Warning, MessageFormat = "Multiple types found by the name of {TypeName}: {FoundTypes}")]
        public event EventHandler MultipleTypesFoundWarning;

        public DynamicTypeDescriptor GetTypeDescriptor(string typeName, string nameSpace = null)
        {
            List<DynamicTypeDescriptor> results = new List<DynamicTypeDescriptor>();
            if (string.IsNullOrWhiteSpace(nameSpace))
            {
                results = DynamicTypeDataRepository.DynamicTypeDescriptorsWhere(d => d.TypeName == typeName).ToList();
            }
            else
            {
                DynamicNamespaceDescriptor nspace = DynamicTypeDataRepository.DynamicNamespaceDescriptorsWhere(ns => ns.Namespace == nameSpace).FirstOrDefault();
                Args.ThrowIfNull(nspace, "nameSpace");
                results = DynamicTypeDataRepository.DynamicTypeDescriptorsWhere(d => d.TypeName == typeName && d.DynamicNamespaceDescriptorId == nspace.Id).ToList();
            }
            if (results.Count > 1)
            {
                FireEvent(MultipleTypesFoundWarning,
                    new DynamicTypeManagerEventArgs
                    {
                        DynamicTypeDescriptors = results.ToArray(),
                        TypeName = typeName,
                        FoundTypes = string.Join(", ", results.Select(dt => $"{dt.DynamicNamespaceDescriptor.Namespace}.{dt.TypeName}").ToArray())
                    });
            }
            return results.FirstOrDefault();
        }

        public DynamicNamespaceDescriptor GetNamespaceDescriptor(string nameSpaceName)
        {
            DynamicNamespaceDescriptor result = DynamicTypeDataRepository.GetOneDynamicNamespaceDescriptorWhere(d => d.Namespace == nameSpaceName);
            if(result != null)
            {
                result = DynamicTypeDataRepository.Retrieve<DynamicNamespaceDescriptor>(result.Id);
            }
            return result;
        }

        public Assembly GenerateAssembly(string nameSpace = null)
        {
            return GenerateAssembly(out string ignore, nameSpace);
        }

        public Assembly GenerateAssembly(out string source, string nameSpace = null)
        {
            source = GenerateSource(nameSpace, out DynamicNamespaceDescriptor ns);
            CompilerResults results = AdHocCSharpCompiler.CompileSource(source, $"{ns.Namespace}.dll");
            if (results.Errors.Count > 0)
            {
                throw new CompilationException(results);
            }

            return results.CompiledAssembly;
        }

        public string GenerateSource(string nameSpace)
        {
            return GenerateSource(nameSpace, out DynamicNamespaceDescriptor ignore);
        }

        public string GenerateSource(string nameSpace, out DynamicNamespaceDescriptor ns)
        {
            List<DynamicTypeDescriptor> types = new List<DynamicTypeDescriptor>();
            ns = null;
            if (!string.IsNullOrEmpty(nameSpace))
            {
                ns = GetNamespaceDescriptor(nameSpace);
            }
            else
            {
                ns = DynamicTypeDataRepository.GetOneDynamicNamespaceDescriptorWhere(d => d.Namespace == DynamicNamespaceDescriptor.DefaultNamespace);
            }
            ulong id = ns.Id;
            types = DynamicTypeDataRepository.DynamicTypeDescriptorsWhere(t => t.DynamicNamespaceDescriptorId == id).ToList();
            StringBuilder src = new StringBuilder();
            foreach (DynamicTypeDescriptor typeDescriptor in types)
            {
                DtoModel dto = new DtoModel
                (
                    ns.Namespace,
                    GetClrTypeName(typeDescriptor.TypeName),
                    typeDescriptor.Properties.Select(p => new DtoPropertyModel { PropertyName = GetClrPropertyName(p.PropertyName), PropertyType = GetClrTypeName(p.PropertyType) }).ToArray()
                );
                src.AppendLine(dto.Render());
            }
            return src.ToString();
        }

        protected string GetClrTypeName(string jsonTypePath)
        {
            string typePath = jsonTypePath;
            string prefix = "arrayOf";
            bool isArray = false;
            if (typePath.StartsWith(prefix))
            {
                typePath = typePath.TruncateFront(prefix.Length).Truncate(1);
                isArray = true;
            }
            string[] splitOnDots = typePath.DelimitSplit(".");
            return splitOnDots[splitOnDots.Length - 1].PascalCase() + (isArray ? "[]": "");
        }

        protected string GetClrPropertyName(string jsonPropertyName)
        {
            return jsonPropertyName.PascalCase(true, new string[] { " ", "_", "-" });
        }

        protected DynamicDataSaveResult ProcessYamlFile(string typeName, FileInfo yamlFile, string nameSpace = null)
        {
            string yaml = yamlFile.ReadAllText();
            string rootHash = yaml.Sha256();
            DynamicTypeDataRepository.SaveAsync(new RootDocument { FileName = yamlFile.Name, Content = yaml, ContentHash = rootHash });
            string json = yaml.YamlToJson();
            
            JObject jobj = (JObject)JsonConvert.DeserializeObject(json);
            Dictionary<object, object> valueDictionary = jobj.ToObject<Dictionary<object, object>>();
            return SaveRootData(rootHash, typeName, valueDictionary, nameSpace);
        }

        protected DynamicDataSaveResult ProcessJsonFile(string typeName, FileInfo jsonFile, string nameSpace = null)
        {
            // read the json
            string json = jsonFile.ReadAllText();
            string rootHash = json.Sha256();
            DynamicTypeDataRepository.SaveAsync(new RootDocument { FileName = jsonFile.Name, Content = json, ContentHash = rootHash });
            JObject jobj = (JObject)JsonConvert.DeserializeObject(json);
            Dictionary<object, object> valueDictionary = jobj.ToObject<Dictionary<object, object>>();
            return SaveRootData(rootHash, typeName, valueDictionary, nameSpace);
        }
 
        protected DynamicDataSaveResult SaveRootData(string rootHash, string typeName, Dictionary<object, object> valueDictionary, string nameSpace = null)
        {
            DynamicDataSaveResult result = new DynamicDataSaveResult
            {
                // 1. save parent descriptor            
                DynamicTypeDescriptor = SaveTypeDescriptor(typeName, valueDictionary, nameSpace),
                // 2. save data
                DataInstance = SaveDataInstance(rootHash, typeName, valueDictionary)
            };
            return result;
        }

        /// <summary>
        /// Save a DynamicTypeDescriptor for the specified values
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="valueDictionary"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        protected DynamicTypeDescriptor SaveTypeDescriptor(string typeName, Dictionary<object, object> valueDictionary, string nameSpace = null)
        {
            DynamicTypeDescriptor descriptor = EnsureType(typeName, nameSpace, out DynamicNamespaceDescriptor namespaceDescriptor);

            foreach (object key in valueDictionary.Keys)
            {
                object value = valueDictionary[key];
                if (value != null)
                {
                    Type childType = value.GetType();
                    string childTypeName = $"{typeName}.{key}";
                    DynamicTypePropertyDescriptor propertyDescriptor = new DynamicTypePropertyDescriptor
                    {
                        DynamicTypeDescriptorId = descriptor.Id,
                        ParentTypeName = descriptor.TypeName,
                        PropertyType = childType.Name,
                        PropertyName = key.ToString(),
                    };

                    if (childType == typeof(JObject) || childType == typeof(Dictionary<object, object>))
                    {
                        propertyDescriptor.PropertyType = childTypeName;
                        SetDynamicTypePropertyDescriptor(propertyDescriptor);
                        Dictionary<object, object> data = value as Dictionary<object, object>;
                        if(data is null)
                        {
                            data = ((JObject)value).ToObject<Dictionary<object, object>>();
                        }
                        SaveTypeDescriptor(childTypeName, data);
                    }
                    else if (childType == typeof(JArray) || childType.IsArray)
                    {
                        propertyDescriptor.PropertyType = $"arrayOf({childTypeName})";
                        SetDynamicTypePropertyDescriptor(propertyDescriptor);

                        foreach (object obj in (IEnumerable)value)
                        {
                            Dictionary<object, object> data = new Dictionary<object, object>();
                            if(obj is JObject jobj)
                            {
                                data = jobj.ToObject<Dictionary<object, object>>();
                                SaveTypeDescriptor(childTypeName, data);
                            }
                        }
                    }
                    else
                    {
                        SetDynamicTypePropertyDescriptor(propertyDescriptor);
                    }
                }
            }

            return DynamicTypeDataRepository.Retrieve<DynamicTypeDescriptor>(descriptor.Id);
        }

        protected DataInstance SaveDataInstance(string rootHash, string typeName, Dictionary<object, object> valueDictionary)
        {
            return SaveDataInstance(rootHash, rootHash, typeName, valueDictionary);
        }

        static Dictionary<string, object> _parentLocks = new Dictionary<string, object>();
        protected DataInstance SaveDataInstance(string rootHash, string parentHash, string typeName, Dictionary<object, object> valueDictionary)
        {
            string instanceHash = valueDictionary.ToJson().Sha256();
            DynamicTypeDescriptor typeDescriptor = GetDynamicTypeDescriptor(typeName, out DynamicNamespaceDescriptor dynamicNamespaceDescriptor);
            DataInstance data = DynamicTypeDataRepository.DataInstancesWhere(di => di.Instancehash == instanceHash && di.ParentHash == parentHash && di.TypeName == typeName).FirstOrDefault();
            if(data == null)
            {
                _parentLocks.AddMissing(parentHash, new object());
                lock (_parentLocks[parentHash])
                {
                    data = DynamicTypeDataRepository.Save(new DataInstance
                    {
                        TypeName = typeName,
                        RootHash = rootHash,
                        ParentHash = parentHash,
                        Instancehash = instanceHash,
                        Properties = new List<DataInstancePropertyValue>()
                    });
                }
            }
            
            foreach(object key in valueDictionary.Keys)
            {
                object value = valueDictionary[key];
                if (value != null)
                {
                    Type childType = value.GetType();
                    string childTypeName = $"{typeName}.{key}";
                    // 3. for each property where the type is JObject
                    //      - repeat from 1
                    if (childType == typeof(JObject))
                    {
                        SaveJObjectData(childTypeName, rootHash, instanceHash, (JObject)value);
                    }
                    // 4. for each property where the type is JArray
                    //      foreach object in jarray
                    //          - repeat from 1
                    else if (childType == typeof(JArray))
                    {
                        foreach (object obj in (JArray)value)
                        {
                            if(obj is JObject jobj)
                            {
                                SaveJObjectData(childTypeName, rootHash, instanceHash, jobj);
                            }
                            else
                            {
                                data.Properties.Add(DynamicTypeDataRepository.Save(new DataInstancePropertyValue
                                {
                                    RootHash = rootHash,
                                    InstanceHash = instanceHash,
                                    ParentTypeName = typeName,
                                    PropertyName = key.ToString(),
                                    Value = obj.ToString()
                                }));
                            }                           
                        }
                    }
                    else
                    {
                        data.Properties.Add(DynamicTypeDataRepository.Save(new DataInstancePropertyValue
                        {
                            RootHash = rootHash,
                            InstanceHash = instanceHash,
                            ParentTypeName = typeName,
                            PropertyName = key.ToString(),
                            Value = value.ToString()
                        }));
                    }
                }
            }

            return DynamicTypeDataRepository.Save(data);
        }
                
        object _typeDescriptorLock = new object();
        protected DynamicTypeDescriptor EnsureType(string typeName, string nameSpace, out DynamicNamespaceDescriptor namespaceDescriptor)
        {
            namespaceDescriptor = EnsureNamespace(nameSpace);

            return EnsureType(typeName, namespaceDescriptor);
        }

        protected DynamicTypeDescriptor EnsureType(string typeName, DynamicNamespaceDescriptor nspace)
        {
            lock (_typeDescriptorLock)
            {
                return DynamicTypeDataRepository.GetOneDynamicTypeDescriptorWhere(td => td.TypeName == typeName && td.DynamicNamespaceDescriptorId == nspace.Id);                
            }
        }

        object _nameSpaceLock = new object();
        protected DynamicNamespaceDescriptor EnsureNamespace(string nameSpace = null)
        {
            lock (_nameSpaceLock)
            {
                nameSpace = nameSpace ?? DynamicNamespaceDescriptor.DefaultNamespace;
                return DynamicTypeDataRepository.GetOneDynamicNamespaceDescriptorWhere(ns => ns.Namespace == nameSpace);
            }
        }

        static Dictionary<int, object> _dynamicTypePropertyLocks = new Dictionary<int, object>();
        private DynamicTypePropertyDescriptor SetDynamicTypePropertyDescriptor(DynamicTypePropertyDescriptor prop)
        {
            int hashCode = prop.GetHashCode();
            _dynamicTypePropertyLocks.AddMissing(hashCode, new object());
            lock (_dynamicTypePropertyLocks[hashCode])
            {
                DynamicTypePropertyDescriptor retrieved = DynamicTypeDataRepository.DynamicTypePropertyDescriptorsWhere(pd =>
                    pd.DynamicTypeDescriptorId == prop.DynamicTypeDescriptorId &&
                    pd.ParentTypeName == prop.ParentTypeName &&
                    pd.PropertyType == prop.PropertyType &&
                    pd.PropertyName == prop.PropertyName).FirstOrDefault();

                if (retrieved == null)
                {
                    retrieved = DynamicTypeDataRepository.Save(prop);
                }
                return retrieved;
            }
        }

        private void SaveJObjectData(string typeName, string rootHash, string parentHash, JObject value)
        {
            Dictionary<object, object> valueDictionary = value.ToObject<Dictionary<object, object>>();
            SaveObjectData(typeName, rootHash, parentHash, valueDictionary);
        }

        private void SaveObjectData(string typeName, string rootHash, string parentHash, Dictionary<object, object> valueDictionary)
        {
            SaveTypeDescriptor(typeName, valueDictionary);
            SaveDataInstance(rootHash, parentHash, typeName, valueDictionary);
        }

        private DynamicTypeDescriptor GetDynamicTypeDescriptor(string partialOrFullyQualifiedTypeName, out DynamicNamespaceDescriptor namespaceDescriptor)
        {
            string typeName = partialOrFullyQualifiedTypeName;
            if (partialOrFullyQualifiedTypeName.Contains("."))
            {
                typeName = partialOrFullyQualifiedTypeName.Substring(partialOrFullyQualifiedTypeName.LastIndexOf("."));
                string nameSpace = partialOrFullyQualifiedTypeName.Substring(0, partialOrFullyQualifiedTypeName.LastIndexOf("."));
                namespaceDescriptor = EnsureNamespace(nameSpace);
                return DynamicTypeDataRepository.GetOneDynamicTypeDescriptorWhere(t => t.TypeName == typeName);
            }
            else
            {
                DynamicTypeDescriptor typeDescriptor = DynamicTypeDataRepository.GetOneDynamicTypeDescriptorWhere(t => t.TypeName == typeName);
                if(typeDescriptor.DynamicNamespaceDescriptorId > 0)
                {
                    namespaceDescriptor = DynamicTypeDataRepository.GetOneDynamicNamespaceDescriptorWhere(ns => ns.Id == typeDescriptor.DynamicNamespaceDescriptorId);
                }
                else
                {
                    namespaceDescriptor = DynamicNamespaceDescriptor.GetDefault(DynamicTypeDataRepository);
                }
                return typeDescriptor;
            }
        }
    }
}
