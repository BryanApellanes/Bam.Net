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
using System.Reflection;
using Naizari.Javascript.JsonExSerialization.TypeConversion;

namespace Naizari.Javascript.JsonExSerialization.MetaData
{
    /// <summary>
    /// Implementation of IPropertyData that uses a <see cref="System.Reflection.PropertyInfo"/> instance
    /// as the underlying property type.
    /// </summary>
    public class PropertyData : MemberInfoPropertyDataBase
    {
        /// <summary>
        /// Initializes an instance of PropertyData with the specified PropertyInfo object that is
        /// not a constructor argument.
        /// </summary>
        /// <param name="property">the backing property object</param>
        public PropertyData(PropertyInfo property) : base(property)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes an instance of PropertyData with the specified PropertyInfo object that is also
        /// a Constructor Parameter at the specified <paramref name="position"/>.
        /// </summary>
        /// <param name="property">the backing property object</param>
        /// <param name="position">the property's 0-based index in the constructor arguments</param>
        public PropertyData(PropertyInfo property, int position)
            : base(property, position)
        {
            Initialize();
            this.position = position;
        }

        /// <summary>
        /// Initializes the object
        /// </summary>
        private void Initialize()
        {
            Initialize(Property);
            if (Property.IsDefined(typeof(JsonExIgnoreAttribute), false)
                || !(Property.GetGetMethod().GetParameters().Length == 0 && Property.CanRead)
                || (!Property.CanWrite))
            {
                this.Ignored = true;
            }
            if (Property.IsDefined(typeof(JsonExPropertyAttribute), false) || IsConstructorArgument)
                this.Ignored = false;
        }

        /// <summary>
        /// The backing property object
        /// </summary>
        protected PropertyInfo Property
        {
            get { return (PropertyInfo)member; }
        }

        /// <summary>
        /// The type for the property
        /// </summary>
        public override Type PropertyType
        {
            get { return Property.PropertyType; }
        }

        /// <summary>
        /// Get the value of the property from the given object
        /// </summary>
        /// <param name="instance">the object to retrieve this property value from</param>
        /// <returns>property value</returns>
        public override object GetValue(object instance)
        {
            return Property.GetValue(instance, null);
        }

        /// <summary>
        /// Sets the value of the property for the object
        /// </summary>
        /// <param name="instance">the object instance to set the property value on</param>
        /// <param name="value">the new value to set</param>
        public override void SetValue(object instance, object value)
        {
            Property.SetValue(instance, value, null);
        }

        /// <summary>
        /// Gets a value indicating whether this property can be written to
        /// </summary>
        public override bool CanWrite
        {
            get { return Property.CanWrite; }
        }
    }
}
