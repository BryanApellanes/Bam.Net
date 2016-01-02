/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Naizari.Javascript.JsonExSerialization.Framework.Expressions;

namespace Naizari.Javascript.JsonExSerialization.Framework.ExpressionHandlers
{

    /// <summary>
    /// IExpressionHandler implementation that handles BooleanExpressions
    /// </summary>
    public class BooleanExpressionHandler : ValueExpressionHandler
    {
        /// <summary>
        /// Initializes a default instance of a BooleanExpressionHandler with no SerializationContext
        /// </summary>
        public BooleanExpressionHandler()
        {
        }

        /// <summary>
        /// Initializes an instance of a BooleanExpressionHandler with a SerializationContext object
        /// </summary>
        public BooleanExpressionHandler(SerializationContext Context)
            : base(Context)
        {
        }

        /// <summary>
        /// Converts a boolean value into a BooleanExpression
        /// </summary>
        /// <param name="data">the data to convert to an expression</param>
        /// <param name="currentPath">the current path to the object, ignored for BooleanExpression</param>
        /// <param name="serializer">serializer instance, ignored</param>
        /// <returns>a BooleanExpression</returns>
        public override Expression GetExpression(object data, JsonPath currentPath, ISerializerHandler serializer)
        {
            return new BooleanExpression(data);
        }

        /// <summary>
        /// Checks to see if this handler can handle this type, returns true for bool type.
        /// </summary>
        /// <param name="objectType">the object type to check</param>
        /// <returns>true if this handler handles the type</returns>
        public override bool CanHandle(Type objectType)
        {
            return (typeof(bool).IsAssignableFrom(objectType));
        }
    }
}
