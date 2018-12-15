/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Javascript.JsonExSerialization.Framework.Expressions
{
    public class CastExpression : Expression
    {
        private Expression _expression;

        public CastExpression(Type CastedType)
        {
            _resultType = CastedType;
        }

        public CastExpression(Type CastedType, Expression Expression)
            : this(CastedType)
        {
            _expression = Expression;
        }

        public override Type ResultType
        {
            get { return base.ResultType; }
            set
            {
                ; // ignore this 
            }
        }

        public override Type DefaultType
        {
            get { return typeof(object); }
        }
        public Expression Expression
        {
            get { return this._expression; }
            set { this._expression = value; }
        }

        public override Expression Parent
        {
            get { return base.Parent; }
            set
            {
                base.Parent = value;
                if (Expression != null)
                    Expression.Parent = value;
            }
        }
    }
}
