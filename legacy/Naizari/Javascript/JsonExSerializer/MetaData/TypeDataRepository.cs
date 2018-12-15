/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Naizari.Javascript.JsonExSerialization.TypeConversion;

namespace Naizari.Javascript.JsonExSerialization.MetaData
{
    /// <summary>
    /// Factory for TypeHandlers
    /// </summary>
    public class TypeDataRepository
    {
   
        private SerializationContext _context;
        private IDictionary<Type, TypeData> _cache;

        public TypeDataRepository(SerializationContext context)
        {
            _context = context;
            _cache = new Dictionary<Type, TypeData>();
        }

        public SerializationContext Context
        {
            get { return this._context; }
        }

        public TypeData this[Type forType]
        {
            get { return CreateTypeHandler(forType); }
        }

        private TypeData CreateTypeHandler(Type forType)
        {
            TypeData handler;
            if (!_cache.ContainsKey(forType))
            {
                _cache[forType] = handler = CreateNew(forType);
            }
            else
            {
                handler = _cache[forType];
            }
            return handler;
            
        }

        protected virtual TypeData CreateNew(Type forType)
        {
            return new TypeData(forType, _context);
            //return new DynamicTypeHandler(forType, _context);
        }

        public void RegisterTypeConverter(Type forType, IJsonTypeConverter converter)
        {
            if (forType.IsPrimitive || forType == typeof(string))
                throw new ArgumentException("Converters can not be registered for primitive types or string. " + forType, "forType");
            this[forType].TypeConverter = converter;      
        }

        public void RegisterTypeConverter(Type forType, string PropertyName, IJsonTypeConverter converter)
        {
            this[forType].FindProperty(PropertyName).TypeConverter = converter;
        }
    }
}
