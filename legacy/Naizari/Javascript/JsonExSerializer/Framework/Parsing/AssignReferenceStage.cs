/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Naizari.Javascript.JsonExSerialization.Framework.Expressions;
using Naizari.Javascript.JsonExSerialization.Framework.Visitors;

namespace Naizari.Javascript.JsonExSerialization.Framework.Parsing
{
    /// <summary>
    /// Resolves references to other expressions
    /// </summary>
    public class AssignReferenceStage : IParsingStage
    {

        public AssignReferenceStage(SerializationContext context)
        {
        }

        public Expression Execute(Expression root)
        {
            IList<ReferenceExpression> references = CollectReferences(root);
            foreach (ReferenceExpression reference in references)
                ResolveReference(reference, root);
            return root;
        }

        private static List<ReferenceExpression> CollectReferences(Expression root)
        {
            CollectReferencesVisitor visitor = new CollectReferencesVisitor();
            root.Accept(visitor);
            return visitor.References;
        }

        private static void ResolveReference(ReferenceExpression reference, Expression root)
        {
            ReferenceVisitor visitor = new ReferenceVisitor(reference.Path);
            visitor.Visit(root);
            if (visitor.ReferencedExpression == null)
                throw new ParseException("Unable to resolve reference to " + reference.Path);
            reference.ReferencedExpression = visitor.ReferencedExpression;
        }

    }
}
