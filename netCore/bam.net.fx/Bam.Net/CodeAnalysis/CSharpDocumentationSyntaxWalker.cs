using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Bam.Net.Documentation.CodeAnalysis
{
    public class CSharpDocumentationSyntaxWalker : CSharpSyntaxWalker
    {
        static HashSet<SyntaxKind> _commentTypes = new HashSet<SyntaxKind>(new[] {
                                                                        SyntaxKind.SingleLineCommentTrivia,
                                                                        SyntaxKind.MultiLineCommentTrivia,
                                                                        SyntaxKind.DocumentationCommentExteriorTrivia,
                                                                        SyntaxKind.SingleLineDocumentationCommentTrivia,
                                                                        SyntaxKind.MultiLineDocumentationCommentTrivia
                                                                      });

        Dictionary<Type, Action<CSharpSyntaxNode>> _syntaxHandlers = new Dictionary<Type, Action<CSharpSyntaxNode>>();
        public CSharpDocumentationSyntaxWalker(string sourceCodeFilePath = null)
          : base(SyntaxWalkerDepth.StructuredTrivia)
        {
        }

        public Action<NamespaceDeclarationSyntax> OnNamespaceLocated
        {
            get
            {
                return _syntaxHandlers.ContainsKey(typeof(NamespaceDeclarationSyntax)) ? _syntaxHandlers[typeof(NamespaceDeclarationSyntax)] : (s) => { };
            }
            set
            {
                _syntaxHandlers[typeof(NamespaceDeclarationSyntax)] = (Action<CSharpSyntaxNode>)value;
            }
        }

        public Action<MethodDeclarationSyntax> OnMethodLocated
        {
            get
            {
                return _syntaxHandlers.ContainsKey(typeof(MethodDeclarationSyntax)) ? _syntaxHandlers[typeof(MethodDeclarationSyntax)] : (s) => { };
            }
            set
            {
                _syntaxHandlers[typeof(MethodDeclarationSyntax)] = (Action<CSharpSyntaxNode>)value;
            }
        }

        public Action<PropertyDeclarationSyntax> OnPropertyLocated
        {
            get
            {
                return _syntaxHandlers.ContainsKey(typeof(PropertyDeclarationSyntax)) ? _syntaxHandlers[typeof(PropertyDeclarationSyntax)] : (s) => { };
            }
            set
            {
                _syntaxHandlers[typeof(PropertyDeclarationSyntax)] = (Action<CSharpSyntaxNode>)value;
            }
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax syntax)
        {
            OnMethodLocated(syntax);
            base.VisitMethodDeclaration(syntax);
        }

        public override void VisitTrivia(SyntaxTrivia trivia)
        {
            Console.WriteLine($"{(SyntaxKind)trivia.RawKind}");
            if (_commentTypes.Contains((SyntaxKind)trivia.RawKind))
            {
                string triviaContent = Write(trivia);

                // Note: When looking for the containingMethodOrPropertyIfAny, we want MemberDeclarationSyntax
                // types such as ConstructorDeclarationSyntax, MethodDeclarationSyntax, IndexerDeclarationSyntax,
                // PropertyDeclarationSyntax but NamespaceDeclarationSyntax and TypeDeclarationSyntax also
                // inherit from MemberDeclarationSyntax and we don't want those
                var containingNode = trivia.Token.Parent;
                var containingMethodOrPropertyIfAny = TryGetContainingNode<MemberDeclarationSyntax>(containingNode);//, n => !(n is NamespaceDeclarationSyntax) && !(n is TypeDeclarationSyntax));
                var containingTypeIfAny = TryGetContainingNode<TypeDeclarationSyntax>(containingNode);
                var containingNameSpaceIfAny = TryGetContainingNode<NamespaceDeclarationSyntax>(containingNode);
                Console.WriteLine(triviaContent);
            }
            base.VisitTrivia(trivia);
        }

        private string Write(object o)
        {
            using(StringWriter writer = new StringWriter())
            {
                o.Invoke("WriteTo", writer);
                return writer.ToString();
            }
        }

        private T TryGetContainingNode<T>(SyntaxNode node, Predicate<T> optionalFilter = null)
          where T : SyntaxNode
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            SyntaxNode currentNode = node;
            while (true)
            {
                if (currentNode is T nodeOfType)
                {
                    if ((optionalFilter == null) || optionalFilter(nodeOfType))
                    {
                        return nodeOfType;
                    }
                }
                if (currentNode.Parent == null)
                {
                    break;
                }
                currentNode = currentNode.Parent;
            }
            return null;
        }
    }
}
