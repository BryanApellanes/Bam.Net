/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Data;
using Naizari.Testing;

namespace Naizari.Logging
{
    public class SQLiteLogger: DbLogger
    {
        public SQLiteLogger()
            : base()
        {
        }

        public SQLiteLogger(SQLiteAgent agent)
            : base(agent)
        {
        }

        public override void Initialize()
        {
            this.Initialize(DaoDbType.SQLite);
            Expect.IsObjectOfType<SQLiteAgent>(this.DatabaseAgent,
                "Agent type missmatch, Dao configuration created a " + this.DatabaseAgent.GetType().Name + " instead of a SQLiteAgent");
        }
    }
}
