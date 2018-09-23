/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Incubation;

namespace Bam.Net.Data
{
    public class FirebirdSqlRegistrarCaller : IRegistrarCaller
    {
        public void Register(Database database)
        {
            FirebirdSqlRegistrar.Register(database);
        }

        public void Register(string connectionName)
        {
            FirebirdSqlRegistrar.Register(connectionName);
        }

        public void Register(Type daoType)
        {
            FirebirdSqlRegistrar.Register(daoType);
        }

        public void Register<T>() where T : Dao
        {
            FirebirdSqlRegistrar.Register<T>();
        }

        public void Register(Incubator incubator)
        {
            FirebirdSqlRegistrar.Register(incubator);
        }
    }
}
