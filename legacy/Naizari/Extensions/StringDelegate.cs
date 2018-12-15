/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Extensions
{
    /// <summary>
    /// Encapsulates a method that takes a string and returns a string.
    /// The returned string is intended to be the result of manipulating
    /// the specified string value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public delegate string StringDelegate(string value);
}
