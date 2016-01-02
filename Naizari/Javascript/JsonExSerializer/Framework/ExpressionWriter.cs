/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Naizari.Javascript.JsonExSerialization.Framework.Expressions;
using System.Globalization;

namespace Naizari.Javascript.JsonExSerialization.Framework
{
    /// <summary>
    /// Writes expressions to the JsonWriter
    /// </summary>
    public class ExpressionWriter
    {
        private Dictionary<Type, WriteMethod> _actions;
        private delegate void WriteMethod(Expression Expression);
        private IJsonWriter _jsonWriter;
        private SerializationContext _context;

        public ExpressionWriter(IJsonWriter jsonWriter, SerializationContext context)
        {
            _jsonWriter = jsonWriter;
            _context = context;
            InitActions();
        }

        private void InitActions()
        {
            _actions = new Dictionary<Type, WriteMethod>();
            _actions[typeof(BooleanExpression)] = WriteBoolean;
            _actions[typeof(NumericExpression)] = WriteNumeric;
            _actions[typeof(ValueExpression)] = WriteValue;
            _actions[typeof(NullExpression)] = WriteNull;
            _actions[typeof(ArrayExpression)] = WriteList;
            _actions[typeof(ObjectExpression)] = WriteObject;
            _actions[typeof(ReferenceExpression)] = WriteReference;
            _actions[typeof(CastExpression)] = WriteCast;
        }

        public static void Write(IJsonWriter writer, SerializationContext context, Expression expression)
        {
            new ExpressionWriter(writer, context).Write(expression);
        }

        public void Write(Expression expression)
        {
            _actions[expression.GetType()](expression);
        }

        private void WriteBoolean(Expression expression)
        {
            _jsonWriter.WriteValue((bool)((BooleanExpression)expression).Value);
        }

        private void WriteNumeric(Expression expression)
        {
            NumericExpression numeric = (NumericExpression)expression;
            object value = numeric.Value;
            switch (Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.Double:
                    _jsonWriter.WriteValue((double)value);
                    break;
                case TypeCode.Single:
                    _jsonWriter.WriteValue((float)value);
                    break;
                case TypeCode.Int64:
                    _jsonWriter.WriteValue((long)value);
                    break;
                case TypeCode.Decimal:
                case TypeCode.UInt64:
                    _jsonWriter.WriteSpecialValue(string.Format("{0}", value));
                    break;
                default:
                    _jsonWriter.WriteValue((long)Convert.ChangeType(value, typeof(long), CultureInfo.InvariantCulture));
                    break;
            }
        }

        private void WriteValue(Expression expression)
        {
            ValueExpression value = (ValueExpression)expression;
            _jsonWriter.WriteQuotedValue(value.StringValue);
        }

        private void WriteNull(Expression expression)
        {
            if (!(expression is NullExpression))
                throw new ArgumentException("Expression should be a NullExpression");
            _jsonWriter.WriteSpecialValue("null");
        }

        private void WriteList(Expression Expression)
        {
            ArrayExpression list = (ArrayExpression)Expression;
            _jsonWriter.WriteArrayStart();
            foreach (Expression item in list.Items)
                Write(item);
            _jsonWriter.WriteArrayEnd();
        }

        private void WriteObject(Expression Expression)
        {
            ObjectExpression obj = (ObjectExpression)Expression;
            bool hasConstructor = false;
            if (obj.ConstructorArguments.Count > 0)
            {
                hasConstructor = true;
                _jsonWriter.WriteConstructorStart(obj.ResultType);
                _jsonWriter.WriteConstructorArgsStart();
                foreach (Expression ctorArg in obj.ConstructorArguments)
                {
                    Write(ctorArg);
                }
                _jsonWriter.WriteConstructorArgsEnd();
            }
            if (!hasConstructor || obj.Properties.Count > 0)
            {
                _jsonWriter.WriteObjectStart();
                foreach (KeyValueExpression keyValue in obj.Properties)
                {
                    _jsonWriter.WriteKey(keyValue.Key);
                    Write(keyValue.ValueExpression);
                }
                _jsonWriter.WriteObjectEnd();
            }
            if (hasConstructor)
                _jsonWriter.WriteConstructorEnd();
        }

        private void WriteReference(Expression Expression)
        {
            ReferenceExpression refExpr = (ReferenceExpression)Expression;
            _jsonWriter.WriteSpecialValue(refExpr.Path.ToString());
        }

        private void WriteCast(Expression Expression)
        {
            CastExpression cast = (CastExpression)Expression;
            if (_context.OutputTypeInformation && !(cast.Expression is ReferenceExpression))
                _jsonWriter.WriteCast(cast.ResultType);
            Write(cast.Expression);
        }
    }
}
