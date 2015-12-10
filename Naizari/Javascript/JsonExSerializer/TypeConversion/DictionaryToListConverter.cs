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
    /// Converts a dictionary of objects to a list.  On Deserialization, a property
    /// of the value type is used as the key.
    /// </summary>
    public class DictionaryToListConverter : JsonConverterBase
    {

        public DictionaryToListConverter()
        {
            _context = "";
        }

        #region IJsonTypeConverter Members

        public override Type GetSerializedType(Type sourceType)
        {
            return typeof(Object);
        }

        public override object ConvertFrom(object item, SerializationContext serializationContext)
        {
            IDictionary dictionary = (IDictionary)item;
            return dictionary.Values;
        }

        public override object ConvertTo(object item, Type sourceType, SerializationContext serializationContext)
        {
            IDictionary dictionary = (IDictionary) Activator.CreateInstance(sourceType);
            ICollection coll = (ICollection)item;
            foreach (object colItem in coll)
            {
                IPropertyData propHandler = serializationContext.GetTypeHandler(colItem.GetType()).FindProperty(Context.ToString());
                if (propHandler == null)
                {
                    throw new MissingMemberException("Type: " + item.GetType().Name + " does not have an accessible property: " + Context);
                }

                dictionary[propHandler.GetValue(colItem)] = colItem;
            }
            return dictionary;
        }

        public override object Context
        {
            get { return base.Context; }
            set { base.Context = value != null ? value.ToString() : ""; }
        }

        #endregion
    }
}
