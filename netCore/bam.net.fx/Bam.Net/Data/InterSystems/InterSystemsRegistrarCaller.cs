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
            InterSystemsRegistrar.Register(database);
        }

        public void Register(string connectionName)
        {
            InterSystemsRegistrar.Register(connectionName);
        }

        public void Register(Type daoType)
        {
            InterSystemsRegistrar.Register(daoType);
        }

        public void Register<T>() where T : Dao
        {
            InterSystemsRegistrar.Register<T>();
        }

        public void Register(Incubator incubator)
        {
            InterSystemsRegistrar.Register(incubator);
        }
    }
}
