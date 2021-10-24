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
    public class YamlDataFile : YamlFile
    {
        Dictionary<ArrayBehavior, Func<FileInfo, object>> _dataRetrievers;
        public YamlDataFile(Type type, FileInfo file, ILogger logger = null) : base(file)
        {
            Logger = logger ?? Log.Default;
            _dataRetrievers = new Dictionary<ArrayBehavior, Func<FileInfo, object>>
            {
                { ArrayBehavior.Throw, GetOneOrThrow },
                { ArrayBehavior.Warn, GetFirst },
                { ArrayBehavior.Normalize, Normalize}
            };
            Type = type;
        }
        public YamlDataFile(Type type, string path, ILogger logger = null) : this(type, new FileInfo(path), logger)
        { }

        public ILogger Logger { get; set; }
        public ArrayBehavior ArrayBehavior { get; set; }
        public Type Type { get; set; }
        public object Data
        {
            get
            {
                return _dataRetrievers[ArrayBehavior](File);
            }
            set
            {
                value.ToYamlFile(File);
            }
        }

        public T As<T>()
        {
            return (T)Data;
        }
        private object GetOneOrThrow(FileInfo file)
        {
            return GetOneOrDo(file, fi => throw new InvalidOperationException($"Specified file has multiple top level values defined: {fi.FullName}"));
        }
        private object GetFirst(FileInfo file)
        {
            return GetOneOrDo(file, fi => Logger.Warning("Specified file has multiple top level values defined: {0}", fi.FullName));
        }

        private object GetOneOrDo(FileInfo file, Action<FileInfo> moreThanOne)
        {
            Args.ThrowIfNull(file, "file");
            object data = file.FromYamlFile();
            object[] datas = data as object[];
            if (datas != null && datas.Length > 1)
            {
                moreThanOne(file);
            }
            return datas.First().CopyAs(Type);
        }

        private object Normalize(FileInfo file)
        {
            return Normalize(Type, file, Logger);
        }

        protected internal static object Normalize(Type type, FileInfo file, ILogger logger = null)
        {
            Args.ThrowIfNull(file, "file");
            DirectoryInfo dir = file.Directory;
            object datum = file.FromYamlFile();
            object[] datas = datum as object[] ?? new object[] { };
            List<string> names = new List<string>();
            object first = null;
            foreach(object data in datas)
            {
                Dictionary<object, object> dict = data as Dictionary<object, object>;
                Args.ThrowIf(!dict.ContainsKey("Name"), "Name not specified");
                string name = NextName(names, dict["Name"].ToString(), logger);
                FileInfo dataFile = new FileInfo(Path.Combine(dir.FullName, $"{name}.yaml"));
                object value = dict.FromDictionary(type);
                if(first == null)
                {
                    first = value;
                }
                value.ToYamlFile(dataFile);
            }
            return first;
        }

        protected internal static string NextName(List<string> names, string name, ILogger logger = null)
        {
            logger = logger ?? Log.Default;
            string result = name;
            int num = 1;
            while (names.Contains(result))
            {
                logger.Warning("{0} name in use", result);
                result = $"{result}_{num}";                
            }
            return result;
        }
    }
}
