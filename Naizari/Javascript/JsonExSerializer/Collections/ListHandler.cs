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
    /// Handler class for classes implementing IList
    /// </summary>
    public class ListHandler : CollectionHandler
    {
        private Type _IListType = typeof(IList);
        private Type _ICollectionGenericType = typeof(ICollection<>);
        private Type _IDictionaryType = typeof(IDictionary);

        public override bool IsCollection(Type collectionType)
        {
            return (_IListType.IsAssignableFrom(collectionType)
                && !_ICollectionGenericType.IsAssignableFrom(collectionType)
                && !_IDictionaryType.IsAssignableFrom(collectionType));
        }

        public override ICollectionBuilder ConstructBuilder(Type collectionType, int itemCount)
        {
            return new ListCollectionBuilder(collectionType);
        }

        public override ICollectionBuilder ConstructBuilder(object collection)
        {
            return new ListCollectionBuilder((IList)collection);
        }
    }
}
