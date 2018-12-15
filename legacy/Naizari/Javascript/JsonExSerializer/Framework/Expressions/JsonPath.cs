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
using System.Globalization;

namespace Naizari.Javascript.JsonExSerialization.Framework.Expressions
{    
    /// <summary>    
    /// a class for working with a reference identifier
    /// e.g. this.Customer.Address[1].Name;
    /// </summary>
    public sealed class JsonPath 
    {
        private string[] parts;
        private int head;

        /// <summary>
        /// Identifier for the Root object
        /// </summary>
        public const string Root = "$";

        public JsonPath()
        {
            parts = new string[] { Root };
        }

        public JsonPath(string partsString)
        {
            this.parts = partsString.Split(new string[] { "['", "']", "[", "]" },StringSplitOptions.RemoveEmptyEntries);
        }

        private JsonPath(string[] parts, string part)
            : this()
        {
            this.parts = new string[parts.Length + 1];
            Array.Copy(parts, this.parts, parts.Length);
            this.parts[parts.Length] = part;
        }

        private JsonPath(string[] parts, int head)
        {
            this.parts = parts;
            this.head = head;
        }
        /// <summary>
        /// Adds a part to the reference.  A part
        /// is one value between the period separators of a reference.
        /// </summary>
        /// <param name="part">the part to add</param>
        public JsonPath Append(string part)
        {
            return new JsonPath(this.parts, part);
        }

        public JsonPath Append(int part)
        {
            return Append(part.ToString());
        }

        /// <summary>
        /// The current piece of the reference
        /// </summary>
        public string Top {
            get
            {
                return parts[head];
            }
        }

        /// <summary>
        /// The current piece as an integer, for collections
        /// </summary>
        public int TopAsInt {
            get
            {
                return int.Parse(Top, CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// The child path
        /// </summary>
        /// <returns>the child path</returns> 
        public JsonPath ChildReference()
        {
            if (IsEmpty)
                throw new InvalidOperationException("The Path is empty");
            return new JsonPath(this.parts, this.head + 1);
        }

        public bool StartsWith(JsonPath value)
        {
            return this.ToString().StartsWith(value.ToString());
        }

        /// <summary>
        /// Returns true if the path is empty
        /// </summary>
        public bool IsEmpty {
            get { return parts.Length == head; }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder(Top);
            for (int i = head+1; i < parts.Length;i++)
            {
                int dummy;
                if (int.TryParse(parts[i], out dummy))
                    result.AppendFormat("[{0}]", parts[i]);
                else
                    result.AppendFormat("['{0}']", parts[i]);
            }
            return result.ToString();
        }
    }
}
