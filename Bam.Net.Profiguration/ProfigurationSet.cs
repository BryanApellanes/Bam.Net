/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.Configuration;
using Bam.Net.Logging;

namespace Bam.Net.Profiguration
{
    public class ProfigurationSet: Loggable
    {
        public ProfigurationSet(DirectoryInfo rootDirectory)
        {
            RootDirectorySet += (o, args) =>
            {
                Load();
            };

            this._namedProfigurations = new Dictionary<string, Profiguration>();
            this.RootDirectory = rootDirectory;
        }

        public ProfigurationSet(string rootDirectory)
            : this(new DirectoryInfo(rootDirectory))
        {
        }

        public event EventHandler RootDirectorySet;

        protected void OnRootDirectorySet(DirectoryInfo dir)
        {
            if (RootDirectorySet != null)
            {
                RootDirectorySet(this, new ProfigurationEventArgs(dir));
            }
        }

        
        Dictionary<string, Profiguration> _namedProfigurations;
        protected Dictionary<string, Profiguration> NamedProfigurations
        {
            get
            {
                return _namedProfigurations;
            }
            set
            {
                _namedProfigurations = value;
            }
        }

        protected void Load()
        {
            RootDirectory.Refresh();
            if(RootDirectory.Exists)
            {
                NamedProfigurations = new Dictionary<string, Profiguration>();
                FileInfo[] files = RootDirectory.GetFiles();
                files.Each(file =>
                {
                    if (file.HasNoExtension())
                    {
                        NamedProfigurations[file.Name] = Profiguration.Load(file.FullName);
                    }
                });
            }
        }

        DirectoryInfo _rootDirectory;
        object _rootDirectoryLock = new object();
        public DirectoryInfo RootDirectory
        {
            get
            {
                return _rootDirectoryLock.DoubleCheckLock(ref _rootDirectory, () => new DirectoryInfo(".\\DefaultProfigurationSet"));
            }
            set
            {
                _rootDirectory = value;
                OnRootDirectorySet(value);
            }
        }

        public string[] ProfigurationNames
        {
            get
            {
                return NamedProfigurations.Keys.ToArray();
            }
        }

        public Profiguration this[string name]
        {
            get
            {
                if (NamedProfigurations.ContainsKey(name))
                {
                    return NamedProfigurations[name];
                }
                else
                {
                    return Get(name);
                }
            }
        }

        /// <summary>
        /// Writes all profigurations back to the disk.
        /// </summary>
        public void Save()
        {
            ProfigurationNames.Each(name =>
            {
                Save(name, NamedProfigurations[name]);
            });
        }

        object _writeLock = new object();
        /// <summary>
        /// Get the profiguration of the specified name, creating it
        /// if necessary.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected internal Profiguration Get(string name)
        {
            lock(_writeLock)
            {
                Profiguration result = new Profiguration();
                string path = Path.Combine(RootDirectory.FullName, name);
                if (File.Exists(path))
                {
                    result = Profiguration.Load(path);
                }
                else
                {
                    result.Save(path);
                }

                NamedProfigurations[name] = result;
                return result;
            }
        }

        protected internal Profiguration Save(string name, Profiguration toSave)
        {
            lock(_writeLock)
            {
                string path = Path.Combine(RootDirectory.FullName, name);
                toSave.Save(path);
                NamedProfigurations[name] = toSave;
                return toSave;
            }
        }
    }
}
