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
    /// An interface for converting from one type to another for serialization/deserialization.  The resulting
    /// type is left unspecified.
    /// </summary>
    /// <typeparam name="SrcT">The type of the object before serialization or after deserialization</typeparam>
    public interface IJsonTypeConverter
    {


        Type GetSerializedType(Type sourceType);

        /// <summary>
        /// This method is called before serialization.  The <paramref name="item"/> parameter should be converted
        /// to a type suitable for serialization and returned.
        /// </summary>
        /// <param name="item">the item to be converted</param>
        /// <returns>the converted item to be serialized</returns>
        object ConvertFrom(object item, SerializationContext serializationContext);

        /// <summary>
        /// This method will be called upon deserialization.  The item returned from ConvertFrom on serialization
        /// will be passed as the <paramref name="item"/> parameter.  This object should be converted back to the
        /// desired type and returned.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>the desired object</returns>
        object ConvertTo(object item, Type sourceType, SerializationContext serializationContext);

        /// <summary>
        /// Context parameter to control conversion
        /// </summary>
        object Context { set; }

    }
}
