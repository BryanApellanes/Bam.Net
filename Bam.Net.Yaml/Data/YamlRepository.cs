using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;

namespace Bam.Net.Yaml.Data
{
    public class YamlRepository : FsRepository
    {
        public YamlRepository(string rootDirectory, Database db, ILogger logger = null) 
            : this(rootDirectory, new DaoRepository(db, logger), logger)
        { }
            
        public YamlRepository(string rootDirectory, DaoRepository daoRepo, ILogger logger = null): base(rootDirectory)
        {
            DaoRepository = daoRepo;
            DataDirectoryName = "Yaml";
            YamlDataDirectory = new YamlDataDirectory(DataDirectory, logger);
        }

        protected override string DataDirectoryName { get; set; }

        public DaoRepository DaoRepository { get; set; }
        public YamlDataDirectory YamlDataDirectory { get; set; }

        public IEnumerable<object> LoadYaml()
        {
            // get names from YamlDataDirectory/load.names
            // load each one at a time and yield
            HashSet<string> names = new HashSet<string>();
            foreach(Type type in StorableTypes)
            {
                FileInfo loadNamesFile = GetLoadNamesFile(type);
                File.ReadAllLines(loadNamesFile.FullName).Each(n => names.Add(n));
                foreach (string name in names)
                {
                    object value = DaoRepository.Query(type, QueryFilter.Where("Name") == name).FirstOrDefault();
                    if (value != null)
                    {
                        WriteYaml(type, value);
                        yield return value;
                    }
                }
            }            
        }

        public IEnumerable<T> LoadYaml<T>(params string[] names) where T : class, new()
        {
            IEnumerable<T> values = DaoRepository.Query<T>(QueryFilter.Where("Name").In(names));
            foreach (T value in values)
            {
                WriteYaml(typeof(T), value);
            }
            return values;
        }
        protected internal FileInfo GetLoadNamesFile(Type type)
        {
            return new FileInfo(Path.Combine(YamlDataDirectory.GetTypeDirectory(type), "load.names"));
        }
        /// <summary>
        /// Add the specified name to the load.names file.
        /// The names.sync file is used to 
        /// </summary>
        /// <param name="name"></param>
        public HashSet<string> AddNameToLoad<T>(string name)
        {
            HashSet<string> result = new HashSet<string>();
            FileInfo loadNamesFile = GetLoadNamesFile(typeof(T));
            if (!loadNamesFile.Exists)
            {
                result.Add(name);
                $"{name}\r\n".SafeWriteToFile(loadNamesFile.FullName, (o) => o.ClearWriteLocks());
            }
            else
            {
                File.ReadLines(loadNamesFile.FullName).Each(n => result.Add(n));
                result.Add(name);
                "".SafeWriteToFile(loadNamesFile.FullName, true);
                result.Each(n => $"{n}\r\n".SafeAppendToFile(loadNamesFile.FullName));
            }
            return result;
        }
        public void ResolveChanges()
        {
            foreach(Type type in StorableTypes)
            {
                FileInfo[] files = YamlDataDirectory.GetYamlFiles(type);
                foreach(FileInfo file in files)
                {
                    YamlData data = YamlData.Load(type, file, Logger);
                    object dao = DaoRepository.Query("Name", data.Name).FirstOrDefault();
                    if(dao == null)
                    {
                        DaoRepository.Save(data.Data);
                    }
                    else
                    {
                        data.FileWins += (o, a) => 
                        {
                            YamlData.YamlDataEventArgs d = (YamlData.YamlDataEventArgs)a;
                            DaoRepository.Save(d.Type, d.Data);
                        };
                        data.DataWins += (o, a) =>
                        {
                            YamlData.YamlDataEventArgs d = (YamlData.YamlDataEventArgs)a;
                            YamlDataDirectory.Save(file, d.Type, d.Data);
                        };
                        data.Data = dao;
                        data.ResolveChanges(type, file);
                    }
                }
            }
        }
        /// <summary>
        /// If true, will write all "Create" operations to files on disk
        /// as well as committing to the underlying DaoRepository
        /// </summary>
        public bool CreateAllFiles { get; set; }
        public override void AddType(Type type)
        {
            DaoRepository.AddType(type);
            base.AddType(type);
        }
        public override bool Delete<T>(T toDelete)
        {
            bool result = DaoRepository.Delete<T>(toDelete); 
            if (result)
            {
                result = YamlDataDirectory.Delete<T>(toDelete);
            }
            return result;
        }

        protected override bool PerformDelete(Type type, object toDelete)
        {
            bool result = DaoRepository.Delete(type, toDelete);
            if (result)
            {
                result = YamlDataDirectory.Delete(type, toDelete);
            }
            return result;
        }

        public override IEnumerable<T> Query<T>(Func<T, bool> query)
        {
            foreach(T val in DaoRepository.Query(query))
            {
                yield return val;
            }
            foreach(YamlDataFile val in YamlDataDirectory.Load(typeof(T)))
            {
                T copy = val.Data.CopyAs<T>();
                if (query(copy))
                {
                    yield return copy;
                }
            }
        }

        public override IEnumerable<object> Query(Type type, Func<object, bool> predicate)
        {
            foreach(object o in DaoRepository.Query(type, predicate))
            {
                yield return o;
            }
            foreach(object o in YamlDataDirectory.Load(type))
            {
                if (predicate(o))
                {
                    yield return o;
                }
            }
        }

        public override object Retrieve(Type objectType, long id)
        {
            object dao = DaoRepository.Retrieve(objectType, id);
            return GetLatest(dao);
        }

        public override object Retrieve(Type objectType, string uuid)
        {
            object dao = DaoRepository.Retrieve(objectType, uuid);
            return GetLatest(dao);
        }

        public override IEnumerable<object> RetrieveAll(Type type)
        {
            foreach(object o in DaoRepository.RetrieveAll(type))
            {
                yield return o;
            }
            foreach(object o in YamlDataDirectory.Load(type))
            {
                yield return o;
            }
                
        }

        public override object Update(Type type, object toUpdate)
        {
            toUpdate = DaoRepository.Update(type, toUpdate);
            FileInfo yamlFile = YamlDataDirectory.GetYamlFile(type, toUpdate);
            if (yamlFile != null)
            {
                YamlDataDirectory.Save(yamlFile, type, toUpdate);
            }
            return toUpdate;
        }

        protected override object PerformCreate(Type type, object toCreate)
        {
            toCreate = DaoRepository.Create(type, toCreate);
            if (CreateAllFiles)
            {
                FileInfo file = new FileInfo(YamlDataDirectory.GetYamlFilePath(type, toCreate.Property("Name").ToString()));
                YamlDataDirectory.Save(file, type, toCreate);
            }
            return toCreate;
        }
        
        private object GetLatest(object dao)
        {
            if (dao != null)
            {
                FileInfo yamlFile = YamlDataDirectory.GetYamlFile(dao.GetType(), dao);
                if (yamlFile != null)
                {
                    YamlData data = new YamlData(dao);
                    return data.Newer(yamlFile);
                }
            }
            return dao;
        }

        private void WriteYaml(Type type, object value)
        {
            FileInfo yamlFile = new FileInfo(YamlDataDirectory.GetYamlFilePath(type, value.Property<string>("Name")));
            YamlDataDirectory.Save(yamlFile, type, value);
        }
    }
}
