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
    public class OleDbRegistrarCaller: IRegistrarCaller
    {
        public void Register(Database database)
        {
            OleDbRegistrar.Register(database);
        }

        public void Register(string connectionName)
        {
            OleDbRegistrar.Register(connectionName);
        }

        public void Register(Type daoType)
        {
            OleDbRegistrar.Register(daoType);
        }

        public void Register<T>() where T : Dao
        {
            OleDbRegistrar.Register<T>();
        }

        public void Register(Incubator incubator)
        {
            OleDbRegistrar.Register(incubator);
        }
    }
}
