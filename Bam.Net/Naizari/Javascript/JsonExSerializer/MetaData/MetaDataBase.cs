/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Naizari.Javascript.JsonExSerialization.TypeConversion;
using System.Reflection;
using System.ComponentModel;

namespace Naizari.Javascript.JsonExSerialization.MetaData
{
    /// <summary>
    /// Base class for <see cref="TypeData" /> and <see cref="PropertyData"/>
    /// </summary>
    public abstract class MetaDataBase
    {
        /// <summary>
        /// The declaring type for this member
        /// </summary>
        protected Type forType;

        /// <summary>
        /// The converter for this MetaData if applicable
        /// </summary>
        protected IJsonTypeConverter converterInstance;

        /// <summary>
        /// Flag to indicate whether an attempt to create the converter has occurred.  If
        /// the converterInstance is null and converterCreated is true then this instance does
        /// not have a converter.
        /// </summary>
        protected bool converterCreated;

        /// <summary>
        /// Initialize an instance for a specific type
        /// </summary>
        /// <param name="forType">the containing type</param>
        protected MetaDataBase(Type forType)
        {
            this.forType = forType;
        }

        /// <summary>
        /// The declaring type for this member
        /// </summary>
        public Type ForType
        {
            get { return this.forType; }
        }

        /// <summary>
        /// Gets or sets the TypeConverter defined for this object
        /// </summary>
        public virtual IJsonTypeConverter TypeConverter
        {
            get
            {
                if (converterInstance == null && !converterCreated)
                {
                    converterInstance = CreateTypeConverter();
                    converterCreated = true;
                }
                return converterInstance;
            }
            set { converterInstance = value; }
        }

        /// <summary>
        /// Returns true if there is a TypeConverter defined for this object
        /// </summary>
        public virtual bool HasConverter
        {
            get { return TypeConverter != null; }
        }

        /// <summary>
        /// Constructs the type converter instance for this instance, if applicable
        /// </summary>
        /// <returns>constructed type converter, or null if no converter defined</returns>
        protected abstract IJsonTypeConverter CreateTypeConverter();

        /// <summary>
        /// Constructs a type converter defined by the custom attributes of this member
        /// </summary>
        /// <param name="provider">Customer attribute provider to read custom attributes from</param>
        /// <returns>constructed type converter or null if no converter defined</returns>
        protected IJsonTypeConverter CreateTypeConverter(ICustomAttributeProvider provider)
        {
            if (provider.IsDefined(typeof(JsonConvertAttribute), false)) {
                JsonConvertAttribute convAttr = (JsonConvertAttribute)provider.GetCustomAttributes(typeof(JsonConvertAttribute), false)[0];
                return CreateTypeConverter(convAttr);
            }
            return null;
        }

        /// <summary>
        /// Constructs a converter from the convert attribute
        /// </summary>
        /// <param name="attribute">the JsonConvertAttribute decorating a property or class</param>
        /// <returns>converter</returns>
        private static IJsonTypeConverter CreateTypeConverter(JsonConvertAttribute attribute)
        {
            IJsonTypeConverter converter = (IJsonTypeConverter)Activator.CreateInstance(attribute.Converter);
            if (attribute.Context != null)
            {
                converter.Context = attribute.Context;
            }            
            return converter;
        }
    }
}
