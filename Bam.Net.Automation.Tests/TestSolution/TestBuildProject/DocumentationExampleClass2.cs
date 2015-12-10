/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBuildProject
{
    /// <summary>
    /// This class exists specifically to provide
    /// examples of the xml documentation that
    /// msbuild produces
    /// </summary>
    public class DocumentationExampleClass2
    {
        /// <summary>
        /// This is a string constant public field
        /// </summary>
        public const string AStringConstantField = "This is a string";

        /// <summary>
        /// This is a method that takes no arguments and returns
        /// nothing
        /// </summary>
        public void TakeNoArgumentsAndReturnVoid()
        {
        }

        /// <summary>
        /// This is a method that takes arguments and returns nothing
        /// </summary>
        /// <example>
        /// This is an example <see cref="string"/>
        /// <code>
        /// public void Monkey()
        /// {
        /// }
        /// </code>
        /// </example>
        /// <param name="aValue">A string</param>
        /// <param name="exampleClass">A Class</param>
        /// <permission cref="string">permission description</permission>
        public void TakeArgumentsAndReturnVoid(string aValue, DocumentationExampleClass exampleClass)
        {
        }

        /// <summary>
        /// This is a method that takes arguments and return a string.
        /// </summary>
        /// <param name="aValue"></param>
        /// <param name="exampleClass"></param>
        /// <returns></returns>
        /// <remarks>
        /// These are some remarks.  These remarks are smart
        /// </remarks>
        /// <seealso cref="String" />
        public string TakeArgumentsAndReturnString(string aValue, DocumentationExampleClass exampleClass)
        {
            return aValue;
        }

        /// <summary>
        /// This is a method that takes arguments and return an object.
        /// </summary>
        /// <param name="aValue"></param>
        /// <param name="exampleClass"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">InvalidOperationExcdepion thrown when this is invalid</exception>
        public DocumentationExampleClass TakeArgumentsAndReturnClassInstance(string aValue, DocumentationExampleClass exampleClass)
        {
            return exampleClass;
        }

        /// <summary>
        /// This is a string property
        /// </summary>
        public string StringProperty { get; set; }

        /// <summary>
        /// This is an int property
        /// </summary>
        public int IntProperty { get; set; }

        /// <summary>
        /// This is an ObjectProperty
        /// </summary>
        public DocumentationExampleClass ObjectProperty { get; set; }

        /// <summary>
        /// This is the summary
        /// </summary>
        /// <typeparam name="T">type param</typeparam>
        public void MethodWithTypeParam<T>()
        {

        }
    }
}
