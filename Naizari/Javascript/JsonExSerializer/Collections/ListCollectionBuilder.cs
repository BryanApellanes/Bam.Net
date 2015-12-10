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
    /// Collection builder for types implementing IList
    /// </summary>
    public class ListCollectionBuilder : ICollectionBuilder
    {
        private IList _list;
        public ListCollectionBuilder(Type instanceType)
        {
            _list = (IList)Activator.CreateInstance(instanceType);
        }

        public ListCollectionBuilder(IList list)
        {
            _list = list;
        }


        #region ICollectionBuilder Members

        public void Add(object item)
        {
            _list.Add(item);
        }

        public virtual object GetResult()
        {
            return _list;
        }

        public virtual object GetReference()
        {
            return _list;
        }
        #endregion
    }
}
