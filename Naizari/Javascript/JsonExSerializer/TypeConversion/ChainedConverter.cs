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

namespace Naizari.Javascript.JsonExSerialization.TypeConversion
{
    /// <summary>
    /// Type converter that wraps multiple converters.  Used when multiple
    /// JsonConvert attributes are specified on an element.
    /// </summary>
    public class ChainedConverter : IJsonTypeConverter
    {
        private List<IJsonTypeConverter> _converters;

        public ChainedConverter()
        {
            _converters = new List<IJsonTypeConverter>();
        }

        
        #region IJsonTypeConverter Members


        public Type GetSerializedType(Type sourceType)
        {
            foreach (IJsonTypeConverter converter in _converters)
            {
                sourceType = converter.GetSerializedType(sourceType);
            }
            return sourceType;
        }

        public object ConvertFrom(object item, SerializationContext serializationContext)
        {
            foreach (IJsonTypeConverter converter in _converters)
            {
                item = converter.ConvertFrom(item, serializationContext);
            }
            return item;
        }

        public object ConvertTo(object item, Type sourceType, SerializationContext serializationContext)
        {
            // create a temp copy for processing
            List<IJsonTypeConverter> clonedList = new List<IJsonTypeConverter>(_converters);
            // loop through list until the last item, determining the source type along the way
            // process the last converter in the list and then remove it
            // repeat until all converters have been processed
            while (clonedList.Count > 0)
            {
                Type tempType = sourceType;
                for (int i = 0; i < clonedList.Count; i++)
                {
                    if (i == clonedList.Count - 1)
                    {
                        // last element, process
                        item = clonedList[i].ConvertTo(item, tempType, serializationContext);
                    }
                    else
                    {
                        // just determine the source type
                        tempType = clonedList[i].GetSerializedType(tempType);
                    }
                }
                clonedList.RemoveAt(clonedList.Count - 1);
            }
            return item;
        }

        public object Context
        {
            set {
                foreach (IJsonTypeConverter converter in _converters)
                {
                    converter.Context = value;
                }
            }
        }

        #endregion

        /// <summary>
        /// The list of type converters for this instance
        /// </summary>
        public List<IJsonTypeConverter> Converters
        {
            get { return this._converters; }
        }
    }
}
