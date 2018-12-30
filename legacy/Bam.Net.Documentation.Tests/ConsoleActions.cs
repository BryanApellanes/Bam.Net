/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Bam.Net.Documentation.CodeAnalysis;

namespace Bam.Net.Documentation.Tests
{
    [Serializable]
	public class ConsoleActions : CommandLineTestInterface
	{
		// See the below for examples of ConsoleActions and UnitTests

		#region ConsoleAction examples
        [ConsoleAction]
        public void CommentSyntaxTest()
        {
            string comment = @"        /// <summary>
        /// A method that takes a string and returns a string.
        /// </summary>
        /// <param name=""aValue"">Any string value</param>
        /// <returns>The value passed in.</returns>";
            SyntaxTree tree = CSharpSyntaxTree.ParseText(comment);
            CSharpDocumentationSyntaxWalker walker = new CSharpDocumentationSyntaxWalker();
            walker.Visit(tree.GetRoot());
        }
		[ConsoleAction]
		public void CodeAnalysisExperiment()
		{
            SyntaxTree tree = CSharpSyntaxTree.ParseText(@"using System;
using System.Collections;
using System.Linq;
using System.Text;
 

/// This is stuff for the namespace
///
///
namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(""Hello, World!"");
        }

        /// <summary>
        /// A method that takes a string and returns a string.
        /// </summary>
        /// <param name=""aValue"">Any string value</param>
        /// <returns>The value passed in.</returns>
        public void MyDocumentedMethod(string aValue)
        {

        }

        public void MyUndocumentedMethod(string s, string d)
        {
        }
    }
}");
            CSharpDocumentationSyntaxWalker walker = new CSharpDocumentationSyntaxWalker();
            walker.Visit(tree.GetRoot());

        }

		#endregion
	}
}