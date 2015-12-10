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

namespace Naizari.Javascript.JsonExSerialization
{
    /// <summary>
    /// This attribute is used to decorate a property that will be used as an argument to the
    /// constructor rather than written out as a normal property.
    /// </summary>
    [global::System.AttributeUsage(AttributeTargets.Property|AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
    public sealed class ConstructorParameterAttribute : Attribute
    {
        readonly int position;
        private readonly string name;

        /// <summary>
        /// Indicates that this property will be passed to the constructor.
        /// </summary>
        /// <param name="index">the 0-based position of the property within the constructor's arguments</param>
        [Obsolete("ConstructorParameters should be specified by name")]
        public ConstructorParameterAttribute(int position)
        {
            this.position = position;
        }

        /// <summary>
        /// Indicates that this property will be passed to the constructor with a constructor argument with the same name as
        /// the property
        /// </summary>
        public ConstructorParameterAttribute()
        {
            this.position = -1;
        }

        /// <summary>
        /// Indicates that this property will be passed to the constructor with a constructor argument with the 
        /// specified <param name="name" />
        /// </summary>
        /// <param name="name">The name of the constructor argument</param>
        public ConstructorParameterAttribute(string name)
        {
            this.position = -1;
            this.name = name;
        }

        /// <summary>
        /// The constructor argument index
        /// </summary>
        public int Position
        {
            get
            {
                return this.position;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }
    }

}
