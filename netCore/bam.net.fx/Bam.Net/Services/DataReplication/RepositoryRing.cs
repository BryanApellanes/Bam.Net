/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Bam.Net;
using Bam.Net.Logging;
using Bam.Net.Data;
using Bam.Net.Services.DataReplication.Data;
using Bam.Net.Data.Repositories;
using System.Collections;
using Bam.Net.Services.DataReplication;

namespace Bam.Net.Services.DataReplication
{
    public class RepositoryRing: Ring<RepositoryService>, IReflectionCrudProvider
    {
        public RepositoryRing()
            : base()
        {
        }

        public RepositoryRing(int arcCount)
            : base()
        {
            SetArcCount(arcCount);
        }

        protected internal override Arc CreateArc()
        {
            return new Arc<RepositoryService>();
        }

        protected Arc FindArc(object value)
        {
            int key = GetRepositoryKey(value);
			return FindArcByKey(key);
        }

        protected override Arc FindArcByKey(int key)
        {
            double slotIndex = Math.Floor((double)(key / ArcSize));
            Arc result = null;
            if (slotIndex < Arcs.Length)
            {
                result = Arcs[(int)slotIndex];
            }

            return result;
        }        

        public UniversalIdentifier UniversalIdentifier { get; set; }
        public ITypeResolver TypeResolver { get; set; }

        public Task<string> GetHashStringAsync(object value)
        {
            return Task.Run(() => GetHashString(value));
        }

        public override string GetHashString(object value)
        {
            Type type = value.GetType();
            
            PropertyInfo[] keyProperties = type.GetPropertiesWithAttributeOfType<RepositoryKey>();
            Type marker = keyProperties.Length > 0 ? typeof(RepositoryKey) : typeof(ColumnAttribute);

            StringBuilder stringToHashBuilder = GetHashStringFromProperties(value, new Type[] { marker }, type);
            string stringToHash = stringToHashBuilder.ToString();
            if (string.IsNullOrEmpty(stringToHash))
            {
                stringToHashBuilder = GetHashStringFromProperties(value, type);
                stringToHash = stringToHashBuilder.ToString();
            }

            return stringToHash.Sha1();
        }

        public override int GetRepositoryKey(object value)
        {
            int code = GetHashString(value).GetHashCode();
            code = code < 0 ? code * -1 : code;
            return code;
        }

        private static StringBuilder GetHashStringFromProperties(object value, Type type)
        {
            PropertyInfo[] properties = type.GetProperties();
            StringBuilder stringToHash = new StringBuilder();
            properties.Each(prop =>
            {
                object val = prop.GetValue(value, null);
                string append = val == null ? "null" : val.ToString();
                stringToHash.Append(append);
            });

            return stringToHash;
        }

        private static StringBuilder GetHashStringFromProperties(object value, Type[] attributesAddorningPropertiesToScrape, Type type)
        {
            PropertyInfo[] properties = type.GetProperties();
            StringBuilder stringToHash = new StringBuilder();
            properties.Each(prop =>
            {
                attributesAddorningPropertiesToScrape.Each(attrType =>
                {
                    if (prop.HasCustomAttributeOfType(attrType))
                    {
                        object val = prop.GetValue(value, null);
                        string append = val == null ? "null" : val.ToString();
                        stringToHash.Append(append);
                    }
                });
            });
            return stringToHash;
        }

        public object Create(object toCreate)
        {
            RepositoryService svc = GetService(toCreate);
            CreateOperation operation = CreateOperation.For(toCreate);
            return svc.Create(operation);
        }

        public bool Delete(object toDelete)
        {
            RepositoryService svc = GetService(toDelete);
            DeleteOperation operation = DeleteOperation.For(toDelete);
            return svc.Delete(operation);
        }

        public IEnumerable<object> Query(Type type, Dictionary<string, object> queryParameters)
        {
            Dictionary<object, object> parameters = new Dictionary<object, object>();
            foreach(string key in queryParameters.Keys)
            {
                parameters.Add(key, queryParameters[key]);
            }
            QueryOperation operation = QueryOperation.For(type, parameters);
            List<object> results = new List<object>();
            ForEachArc(svc =>
            {
                results.AddRange(svc.Query(operation));
            });
            return results;
        }

        public object Retrieve(string typeName, string instanceIdentifier)
        {
            return Retrieve(TypeResolver.ResolveType(typeName), instanceIdentifier);
        }

        public object Retrieve(Type objectType, string identifier)
        {
            RetrieveOperation operation = RetrieveOperation.For(objectType, identifier, UniversalIdentifier);
            List<object> results = new List<object>();
            ForEachArc(svc =>
            {
                results.Add(svc.Retrieve(operation));
            });
            if(results.Count > 1)
            {
                throw new MultipleEntriesFoundException();
            }
            return results.First();
        }

        public object Save(object toSave)
        {
            throw new NotImplementedException();
        }

        public IEnumerable SaveCollection(IEnumerable values)
        {
            throw new NotImplementedException();
        }

        public object Update(object toUpdate)
        {
            throw new NotImplementedException();
        }

        private RepositoryService GetService(object example)
        {
            Arc<RepositoryService> arc = (Arc<RepositoryService>)FindArc(example);
            RepositoryService svc = arc.GetTypedServiceProvider();
            return svc;
        }

        private void ForEachArc(Action<RepositoryService> func)
        {
            Parallel.ForEach(Arcs, (arc)=>
            {
                Arc<RepositoryService> typedArc = (Arc<RepositoryService>)arc;
                func(typedArc.GetTypedServiceProvider());
            });
        }
    }
}
