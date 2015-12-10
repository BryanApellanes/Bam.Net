/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Naizari.Javascript.JsonExSerialization.Framework.Expressions;

namespace Naizari.Javascript.JsonExSerialization.Framework.ExpressionHandlers.Collections
{
    public class QueueHandler : CollectionObjectHandlerBase
    {
        public QueueHandler() { }
        public QueueHandler(SerializationContext Context) : base(Context) { }

        protected override void AddItem(object Collection, object itemResult)
        {
            ((Queue)Collection).Enqueue(itemResult);
        }

        public override bool CanHandle(Type ObjectType)
        {
            return typeof(Queue).IsAssignableFrom(ObjectType);
        }
    }

    public class GenericQueueHandler : CollectionObjectHandlerBase
    {
        public GenericQueueHandler() { }
        public GenericQueueHandler(SerializationContext Context) : base(Context) { }

        public override bool CanHandle(Type ObjectType)
        {
            return ObjectType.IsGenericType && typeof(Queue<>).IsAssignableFrom(ObjectType.GetGenericTypeDefinition());
        }

        public override object Evaluate(Expression Expression, object existingObject, IDeserializerHandler deserializer)
        {
            Expression.OnObjectConstructed(existingObject);
            Type collectionType = null;
            if (existingObject != null)
                collectionType = existingObject.GetType();
            else
                collectionType = Expression.ResultType;
            Type itemType = GetItemType(collectionType);
            Type wrapperType = typeof(GenericQueueWrapper<>).MakeGenericType(itemType);
            IList wrapper = (IList) Activator.CreateInstance(wrapperType, existingObject);
            foreach (Expression itemExpr in ((ArrayExpression)Expression).Items)
            {
                itemExpr.ResultType = itemType;
                wrapper.Add(deserializer.Evaluate(itemExpr));
            }
            if (existingObject is IDeserializationCallback)
                ((IDeserializationCallback)existingObject).OnAfterDeserialization();
            return existingObject;
        }
        protected override void EvaluateItems(ArrayExpression Expression, object Collection, Type ItemType, IDeserializerHandler deserializer)
        {
            base.EvaluateItems(Expression, Collection, ItemType, deserializer);
        }

        protected override void AddItem(object Collection, object itemResult)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override Type GetItemType(Type CollectionType)
        {
            return CollectionType.GetGenericArguments()[0];
        }

        private class GenericQueueWrapper<T> : IList
        {
            public Queue<T> instance;
            public GenericQueueWrapper(Queue<T> queue)
            {
                instance = queue ?? new Queue<T>();
            }

            public object Value
            {
                get { return instance; }
            }

            public int Add(object Item)
            {
                instance.Enqueue((T)Item);
                return instance.Count;
            }

            public void Clear()
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public bool Contains(object value)
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public int IndexOf(object value)
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public void Insert(int index, object value)
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public bool IsFixedSize
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            public bool IsReadOnly
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            public void Remove(object value)
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public void RemoveAt(int index)
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public object this[int index]
            {
                get
                {
                    throw new Exception("The method or operation is not implemented.");
                }
                set
                {
                    throw new Exception("The method or operation is not implemented.");
                }
            }

            public void CopyTo(Array array, int index)
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public int Count
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            public bool IsSynchronized
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            public object SyncRoot
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            public IEnumerator GetEnumerator()
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }
    }
}
