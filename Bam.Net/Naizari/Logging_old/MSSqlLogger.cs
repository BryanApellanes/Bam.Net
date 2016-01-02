/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Data;
using Naizari.Configuration;
using Naizari.Testing;

namespace Naizari.Logging
{
    public class MSSqlLogger: DbLogger
    {
        public MSSqlLogger()
            : base()
        {
        }

        public override void Initialize()
        {
            this.Initialize(DaoDbType.MSSql);
            Expect.IsObjectOfType<MSSqlAgent>(this.DatabaseAgent, 
                "Agent type missmatch, Dao configuration created a " + this.DatabaseAgent.GetType().Name + " instead of a MSSqlAgent");
        }
    }
}
