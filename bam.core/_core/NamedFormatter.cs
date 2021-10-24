/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace Bam.Net
{
    public static class NamedFormatter
    {
        /// <summary>
        /// Replaces instances of named properties in the specified format with the property values of the specified dataSource.
        /// Named properties in the specified format string should be in the form "{PropertyName}".  This uses a basic string
        /// replacement mechanism, for more robust templating use a template engine.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="dataSource"></param>
        /// <param name="opener"></param>
        /// <param name="closer"></param>
        /// <returns></returns>
        public static string NamedFormat(this string format, object dataSource, string opener = "{", string closer = "}")
        {
            Args.ThrowIfNull(dataSource, nameof(dataSource));
            PropertyInfo[] props = dataSource.GetType().GetProperties();
            string returnValue = format;
            foreach (PropertyInfo prop in props)
            {
                returnValue = returnValue.Replace($"{opener}{prop.Name}{closer}", prop.GetValue(dataSource)?.ToString());
            }
            return returnValue;
        }

        /// <summary>
        /// Replaces instances of named properties in the specified format with the property values of the specified dataSource.
        /// Named properties in the specified format string should be in the form "{PropertyName}".  This uses a basic string
        /// replacement mechanism, for more robust templating use a template engine.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="dataSource"></param>
        /// <param name="opener"></param>
        /// <param name="closer"></param>
        /// <returns></returns>
        public static string NamedFormat(this string format, Dictionary<string, string> dataSource, string opener = "{", string closer = "}")
        {
            Args.ThrowIfNull(dataSource, nameof(dataSource));
            string returnValue = format;
            foreach (string key in dataSource.Keys)
            {
                returnValue = returnValue.Replace($"{opener}{key}{closer}", dataSource[key]);
            }

            return returnValue;
        }
    }
}
