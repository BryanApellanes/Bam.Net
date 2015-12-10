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
    /// A default instance of IExpressionHandler to use as a base class.
    /// </summary>
    public abstract class ExpressionHandlerBase : IExpressionHandler, IContextAware
    {
        private SerializationContext _context;

        /// <summary>
        /// Initializes a default instance without a Serialization Context.  Protected since the class is abstract.
        /// </summary>
        protected ExpressionHandlerBase()
        {
        }

        /// <summary>
        /// Initializes an instance with a Serialization Context.
        /// </summary>
        /// <param name="Context">the Serialization Context</param>
        protected ExpressionHandlerBase(SerializationContext Context)
        {
            _context = Context;
        }

        /// <summary>
        /// Gets/sets the Serialization Context, which can be used to retrieve type information, serialization options, etc.
        /// </summary>
        public virtual SerializationContext Context
        {
            get { return _context; }
            set { _context = value; }
        }

        /// <summary>
        /// Take an object and convert it to an expression to be serialized.  Child names/indexes should be appended to the
        /// currentPath before serializing child objects.
        /// </summary>
        /// <param name="data">the object to convert</param>
        /// <param name="CurrentPath">the current path to this object from the root, used for tracking references</param>
        /// <param name="serializer">serializer instance for serializing child objects</param>
        /// <returns>an expression which represents a json structure</returns>
        public abstract Expression GetExpression(object data, JsonPath currentPath, ISerializerHandler serializer);

        /// <summary>
        /// Checks to see if this handler is able to convert an this object type to an expression
        /// </summary>
        /// <param name="objectType">the object type that will be serialized</param>
        /// <returns>true if this handler can handle the type</returns>
        public abstract bool CanHandle(Type objectType);

        /// <summary>
        /// Checks to see if this handler is able to convert the expression back into an object.
        /// </summary>
        /// <param name="expression">the expression that will be deserialized</param>
        /// <returns>true if this handler can handle the expression</returns>
        public virtual bool CanHandle(Expression expression)
        {
            return false;
        }

        /// <summary>
        /// Convert the expression into an object by creating a new instance of the desired type and
        /// populating it with any necessary values.
        /// </summary>
        /// <param name="expression">the epxression to deserialize</param>
        /// <param name="deserializer">deserializer instance to use to deserialize any child expressions</param>
        /// <returns>a fully deserialized object</returns>
        public abstract object Evaluate(Expression expression, IDeserializerHandler deserializer);

        /// <summary>
        /// Convert the expression into an object by populating an existing object with any necessary values.
        /// The existingObject will usually come from the get method of a property on an object that doesn't
        /// allow writing to the property.
        /// </summary>
        /// <param name="expression">the epxression to deserialize</param>
        /// <param name="existingObject">an existing object to populate</param>
        /// <param name="deserializer">deserializer instance to use to deserialize any child expressions</param>
        /// <returns>a fully deserialized object</returns>
        public abstract object Evaluate(Expression expression, object existingObject, IDeserializerHandler deserializer);
    }
}
