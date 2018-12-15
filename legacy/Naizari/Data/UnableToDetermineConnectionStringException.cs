/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Data
{
    public class UnableToDetermineConnectionStringException: Exception
    {
        public UnableToDetermineConnectionStringException(string contextName, DaoDbType type)
            : base("Unable to determine connection string from default configuration for contextName '" + contextName + "' DaoDbType '" + type.ToString() + "'")
        {
        }
    }
}
