/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Naizari.Javascript.JsonExSerialization.Framework.Expressions;

namespace Naizari.Javascript.JsonExSerialization.Framework.ExpressionHandlers
{
    /// <summary>
    /// An expression handler instance for handling numbers and numeric expressions.  The NumericExpresisonHandler
    /// handlers byte, short, int, long, float, double, decimal as well as any signed/unsigned equivalent types.
    /// </summary>
    public class NumericExpressionHandler : ValueExpressionHandler
    {
        /// <summary>
        /// Initializes a default instance with no Serialization Context
        /// </summary>
        public NumericExpressionHandler()
        {
        }

        /// <summary>
        /// Initializes an instance with a Serialization Context
        /// </summary>
        public NumericExpressionHandler(SerializationContext Context)
            : base(Context)
        {
        }

        /// <summary>
        /// Creates a numeric expression from the data
        /// </summary>
        /// <param name="data">the data, should be a number type</param>
        /// <param name="currentPath">current path to the value</param>
        /// <param name="serializer">serializer instance</param>
        /// <returns>a numeric expression representing the data</returns>
        public override Expression GetExpression(object data, JsonPath currentPath, ISerializerHandler serializer)
        {
            return new NumericExpression(data);
        }

        /// <summary>
        /// Determines whether this handler can handle a specific type.  
        /// </summary>
        /// <param name="objectType">the object type</param>
        /// <returns></returns>
        public override bool CanHandle(Type objectType)
        {
            switch (Type.GetTypeCode(objectType))
            {
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.Double:
                case TypeCode.Single:
                case TypeCode.UInt64:
                case TypeCode.Decimal:
                    return true;
                default:
                    return false;
            }
        }

    }
}
