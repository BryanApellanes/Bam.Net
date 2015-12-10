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
    /// <summary>
    /// An equality comparer for a Dictionary instance that compares equality using ReferenceEquals rather
    /// than Equals.  This is to ensure that 2 objects are actually the same and not just equal for reference
    /// checking purposes.
    /// </summary>
    /// <typeparam name="T">the type of object to check</typeparam>
    public class ReferenceEqualityComparer<T> : EqualityComparer<T>
    {
        public override bool Equals(T x, T y)
        {
            return object.ReferenceEquals(x, y);
        }

        public override int GetHashCode(T obj)
        {
            return System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(obj);
        }
    }
}
