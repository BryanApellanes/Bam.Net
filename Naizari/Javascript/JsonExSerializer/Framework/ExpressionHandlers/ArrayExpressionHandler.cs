/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Naizari.Javascript.JsonExSerialization.Framework.Expressions;
using Naizari.Javascript.JsonExSerialization.MetaData;
using Naizari.Javascript.JsonExSerialization.Collections;

namespace Naizari.Javascript.JsonExSerialization.Framework.ExpressionHandlers
{
    /// <summary>
    /// Handler for a Json List/Array expression and collections
    /// </summary>
    public class ArrayExpressionHandler : ExpressionHandlerBase
    {

        /// <summary>
        /// Initializes a default instance with no Serialization Context
        /// </summary>
        public ArrayExpressionHandler()
        {
        }

        /// <summary>
        /// Initializes an instance with a Serialization Context
        /// </summary>
        public ArrayExpressionHandler(SerializationContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Determines if the specified type can be handled by this handler.  This handler handles
        /// collections using the CollectionHandler/ICollectionBuilder classes.
        /// </summary>
        /// <param name="objectType">the type of the object</param>
        /// <returns>true if this object is a collection</returns>
        /// <seealso cref="JsonExSerializer.Collections.CollectionHandler"/>
        /// <seealso cref="JsonExSerializer.Collections.ICollectionBuilder"/>
        public override bool CanHandle(Type objectType)
        {
            return Context.TypeHandlerFactory[objectType].IsCollection();
        }

        /// <summary>
        /// Serializes the data into a json array expression.
        /// </summary>
        /// <param name="data">the data to serialize</param>
        /// <param name="currentPath">the current path to the data</param>
        /// <param name="serializer">serializer instance to use to serialize list items</param>
        /// <returns>a json array expression representation</returns>
        public override Expression GetExpression(object data, JsonPath currentPath, ISerializerHandler serializer)
        {
            TypeData handler = Context.GetTypeHandler(data.GetType());

            CollectionHandler collectionHandler = handler.GetCollectionHandler();
            Type elemType = collectionHandler.GetItemType(handler.ForType);

            int index = 0;

            ArrayExpression expression = new ArrayExpression();
            foreach (object value in collectionHandler.GetEnumerable(data))
            {
                Expression itemExpr = serializer.Serialize(value, currentPath.Append(index));
                if (value != null && !ReflectionUtils.AreEquivalentTypes(value.GetType(), elemType))
                {
                    itemExpr = new CastExpression(value.GetType(), itemExpr);
                }
                expression.Add(itemExpr);
                index++;
            }
            return expression;
        }

        /// <summary>
        /// Deserializes an expression into a collection instance
        /// </summary>
        /// <param name="expression">the expression to deserialize</param>
        /// <param name="deserializer">deserializer to deserialize list items</param>
        /// <returns>deserialized object</returns>
        public override object Evaluate(Expression expression, IDeserializerHandler deserializer)
        {
            return Evaluate(expression, null, deserializer);
        }

        /// <summary>
        /// Deserializes an expression by populating an existing object collection with the expression's items.
        /// </summary>
        /// <param name="expression">the expression to deserialize</param>
        /// <param name="existingObject">the collection to populate</param>
        /// <param name="deserializer">deserializer to deserialize list items</param>
        /// <returns>deserialized object</returns>
        public override object Evaluate(Expression expression, object existingObject, IDeserializerHandler deserializer)
        {
            Type ItemType;
            ArrayExpression list = (ArrayExpression)expression;
            ICollectionBuilder builder = ConstructBuilder(existingObject, list, out ItemType);
            object result = EvaluateItems(list, builder, ItemType, deserializer);
            if (result is IDeserializationCallback)
            {
                ((IDeserializationCallback)result).OnAfterDeserialization();
            }
            return result;
        }

        /// <summary>
        /// Evaluates the items in the expression and assigns them to the collection using the builder
        /// </summary>
        /// <param name="expression">the expression to evaluate</param>
        /// <param name="builder">builder used to build the collection</param>
        /// <param name="itemType">the type of the collection's elements</param>
        /// <param name="deserializer">deserializer instance to deserialize items</param>
        /// <returns>evaluated collection object</returns>
        protected virtual object EvaluateItems(ArrayExpression expression, ICollectionBuilder builder, Type itemType, IDeserializerHandler deserializer)
        {
            object result = null;
            bool constructedEventSent = false;
            try
            {
                result = builder.GetReference();
                expression.OnObjectConstructed(result);
                constructedEventSent = true;
            }
            catch
            {
                // this might fail if the builder's not ready
            }
            foreach (Expression item in expression.Items)
            {
                item.ResultType = itemType;
                object itemResult = deserializer.Evaluate(item);
                builder.Add(itemResult);
            }
            result = builder.GetResult();
            if (!constructedEventSent)
                expression.OnObjectConstructed(result);
            return result;
        }

        /// <summary>
        /// Constructs a builder used to build the deserialized collection
        /// </summary>
        /// <param name="collection">an existing collection object or null for a new collection</param>
        /// <param name="list">the list expression</param>
        /// <param name="itemType">the type of the items</param>
        /// <returns>collection builder</returns>
        protected virtual ICollectionBuilder ConstructBuilder(object collection, ArrayExpression list, out Type itemType)
        {
            Type listType = collection != null ? collection.GetType() : list.ResultType;
            TypeData typeHandler = Context.GetTypeHandler(listType);
            CollectionHandler collHandler = typeHandler.GetCollectionHandler();
            itemType = collHandler.GetItemType(listType);
            if (itemType == null)
                throw new Exception("Null item type returned from " + collHandler.GetType() + " for Collection type: " + listType);

            if (collection != null)
                return collHandler.ConstructBuilder(collection);
            else
                return collHandler.ConstructBuilder(listType, list.Items.Count);
        }
    }
}
