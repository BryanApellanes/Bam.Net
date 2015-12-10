/*
	Copyright © Bryan Apellanes 2015  
*/
using Naizari.Data;
using Naizari.Logging;

namespace Naizari.Data.Common
{
    public static class AppDb
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
            SQLiteAgent retVal = new SQLiteAgent();
            string appName = Log.Default.ApplicationName;
            if(string.IsNullOrEmpty(appName))
                appName = "defaultAppDb";
            retVal.Default(appName + ".db3");
            return retVal;
            //return new SQLiteAgent(@"Data Source=.\StateData\" + LogManager.Current.ApplicationName + ".db3;Version=3;");
        }
    }
}
