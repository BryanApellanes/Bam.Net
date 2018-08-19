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
        T Read<T>(ulong id);
		object Read(Type type, long id);
        object Read(Type type, ulong id);
        T Read<T>(string uuid);
		object Read(Type type, string uuid);
		T ReadByHash<T>(string hash);
		object ReadByHash(Type type, string hash);
		string RootDirectory { get; set; }
		void Enqueue(Type type, object data);
		void Write(Type type, object data);
		bool Delete(object data, Type type = null);
		event EventHandler WriteObjectFailed;
		event EventHandler WriteObjectPropertiesFailed;

		T[] QueryProperty<T>(string propertyName, object value);
		T[] QueryProperty<T>(string propertyName, Func<object, bool> predicate);
		T[] Query<T>(Func<T, bool> predicate);
		object[] Query(Type type, Func<object, bool> predicate);
		DirectoryInfo GetTypeDirectory(Type type);
		DirectoryInfo GetPropertyDirectory(PropertyInfo prop);
		T ReadProperty<T>(PropertyInfo prop, long id);
        T ReadProperty<T>(PropertyInfo prop, ulong id);
        T ReadProperty<T>(PropertyInfo prop, string uuid);
		T ReadPropertyVersion<T>(PropertyInfo prop, string hash, int version);
		void WriteProperty(PropertyInfo prop, object value);
	}
}
