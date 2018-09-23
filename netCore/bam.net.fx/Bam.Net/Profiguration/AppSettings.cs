/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Profiguration
{
    public class AppSettings
    {
        Dictionary<string, string> _keyedValues;
        public AppSettings()
        {
            this._keyedValues = new Dictionary<string, string>();
        }

        
        public KeyValuePair[] Values
        {
            get
            {
                KeyValuePair[] result = new KeyValuePair[_keyedValues.Count];
                int i = 0;
                foreach(string key in _keyedValues.Keys)
                {
                    result[i] = new KeyValuePair { Key = key, Value = _keyedValues[key] };
                    i++;
                }

                return result;
            }
            set
            {
                _keyedValues = value.ToDictionary<KeyValuePair, string, string>((kvp) => kvp.Key, (kvp) => kvp.Value);
            }
        }

        public string this[string key]
        {
            get
            {
                if (_keyedValues.ContainsKey(key))
                {
                    return _keyedValues[key];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (_keyedValues.ContainsKey(key))
                {
                    _keyedValues[key] = value;
                }
                else
                {
                    _keyedValues.Add(key, value);
                }
            }
        }

        public string[] Keys
        {
            get
            {
                return _keyedValues.Keys.ToArray();
            }
        }
    }
}
