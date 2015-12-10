/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Naizari.Javascript.JsonExSerialization.Framework.Expressions;
using System.Collections;

namespace Naizari.Javascript.JsonExSerialization.Framework.ExpressionHandlers.Collections
{
    public abstract class CollectionObjectHandlerBase : ExpressionHandlerBase
    {
        protected CollectionObjectHandlerBase()
        {
        }
        protected CollectionObjectHandlerBase(SerializationContext Context)
            : base(Context)
        {
        }

        protected virtual Type GetItemType(Type CollectionType)
        {
            return typeof(object);
        }

        public override Expression GetExpression(object data, JsonPath CurrentPath, ISerializerHandler serializer)
        {
            return GetExpression((IEnumerable)data, GetItemType(data.GetType()), CurrentPath, serializer);
        }

        protected virtual Expression GetExpression(IEnumerable Items, Type ItemType, JsonPath CurrentPath, ISerializerHandler serializer)
        {
            int index = 0;

            ArrayExpression expression = new ArrayExpression();
            foreach (object value in Items)
            {
                Expression itemExpr = serializer.Serialize(value, CurrentPath.Append(index));
                if (value != null && !ReflectionUtils.AreEquivalentTypes(value.GetType(), ItemType))
                {
                    itemExpr = new CastExpression(value.GetType(), itemExpr);
                }
                expression.Add(itemExpr);
                index++;
            }
            return expression;
        }

        public override object Evaluate(Expression Expression, IDeserializerHandler deserializer)
        {
            object collection = ConstructCollection((ArrayExpression)Expression, deserializer);
            return Evaluate(Expression, collection, deserializer);
        }

        public override object Evaluate(Expression Expression, object existingObject, IDeserializerHandler deserializer)
        {
            Expression.OnObjectConstructed(existingObject);
            Type itemType = GetItemType(existingObject.GetType());
            EvaluateItems((ArrayExpression) Expression, existingObject, itemType, deserializer);
            if (existingObject is IDeserializationCallback)
                ((IDeserializationCallback)existingObject).OnAfterDeserialization();
            return existingObject;
        }

        protected virtual object ConstructCollection(ArrayExpression Expression, IDeserializerHandler deserializer)
        {
            object result = Activator.CreateInstance(Expression.ResultType);
            return result;
        }

        protected virtual void EvaluateItems(ArrayExpression Expression, object Collection, Type ItemType, IDeserializerHandler deserializer)
        {
            foreach (Expression item in Expression.Items)
            {
                item.ResultType = ItemType;
                object itemResult = deserializer.Evaluate(item);
                AddItem(Collection, itemResult);
            }
        }

        /// <summary>
        /// Adds the item to the collection
        /// </summary>
        /// <param name="Collection"></param>
        /// <param name="itemResult"></param>
        protected abstract void AddItem(object Collection, object itemResult);
    }
}
