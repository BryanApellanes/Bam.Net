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
    /// An IExpressionHandler instance that handles CastExpressions.
    /// </summary>
    public class CastExpressionHandler : ExpressionHandlerBase
    {
        /// <summary>
        /// Initializes a default instance of the class with no Serialization Context.
        /// </summary>
        public CastExpressionHandler()
        {
        }

        /// <summary>
        /// Initializes an instance of the class with a Serialization Context
        /// </summary>
        /// <param name="Context">the Serialization Context</param>
        public CastExpressionHandler(SerializationContext Context)
            : base(Context)
        {
        }

        /// <summary>
        /// GetExpression is not valid for a CastExpression.  CastExpressions should be created directly
        /// during serialization whenever type information is needed.
        /// </summary>
        /// <param name="data">data to serialize</param>
        /// <param name="currentPath">the current path to the object</param>
        /// <param name="serializer">serializer instance</param>
        /// <returns>expression</returns>
        /// <exception cref="InvalidOperationException">This will throw an exception if called</exception>
        public override Expression GetExpression(object data, JsonPath currentPath, ISerializerHandler serializer)
        {
            throw new InvalidOperationException("CastObjectHandler should not be called during Serialization");
        }

        /// <summary>
        /// CanHandle(Type) will always return false for CastExpression because it can't be determined if a cast
        /// is needed by type only.
        /// </summary>
        /// <param name="objectType">the object type</param>
        /// <returns>false</returns>
        public override bool CanHandle(Type objectType)
        {
            return false;
        }

        /// <summary>
        /// Checks to see if this handler can handle the expression.  The CastExpressionHandler handles
        /// CastExpression only.
        /// </summary>
        /// <param name="expression">the expression to check</param>
        /// <returns>true if this handler handles the expression</returns>
        public override bool CanHandle(Expression expression)
        {
            return (expression is CastExpression);
        }

        /// <summary>
        /// Evaluates an expression and constructs the correct object instance
        /// </summary>
        /// <param name="expression">the epxression to evaluate</param>
        /// <param name="deserializer">the deserializer instance</param>
        /// <returns>constructed object</returns>
        public override object Evaluate(Expression expression, IDeserializerHandler deserializer)
        {
            Expression innerExpression = ((CastExpression)expression).Expression;
            innerExpression.ResultType = expression.ResultType;
            return deserializer.Evaluate(innerExpression);
        }

        /// <summary>
        /// Evaluates an expression and populates an existing object with any necessary values
        /// </summary>
        /// <param name="expression">expression to evaluate</param>
        /// <param name="existingObject">the object to populate</param>
        /// <param name="deserializer">the deserializer instance</param>
        /// <returns>constructed object</returns>
        public override object Evaluate(Expression expression, object existingObject, IDeserializerHandler deserializer)
        {
            Expression innerExpression = ((CastExpression)expression).Expression;
            innerExpression.ResultType = expression.ResultType;
            return deserializer.Evaluate(innerExpression, existingObject);
        }
    }
}