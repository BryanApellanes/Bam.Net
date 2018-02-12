/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Bam.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bam.Net.Web
{
    /// <summary>
    /// Represents arguments passed to and from Http
    /// requests and responses; either query string or
    /// posted form data
    /// </summary>
    public class HttpArgs
    {
        Dictionary<string, string> _pairs;
        public HttpArgs()
        {
            _pairs = new Dictionary<string, string>();
            _ordered = new List<string>();
        }

        public HttpArgs(string inputString, string contentType = null)
            : this()
        {
            InputString = inputString;
            ContentType = contentType;
            ParseInput();
        }
        
        public string InputString
        {
            get;
            set;
        }

        string _contentType;
        public string ContentType
        {
            get
            {
                if (_contentType == null)
                {
                    _contentType = "application/x-www-form-urlencoded; charset=utf-8";
                }

                return _contentType.ToLowerInvariant();
            }
            set { _contentType = value; }
        }
        
        protected virtual void ParseInput()
        {
            if (ContentType.Contains("application/json") || ContentType.Equals("application/json"))
            {
                ParseJson(InputString);               
            }
            else if(ContentType.Contains("application/x-www-form-urlencoded") || ContentType.Equals("application/x-www-form-urlencoded"))
            {
                ParseForm(InputString);
            }
        }

        public void ParseForm(string inputString)
        {
            string[] pairs = inputString.DelimitSplit("?", "&");
            foreach (string pair in pairs)
            {
                string[] keyVal = pair.DelimitSplit("=");
                if (keyVal.Length == 2)
                {
                    string val = Uri.UnescapeDataString(keyVal[1]);
                    this.Add(keyVal[0], val);
                }
                else
                {
                    this.Add(keyVal[0], "");
                }
            }
        }

        public void ParseJson(string inputString)
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                JObject obj = (JObject)JsonConvert.DeserializeObject(inputString);

                obj.Children().Each(jtoken =>
                {
                    if (jtoken is JProperty prop)
                    {
                        this.Add(prop.Name, prop.Value.ToString());
                    }
                });
            }
        }

        public void Add(string key, string value)
        {
            _pairs.Add(key, value);
            _ordered.Add(value);
        }

        List<string> _ordered;
        public string[] Ordered
        {
            get
            {
                return _ordered.ToArray();
            }
        }

        public string[] Keys
        {
            get
            {
                return _pairs.Keys.ToArray();                    
            }
        }

        public int Count
        {
            get
            {
                return _pairs.Values.Count;
            }
        }

        public bool Has(string key)
        {
            return Has(key, out string ignore);
        }

        /// <summary>
        /// Returns true if the current HttpArgs instance
        /// has the specified key
        /// </summary>
        /// <param name="key">The key to look for</param>
        /// <param name="value">The value associated with key</param>
        /// <returns></returns>
        public bool Has(string key, out string value)
        {
            value = this[key];
            return !string.IsNullOrEmpty(value);
        }

        public string this[string key]
        {
            get
            {
                if (_pairs.ContainsKey(key))
                {
                    return _pairs[key];
                }

                return string.Empty;
            }
            set
            {
                _pairs[key] = Uri.EscapeDataString(value);
            }
        }

    }
}
