/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Naizari.Javascript.JsonExSerialization.Framework.Expressions;

namespace Naizari.Javascript.JsonExSerialization.Framework.ExpressionHandlers
{
    /// <summary>
    /// ExpressionHandler that is capable of handling an IDictionary or IDictionary{K,V} instance.
    /// </summary>
    public class DictionaryObjectExpressionHandler : ObjectExpressionHandler
    {

        /// <summary>
        /// Initializes a default instance with no Serialization Context
        /// </summary>
        public DictionaryObjectExpressionHandler()
        {
        }

        /// <summary>
        /// Initializes an instance with a Serialization Context
        /// </summary>
        /// <param name="Context">the Serialization Context</param>
        public DictionaryObjectExpressionHandler(SerializationContext Context)
            : base(Context)
        {
        }

        /// <summary>
        /// Serialize an object implementing IDictionary.  The serialized data is similar to a regular
        /// object, except that the keys of the dictionary are used instead of properties.
        /// </summary>
        /// <param name="data">the dictionary object</param>
        /// <param name="currentPath">object's path</param>
        /// <param name="serializer">the serializer instance, used to serialize keys and values</param>
        public override Expression GetExpression(object data, JsonPath currentPath, ISerializerHandler serializer)
        {
            IDictionary dictionary = (IDictionary)data;
            Type itemType = typeof(object);
            Type genericDictionary = null;

            if ((genericDictionary = dictionary.GetType().GetInterface(typeof(IDictionary<,>).Name)) != null)
            {
                itemType = genericDictionary.GetGenericArguments()[1];
            }

            ObjectExpression expression = new ObjectExpression();
            foreach (DictionaryEntry pair in dictionary)
            {
                //may not work in all cases
                object value = pair.Value;
                Expression valueExpr = serializer.Serialize(value, currentPath.Append(pair.Key.ToString()));
                if (value != null && !ReflectionUtils.AreEquivalentTypes(value.GetType(), itemType))
                {
                    valueExpr = new CastExpression(value.GetType(), valueExpr);
                }
                expression.Add(pair.Key.ToString(), valueExpr);
            }
            return expression;
        }

        /// <summary>
        /// Checks to see if this handler can handle the object type.  This handler can only handle IDictionary 
        /// and IDictionary{K,V} instances.
        /// </summary>
        /// <param name="objectType">the object type to check</param>
        /// <returns>true if this handler handles the type</returns>
        public override bool CanHandle(Type objectType)
        {
            return typeof(IDictionary).IsAssignableFrom(objectType);
        }

        /// <summary>
        /// Evaluates the expression and populates an existing object with keys and values.
        /// </summary>
        /// <param name="expression">the expression to evaluate</param>
        /// <param name="existingObject">the existing object to populate</param>
        /// <param name="deserializer">the deserializer instance to use to deserialize other expressions</param>
        /// <returns>a populated object</returns>
        public override object Evaluate(Expression expression, object existingObject, IDeserializerHandler deserializer)
        {
            Type _dictionaryKeyType = typeof(string);
            Type _dictionaryValueType = null;
            Type genDict = existingObject.GetType().GetInterface(typeof(IDictionary<,>).Name);
            // attempt to figure out what the types of the values are, if no type is set already
            if (genDict != null)
            {
                Type[] genArgs = genDict.GetGenericArguments();
                _dictionaryKeyType = genArgs[0];
                _dictionaryValueType = genArgs[1];
            }

            ObjectExpression objectExpression = (ObjectExpression)expression;
            foreach (KeyValueExpression keyValue in objectExpression.Properties)
            {
                // if no type set, set one
                keyValue.KeyExpression.ResultType = _dictionaryKeyType;
                if (_dictionaryValueType != null)
                    keyValue.ValueExpression.ResultType = _dictionaryValueType;

                object keyObject = deserializer.Evaluate(keyValue.KeyExpression);
                object result = deserializer.Evaluate(keyValue.ValueExpression);
                ((IDictionary)existingObject)[keyObject] = result;
            }
            return existingObject;
        }
    }
}
