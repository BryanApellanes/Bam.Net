/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using System.Collections;

namespace Bam.Net.Data.Repositories
{
    /// <summary>
    /// An incomplete Repository implementation using Mongo as a backing 
    /// data store
    /// </summary>
	public class MongoRepository: Repository, IQueryFilterable
	{
		private string _connectionString;
		private string _databaseName;

		public MongoRepository(string connectionString = "mongodb://localhost", string databaseName = "test") : base()
		{
			this._connectionString = connectionString;
			this._databaseName = databaseName;			
		}

		MongoClient _client;
		object _clientLock = new object();
		protected MongoClient Client 
		{
			get { return _clientLock.DoubleCheckLock(ref _client, () => new MongoClient(_connectionString)); }
		}

		MongoServer _server;
		object _serverLock = new object();
		protected MongoServer Server 
		{
			get { return _serverLock.DoubleCheckLock(ref _server, () => _client.GetServer()); }
		}

		MongoDatabase _database;
		object _databaseLock = new object();

		protected MongoDatabase Database 
		{
			get { return _databaseLock.DoubleCheckLock(ref _database, () => GetDatabase(_databaseName)); }
		}

		protected MongoDatabase GetDatabase(string databaseName) 
		{
			return Server.GetDatabase(databaseName);
		}

		protected MongoCollection GetCollection(string collectionName) 
		{
			return Database.GetCollection(collectionName);
		}

		protected MongoCollection GetCollection<T>() 
		{
			return GetCollection(typeof(T).FullName);
		}

		public WriteConcernResult LastWriteConcernResult { get; private set; }

		public override void AddType<T>()
		{
			BsonClassMap.RegisterClassMap<T>();
			base.AddType(typeof(T));
		}

		#region IRepository Members

		public override T Create<T>(T toCreate)
		{
			return (T)Create((object)toCreate);
		}
        public override object Create(object toCreate)
        {
            return Create(toCreate.GetType(), toCreate);
        }

        public override object Create(Type type, object toCreate)
		{
			try
			{
				MongoCollection collection = GetCollection(type.FullName);
				LastWriteConcernResult = collection.Insert(toCreate);
				return toCreate;
			}
			catch (Exception ex)
			{
				LastException = ex;
				OnCreateFailed(new RepositoryEventArgs(toCreate));
				return null;
			}
		}

		public override T Retrieve<T>(int id) 
		{
			return Retrieve<T>((long) id);
		}

		public override T Retrieve<T>(long id) 
		{
			return (T)Retrieve(typeof(T), id);
		}

        public override T Retrieve<T>(ulong id)
        {
            return (T)Retrieve(typeof(T), id);
        }

        public override object Retrieve(Type objectType, long id)
		{
			try
			{
				PropertyInfo keyProp = GetKeyProperty(objectType);
				MongoCollection collection = GetCollection(objectType.FullName);
				QueryDocument query = new QueryDocument(keyProp.Name, id);
				return collection.FindOneAs(objectType, query);
			}
			catch (Exception ex)
			{
				LastException = ex;
				OnRetrieveFailed(new RepositoryEventArgs(id));
				return null;
			}
		}

        public override object Retrieve(Type objectType, ulong id)
        {
            try
            {
                PropertyInfo keyProp = GetKeyProperty(objectType);
                MongoCollection collection = GetCollection(objectType.FullName);
                QueryDocument query = new QueryDocument(keyProp.Name, BsonValue.Create(id));
                return collection.FindOneAs(objectType, query);
            }
            catch (Exception ex)
            {
                LastException = ex;
                OnRetrieveFailed(new RepositoryEventArgs(id));
                return null;
            }
        }

        public override T Retrieve<T>(string uuid)
        {
            return (T)Retrieve(typeof(T), uuid);
        }

		public override object Retrieve(Type objectType, string uuid)
		{
			try
			{
				MongoCollection collection = GetCollection(objectType.FullName);
				QueryDocument query = new QueryDocument("Uuid", uuid);
				return collection.FindOneAs(objectType, query);
			}
			catch (Exception ex)
			{
				LastException = ex;
				OnRetrieveFailed(new RepositoryEventArgs(uuid));
				return null;
			}
		}

		public override IEnumerable<T> RetrieveAll<T>()
		{
			throw new NotImplementedException(); // see Mongo docs to implement this correctly
		}

        public override void BatchRetrieveAll(Type type, int batchSize, Action<IEnumerable<object>> processor)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<object> RetrieveAll(Type type)
		{
			throw new NotImplementedException();
		}

		public override IEnumerable<object> Query(Type type, Func<object, bool> predicate)
		{
			throw new NotImplementedException();
		}

		public override IEnumerable<T> Query<T>(Func<T, bool> predicate)
		{
			throw new NotImplementedException();
		}

        public override IEnumerable<T> Query<T>(Dictionary<string, object> queryParameters)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<object> Query(Type type, Dictionary<string, object> queryParameters)
        {
            throw new NotImplementedException();
        }

		public override IEnumerable<T> Query<T>(dynamic query) //where T: new()
		{
			MongoCollection collection = GetCollection<T>();
			List<T> results = new List<T>(collection.FindAs(typeof(T), query));			
			return results;
		}

        public override IEnumerable<object> Query(Type type, QueryFilter query)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<T> Query<T>(QueryFilter query)
        {
            throw new NotImplementedException();
        }

        public override T Update<T>(T toUpdate)
		{
			return (T)Update((object)toUpdate);
		}

        public override object Update(object toUpdate)
        {
            return Update(toUpdate.GetType(), toUpdate);
        }

        public override object Update(Type type, object toUpdate)
		{
			try
			{
				MongoCollection collection = GetCollection(type.FullName);
				LastWriteConcernResult = collection.Save(toUpdate);
				return toUpdate;
			}
			catch (Exception ex)
			{
				LastException = ex;
				OnUpdateFailed(new RepositoryEventArgs(toUpdate));
				return null;
			}
		}

		public override bool Delete<T>(T toDelete) 
		{
			return Delete((object)toDelete);
		}

        public override bool Delete(object toDelete)
        {
            return Delete(toDelete.GetType(), toDelete);
        }

        public override bool Delete(Type type, object toDelete)
		{
			try
			{
				PropertyInfo keyProp = GetKeyProperty(type);
				MongoCollection collection = GetCollection(type.FullName);
				QueryDocument query = new QueryDocument(keyProp.Name, (BsonValue)keyProp.GetValue(toDelete));
				LastWriteConcernResult = collection.Remove(query);
				return true;
			}
			catch (Exception ex)
			{
				LastException = ex;
				OnDeleteFailed(new RepositoryEventArgs(toDelete));
				return false;
			}
		}
        
		public override IEnumerable<object> Query(string propertyName, object value)
		{
			throw new NotImplementedException();
		}
		public override IEnumerable<object> Query(dynamic query)
		{
			throw new NotImplementedException();
		}
		public IEnumerable<T> Query<T>(IMongoQuery query)
		{
			throw new NotImplementedException();
		}
        #endregion
    }
}
