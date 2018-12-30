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
    /// <see cref="String" />
    /// </summary>
    public class DocumentationExampleClass
    {
        /// <summary>
        /// This is a string constant public field
        /// <see cref="string"/>
        /// </summary>
        public const string AStringConstantField = "This is a string";

        /// <summary>
        /// This is a method that takes no arguments and returns
        /// nothing
        /// <seealso cref="string" />
        /// </summary>
        public void TakeNoArgumentsAndReturnVoid()
        {
        }

        /// <summary>
        /// This is a method that takes arguments and returns nothing
        /// </summary>
        /// <param name="aValue">A string</param>
        /// <param name="exampleClass">A Class</param>
        public void TakeArgumentsAndReturnVoid(string aValue, DocumentationExampleClass exampleClass)
        {
        }

        /// <summary>
        /// This is a method that takes arguments and return a string.
        /// </summary>
        /// <param name="aValue"></param>
        /// <param name="exampleClass"></param>
        /// <returns></returns>
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
    }
}
