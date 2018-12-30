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
    /// Expression Handler for handling null values and expressions
    /// </summary>
    public class NullExpressionHandler : ExpressionHandlerBase
    {
        /// <summary>
        /// Creates a null expression
        /// </summary>
        /// <param name="data">the data</param>
        /// <param name="currentPath">current path</param>
        /// <param name="serializer">serializer instance</param>
        /// <returns>NullExpression</returns>
        public override Expression GetExpression(object data, JsonPath currentPath, ISerializerHandler serializer)
        {
            return new NullExpression();
        }

        /// <summary>
        /// CanHandle always returns false.  This handler should be set as the NullHandler on the ExpressionHandlerCollection.
        /// </summary>
        /// <param name="objectType">the object type</param>
        /// <returns>always returns false</returns>
        public override bool CanHandle(Type objectType)
        {
            return false;
        }

        /// <summary>
        /// Determines whether this handler can deserialize a specific expression.  The NullExpressionHandler
        /// only handles NullExpressions.
        /// </summary>
        /// <param name="expression">the expression</param>
        /// <returns>true if this handler handles the expression</returns>
        public override bool CanHandle(Expression expression)
        {
            return (expression is NullExpression);
        }

        /// <summary>
        /// Converts the expression back into an object
        /// </summary>
        /// <param name="expression">the expression</param>
        /// <param name="deserializer">the deserializer</param>
        /// <returns>null</returns>
        public override object Evaluate(Expression expression, IDeserializerHandler deserializer)
        {
            if (!(expression is NullExpression))
                throw new ArgumentException("expression should be NullExpression");
            return null;
        }

        /// <summary>
        /// Converts an existing object
        /// </summary>
        /// <param name="expression">the expression</param>
        /// <param name="existingObject">an existing object</param>
        /// <param name="deserializer">deserializer</param>
        /// <returns>the existing object</returns>
        public override object Evaluate(Expression expression, object existingObject, IDeserializerHandler deserializer)
        {
            return existingObject;
        }
    }
}
