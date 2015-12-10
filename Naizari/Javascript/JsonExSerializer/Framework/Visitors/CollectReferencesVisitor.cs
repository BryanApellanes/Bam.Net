/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Naizari.Javascript.JsonExSerialization.Framework.Expressions;

namespace Naizari.Javascript.JsonExSerialization.Framework.Visitors
{
    public class CollectReferencesVisitor : VisitorBase
    {
        private List<ReferenceExpression> _references = new List<ReferenceExpression>();

        /// <summary>
        /// The list of references that were collected
        /// </summary>
        public List<ReferenceExpression> References {
            get { return _references; }
        }

        public void Visit(ReferenceExpression reference) {
            References.Add(reference);
        }

        public void VisitComplex(ComplexExpressionBase ComplexExpression)
        {
            foreach (Expression expr in ComplexExpression.ConstructorArguments)
                Visit(expr);
        }

        public void Visit(ArrayExpression list)
        {
            VisitComplex(list);

            foreach (Expression item in list.Items)
                Visit(item);
        }

        public void Visit(ObjectExpression objExpr)
        {
            VisitComplex(objExpr);
            foreach (KeyValueExpression item in objExpr.Properties)
                Visit(item);
        }

        public void Visit(KeyValueExpression KeyValue)
        {
            Visit(KeyValue.ValueExpression);
        }

        public void Visit(CastExpression Cast)
        {
            Visit(Cast.Expression);
        }
    }
}
