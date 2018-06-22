/*
	Copyright © Bryan Apellanes 2015  
*/
using Bam.Net.Incubation;
using System;

namespace Bam.Net.Data
{
    public class InterSystemsRegistrarCaller : IRegistrarCaller
    {
        public void Register(Database database)
        {
            CacheRegistrar.Register(database);
        }

        public void Register(string connectionName)
        {
            CacheRegistrar.Register(connectionName);
        }

        public void Register(Type daoType)
        {
            CacheRegistrar.Register(daoType);
        }

        public void Register<T>() where T : Dao
        {
            CacheRegistrar.Register<T>();
        }

        public void Register(Incubator incubator)
        {
            CacheRegistrar.Register(incubator);
        }
    }
}
