using Bam.Net.Caching.File;
using Bam.Net.Configuration;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services
{
    public class FileSystemKeyValueStore : IKeyValueStore
    {
        public const string DirectoryName = "KeyValueStore";
        readonly HashSet<string> _loadedFiles;
        public FileSystemKeyValueStore(ILogger logger = null)
        {
            _loadedFiles = new HashSet<string>();
            LocalStorage = new DirectoryInfo(Path.Combine(DefaultDataDirectoryProvider.Current.GetAppFilesDirectory(DefaultConfigurationApplicationNameProvider.Instance).FullName, DirectoryName));
            FileCache = new TextFileCache();
            Logger = logger;
        }

        public FileSystemKeyValueStore(DirectoryInfo localStorage, ILogger logger = null) : this(logger)
        {
            LocalStorage = localStorage;
        }

        protected FileCache FileCache { get; set; }
        public DirectoryInfo LocalStorage { get; set; }
        public ILogger Logger { get; set; }

        public string Get(string key)
        {
            string filePath = GetFilePath(key);
            if (File.Exists(filePath))
            {
                EnsureFileIsLoaded(filePath);
                return FileCache.GetText(new FileInfo(filePath));
            }
            return string.Empty;
        }

        public bool Set(string key, string value)
        {
            try
            {
                string filePath = GetFilePath(key);
                value.SafeWriteToFile(filePath, true);
                return true;
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Exception setting key value pair: key={0}, value={1}: {2}", ex, key, value, ex.Message);
                return false;
            }
        }

        public string GetFilePath(string key)
        {
            return Path.Combine(LocalStorage.FullName, $"{key}.txt");
        }

        private void EnsureFileIsLoaded(string filePath)
        {
            if (!_loadedFiles.Contains(filePath))
            {
                _loadedFiles.Add(filePath);
                FileCache.EnsureFileIsLoaded(new FileInfo(filePath));
            }
        }
    }
}
