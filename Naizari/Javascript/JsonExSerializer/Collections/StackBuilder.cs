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
using System.Collections;

namespace Naizari.Javascript.JsonExSerialization.Collections
{

    /// <summary>
    /// Builder for a non-generic System.Collections.Stack
    /// class.
    /// </summary>
    public class StackBuilder : ICollectionBuilder
    {
        private object _stack;

        public StackBuilder(Type stackType)
        {
            _stack = Activator.CreateInstance(stackType);
        }

        public StackBuilder(Stack stack)
        {
            _stack = stack;
        }

        #region ICollectionBuilder Members

        public virtual void Add(object item)
        {
            ((Stack)_stack).Push(item);
        }

        public virtual object GetResult()
        {
            return _stack;
        }

        public virtual object GetReference()
        {
            return _stack;
        }
        #endregion
    }
}
