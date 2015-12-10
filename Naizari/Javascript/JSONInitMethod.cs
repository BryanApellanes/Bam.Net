/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Javascript
{
    // TODO: Confirm that this attribute isn't used anywhere in EDW and delete this file.
    /// <summary>
    /// Used to designate that a method is used to initialize a 
    /// client side instance of the object that it is defined on.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    [Obsolete("This attribute should not be used.  Ensure that the object has a parameterless ctor.")]
    public class JSONInitMethod : Attribute
    {

    }
}
