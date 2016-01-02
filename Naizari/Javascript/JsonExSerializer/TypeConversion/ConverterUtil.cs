/*
	Copyright © Bryan Apellanes 2015  
*/
/*
 * Copyright (c) 2007, Ted Elliott
 * Code licensed under the New BSD License:
 * http://code.google.com/p/jsonexserializer/wiki/License
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Naizari.Javascript.JsonExSerialization.MetaData;

namespace Naizari.Javascript.JsonExSerialization.TypeConversion
{
    /// <summary>
    /// Helper class for custom type conversion.  Will populate an object from a dictionary
    /// or copy its properties to a dictionary.
    /// </summary>
    public sealed class ConverterUtil
    {
        private TypeData _handler;
        public ConverterUtil(Type forType, SerializationContext context)
        {
            _handler = context.GetTypeHandler(forType);
        }

        /// <summary>
        /// Populates the properties of an object from a map
        /// </summary>
        /// <param name="instance">the object to populate, the type must match the one that this instance was created for</param>
        /// <param name="values">a dictionary of values</param>
        /// <param name="ignoreMissingProperties">true to ignore any keys in the dictionary that are not properties on the object.
        /// If false, an exception will be thrown if a property cannot be found.</param>
        public void PopulateFromDictionary(object instance, IDictionary values, bool ignoreMissingProperties)
        {
            if (_handler.ForType.IsInstanceOfType(instance))
            {
                foreach (object key in values.Keys)
                {
                    string stringKey = key.ToString();
                    IPropertyData prop = _handler.FindProperty(stringKey);
                    if (prop == null && !ignoreMissingProperties)
                    {
                        throw new MissingMemberException("Can't find a property for " + stringKey);
                    }
                    else if (prop != null)
                    {
                        prop.SetValue(instance, values[key]);
                    }
                }
            }
            else
            {
                throw new ArgumentException("Object instance is of a different type than this class was constructed for");
            }
        }

        /// <summary>
        /// Copies an object's properties to a dictionary
        /// </summary>
        /// <param name="instance">the object to copy</param>
        /// <param name="values">the dictionary to copy to, cannot be null</param>
        public void CopyToDictionary(object instance, IDictionary values)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("Parameter instance can not be null");
            }
            if (values == null)
            {
                throw new ArgumentNullException("Parameter values can not be null");
            }
            foreach (PropertyData prop in _handler.Properties)
            {
                values[prop.Name] = prop.GetValue(instance);
            }
        }
    }
}
