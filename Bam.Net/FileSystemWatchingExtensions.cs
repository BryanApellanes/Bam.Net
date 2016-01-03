using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bam.Net
{
    public static class FileSystemWatchingExtensions
    {
        static Dictionary<string, FileSystemWatcher> _watchers;
        static FileSystemWatchingExtensions()
        {
            _watchers = new Dictionary<string, FileSystemWatcher>();
        }

        public static FileSystemWatcher OnChange(this FileSystemInfo fs, FileSystemEventHandler changeHandler)
        {
            FileSystemWatcher watcher = Get(fs.FullName);
            return OnChange(watcher, changeHandler);
        }

        public static FileSystemWatcher OnChange(this FileSystemWatcher watcher, FileSystemEventHandler changeHandler)
        {
            watcher.Changed += changeHandler;
            return watcher;
        }
        public static FileSystemWatcher OnCreated(this FileSystemInfo fs, FileSystemEventHandler createdHandler)
        {
            FileSystemWatcher watcher = Get(fs.FullName);
            return OnCreated(watcher, createdHandler);
        }

        public static FileSystemWatcher OnCreated(this FileSystemWatcher watcher, FileSystemEventHandler createdHandler)
        {
            watcher.Created += createdHandler;
            return watcher;
        }

        public static FileSystemWatcher OnDeleted(this FileSystemInfo fs, FileSystemEventHandler deletedHandler)
        {
            FileSystemWatcher watcher = Get(fs.FullName);
            return OnDeleted(watcher, deletedHandler);
        }

        public static FileSystemWatcher OnDeleted(this FileSystemWatcher watcher, FileSystemEventHandler deletedHandler)
        {
            watcher.Deleted += deletedHandler;
            return watcher;
        }
        public static FileSystemWatcher OnError(this FileSystemInfo fs, ErrorEventHandler errorHandler)
        {
            FileSystemWatcher watcher = Get(fs.FullName);
            return OnError(watcher, errorHandler);
        }

        public static FileSystemWatcher OnError(this FileSystemWatcher watcher, ErrorEventHandler errorHandler)
        {
            watcher.Error += errorHandler;
            return watcher;
        }
        public static FileSystemWatcher OnRenamed(this FileSystemInfo fs, RenamedEventHandler renamedHandler)
        {
            FileSystemWatcher watcher = Get(fs.FullName);
            return OnRenamed(watcher, renamedHandler);
        }

        public static FileSystemWatcher OnRenamed(this FileSystemWatcher watcher, RenamedEventHandler renamedHandler)
        {
            watcher.Renamed += renamedHandler;
            return watcher;
        }
        private static FileSystemWatcher Get(string path)
        {
            path = path.ToLowerInvariant();
            if (!_watchers.ContainsKey(path))
            {
                _watchers.Add(path, new FileSystemWatcher { Path = path, EnableRaisingEvents = true, IncludeSubdirectories = true });
            }
            return _watchers[path];
        }
    }
}
