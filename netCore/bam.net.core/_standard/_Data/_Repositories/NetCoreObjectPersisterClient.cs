using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Bam.Net.Data.Repositories
{
    // TODO: implement this as a client to an ObjectPersister service
    public class NetCoreObjectPersisterClient : IObjectPersister
    {
        public string RootDirectory { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler WriteObjectFailed;
        public event EventHandler WriteObjectPropertiesFailed;
        public event EventHandler DeleteFailed;

        public bool Delete(object data, Type type = null)
        {
            throw new NotImplementedException();
        }

        public void Enqueue(Type type, object data)
        {
            throw new NotImplementedException();
        }

        public DirectoryInfo GetPropertyDirectory(PropertyInfo prop)
        {
            throw new NotImplementedException();
        }

        public DirectoryInfo GetPropertyDirectory(Type type, PropertyInfo prop)
        {
            throw new NotImplementedException();
        }

        public DirectoryInfo GetTypeDirectory(Type type)
        {
            throw new NotImplementedException();
        }

        public T[] Query<T>(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public object[] Query(Type type, Func<object, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public T[] QueryProperty<T>(string propertyName, object value)
        {
            throw new NotImplementedException();
        }

        public T[] QueryProperty<T>(string propertyName, Func<object, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public T Read<T>(long id)
        {
            throw new NotImplementedException();
        }

        public T Read<T>(ulong id)
        {
            throw new NotImplementedException();
        }

        public object Read(Type type, long id)
        {
            throw new NotImplementedException();
        }

        public object Read(Type type, ulong id)
        {
            throw new NotImplementedException();
        }

        public T Read<T>(string uuid)
        {
            throw new NotImplementedException();
        }

        public object Read(Type type, string uuid)
        {
            throw new NotImplementedException();
        }

        public T ReadByHash<T>(string hash)
        {
            throw new NotImplementedException();
        }

        public object ReadByHash(Type type, string hash)
        {
            throw new NotImplementedException();
        }

        public T ReadProperty<T>(PropertyInfo prop, long id)
        {
            throw new NotImplementedException();
        }

        public T ReadProperty<T>(PropertyInfo prop, ulong id)
        {
            throw new NotImplementedException();
        }

        public T ReadProperty<T>(PropertyInfo prop, string uuid)
        {
            throw new NotImplementedException();
        }

        public T ReadPropertyVersion<T>(PropertyInfo prop, string hash, int version)
        {
            throw new NotImplementedException();
        }

        public void Write(Type type, object data)
        {
            throw new NotImplementedException();
        }

        public void WriteProperty(PropertyInfo prop, object value)
        {
            throw new NotImplementedException();
        }
    }
}
