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
using System.IO;
using System.Collections;
using Naizari.Javascript.JsonExSerialization.TypeConversion;
using Naizari.Javascript.JsonExSerialization.Collections;
using System.Reflection;
using Naizari.Javascript.JsonExSerialization.MetaData;
using Naizari.Javascript.JsonExSerialization.Framework.Expressions;
using Naizari.Javascript.JsonExSerialization.Framework.ExpressionHandlers;

namespace Naizari.Javascript.JsonExSerialization.Framework
{
    /// <summary>
    /// Class to do the work of serializing an object
    /// </summary>
    sealed class SerializerHelper : JsonWriter, ISerializerHandler
    {
        private Type _serializedType;
        private SerializationContext _context;
        //private TextWriter _writer;
        private const int indentStep = 3;
        private IDictionary<object, ReferenceInfo> _refs;

        internal SerializerHelper(Type t, SerializationContext context, TextWriter writer) : base(writer, !context.IsCompact)
        {
            _serializedType = t;
            _context = context;
            //_writer = writer;
            _refs = new Dictionary<object, ReferenceInfo>(new ReferenceEqualityComparer<object>());
        }

        /// <summary>
        /// Serialize the given object
        /// </summary>
        /// <param name="value">object to serialize</param>
        public void Serialize(object value)
        {
            //if (value != null && _context.OutputTypeComment)
            //{
            //    string comment = "";
            //    comment += "/*" + "\r\n";
            //    comment += "  Created by JsonExSerializer" + "\r\n";
            //    comment += "  Assembly: " + value.GetType().Assembly.ToString() + "\r\n";
            //    comment += "  Type: " + value.GetType().FullName + "\r\n";
            //    comment += "*/" + "\r\n";
            //    this.WriteComment(comment);
            //}
            Expression expr = Serialize(value, new JsonPath(), null);
            if (value != null && !ReflectionUtils.AreEquivalentTypes(value.GetType(), _serializedType))
            {
                expr = new CastExpression(value.GetType(), expr);
            }
            ExpressionWriter.Write(this, _context, expr);
        }

        public Expression Serialize(object value, JsonPath currentPath)
        {
            return Serialize(value, currentPath, null);
        }
        /// <summary>
        /// Serialize the given object at the current indent level.  The path to the object is represented by
        /// currentPath such as "this.name", etc.  This is an internal method that can be called recursively.
        /// </summary>
        /// <param name="value">the object to serialize</param>
        /// <param name="currentPath">the current path for reference writing</param>
        public Expression Serialize(object value, JsonPath currentPath, IJsonTypeConverter converter)
        {
            if (value == null)
            {
                return new NullExpression();
            }
            else
            {

                Expression expr = HandleReference(value, currentPath);
                if (expr != null)
                    return expr;

                ISerializationCallback callback = value as ISerializationCallback;
                if (callback != null)
                    callback.OnBeforeSerialization();

                try
                {
                    //TODO: this is too early for converters
                    SetCanReference(value);    // regular object, can reference at any time
                    IExpressionHandler objHandler;
                    if (converter != null)
                    {
                        TypeConverterExpressionHandler converterHandler = (TypeConverterExpressionHandler)_context.ExpressionHandlers.Find(typeof(TypeConverterExpressionHandler));
                        //TODO: make sure it exists
                        return converterHandler.GetExpression(value, converter, currentPath, this);
                    }
                    objHandler = _context.ExpressionHandlers.GetHandler(value);
                    return objHandler.GetExpression(value, currentPath, this);
                }
                finally
                {
                    if (callback != null)
                        callback.OnAfterSerialization();
                }
            }
        }

        public Expression HandleReference(object value, JsonPath CurrentPath)
        {
            if (!IsReferenceable(value))
                return null;

            ReferenceInfo refInfo = null;
            if (_refs.ContainsKey(value))
            {
                /*
                 * This object has already been seen by the serializer so
                 * determine what to do with it.  If its part of the current path
                 * then its a circular reference and an error needs to be thrown or it should
                 * be ignored depending on the option. Otherwise write a reference to it
                 */
                refInfo = _refs[value];
                JsonPath refPath = refInfo.Path;
                switch (_context.ReferenceWritingType)
                {
                    case SerializationContext.ReferenceOption.WriteIdentifier:
                        if (!refInfo.CanReference)
                            throw new InvalidOperationException("Can't reference object: " + refPath + " from " + CurrentPath + ", either it is a collection, or it has not been converted yet");

                        return new ReferenceExpression(refPath);
                    case SerializationContext.ReferenceOption.IgnoreCircularReferences:
                        if (CurrentPath.StartsWith(refPath))
                        {
                            return new NullExpression();
                        }
                        break;
                    case SerializationContext.ReferenceOption.ErrorCircularReferences:
                        if (CurrentPath.StartsWith(refPath))
                        {
                            throw new InvalidOperationException("Circular reference detected.  Current path: " + CurrentPath + ", reference to: " + refPath);
                        }
                        break;
                }
            }
            else
            {
                refInfo = new ReferenceInfo(CurrentPath);
                _refs[value] = refInfo;
            }
            return null;
        }

        private static bool IsReferenceable(object value)
        {
            return value.GetType().IsClass && !(value is string);
        }

        /// <summary>
        /// Indicates that the object can now be referenced.  Any attempts to build a reference to the current object before
        /// this method is called will result in an exception.
        /// </summary>
        /// <param name="value">the object being referenced</param>
        public void SetCanReference(object value) {
            if (!IsReferenceable(value))
                return;

            ReferenceInfo refInfo;
            if (!_refs.TryGetValue(value, out refInfo))
                throw new ArgumentException(string.Format("No reference information available for {0}.  HandleReference must be called before calling SetCanReference", value), "value");

            refInfo.CanReference = true;
        }

        /// <summary>
        /// Writes out the type for an object in regular C# code syntax
        /// </summary>
        /// <param name="t">the type to write</param>
        protected override void WriteTypeInfo(Type t)
        {
            string alias = _context.TypeAliases[t];
            if (alias != null) {
                WriteTypeInfo(alias);
                return;
            }
            base.WriteTypeInfo(t);
        }

        /// <summary>
        /// Helper class to store information about a reference
        /// </summary>
        private class ReferenceInfo
        {
            public JsonPath Path;
            public bool CanReference;

            public ReferenceInfo(JsonPath Path)
            {
                this.Path = Path;
            }
        }

    }
}
