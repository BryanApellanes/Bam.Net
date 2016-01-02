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
using System.Reflection;

namespace Naizari.Javascript.JsonExSerialization.Collections
{

    /// <summary>
    /// Handler class for Generic ICollection interface
    /// </summary>
    public class GenericCollectionHandler : CollectionHandler
    {

        private Type _IDictionaryType = typeof(IDictionary);
        private string _IGenericCollectionName = typeof(ICollection<>).Name;

        public override bool IsCollection(Type collectionType)
        {
            return (!collectionType.IsArray 
                    && collectionType.GetInterface(_IGenericCollectionName) != null
                    && !_IDictionaryType.IsAssignableFrom(collectionType));
        }

        public override ICollectionBuilder ConstructBuilder(Type collectionType, int itemCount)
        {
            Type itemType = GetItemType(collectionType);
            return (ICollectionBuilder) Activator.CreateInstance(typeof(GenericCollectionBuilder<>).MakeGenericType(itemType), collectionType);
        }

        public override ICollectionBuilder ConstructBuilder(object collection)
        {
            Type itemType = GetItemType(collection.GetType());
            return (ICollectionBuilder)Activator.CreateInstance(typeof(GenericCollectionBuilder<>).MakeGenericType(itemType), collection);
        }

        public override Type GetItemType(Type CollectionType)
        {
            Type intfType = CollectionType.GetInterface(_IGenericCollectionName);
            return intfType.GetGenericArguments()[0];            
        }

    }
}
