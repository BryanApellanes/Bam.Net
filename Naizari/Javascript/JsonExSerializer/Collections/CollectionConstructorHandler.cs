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
    /// Handles collection classes implementing ICollection
    /// with an constructor matching (ICollection) or (IEnumerable&lt;&gt;)
    /// or (IEnumerable).
    /// </summary>
    public class CollectionConstructorHandler : CollectionHandler
    {
        public override bool IsCollection(Type collectionType)
        {
            // Implements ICollection and has a constructor that takes a single element of type ICollection
            if ((typeof(ICollection).IsAssignableFrom(collectionType)
                && (collectionType.GetConstructor(new Type[] { typeof(ICollection) }) != null)))
                return true;


            Type ienumGeneric = collectionType.GetInterface(typeof(IEnumerable<>).Name);
            if (ienumGeneric != null && collectionType.GetConstructor(new Type[] { ienumGeneric }) != null)
                return true;
            else
                return false;
        }

        public override ICollectionBuilder ConstructBuilder(Type collectionType, int itemCount)
        {
            Type itemType = GetItemType(collectionType);
            // will make a generic builder either way, but itemType might be object
            return (ICollectionBuilder)Activator.CreateInstance(typeof(GenericCollectionCtorBuilder<>).MakeGenericType(itemType), collectionType);
        }

        public override ICollectionBuilder ConstructBuilder(object collection)
        {
            throw new InvalidOperationException("CollectionConstructorHandler does not support modify existing collections");
        }

        public override Type GetItemType(Type CollectionType)
        {
            Type t = null;
            if ((t = CollectionType.GetInterface(typeof(ICollection<>).Name)) != null)
            {
                return t.GetGenericArguments()[0];
            }
            else if ((t = CollectionType.GetInterface(typeof(IEnumerable<>).Name)) != null)
            {
                return t.GetGenericArguments()[0];
            }
            else
            {
                return typeof(object);
            }
        }

    }
}
