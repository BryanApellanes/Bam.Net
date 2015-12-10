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
    /// Defines methods to help serialize/deserialize objects to and from expression objects
    /// </summary>
    public interface IExpressionHandler
    {
        /// <summary>
        /// Take an object and convert it to an expression to be serialized.  Child names/indexes should be appended to the
        /// currentPath before serializing child objects.
        /// </summary>
        /// <param name="data">the object to convert</param>
        /// <param name="CurrentPath">the current path to this object from the root, used for tracking references</param>
        /// <param name="serializer">serializer instance for serializing child objects</param>
        /// <returns>an expression which represents a json structure</returns>
        Expression GetExpression(object data, JsonPath currentPath, ISerializerHandler serializer);

        /// <summary>
        /// Determines whether this handler is able to convert an this object type to an expression
        /// </summary>
        /// <param name="objectType">the object type that will be serialized</param>
        /// <returns>true if this handler can handle the type</returns>
        bool CanHandle(Type objectType);

        /// <summary>
        /// Determines whether this handler is able to convert the expression back into an object.
        /// </summary>
        /// <param name="expression">the expression that will be deserialized</param>
        /// <returns>true if this handler can handle the expression</returns>
        bool CanHandle(Expression expression);

        /// <summary>
        /// Convert the expression into an object by creating a new instance of the desired type and
        /// populating it with any necessary values.
        /// </summary>
        /// <param name="expression">the expression to deserialize</param>
        /// <param name="deserializer">deserializer instance to use to deserialize any child expressions</param>
        /// <returns>a fully deserialized object</returns>
        object Evaluate(Expression expression, IDeserializerHandler deserializer);

        /// <summary>
        /// Convert the expression into an object by populating an existing object with any necessary values.
        /// The existingObject will usually come from the get method of a property on an object that doesn't
        /// allow writing to the property.
        /// </summary>
        /// <param name="expression">the expression to deserialize</param>
        /// <param name="existingObject">an existing object to populate</param>
        /// <param name="deserializer">deserializer instance to use to deserialize any child expressions</param>
        /// <returns>a fully deserialized object</returns>
        object Evaluate(Expression expression, object existingObject, IDeserializerHandler deserializer);
    }
}
