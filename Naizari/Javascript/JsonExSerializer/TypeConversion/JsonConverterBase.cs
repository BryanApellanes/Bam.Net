/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Javascript.JsonExSerialization.TypeConversion
{
    public abstract class JsonConverterBase : IJsonTypeConverter
    {
        protected object _context;

        #region IJsonTypeConverter Members

        public abstract Type GetSerializedType(Type sourceType);

        public abstract object ConvertFrom(object item, SerializationContext serializationContext);

        public abstract object ConvertTo(object item, Type sourceType, SerializationContext serializationContext);

        /// <summary>
        /// Provides an optional parameter to the converter to control some of its functionality.   The Context
        /// is Converter-dependent.
        /// </summary>
        public virtual object Context
        {
            get { return _context; }
            set { _context = value; }
        }

        #endregion
    }
}
