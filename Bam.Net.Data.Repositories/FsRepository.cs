using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Lucene.Net.Documents;
using System.Reflection;
using Bam.Net;
using Bam.Net.Logging;

namespace Bam.Net.Data.Repositories
{
    public abstract class FsRepository : AsyncRepository
    {
        public FsRepository(string rootDirectory)
        {
            RootDirectory = rootDirectory;
            LuceneIndex = new LuceneIndex(IndexDirectory.FullName);
            DynamicSearchResultTypeName = $"{nameof(Repositories)}.{GetType().Name}SearchResult";            
        }

        public FsRepository(DefaultDataSettingsProvider settings) : this(settings.GetFilesDirectory().FullName)
        { }

        protected abstract string DataDirectoryName { get; set; }
        protected string IndexDirectoryName
        {
            get
            {
                return "lucene_index";
            }
        }
        protected LuceneIndex LuceneIndex { get; set; }
        protected string RootDirectory { get; set; }
        protected string DynamicSearchResultTypeName { get; set; }
        protected DirectoryInfo DataDirectory
        {
            get
            {
                return new DirectoryInfo(Path.Combine(RootDirectory, DataDirectoryName));
            }
        }
        protected DirectoryInfo IndexDirectory
        {
            get
            {
                return new DirectoryInfo(Path.Combine(RootDirectory, IndexDirectoryName));
            }
        }
        public override T Create<T>(T toCreate)
        {
            return (T)Create(typeof(T), toCreate);
        }

        public override object Create(object toCreate)
        {
            Args.ThrowIfNull(toCreate, "toCreate");
            return Create(toCreate.GetType(), toCreate);
        }

        public override object Create(Type type, object toCreate)
        {
            object result = PerformCreate(type, toCreate);
            LuceneIndex.Index(result);
            return result;
        }

        protected abstract object PerformCreate(Type type, object toCreate);
        public override bool Delete(object toDelete)
        {
            return Delete(toDelete.GetType(), toDelete);
        }

        public override bool Delete(Type type, object toDelete)
        {
            bool result = PerformDelete(type, toDelete);
            LuceneIndex.UnIndex(toDelete);
            return result;
        }

        protected abstract bool PerformDelete(Type type, object toDelete);

        public override IEnumerable<object> Query(string propertyName, object propertyValue)
        {
            foreach(Document doc in LuceneIndex.Search(propertyName, propertyValue.ToString()))
            {
                yield return doc.ToDynamicInstance(DynamicSearchResultTypeName);
            }
        }

        public override IEnumerable<object> Query(dynamic query)
        {
            Type dyn = query.GetType();
            Dictionary<object, object> typeDefinition = new Dictionary<object, object>();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            foreach (PropertyInfo prop in dyn.GetProperties())
            {
                object val = prop.GetValue(query);
                typeDefinition[prop.Name] = val;
                parameters[prop.Name] = val;
            }

            return Query(typeDefinition.ToDynamicType(DynamicSearchResultTypeName), parameters);
        }

        public abstract override IEnumerable<T> Query<T>(Func<T, bool> query);

        public override IEnumerable<T> Query<T>(Dictionary<string, object> queryParameters)
        {
            return Query(typeof(T), queryParameters).CopyAs<T>();
        }

        public override IEnumerable<object> Query(Type type, Dictionary<string, object> queryParameters)
        {
            List<Document> results = new List<Document>();
            Parallel.ForEach(queryParameters.Keys, key =>
            {
                results.AddRange(LuceneIndex.Search(key, queryParameters[key].ToString()));
            });
            return results.Select(d => d.ToDynamicInstance(DynamicSearchResultTypeName));
        }

        public abstract override IEnumerable<object> Query(Type type, Func<object, bool> predicate);

        public override IEnumerable<T> Query<T>(dynamic query)
        {
            return Query(typeof(T), query.ToDictionary()).CopyAs<T>();
        }

        public override IEnumerable<T> Query<T>(QueryFilter query)
        {
            return Query(typeof(T), query).CopyAs<T>();
        }

        [Verbosity(VerbosityLevel.Warning, MessageFormat ="QueryFilter Query executed, FsRepository implementation uses LuceneIndex and may not respond as expected")]
        public event EventHandler QueryFilterWarning;
        public override IEnumerable<object> Query(Type type, QueryFilter query)
        {
            FireEvent(QueryFilterWarning);
            return Query(type, query.ToSearchTerms());
        }

        public override T Retrieve<T>(int id)
        {
            return (T)Retrieve(typeof(T), id);
        }

        public override T Retrieve<T>(long id)
        {
            return (T)Retrieve(typeof(T), id);
        }

        public override T Retrieve<T>(string uuid)
        {
            return (T)Retrieve(typeof(T), uuid);
        }

        public abstract override object Retrieve(Type objectType, long id);

        public abstract override object Retrieve(Type objectType, string uuid);

        public override IEnumerable<T> RetrieveAll<T>()
        {
            return RetrieveAll(typeof(T)).CopyAs<T>();
        }

        public abstract override IEnumerable<object> RetrieveAll(Type type);

        public override T Update<T>(T toUpdate)
        {
            return Update(typeof(T), toUpdate).CopyAs<T>();
        }

        public override object Update(object toUpdate)
        {
            return Update(toUpdate.GetType(), toUpdate);
        }

        public abstract override object Update(Type type, object toUpdate);
    }
}
