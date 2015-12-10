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
using System.Globalization;

namespace Naizari.Javascript.JsonExSerialization.Framework.Expressions
{
    /// <summary>
    /// Value types such as string, bool, or number
    /// </summary>
    public class ValueExpression: Expression
    {
        private object _value;

        public ValueExpression(object value)
        {
            _value = value;
        }

        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// The value for the expression
        /// </summary>
        public virtual string StringValue
        {
            get {
                if (this._value == null)
                    return string.Empty;
                else
                    return Convert.ToString(this._value, CultureInfo.InvariantCulture);
            }
        }

        public override Type DefaultType
        {
            get { return typeof(string); }
        }
    }

    /// <summary>
    /// distinguished types for evaluator purposes
    /// </summary>
    public sealed class NumericExpression : ValueExpression
    {
        public NumericExpression(object value)
            : base(value)
        {
        }

        public bool IsFloatingPoint()
        {
            switch (Type.GetTypeCode(this.ResultType))
            {
                case TypeCode.Double:
                case TypeCode.Single:
                case TypeCode.Decimal:
                    return true;
                default:
                    return StringValue.Contains(".");
            }
        }

        public override Type DefaultType
        {
            get
            {
                if (StringValue.Contains("."))
                    return typeof(double);
                else
                    return typeof(long);
            }
        }
    }

    /// <summary>
    /// distinguished types for evaluator purposes
    /// </summary>
    public sealed class BooleanExpression : ValueExpression
    {
        public BooleanExpression(object value)
            : base(value)
        {
        }

        public override Type DefaultType
        {
            get
            {
                return typeof(bool);
            }
        }
    }
}
