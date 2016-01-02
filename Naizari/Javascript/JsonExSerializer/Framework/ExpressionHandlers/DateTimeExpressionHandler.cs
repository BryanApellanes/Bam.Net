/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Naizari.Javascript.JsonExSerialization.Framework.Expressions;
using System.Globalization;

namespace Naizari.Javascript.JsonExSerialization.Framework.ExpressionHandlers
{
    public class DateTimeExpressionHandler : ValueExpressionHandler
    {
        private string dateFormat = "O";
        private DateTimeStyles dateTimeStyle = DateTimeStyles.RoundtripKind;
        private CultureInfo culture = CultureInfo.InvariantCulture;

        public DateTimeExpressionHandler()
        {
        }

        public DateTimeExpressionHandler(SerializationContext context)
            : base(context)
        {
        }

        public DateTimeExpressionHandler(SerializationContext context, string dateFormat)
            : base(context)
        {
            this.dateFormat = dateFormat;
        }

        public DateTimeExpressionHandler(string dateFormat)
        {
            this.dateFormat = dateFormat;
        }

        /// <summary>
        /// Sets the date time format to use when deserializing dates
        /// </summary>
        public string DateFormat
        {
            get { return this.dateFormat; }
            set { this.dateFormat = value; }
        }

        /// <summary>
        /// Date time styles to use when serializing and deserializing dates
        /// </summary>
        public DateTimeStyles DateTimeStyle
        {
            get { return this.dateTimeStyle; }
            set { this.dateTimeStyle = value; }
        }

        /// <summary>
        /// The culture to use for formatting when serializing and deserializing dates
        /// </summary>
        public CultureInfo Culture
        {
            get { return this.culture; }
            set { this.culture = value; }
        }

        public override bool CanHandle(Type ObjectType)
        {
            return typeof(DateTime).IsAssignableFrom(ObjectType);
        }

        public override Expression GetExpression(object data, JsonPath currentPath, ISerializerHandler serializer)
        {
            string value = ((DateTime)data).ToString(dateFormat, Culture);
            return new ValueExpression(value);
        }

        public override object Evaluate(Expression expression, IDeserializerHandler deserializer)
        {
            ValueExpression valueExpr = (ValueExpression)expression;
            return DateTime.ParseExact(valueExpr.StringValue, dateFormat, Culture, dateTimeStyle);
        }
    }
}
