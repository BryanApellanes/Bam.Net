/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Reflection;

namespace Naizari.Helpers
{
    public static class Debug
    {
        public static string PropertiesToString(object obj)
        {
            string ret = "*****************\r\n";
            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
                try
                {
                    object val = prop.GetValue(obj, null);
                    string value = val == null ? "null" : val.ToString();

                    ret += prop.Name + ": " + value;
                    ret += "\r\n";
                }
                catch (Exception ex)
                {
                    ret += string.Format("An error occurred getting property value {0}: {1}\r\n", prop.Name, ex.Message);
                }
            }

            ret += "*****************\r\n";
            return ret;
        }

        public static string TryPropertiesToString(object obj)
        {
            try
            {
                return PropertiesToString(obj);
            }
            catch (Exception ex)
            {
                return "An error occurred reflecting object properties: " + ex.Message;
            }
        }
    }
}
