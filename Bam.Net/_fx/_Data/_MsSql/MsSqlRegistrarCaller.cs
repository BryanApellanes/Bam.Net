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
    public class MsSqlRegistrarCaller: IRegistrarCaller
    {
        public void Register(Database database)
        {
            MsSqlRegistrar.Register(database);
        }

        public void Register(string connectionName)
        {
            MsSqlRegistrar.Register(connectionName);
        }

        public void Register(Type daoType)
        {
            MsSqlRegistrar.Register(daoType);
        }

        public void Register<T>() where T : Dao
        {
            MsSqlRegistrar.Register<T>();
        }

        public void Register(Incubator incubator)
        {
            MsSqlRegistrar.Register(incubator);
        }
    }
}
