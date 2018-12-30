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
    /// Collection handler class for arrays
    /// </summary>
    public class ArrayHandler : CollectionHandler
    {
        public override bool IsCollection(Type collectionType)
        {
            return collectionType.IsArray;
        }

        public override ICollectionBuilder ConstructBuilder(Type collectionType, int itemCount)
        {
            return new ArrayBuilder(collectionType, itemCount);
        }

        public override ICollectionBuilder ConstructBuilder(object collection)
        {
            throw new InvalidOperationException("ArrayHandler does not support modify existing collections");
        }

        public override Type GetItemType(Type CollectionType)
        {
            return CollectionType.GetElementType();
        }

    }
}
