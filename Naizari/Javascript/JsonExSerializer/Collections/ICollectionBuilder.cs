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
    /// Interface for an item that can build a collection object
    /// </summary>
    public interface ICollectionBuilder
    {
        void Add(object item);
        object GetResult();
        object GetReference();
    }
}
