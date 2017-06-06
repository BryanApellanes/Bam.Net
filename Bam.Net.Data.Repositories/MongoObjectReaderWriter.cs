/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;

namespace Bam.Net.Data.Repositories
{
	public class MongoObjectReaderWriter: ObjectReaderWriter
	{
		private string _connectionString;
		private string _databaseName;

		public MongoObjectReaderWriter(string connectionString = "mongodb://localhost", string databaseName = "MongoObjectReaderWriterData")
			: base(".\\{0}"._Format(databaseName))
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
			get { return _serverLock.DoubleCheckLock(ref _server, () => Client.GetServer()); }
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

		protected MongoCollection GetCollection(Type type)
		{
			return Database.GetCollection(type, type.FullName);
		}

		public WriteConcernResult LastWriteConcernResult { get; private set; }

		#region ObjectReaderWriter
		public override T ReadByHash<T>(string hash)
		{
			return base.ReadByHash<T>(hash);			
		}
		public override object ReadByHash(Type type, string hash)
		{
			MongoCollection collection = GetCollection(type);
			QueryDocument query = new QueryDocument("Hash", hash);
			return collection.FindOneAs(type, query);
		}

		public override void Write(Type type, object data)
		{
			MongoCollection collection = GetCollection(data.GetType());
			if (Meta.IsNew(data))
			{
				Meta meta = new Meta(data, this, true);
				LastWriteConcernResult = collection.Insert(meta);
			}
			else
			{
				Meta meta = new Meta(data, this, false);
				LastWriteConcernResult = collection.Save(meta);
			}
		}

		public override bool Delete(object data, Type type = null)
		{
			bool result = false;
			try
			{
				MongoCollection collection = GetCollection(data.GetType());
				Meta meta = new Meta(data, this, false);
				QueryDocument query = new QueryDocument("Hash", meta.Hash);
				LastWriteConcernResult = collection.Remove(query);
				result = true;
			}
			catch (Exception ex)
			{
				Message = ex.Message;
				OnDeleteFailed();
			}

			return result;
		}
		#endregion
	}
}
