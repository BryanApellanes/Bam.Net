/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Web
{
    public static class FormParameterExtensionscs
    {
        public static string FormEncode(this object value)
        {
            StringBuilder result = new StringBuilder();
            value.GetType().GetProperties().Each(prop =>
            {
                result.AppendLine(string.Format("{0}: {1}", prop.Name, Uri.EscapeUriString(prop.GetValue(value).ToString())));
            });
            return result.ToString();
        }
    }
}
