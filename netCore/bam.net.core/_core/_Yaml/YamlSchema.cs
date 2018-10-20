/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net;
using Bam.Net.Logging;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.Schema;
using System.Reflection;

namespace Bam.Net.Yaml
{
	public partial class YamlSchema: Loggable
    {
        public YamlSchema(DirectoryInfo root, ILogger logger = null)
        : base()
        {
            this.YamlFiles = new List<YamlFile>();
            this.RootDirectory = root;
            this.Failures = new List<YamlDeserializationFailure>();
            if (logger != null)
            {
                this.Subscribe(logger);
            }
        }

        public FileInfo[] Files
        {
            get
            {
                return YamlFiles.Select(yf => yf.File).ToArray();
            }
        }
        

        [Verbosity(VerbosityLevel.Warning)]
        public event EventHandler YamlDeserializationFailed;

        public List<YamlFile> YamlFiles { get; private set; }

        public void AddFile(FileInfo file)
        {
            AddFile(new YamlFile(file));
        }

        public void AddFile(YamlFile file)
        {
            YamlFiles.Add(file);
        }

        public void AddFiles(List<FileInfo> files)
        {
            files.Each(file => AddFile(file));
        }

        public void AddFiles(List<YamlFile> files)
        {
            YamlFiles.AddRange(files);
        }
    }
}
