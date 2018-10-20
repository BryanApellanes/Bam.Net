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
    public partial class YamlDataDirectory
    {
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
    }
}
