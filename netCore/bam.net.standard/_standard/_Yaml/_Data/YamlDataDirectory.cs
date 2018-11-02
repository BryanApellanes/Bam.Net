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
        public YamlDataFile Save(FileInfo file, Type type, object data)
        {
            Args.ThrowIfNull(data);
            object name = data.Property("Name");
            Args.ThrowIfNull(name);
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }

            data.ToYamlFile(file);
            return new YamlDataFile(type, file, Logger) { ArrayBehavior = ArrayBehavior };
        }
    }
}
