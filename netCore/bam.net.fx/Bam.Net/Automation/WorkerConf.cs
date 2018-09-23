/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using Bam.Net.Yaml;

namespace Bam.Net.Automation
{
    public class WorkerConf
    {
        protected static Dictionary<string, Func<string, WorkerConf>> _deserializers;
        protected static Dictionary<string, Action<string, WorkerConf>> _serializers;
        static WorkerConf()
        {
            _deserializers = new Dictionary<string, Func<string, WorkerConf>>();
            _serializers = new Dictionary<string, Action<string, WorkerConf>>();
            
            _deserializers[".yaml"] = (path) =>
            {
                return path.SafeReadFile().FromYaml<WorkerConf>();
            };
            _deserializers[".json"] = (path) =>
            {
                return path.SafeReadFile().FromJson<WorkerConf>();
            };
            _deserializers[".xml"] = (path) =>
            {
                return path.SafeReadFile().FromXml<WorkerConf>();
            };

            _serializers[".yaml"] = (path, conf) =>
            {
                conf.ToYamlFile(path);
            };
            _serializers[".json"] = (path, conf) =>
            {
                conf.ToJsonFile(path);
            };
            _serializers[".xml"] = (path, conf) =>
            {
                conf.ToXmlFile(path);
            };
        }

        public WorkerConf()
        {
            this._properties = new List<KeyValuePair>();
        }

        public WorkerConf(Worker worker)
            : this()
        {
            this.WorkerTypeName = worker.GetType().AssemblyQualifiedName;
            this.Name = worker.Name;
        }

        public WorkerConf(string name, Type workerType)
            : this()
        {
            this.WorkerTypeName = workerType.AssemblyQualifiedName;
            this.Name = name;
        }

        protected internal Type WorkerType
        {
            get;
            set;
        }
        
        string _workerTypeName;
        public string WorkerTypeName
        {
            get
            {
                return _workerTypeName;
            }
            set
            {
                _workerTypeName = value;
                WorkerType = Type.GetType(value, true);
            }
        }

        public int StepNumber { get; set; }

        public string Name { get; set; }

        public Worker CreateWorker(Job job = null)
        {
            if (WorkerType == null)
            {
                throw new InvalidOperationException("Specified WorkerTypeName ({0}) was not found"._Format(WorkerTypeName));
            }

            Worker result = WorkerType.Construct<Worker>();
            result.Name = this.Name;
            result.StepNumber = this.StepNumber;
            result.Configure(this);
            if (job != null)
            {
                result.Job = job;
            }
            return result;
        }

        public static WorkerConf Load(string filePath)
        {
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (!_deserializers.ContainsKey(ext))
            {
                ext = ".json";
            }

            return _deserializers[ext](filePath);
        }

        public void SetProperties(Dictionary<string, string> propertiesToSet)
        {
            List<KeyValuePair> properties = new List<KeyValuePair>();
            propertiesToSet.Keys.Each(propName =>
            {
                properties.Add(new KeyValuePair(propName, propertiesToSet[propName]));
            });
            this.Properties = properties.ToArray();
        }

        public void AddProperties(Dictionary<string, string> propertiesToAdd)
        {
            propertiesToAdd.Keys.Each(propName =>
            {
                AddProperty(propName, propertiesToAdd[propName]);
            });
        }

        public void SetProperty(string name, string value)
        {
            KeyValuePair prop = Properties.Where(kvp => kvp.Key.Equals(name)).FirstOrDefault();
            if (prop == null)
            {
                prop = new KeyValuePair(name, value);
            }
        }

        public void AddProperty(string name, string value)
        {
            KeyValuePair existing = Properties.Where(kvp => kvp.Key.Equals(name)).FirstOrDefault();
            if (existing != null)
            {
                throw new InvalidOperationException("Specified property is already set, use 'SetProperty' to change the value");
            }

            _properties.Add(new KeyValuePair(name, value));
        }

        List<KeyValuePair> _properties;
        public KeyValuePair[] Properties
        {
            get
            {
                return _properties.ToArray();
            }
            set
            {
                _properties = new List<KeyValuePair>();
                _properties.AddRange(value);
            }
        }

        public virtual void Save()
        {
            Save(".\\{0}_WorkerConf.json"._Format(this.Name));
        }

        public void Save(string filePath)
        {
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (!_serializers.ContainsKey(ext))
            {
                ext = ".json";
            }

            _serializers[ext](filePath, this);
        }
    }
}
