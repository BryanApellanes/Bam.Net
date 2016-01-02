/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Bam.Net.Profiguration
{
    public class ConnectionStrings
    {
        Dictionary<string, ConnectionStringValue> _keyedValues;
        public ConnectionStrings()
        {
            this._keyedValues = new Dictionary<string, ConnectionStringValue>();
        }

        public ConnectionStringValue[] Values
        {
            get
            {
                ConnectionStringValue[] result = new ConnectionStringValue[_keyedValues.Count];
                int i = 0;
                foreach (string key in _keyedValues.Keys)
                {
                    ConnectionStringValue value = _keyedValues[key];
                    result[i] = value;
                    i++;
                }
                return result;
            }
            set
            {
                _keyedValues = value.ToDictionary<ConnectionStringValue, string>((cs) => cs.ConnectionString.Key);
            }
        }

        public string[] Names
        {
            get
            {
                return _keyedValues.Keys.ToArray();
            }
        }

        public ConnectionStringValue this[string name]
        {
            get
            {
                if (_keyedValues.ContainsKey(name))
                {
                    return _keyedValues[name];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (_keyedValues.ContainsKey(name))
                {
                    _keyedValues[name] = value;
                }
                else
                {
                    _keyedValues.Add(name, value);
                }
            }
        }
    }
}
