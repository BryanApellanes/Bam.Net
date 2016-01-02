/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.IO;
using System.Reflection;

namespace Bam.Net.Data.Repositories
{
	public interface IObjectReaderWriter
	{
		T Read<T>(long id);
		object Read(Type type, long id);
		T Read<T>(string uuid);
		object Read(Type type, string uuid);
		T ReadByHash<T>(string hash);
		object ReadByHash(Type type, string hash);
		string RootDirectory { get; set; }
		void Enqueue(object data);
		void Write(object data);
		bool Delete(object data);
		event EventHandler WriteObjectFailed;
		event EventHandler WriteObjectPropertiesFailed;

		T[] QueryProperty<T>(string propertyName, object value);
		T[] QueryProperty<T>(string propertyName, Func<object, bool> predicate);
		T[] Query<T>(Func<T, bool> predicate);
		object[] Query(Type type, Func<object, bool> predicate);
		DirectoryInfo GetTypeDirectory(Type type);
		DirectoryInfo GetPropertyDirectory(PropertyInfo prop);
		T ReadProperty<T>(PropertyInfo prop, long id);
		T ReadProperty<T>(PropertyInfo prop, string uuid);
		T ReadPropertyVersion<T>(PropertyInfo prop, string hash, int version);
		void WriteProperty(PropertyInfo prop, object value);
	}
}
