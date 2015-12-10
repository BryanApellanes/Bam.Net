/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Data
{
    public class DaoStub<T> where T: DaoObject, new()
    {
        public DaoStub(long id)
        {
            this.Id = id;
            T proxy = new T();
            this.DatabaseAgent = DaoContext.Get(proxy.DataContextName).DatabaseAgent;
        }

        public DaoStub(int id)
        {
            this.Id = id;
            T proxy = new T();
            this.DatabaseAgent = DaoContext.Get(proxy.DataContextName).DatabaseAgent;
        }

        public DaoStub(long id, DatabaseAgent agent)
            : this(id)
        {
            this.DatabaseAgent = agent;
        }

        public DaoStub(int id, DatabaseAgent agent)
            : this(id)
        {
            this.DatabaseAgent = agent;
        }

        public long Id { get; set; }
        public DatabaseAgent DatabaseAgent { get; internal set; }

        public static implicit operator T(DaoStub<T> stub)
        {
            return stub.DatabaseAgent.SelectById<T>(stub.Id);
        }
    }
}
