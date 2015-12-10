/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Data;
using Naizari.Logging;
using Naizari.Helpers;
using Naizari.Extensions;

namespace Naizari.Data.Common
{
    public class UserDb
    {
        static SQLiteAgent agent;
        static object lockOn = new object();
        public static SQLiteAgent Current
        {
            get
            {
                lock (lockOn)
                {
                    if (agent == null)
                    {
                        agent = GetAgent();
                    }
                }
                return agent;
            }
        }

        private static SQLiteAgent GetAgent()
        {
            string appName = Log.Default.ApplicationName;
            if (string.IsNullOrEmpty(appName))
                appName = "defaultAppDb";

            string user = UserUtil.GetCurrentUser(true).Replace("\\", "_").ToCSharpVariableNameWithoutNumbers();
            SQLiteAgent retVal = new SQLiteAgent();
            retVal.Default(appName + "_" + user + ".db3");
            return retVal;// new SQLiteAgent(@"Data Source=.\StateData\" + LogManager.Current.ApplicationName + "_" + user + ".db3;Version=3;"); 
            //new SQLiteAgent(@"Data Source=.\StateData\" + LogManager.Current.ApplicationName + ".db3;Version=3;");
        }
        
    }
}
