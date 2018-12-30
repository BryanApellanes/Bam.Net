/*
	Copyright © Bryan Apellanes 2015  
*/
/*
 * Copyright (c) 2007, Ted Elliott
 * Code licensed under the New BSD License:
 * http://code.google.com/p/jsonexserializer/wiki/License
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Javascript.JsonExSerialization.Collections
{
    public class GenericStackBuilder<T> : ICollectionBuilder
    {
        private Stack<T> _stack;
        public GenericStackBuilder()
        {
            _stack = new Stack<T>();
        }

        public GenericStackBuilder(Stack<T> stack)
        {
            _stack = stack;
        }

        #region ICollectionBuilder Members

        public void Add(object item)
        {
            _stack.Push((T)item);
        }

        public object GetResult()
        {
            return _stack;
        }

        public object GetReference()
        {
            return _stack;
        }

        #endregion
    }
}
