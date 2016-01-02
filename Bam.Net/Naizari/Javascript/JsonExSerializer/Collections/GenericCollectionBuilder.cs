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
    /// <summary>
    /// Implements a collection builder for types implementing the
    /// generic version of ICollection.
    /// </summary>
    /// <typeparam name="ItemT">the item type of the ICollection interface</typeparam>
    public class GenericCollectionBuilder<ItemT> : ICollectionBuilder
    {

        protected ICollection<ItemT> _collector;

        public GenericCollectionBuilder(Type _instanceType)
        {
            _collector = (ICollection<ItemT>)Activator.CreateInstance(_instanceType);
        }

        public GenericCollectionBuilder(ICollection<ItemT> collection)
        {
            _collector = collection;
        }

        #region ICollectionBuilder Members

        public void Add(object item)
        {
            _collector.Add((ItemT)item);
        }

        public virtual object GetResult()
        {
            return _collector;
        }

        public virtual object GetReference()
        {
            return _collector;
        }
        #endregion
    }
}
