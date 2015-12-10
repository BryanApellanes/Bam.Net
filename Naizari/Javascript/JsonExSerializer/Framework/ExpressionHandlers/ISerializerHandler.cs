/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Naizari.Javascript.JsonExSerialization.Framework.Expressions;
using Naizari.Javascript.JsonExSerialization.TypeConversion;

namespace Naizari.Javascript.JsonExSerialization.Framework.ExpressionHandlers
{
    /// <summary>
    /// Defines methods to serialize objects into json expressions
    /// </summary>
    public interface ISerializerHandler
    {

        /// <summary>
        /// Serialize the object into an expression.
        /// </summary>
        /// <param name="value">the value to serialize</param>
        /// <param name="currentPath">the current path to the value</param>
        /// <returns>a json expression representing the value</returns>
        Expression Serialize(object value, JsonPath currentPath);

        /// <summary>
        /// Serialize an object into an expression using a specific type converter
        /// </summary>
        /// <param name="value">the value to serialize</param>
        /// <param name="currentPath">the current path to the value</param>
        /// <param name="converter">the type converter to use to convert the object</param>
        /// <returns>a json expression representing the value</returns>
        Expression Serialize(object value, JsonPath currentPath, IJsonTypeConverter converter);

        /// <summary>
        /// Checks for previous references to this object and acts accordingly based on the Reference options.
        /// If an expression is returned, then further evaluation of the object should stop.  If no expression
        /// is returned then the object should be serialized normally.
        /// </summary>
        /// <param name="referenceValue"></param>
        /// <param name="CurrentPath"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Throws InvalidOperationException if there is a circular reference or an object
        /// is not in a referencable state</exception>
        Expression HandleReference(object referenceValue, JsonPath currentPath);

        /// <summary>
        /// Indicates that the object can now be referenced.  Any attempts to build a reference to the current object before
        /// this method is called will result in an exception.
        /// </summary>
        /// <param name="value">the object being referenced</param>
        void SetCanReference(object value);
    }
}
