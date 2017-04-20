using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;

namespace Bam.Net.Yaml.Data
{
    public class YamlData: Loggable
    {
        public class YamlDataEventArgs: EventArgs
        {
            public Type Type { get; set; }
            public object Data { get; set; }
        }

        public YamlData(object data, ILogger logger = null)
        {
            Data = data;
            if(logger != null)
            {
                Subscribe(logger);
            }
        }
        public object Data { get; set; }
        public event EventHandler DataWins;
        public event EventHandler FileWins;
        public string Name => Data.Property<string>("Name");
        public object Newer(FileInfo file)
        {
            DateTime? modified = Data.Property<DateTime?>("Modified");
            DateTime fileModified = file.LastWriteTimeUtc;
            if (modified <= fileModified)
            {
                return ((Dictionary<object, object>)file.FromYamlFile().First()).FromDictionary(Data.GetType());
            }
            else
            {
                return Data;
            }
        }
        public static YamlData Load(Type type, FileInfo file, ILogger logger = null)
        {
            object data = file.FromYamlFile().First();
            Dictionary<object, object> dict = data as Dictionary<object, object>;
            if(dict != null)
            {
                data = dict.FromDictionary(type);
            }
            return new YamlData(data, logger);
        }
        public object Synchronize(Type type, FileInfo file)
        {
            DateTime? modified = Data.Property<DateTime?>("Modified");
            DateTime fileModified = file.LastWriteTimeUtc;
            if(modified <= fileModified)
            {
                Data = file.FromYamlFile().First();
                FileWins?.Invoke(this, new YamlDataEventArgs { Type = type, Data = Data.CopyAs(type) });
            }
            else
            {
                Data.ToYamlFile(file);
                DataWins?.Invoke(this, new YamlDataEventArgs { Type = type, Data = Data.CopyAs(type) });
            }
            return Data;
        }
    }
}
