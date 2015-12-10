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
    /// Expression handler for handling Json references, a serializer json extension.
    /// </summary>
    public class ReferenceExpressionHandler : ExpressionHandlerBase
    {

        /// <summary>
        /// Initializes a default instance with no Serialization Context
        /// </summary>
        public ReferenceExpressionHandler()
        {
        }

        /// <summary>
        /// Initializes an instance with a Serialization Context
        /// </summary>
        public ReferenceExpressionHandler(SerializationContext Context)
            : base(Context)
        {
        }

        /// <summary>
        /// Returns a reference expression to the current data
        /// </summary>
        /// <param name="data">the object data</param>
        /// <param name="currentPath">current path to this object</param>
        /// <param name="serializer">serializer instance</param>
        /// <returns>reference expression</returns>
        public override Expression GetExpression(object data, JsonPath currentPath, ISerializerHandler serializer)
        {
            return serializer.HandleReference(data, currentPath);
        }

        /// <summary>
        /// Determines whether this handler is able to convert an this object type to an expression
        /// </summary>
        /// <param name="objectType">the object type that will be serialized</param>
        /// <returns>true if this handler can handle the type</returns>
        public override bool CanHandle(Type objectType)
        {
            return false;
        }

        /// <summary>
        /// Determines whether this handler is able to convert the expression back into an object.
        /// </summary>
        /// <param name="expression">the expression that will be deserialized</param>
        /// <returns>true if this handler can handle the expression</returns>
        public override bool CanHandle(Expression expression)
        {
            return (expression is ReferenceExpression);
        }

        /// <summary>
        /// Resolves the reference to another object and returns that object
        /// </summary>
        /// <param name="expression">the expression to deserialize</param>
        /// <param name="deserializer">deserializer instance to use to deserialize any child expressions</param>
        /// <returns>a fully deserialized object</returns>
        public override object Evaluate(Expression expression, IDeserializerHandler deserializer)
        {
            return ((ReferenceExpression)expression).ReferencedValue;
        }

        /// <summary>
        /// This method is invalid for Reference expressions.  References can't be updated
        /// </summary>
        /// <param name="expression">reference expression</param>
        /// <param name="existingObject">existing object, ignored</param>
        /// <param name="deserializer">deserializer instance</param>
        /// <returns>nothing</returns>
        /// <exception cref="InvalidOperationException">References cannot be updated</exception>
        public override object Evaluate(Expression expression, object existingObject, IDeserializerHandler deserializer)
        {
            throw new InvalidOperationException("Cannot update a reference");
        }
    }
}
