/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Data;
using System.Reflection;
using Bam.Net.Configuration;

namespace Bam.Net.Distributed
{
    /// <summary>
    /// Provides a mechanism by which one can expose
    /// on demand DTOs for a given Type by addorning
    /// properties with this Attribute.
    /// </summary>
    public class InfoAttribute: Attribute
    {
        public InfoAttribute() { }
        public InfoAttribute(string text)
        {
            this.Text = text;
        }

        public string Text { get; set; }

        internal static object GetInfo(object value)
        {
            Type infoType = value.CreateDynamicType<InfoAttribute>();
            object result = infoType.Construct();
            result.CopyProperties(value);
            return result;
        }

        internal static Dictionary<string, string> GetInfoDictionary(object value)
        {
            object info = GetInfo(value);
            Dictionary<string, string> result = new Dictionary<string, string>();
            info.EachPropertyInfo((p, i) =>
            {
                object val = p.GetValue(info, null);
                result[p.Name] = val == null ? "null" : val.ToString();
            });

            return result;
        }
    }
}
