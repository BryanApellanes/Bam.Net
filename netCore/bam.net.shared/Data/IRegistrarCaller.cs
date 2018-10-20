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
    public interface IRegistrarCaller
    {
        void Register(Database database);
        void Register(string connectionName);
        void Register(Type daoType);
        void Register<T>() where T : Dao;
        void Register(Incubator incubator);
    }
}
