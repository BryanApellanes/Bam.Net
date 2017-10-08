/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Bam.Net.Js
{
    /// <summary>
    /// </summary>
    public static class DataSetTagBuilderExtensions
    {
        static string dataSetFormat = "data-{0}{1}";
        static string prefix = string.Empty;
        public static string Prefix 
        {
            get
            {
                return prefix;
            }
            set
            {
                prefix = value;
                if (!prefix.EndsWith("-"))
                {
                    prefix += "-";
                }
            }
        }

        public static TagBuilder DataSet(this TagBuilder tagBuilder, string key, string value)
        {
            tagBuilder.Attributes.Add(string.Format(dataSetFormat, Prefix, key), value);
            return tagBuilder;
        }

        public static string DataSet(this TagBuilder tagBuilder, string key)
        {
            return tagBuilder.Attributes[string.Format(dataSetFormat, Prefix, key)];
        }

        public static TagBuilder DataSetIf(this TagBuilder tagBuilder, bool condition, string key, string value)
        {
            if (condition)
            {
                tagBuilder.DataSet(key, value);
            }

            return tagBuilder;
        }

        
    }
}
