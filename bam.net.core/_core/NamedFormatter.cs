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
        public static string NamedFormat(this string format, object source)
        {
            PropertyInfo[] props = source.GetType().GetProperties();
            string returnValue = format;
            foreach (PropertyInfo prop in props)
            {
                returnValue = returnValue.Replace($"{{{prop.Name}}}", prop.GetValue(source)?.ToString());
            }
            return returnValue;
        }
    }
}
