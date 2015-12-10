/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Naizari.Extensions;

namespace Naizari.Javascript.JsonControls
{
    public class JsonAttributes
    {
        Dictionary<string, string> attributes;

        public JsonAttributes()
        {
            attributes = new Dictionary<string, string>();
        }

        public string this[string attributeName]
        {
            get
            {
                if (attributes.ContainsKey(attributeName))
                    return attributes[attributeName];

                return string.Empty;
            }
            set
            {
                if (attributes.ContainsKey(attributeName))
                    attributes[attributeName] = value;
                else
                    attributes.Add(attributeName, value);
            }
        }

        public void SetAttribute(string attributeName, string value)
        {
            if (attributes.ContainsKey(attributeName))
                attributes[attributeName] = value;
            else
                attributes.Add(attributeName, value);
        }

        internal void AddAttributes(HtmlGenericControl control)
        {
            if (attributes.Count > 0)
            {
                foreach (string key in attributes.Keys)
                {
                    if (!string.IsNullOrEmpty(attributes[key].Trim()))
                        control.Attributes.Add(key, attributes[key]);
                }
            }
        }

        public void Clear() { attributes.Clear(); }

        public override string ToString()
        {
            return DictionaryExtensions.ToDelimited<string, string>(attributes, ";", ":");
        }
    }
}
