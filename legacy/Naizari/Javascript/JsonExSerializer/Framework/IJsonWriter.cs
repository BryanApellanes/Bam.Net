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

namespace Naizari.Javascript.JsonExSerialization.Framework
{
    public interface IJsonWriter : IDisposable
    {
        /// <summary>
        /// Starts a constructed object
        /// </summary>
        /// <param name="constructorType"></param>
        /// <returns></returns>
        IJsonWriter WriteConstructorStart(Type constructorType);

        /// <summary>
        /// Starts a constructed object with the given type information
        /// </summary>
        /// <param name="namespaceAndClass">The fully-qualified class name without assembly reference</param>
        /// <returns></returns>
        IJsonWriter WriteConstructorStart(string namespaceAndClass);

        /// <summary>
        /// Starts a constructed object
        /// </summary>
        /// <param name="namespaceAndClass">The fully-qualified class name without assembly reference</param>
        /// <param name="assembly">The assembly name</param>
        /// <returns></returns>
        IJsonWriter WriteConstructorStart(string namespaceAndClass, string assembly);

        /// <summary>
        /// Starts the arguments for a constructed object
        /// </summary>
        /// <returns></returns>
        IJsonWriter WriteConstructorArgsStart();

        /// <summary>
        /// Ends the arguments for a constructed object
        /// </summary>
        /// <returns></returns>
        IJsonWriter WriteConstructorArgsEnd();
        

        /// <summary>
        /// Ends the constructed object
        /// </summary>
        /// <returns></returns>
        IJsonWriter WriteConstructorEnd();
        
 

        /// <summary>
        /// Starts an object
        /// </summary>
        /// <returns>the writer instance for stacking</returns>
        IJsonWriter WriteObjectStart();

        IJsonWriter WriteKey(string key);

        /// <summary>
        /// Ends an object definition
        /// </summary>
        /// <returns>the writer instance for stacking</returns>
        IJsonWriter WriteObjectEnd();

        /// <summary>
        /// Starts an array sequence
        /// </summary>
        /// <returns></returns>
        IJsonWriter WriteArrayStart();

        /// <summary>
        /// Ends an array
        /// </summary>
        /// <returns></returns>
        IJsonWriter WriteArrayEnd();

        /// <summary>
        /// Writes a boolean value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IJsonWriter WriteValue(bool value);

        /// <summary>
        /// Writes a long value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IJsonWriter WriteValue(long value);

        /// <summary>
        /// Writes a double value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IJsonWriter WriteValue(double value);

        /// <summary>
        /// Writes a float value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IJsonWriter WriteValue(float value);

        /// <summary>
        /// Writes a quoted value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IJsonWriter WriteQuotedValue(string value);

        /// <summary>
        /// Writes a special string value that is not
        /// quoted such as null, or some other keyword.
        /// </summary>
        /// <param name="value">the value to write</param>
        /// <returns></returns>
        IJsonWriter WriteSpecialValue(string value);

        /// <summary>
        /// Writes a comment.  The comment characters /* */ or // should be included in the comment string
        /// </summary>
        /// <param name="comment">the comment string</param>
        /// <returns></returns>
        IJsonWriter WriteComment(string comment);

        /// <summary>
        /// Writes an object cast
        /// (MyClass) ...
        /// </summary>
        /// <param name="castedType">The type for the cast</param>
        /// <returns></returns>
        IJsonWriter WriteCast(Type castType);

        /// <summary>
        /// Writes an object cast with the type name specified as a string.  The NamespaceAndClass
        /// contains the class name and possibly the Namespace but no assembly.
        /// (MyNamespace.MyClass) ...
        /// </summary>
        /// <param name="namespaceAndClass">The fully-qualified class name without assembly reference</param>
        /// <returns></returns>
        IJsonWriter WriteCast(string namespaceAndClass);

        /// <summary>
        /// Writes an object cast with the fully qualified type name and assemble reference
        /// ("MyNamespace.MyClass, MyAssembly") ...
        /// </summary>
        /// <param name="namespaceAndClass">The fully-qualified class name without assembly reference</param>
        /// <param name="assembly">The assembly name</param>
        /// <returns></returns>
        IJsonWriter WriteCast(string namespaceAndClass, string assembly);
    }
}
