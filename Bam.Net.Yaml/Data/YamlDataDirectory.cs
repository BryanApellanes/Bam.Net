using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;

namespace Bam.Net.Yaml.Data
{
    public class YamlDataDirectory
    {
        public YamlDataDirectory(DirectoryInfo dir, ILogger logger = null)
        {
            Root = dir;
            Logger = logger ?? Log.Default;
        }
        public YamlDataDirectory(string directoryPath, ILogger logger = null) : this(new DirectoryInfo(directoryPath), logger)
        { }

        public ArrayBehavior ArrayBehavior { get; set; }
        public DirectoryInfo Root { get; set; }
        public ILogger Logger { get; set; }
        
        public IEnumerable<YamlDataFile> Load(Type type)
        {
            FileInfo[] yamlFiles = GetYamlFiles(type);
            foreach (FileInfo file in yamlFiles)
            {
                yield return new YamlDataFile(type, file, Logger) { ArrayBehavior = ArrayBehavior };
            }
        }
        public YamlDataFile Load(Type type, string name)
        {
            FileInfo yamlFile = GetYamlFile(type, name);
            return new YamlDataFile(type, yamlFile, Logger) { ArrayBehavior = ArrayBehavior };
        }
        public YamlDataFile Save(object data)
        {
            return Save(data.GetType(), data);
        }
        public YamlDataFile Save<T>(object data)
        {
            return Save(typeof(T), data);
        }
        public YamlDataFile Save(Type type, object data)
        {
            return Save(GetYamlFile(type, data), type, data);
        }
        public YamlDataFile Save(FileInfo file, object data)
        {
            return Save(file, data.GetType(), data);
        }
        public YamlDataFile Save(FileInfo file, Type type, object data)
        {
            Args.ThrowIfNull(data);
            object name = data.Property("Name");
            Args.ThrowIfNull(name);
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
            
            data.ToDynamicData(type.Name).ToYamlFile(file);
            return new YamlDataFile(type, file, Logger) { ArrayBehavior = ArrayBehavior };
        }
        public bool Delete<T>(T data)
        {
            return Delete(typeof(T), data);
        }
        public bool Delete(Type type, object data)
        {
            try
            {
                object name = data.Property("Name");
                Args.ThrowIfNull(name, "Name");
                string filePath = GetYamlFile(type, data).FullName;
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("Error deleting data: {0}\r\n{1}", ex.Message, data?.PropertiesToString());
                return false;
            }
        }

        public FileInfo GetYamlFile(Type type, object instance)
        {
            Args.ThrowIfNull(instance, "instance");
            string name = instance.Property("Name").ToString();
            return GetYamlFile(type, name);
        }

        public FileInfo GetYamlFile(Type type, string name)
        {
            string yamlFilePath = GetYamlFilePath(type, name);
            if (File.Exists(yamlFilePath))
            {
                return new FileInfo(yamlFilePath);
            }
            return null;
        }

        public string GetYamlFilePath(Type type, string name)
        {
            string dirPath = GetTypeDirectory(type);
            string yamlFilePath = Path.Combine(dirPath, $"{name}.yaml");
            return yamlFilePath;
        }

        public string GetTypeDirectory(Type type)
        {
            return Path.Combine(Root.FullName, type.Name);
        }

        public FileInfo[] GetYamlFiles(Type type)
        {
            string dirPath = Path.Combine(Root.FullName, type.Name);
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            if (dir.Exists)
            {
                FileInfo[] yamlFiles = dir.GetFiles("*.yaml");
                return yamlFiles;
            }
            return new FileInfo[] { };
        }
    }
}
