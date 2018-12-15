/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data
{
    /// <summary>
    /// Registrar caller used to register Oracle as the 
    /// handler for a database
    /// </summary>
	public class OracleRegistrarCaller : IRegistrarCaller
    {
        public void Register(Database database)
        {
            OracleRegistrar.Register(database);
        }
        public void Register(string connectionName)
        {
            OracleRegistrar.Register(connectionName);
        }

        public void Register(Type daoType)
        {
            OracleRegistrar.Register(daoType);
        }

        public void Register<T>() where T : Dao
        {
            OracleRegistrar.Register<T>();
        }

        public void Register(Incubation.Incubator incubator)
        {
            OracleRegistrar.Register(incubator);
        }
    }
}
