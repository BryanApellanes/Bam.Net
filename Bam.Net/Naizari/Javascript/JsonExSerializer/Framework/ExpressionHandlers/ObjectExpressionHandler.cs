/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Naizari.Javascript.JsonExSerialization.Framework.Expressions;
using Naizari.Javascript.JsonExSerialization.MetaData;
using System.Collections;
using Naizari.Javascript.JsonExSerialization.TypeConversion;

namespace Naizari.Javascript.JsonExSerialization.Framework.ExpressionHandlers
{
    /// <summary>
    /// ExpressionHandler for a json object or non-primitive object that is not a collection.  This is usually the
    /// default handler.
    /// </summary>
    public class ObjectExpressionHandler : ExpressionHandlerBase
    {
        /// <summary>
        /// Initializes a default instance with no Serialization Context
        /// </summary>
        public ObjectExpressionHandler()
        {
        }

        /// <summary>
        /// Initializes an instance with a Serialization Context
        /// </summary>
        public ObjectExpressionHandler(SerializationContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Creates an json object expression from object data.
        /// </summary>
        /// <param name="data">the data to serialize</param>
        /// <param name="currentPath">current path to the object</param>
        /// <param name="serializer">serializer instance used to serialize key values</param>
        /// <returns>json object expression</returns>
        public override Expression GetExpression(object data, JsonPath currentPath, ISerializerHandler serializer)
        {
             TypeData handler = Context.GetTypeHandler(data.GetType());

            ObjectExpression expression = new ObjectExpression();

            foreach (IPropertyData prop in handler.Properties)
            {
                object value = prop.GetValue(data);
                Expression valueExpr;
                if (prop.HasConverter)
                {
                    valueExpr = serializer.Serialize(value, currentPath.Append(prop.Name), prop.TypeConverter);
                }
                else
                {
                    valueExpr = serializer.Serialize(value, currentPath.Append(prop.Name));
                }
                if (value != null && !ReflectionUtils.AreEquivalentTypes(value.GetType(), prop.PropertyType))
                {
                    valueExpr = new CastExpression(value.GetType(), valueExpr);
                }
                expression.Add(prop.Name, valueExpr);
            }
            return expression;
        }

        /// <summary>
        /// Evaluates the expression and deserializes it.
        /// </summary>
        /// <param name="expression">json object expression</param>
        /// <param name="deserializer">deserializer for deserializing key values</param>
        /// <returns>deserialized object</returns>
        public override object Evaluate(Expression expression, IDeserializerHandler deserializer)
        {
            object value = ConstructObject((ObjectExpression)expression, deserializer);
            value = Evaluate(expression, value, deserializer);
            if (value is IDeserializationCallback)
                ((IDeserializationCallback)value).OnAfterDeserialization();
            return value;
        }

        /// <summary>
        /// Evaluates the expression and populates an existing object with the expression's properties
        /// </summary>
        /// <param name="expression">json object expression</param>
        /// <param name="existingObject">the existing object to populate</param>
        /// <param name="deserializer">deserializer for deserializing key values</param>
        /// <returns>deserialized object</returns>
        public override object Evaluate(Expression expression, object existingObject, IDeserializerHandler deserializer)
        {
            TypeData typeHandler = Context.GetTypeHandler(existingObject.GetType());
            ObjectExpression objectExpression = (ObjectExpression)expression;
            foreach (KeyValueExpression Item in objectExpression.Properties)
            {
                // evaluate the item and let it assign itself?
                IPropertyData hndlr = typeHandler.FindProperty(Item.Key);
                if (hndlr == null)
                {
                    throw new Exception(string.Format("Could not find property {0} for type {1}", Item.Key, typeHandler.ForType));
                }
                if (hndlr.Ignored)
                {
                    switch (Context.IgnoredPropertyAction)
                    {
                        case SerializationContext.IgnoredPropertyOption.Ignore:
                            continue;
                        case SerializationContext.IgnoredPropertyOption.SetIfPossible:
                            if (!hndlr.CanWrite)
                                continue;
                            break;
                        case SerializationContext.IgnoredPropertyOption.ThrowException:
                            throw new Exception(string.Format("Can not set property {0} for type {1} because it is ignored and IgnorePropertyAction is set to ThrowException", Item.Key, typeHandler.ForType));
                    }
                }
                Expression valueExpression = Item.ValueExpression;
                valueExpression.ResultType = hndlr.PropertyType;
                object result = null;
                TypeConverterExpressionHandler converterHandler = null;
                IJsonTypeConverter converter = null;
                if (hndlr.HasConverter)
                {
                    converterHandler = (TypeConverterExpressionHandler) Context.ExpressionHandlers.Find(typeof(TypeConverterExpressionHandler));
                    converter = hndlr.TypeConverter;
                }
                
                if (!hndlr.CanWrite)
                {
                    result = hndlr.GetValue(existingObject);
                    if (converterHandler != null)
                    {
                        converterHandler.Evaluate(valueExpression, result, deserializer, converter);

                    }
                    else
                    {
                        deserializer.Evaluate(valueExpression, result);
                    }
                }
                else
                {
                    if (hndlr.HasConverter)
                        hndlr.SetValue(existingObject, converterHandler.Evaluate(valueExpression,deserializer,converter));
                    else
                        hndlr.SetValue(existingObject, deserializer.Evaluate(valueExpression));
                }
            }
            return existingObject;
        }

        /// <summary>
        /// Constructs a new instance of the object represented by the expression.
        /// </summary>
        /// <param name="expression">json object expression</param>
        /// <param name="deserializer">deserializer for deserializing constructor arguments if any</param>
        /// <returns>constructed, but unpopulated object</returns>
        protected virtual object ConstructObject(ObjectExpression expression, IDeserializerHandler deserializer)
        {
            TypeData handler = Context.GetTypeHandler(expression.ResultType);
            // set the default type if none set
            if (expression.ConstructorArguments.Count > 0)
            {
                // old way expects parameters in the constructor list
                ResolveConstructorTypes(Context, expression);
            }
            else
            {
                foreach (IPropertyData parameter in handler.ConstructorParameters)
                {
                    int propLocation = expression.IndexOf(parameter.Name);
                    if (propLocation >= 0)
                    {
                        Expression arg = expression.Properties[propLocation].ValueExpression;
                        arg.ResultType = parameter.PropertyType;
                        expression.ConstructorArguments.Add(arg);
                        expression.Properties.RemoveAt(propLocation);
                    }
                    else
                    {
                        expression.ConstructorArguments.Add(new NullExpression());
                    }
                }
            }

            object[] args = new object[expression.ConstructorArguments.Count];

            for (int i = 0; i < args.Length; i++)
            {
                Expression carg = expression.ConstructorArguments[i];
                args[i] = deserializer.Evaluate(carg);
            }
            object result = handler.CreateInstance(args);
            expression.OnObjectConstructed(result);
            return result;
        }

        /// <summary>
        /// Resolves and updates the types of any constructor arguments
        /// </summary>
        /// <param name="context">serialization context</param>
        /// <param name="expression">object expression</param>
        protected static void ResolveConstructorTypes(SerializationContext context, ObjectExpression expression)
        {
            TypeData handler = context.GetTypeHandler(expression.ResultType);
            Type[] definedTypes = GetConstructorParameterTypes(handler.ConstructorParameters);

            CtorArgTypeResolver resolver = new CtorArgTypeResolver(expression, context, definedTypes);
            Type[] resolvedTypes = resolver.ResolveTypes();
            for (int i = 0; i < resolvedTypes.Length; i++)
            {
                if (resolvedTypes[i] != null)
                    expression.ConstructorArguments[i].ResultType = resolvedTypes[i];
            }
        }

        /// <summary>
        /// Gets the default types of any constructor parameters from the type metadata
        /// </summary>
        /// <param name="constructorParameters">constructor parameter list</param>
        /// <returns>default types</returns>
        protected static Type[] GetConstructorParameterTypes(IList<IPropertyData> constructorParameters)
        {
            Type[] types = new Type[constructorParameters.Count];
            for (int i = 0; i < constructorParameters.Count; i++)
            {
                types[i] = constructorParameters[i].PropertyType;
            }
            return types;
        }

        /// <summary>
        /// Determines whether this handler can handle a specific object type
        /// </summary>
        /// <param name="objectType">the object type</param>
        /// <returns>true if this handler handles the type</returns>
        public override bool CanHandle(Type objectType)
        {
            return true;
        }
    }
}
