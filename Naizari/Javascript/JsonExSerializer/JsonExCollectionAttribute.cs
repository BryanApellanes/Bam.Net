/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Javascript.JsonExSerialization
{
    /// <summary>
    /// Specifies a collection handler for a class, and/or overrides the item type for an existing
    /// handler.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple=false,Inherited=true)]
    public sealed class JsonExCollectionAttribute : System.Attribute
    {
        private Type _collectionHandlerType;
        private string _collectionHandlerTypeName;
        private Type _itemType;
        private string _itemTypeName;

        /// <summary>
        /// Assign the CollectionHandler to this class.  The class will then be treated as a JSON array.
        /// </summary>
        /// <param name="CollectionHandlerType">The type for the CollectionHandler</param>
        /// <see cref="http://code.google.com/p/jsonexserializer/wiki/Collections"/>
        public JsonExCollectionAttribute()
        {
        }


        public Type GetCollectionHandlerType()
        {
            if (_collectionHandlerType != null)
                return _collectionHandlerType;
            else if (!string.IsNullOrEmpty(_collectionHandlerTypeName))
                return Type.GetType(_collectionHandlerTypeName);
            else
                return null;
        }

        public Type GetItemType()
        {
            if (_itemType != null)
                return _itemType;
            else if (!string.IsNullOrEmpty(_itemTypeName))
                return Type.GetType(_itemTypeName);
            else
                return null;
        }

        public bool IsValid()
        {
            return GetCollectionHandlerType() != null || GetItemType() != null;
        }

        public System.Type CollectionHandlerType
        {
            get { return this._collectionHandlerType; }
            set { this._collectionHandlerType = value; }
        }

        public string CollectionHandlerTypeName
        {
            get { return this._collectionHandlerTypeName; }
            set { this._collectionHandlerTypeName = value; }
        }

        public System.Type ItemType
        {
            get { return this._itemType; }
            set { this._itemType = value; }
        }

        public string ItemTypeName
        {
            get { return this._itemTypeName; }
            set { this._itemTypeName = value; }
        }
    }
}
