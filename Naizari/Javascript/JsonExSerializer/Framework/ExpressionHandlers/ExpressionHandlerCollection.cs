/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Naizari.Javascript.JsonExSerialization.Framework.Expressions;
using Naizari.Javascript.JsonExSerialization.Framework.ExpressionHandlers.Collections;

namespace Naizari.Javascript.JsonExSerialization.Framework.ExpressionHandlers
{
    /// <summary>
    /// Collection of ExpressionHandler objects as well as methods for looking up a handler based on Type, Expression, or object.
    /// </summary>
    public sealed class ExpressionHandlerCollection : CollectionBase, IContextAware, IList<IExpressionHandler>
    {
        /// <summary>
        /// The handler used for handling null objects
        /// </summary>
        private IExpressionHandler _nullHandler;

        /// <summary>
        /// The handler used for handling any object, type or expression without an explicit handler
        /// </summary>
        private IExpressionHandler _defaultHandler;

        /// <summary>
        /// Serialization context
        /// </summary>
        private SerializationContext _context;

        /// <summary>
        /// Cache for finding a handler based on type.  Once a handler is found for a type, the combination
        /// is stored for quick retrieval by subsequent calls.
        /// </summary>
        private Dictionary<Type, IExpressionHandler> _cache = new Dictionary<Type, IExpressionHandler>();

        /// <summary>
        /// Initializes an instance with the serialization context and a default set of handlers.
        /// </summary>
        /// <param name="Context">serialization context</param>
        public ExpressionHandlerCollection(SerializationContext Context)
        {
            _context = Context;
            InitializeDefaultHandlers();
        }

        /// <summary>
        /// Initializes this context with the default Expression Handlers
        /// </summary>
        private void InitializeDefaultHandlers()
        {
            Add(new DateTimeExpressionHandler());
            Add(new NumericExpressionHandler(Context));
            Add(new BooleanExpressionHandler(Context));
            Add(new ValueExpressionHandler(Context));
            Add(new TypeConverterExpressionHandler(Context));
            //Add(new GenericQueueHandler(Context));
            Add(new ArrayExpressionHandler(Context));
            Add(new DictionaryObjectExpressionHandler(Context));
            _nullHandler = new NullExpressionHandler();
            Add(_nullHandler);
            Add(new CastExpressionHandler(Context));
            Add(new ReferenceExpressionHandler(Context));
            _defaultHandler = new ObjectExpressionHandler(Context);
        }

        /// <summary>
        /// Gets or sets the default expression handler that will be used when no handler in the standard
        /// handler list explicitly handles a type or expression.
        /// </summary>
        public IExpressionHandler DefaultHandler
        {
            get { return this._defaultHandler; }
            set { this._defaultHandler = value; }
        }

        /// <summary>
        /// Gets or sets the handler used to handle null values.
        /// </summary>
        public IExpressionHandler NullHandler
        {
            get { return this._nullHandler; }
            set { this._nullHandler = value; }
        }

        /// <summary>
        /// The serialization context
        /// </summary>
        public SerializationContext Context
        {
            get { return this._context; }
            set { this._context = value; }
        }

        /// <summary>
        /// Adds a handler to the end of the list.
        /// </summary>
        /// <param name="Handler">the expression handler to add</param>
        public void Add(IExpressionHandler Handler)
        {
            List.Add(Handler);
        }

        /// <summary>
        /// Finds the first handler of a given type in the list or null if no handler
        /// of that type is available.
        /// </summary>
        /// <param name="handlerType">the type of handler to find</param>
        /// <returns>the handler if found</returns>
        public IExpressionHandler Find(Type handlerType)
        {
            foreach (IExpressionHandler handler in this)
                if (handler.GetType() == handlerType)
                    return handler;
            return null;
        }

        /// <summary>
        /// Retrieves a handler that can serialize the specified object.  Generally the first handler that
        /// can handle the object is returned.
        /// </summary>
        /// <param name="data">object to get a handler for</param>
        /// <returns>an expression handler that can serialize the object</returns>
        public IExpressionHandler GetHandler(object data)
        {
            if (data == null)
                return NullHandler;

            Type dataType = data.GetType();
            return GetHandler(dataType);
        }

        /// <summary>
        /// Get a handler based on Expression type.  Generally the first handler that can handle the
        /// expression is returned
        /// </summary>
        /// <param name="expression">expression to find a handler for</param>
        /// <returns>and expression handler that can deserialize the expression</returns>
        public IExpressionHandler GetHandler(Expression expression)
        {
            foreach (IExpressionHandler handler in this)
                if (handler.CanHandle(expression))
                    return handler;
            return GetHandler(expression.ResultType);
        }

        /// <summary>
        /// Get a handler based on data type
        /// </summary>
        public IExpressionHandler GetHandler(Type dataType)
        {
            IExpressionHandler h = null;
            if (!_cache.TryGetValue(dataType, out h))
            {
                foreach (IExpressionHandler handler in this)
                    if (handler.CanHandle(dataType))
                    {
                        return _cache[dataType] = handler;
                    }
            }
            return h ?? DefaultHandler;
        }

        /// <summary>
        /// Determines the index of a specific handler in the list
        /// </summary>
        /// <param name="item">the item to find</param>
        /// <returns>the handler's index or -1 if not found</returns>
        public int IndexOf(IExpressionHandler item)
        {
            return List.IndexOf(item);
        }

        /// <summary>
        /// Inserts a handler into the list at the specified index
        /// </summary>
        /// <param name="index">the insertion index</param>
        /// <param name="item">the handler to insert</param>
        public void Insert(int index, IExpressionHandler item)
        {
            List.Insert(index, item);
        }

        /// <summary>
        /// Inserts a handler into the list before an existing handler of a specified type
        /// </summary>
        /// <param name="handlerType">the handler type that new handler will be inserted before</param>
        /// <param name="item">the handler to insert</param>
        public void InsertBefore(Type handlerType, IExpressionHandler item)
        {
            int index = IndexOf(handlerType);
            if (index == -1)
                index = Count;
            Insert(index, item);
        }

        /// <summary>
        /// Inserts a handler into the list after an existing handler of a specified type
        /// </summary>
        /// <param name="handlerType">the handler type that new handler will be inserted after</param>
        /// <param name="item">the handler to insert</param>
        public void InsertAfter(Type handlerType, IExpressionHandler item)
        {
            int index = IndexOf(handlerType);
            if (index == -1)
                index = Count;
            else
                index = Math.Min(index + 1, Count);
            Insert(index, item);
        }

        /// <summary>
        /// Finds the index of a specific type of handler in the list
        /// </summary>
        /// <param name="handlerType">the type of handler to find</param>
        /// <returns>the handler's index or -1 if not found</returns>
        private int IndexOf(Type handlerType)
        {
            int index;
            for (index = 0; index < Count; index++)
                if (this[index].GetType() == handlerType)
                    return index;
            return -1;
        }

        /// <summary>
        /// Gets or sets an item in the list
        /// </summary>
        /// <param name="index">The 0-based index of the item to get or set</param>
        /// <returns>the item at the specified index in the list</returns>
        public IExpressionHandler this[int index]
        {
            get { return (IExpressionHandler) List[index]; }
            set { List[index] = value; }
        }

        /// <summary>
        /// Determines whether the list contains the handler
        /// </summary>
        /// <param name="item">the handler to find</param>
        /// <returns>true if the list contains the handler</returns>
        public bool Contains(IExpressionHandler item)
        {
            return List.Contains(item);
        }

        /// <summary>
        /// Copies the handler list to an array
        /// </summary>
        /// <param name="array">the array to copy to</param>
        /// <param name="arrayIndex">the 0-based index in the array at which copying begins</param>
        public void CopyTo(IExpressionHandler[] array, int arrayIndex)
        {
            List.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets a value indicating whether the list is read only
        /// </summary>
        public bool IsReadOnly
        {
            get { return List.IsReadOnly; }
        }

        /// <summary>
        /// Removes a handler from the list
        /// </summary>
        /// <param name="item">the handler to remove</param>
        /// <returns>true if the handler existed in the list</returns>
        public bool Remove(IExpressionHandler item)
        {
            int index = IndexOf(item);
            if (index != -1)
                RemoveAt(index);
            return index != -1;
        }

        /// <summary>
        /// Returns an enumerator over the list
        /// </summary>
        /// <returns>an enumerator</returns>
        public new IEnumerator<IExpressionHandler> GetEnumerator()
        {
            foreach (IExpressionHandler handler in List)
                yield return handler;
        }

        /// <summary>
        /// Invalidates the Type/Handler cache
        /// </summary>
        private void InvalidateCache()
        {
            _cache.Clear();
        }

        /// <summary>
        /// Handles the clear method by invalidating the cache
        /// </summary>
        protected override void OnClearComplete()
        {
            base.OnClearComplete();
            InvalidateCache();
        }

        /// <summary>
        /// Invalidates the Type/Handler cache whenever a new handler is inserted
        /// </summary>
        /// <param name="index">the insertion point</param>
        /// <param name="value">the inserted item</param>
        protected override void OnInsertComplete(int index, object value)
        {
            base.OnInsertComplete(index, value);
            InvalidateCache();
            this.Context.SetContextAware(value);    
        }

        /// <summary>
        /// Invalidates the Type/Handler cache whenever a handler is updated
        /// </summary>
        /// <param name="index">the 0-based index of the item</param>
        /// <param name="oldValue">the old value</param>
        /// <param name="newValue">the new value</param>
        protected override void OnSetComplete(int index, object oldValue, object newValue)
        {
            base.OnSetComplete(index, oldValue, newValue);
            InvalidateCache();
            if (!object.ReferenceEquals(oldValue, newValue))
                this.Context.SetContextAware(newValue);
        }

        /// <summary>
        /// Clears all existing handlers and resets to the default handlers
        /// </summary>
        public void Reset()
        {
            Clear();
            InitializeDefaultHandlers();
        }
    }

}
