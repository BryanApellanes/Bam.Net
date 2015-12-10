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
using System.Collections;
using Naizari.Javascript.JsonExSerialization.Collections;
using Naizari.Javascript.JsonExSerialization.MetaData;
using Naizari.Javascript.JsonExSerialization.TypeConversion;
using Naizari.Javascript.JsonExSerialization.Framework;
using System.Diagnostics;

namespace Naizari.Javascript.JsonExSerialization.MetaData
{
    /// <summary>
    /// Helper class for dealing with types during serialization
    /// </summary>
    public class TypeData : MetaDataBase
    {
        /// <summary>
        /// The properties for this type
        /// </summary>
        protected IList<IPropertyData> properties;

        /// <summary>
        /// The properties that also correspond to constructor parameters
        /// </summary>
        protected IList<IPropertyData> constructorArgs;

        /// <summary>
        /// Flag that indicates whether the collection handler lookup has been attempted
        /// </summary>
        private bool collectionLookedUp;

        /// <summary>
        /// The collection handler for this type
        /// </summary>
        private CollectionHandler collectionHandler;

        /// <summary>
        /// The serializer's context
        /// </summary>
        protected SerializationContext context;

        /// <summary>
        /// flag indicating whether this type has any properties that are not ignored
        /// </summary>
        private bool? empty;

        /// <summary>
        /// Initializes an instance with the specific <paramref name="type"/> and
        /// <paramref name="context" />.
        /// </summary>
        /// <param name="type">the .NET type that the metadata is for</param>
        /// <param name="context">the serializer context</param>
        public TypeData(Type type, SerializationContext context) : base(type)
        {
            this.context = context;
        }

        /// <summary>
        /// Loads the properties for the type if they haven't already been loaded
        /// </summary>
        protected virtual void LoadProperties()
        {
            if (properties == null)
            {
                this.properties = ReadDeclaredProperties();
                MergeBaseProperties(this.properties);
            }
        }

        private void LoadConstructorParameters()
        {
            if (this.constructorArgs == null)
            {
                this.constructorArgs = new List<IPropertyData>(GetConstructorParameters(properties));
                if (constructorArgs.Count > 0)
                {
                    constructorArgs = SortConstructorParameters(constructorArgs);
                }
            }
        }

        /// <summary>
        /// Reads the properties and constructor arguments from type metadata declared on this type
        /// </summary>
        /// <param name="Properties">properties collection</param>
        /// <param name="ConstructorArguments">constructor arguments</param>
        protected virtual IList<IPropertyData> ReadDeclaredProperties()
        {
            IList<IPropertyData> properties = new List<IPropertyData>();

            MemberInfo[] members = ForType.GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (MemberInfo member in members)
            {
                IPropertyData property = null;
                if (member is PropertyInfo)
                {
                    property = CreatePropertyHandler((PropertyInfo) member);
                }
                else if (member is FieldInfo)
                {
                    property = CreateFieldHandler((FieldInfo) member);
                }
                if (property != null) {
                    properties.Add(property);
                }
            }
            return properties;
        }

        /// <summary>
        /// Merges in the properties from the base class to the property list
        /// </summary>
        /// <param name="properties">property list to merge into</param>
        protected virtual void MergeBaseProperties(IList<IPropertyData> properties) {
            if (this.forType.BaseType == typeof(object) || this.forType.BaseType == null)
                return;

            TypeData baseTypeData = this.context.TypeHandlerFactory[this.forType.BaseType];
            List<IPropertyData> baseProps = new List<IPropertyData>(baseTypeData.AllProperties);
            foreach (IPropertyData baseProp in baseProps)
            {
                bool found = false;
                foreach (IPropertyData localProp in properties)
                {
                    if (localProp.Name == baseProp.Name)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    properties.Add(baseProp);
            }
        }

        /// <summary>
        /// Returns an enumerator of the Constructor Parameters in the <paramref name="properties"/> list.
        /// </summary>
        /// <param name="properties">the properties to extract constructor parameters from</param>
        /// <returns>enumerable list of constructor parameters</returns>
        private static IEnumerable<IPropertyData> GetConstructorParameters(IList<IPropertyData> properties)
        {
            foreach (IPropertyData property in properties)
                if (property.IsConstructorArgument)
                    yield return property;
        }

        private IList<IPropertyData> SortConstructorParameters(IList<IPropertyData> parameters)
        {
            IList<IPropertyData> newList = null;
            newList = SortConstructorParameters(parameters, true);
            if (newList != null)
                return newList;

            newList = SortConstructorParameters(parameters, false);
            if (newList != null)
                return newList;

            throw new Exception("Unable to find suitable public constructor matching constructor parameters for " + this.ForType);
        }

        private IList<IPropertyData> SortConstructorParameters(IList<IPropertyData> parameters, bool exactMatch)
        {
            StringComparison comparison = exactMatch ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
            foreach(ConstructorInfo constructor in ForType.GetConstructors()) {
                ParameterInfo[] ctorParms = constructor.GetParameters();
                if (ctorParms.Length != parameters.Count)
                    continue;

                IPropertyData[] result = new IPropertyData[parameters.Count];
                int matchCount = 0;
                foreach (IPropertyData property in parameters)
                {
                    if (property.Position >= 0)
                    {
                        if (exactMatch && ctorParms[property.Position].ParameterType != property.PropertyType)
                            break;

                        //if (!exactMatch && !ctorParms[property.Position].ParameterType.IsAssignableFrom(property.PropertyType))
                        //    break;

                        if (result[property.Position] == null)
                        {
                            result[property.Position] = property;
                            matchCount++;
                        }
                        else
                        {
                            // something else occupies this spot already, so there's a conflict with this constructor so try another
                            // most likely none of them will work in this situation, but we'll let the outer method determine that
                            break;
                        }
                    }
                    else
                    {
                        // named argument, search the list
                        int i;
                        for (i = 0; i < ctorParms.Length; i++)
                        {
                            string parmName = ctorParms[i].Name;
                            if (ctorParms[i].IsDefined(typeof(ConstructorParameterAttribute), false))
                                parmName = ReflectionUtils.GetAttribute<ConstructorParameterAttribute>(ctorParms[i], false).Name;

                            if (parmName.Equals(property.ConstructorParameterName, comparison)
                                && ((exactMatch && ctorParms[i].ParameterType == property.PropertyType)
                                    || (!exactMatch/* && ctorParms[i].ParameterType.IsAssignableFrom(property.PropertyType)*/)))
                                break;
                        }
                        if (i < ctorParms.Length && result[i] == null)
                        {
                            result[i] = property;
                            matchCount++;
                        }
                        else
                            break;
                    }
                }
                if (matchCount == result.Length)
                {
                    // found a match, return it
                    if (exactMatch)
                        return result;

                    if (IsLooseMatch(constructor, result))
                        return result;
                }
            }
            return null;
        }

        /// <summary>
        /// Checks that the constructor matches the constructor parameters by doing type conversion if necessary
        /// </summary>
        /// <param name="constructor">the constructor</param>
        /// <param name="properties">constructor parameter fields</param>
        /// <returns>true if this constructor matches the types in the constructor parameters</returns>
        private bool IsLooseMatch(ConstructorInfo constructor, IPropertyData[] properties)
        {
            Type[] types = new Type[properties.Length];
            for(int i = 0; i < properties.Length; i++)
                types[i] = properties[i].PropertyType;

            try
            {
                MethodBase mb = Type.DefaultBinder.SelectMethod(BindingFlags.Default, new MethodBase[] { constructor }, types, new ParameterModifier[0]);
                return mb == constructor;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Constructs a PropertyHandler instance from the PropertyInfo
        /// </summary>
        /// <param name="Property"></param>
        /// <returns></returns>
        protected virtual PropertyData CreatePropertyHandler(PropertyInfo Property)
        {
            return new PropertyData(Property);
        }

        /// <summary>
        /// Constructs a FieldHandler instance from the FieldInfo
        /// </summary>
        /// <param name="Field"></param>
        /// <returns></returns>
        protected virtual FieldData CreateFieldHandler(FieldInfo Field)
        {
            return new FieldData(Field);
        }

        /// <summary>
        /// Creates an instance of this Type with the specified arguments
        /// </summary>
        /// <param name="args">the arguments passed to the constructor if any</param>
        /// <returns>the created object</returns>
        public virtual object CreateInstance(object[] args)
        {
            return Activator.CreateInstance(this.ForType, args);
        }

        /// <summary>
        /// Get the list of constructor parameters for this type
        /// </summary>
        public virtual IList<IPropertyData> ConstructorParameters
        {
            get
            {
                LoadProperties();
                LoadConstructorParameters();
                return this.constructorArgs;
            }
        }

        /// <summary>
        /// Indicates whether there are any properties for this object that are not ignored.
        /// </summary>
        public virtual bool IsEmpty
        {
            get
            {
                if (!empty.HasValue)
                    foreach (IPropertyData prop in AllProperties)
                    {
                        if (!prop.Ignored)
                        {
                            empty = false;
                            break;
                        }
                    }
                if (!empty.HasValue)
                    empty = true;


                return empty.Value;
            }
        }

        public virtual IEnumerable<IPropertyData> Properties
        {
            get
            {
                foreach (IPropertyData prop in AllProperties)
                {
                    if (!prop.Ignored)
                        yield return prop;
                }
            }
        }
        /// <summary>
        /// Get the list of properties for this type
        /// </summary>
        public virtual IEnumerable<IPropertyData> AllProperties
        {
            get {
                LoadProperties();
                return properties; 
            }
        }

        /// <summary>
        /// Finds a property by its name.  The property must follow the same rules as
        /// those returned from the Properties list, i.e. must be readable and writable and
        /// not have an ignore attribute.
        /// </summary>
        /// <param name="Name">the name of the property</param>
        /// <returns>TypeHandlerProperty instance for the property or null if not found</returns>
        public IPropertyData FindProperty(string Name)
        {
            foreach (IPropertyData prop in AllProperties)
            {
                if (prop.Name == Name)
                    return prop;
            }
            return null;
        }

        /// <summary>
        /// Ignore a property to keep from being serialized, same as if the JsonExIgnore attribute had been set
        /// </summary>
        /// <param name="name">the name of the property</param>
        public virtual void IgnoreProperty(string name)
        {
            IPropertyData handler = FindProperty(name);
            if (handler == null)
                throw new ArgumentException("Property " + name + " does not exist on Type " + this.ForType, "name");
            handler.Ignored = true;
        }

        /// <summary>
        /// Returns true if this type is a collection type
        /// </summary>
        /// <param name="context">the serialization context</param>
        /// <returns>true if a collection</returns>
        public virtual bool IsCollection()
        {
            if (!collectionLookedUp)
            {
                if (this.ForType.IsDefined(typeof(JsonExCollectionAttribute), true))
                    collectionHandler = GetCollectionHandlerFromAttribute();
                else
                    collectionHandler = FindCollectionHandler();
                collectionLookedUp = true;
            }
            return collectionHandler != null;
        }

        private CollectionHandler FindCollectionHandler()
        {
            foreach (CollectionHandler handler in context.CollectionHandlers)
            {
                if (handler.IsCollection(ForType))
                {
                    return handler;
                }
            }
            return null;
        }

        /// <summary>
        /// Reads the JsonExCollection attribute for the class and loads the collection handler from that.  It first
        /// checks the list of collectionhandlers to see if its already been loaded.
        /// </summary>
        /// <returns>CollectionHandler specified by the JsonExCollection attribute</returns>
        protected virtual CollectionHandler GetCollectionHandlerFromAttribute() {
            JsonExCollectionAttribute attr = ((JsonExCollectionAttribute[])this.ForType.GetCustomAttributes(typeof(JsonExCollectionAttribute), true))[0];
            if (!attr.IsValid())
                throw new Exception("Invalid JsonExCollectionAttribute specified for " + this.ForType + ", either CollectionHandlerType or ItemType or both must be specified");

            Type collHandlerType = attr.GetCollectionHandlerType();
            Type itemType = attr.GetItemType();

            // Try exact type match first
            CollectionHandler handler = null;

            if (collHandlerType == null) {
                handler = FindCollectionHandler();
                handler = new CollectionHandlerWrapper(handler, this.ForType, itemType);
            }

            if (handler == null)
            {
                handler = context.CollectionHandlers.Find(delegate(CollectionHandler h) { return h.GetType() == collHandlerType; });
                if (handler != null)
                    return handler;

                // try inherited type next
                handler = context.CollectionHandlers.Find(delegate(CollectionHandler h) { return collHandlerType.IsInstanceOfType(h); });
                if (handler != null)
                    return handler;

                // create the handler
                handler = (CollectionHandler)Activator.CreateInstance(collHandlerType);
            }

            // install the handler
            context.RegisterCollectionHandler(handler);
            return handler;
        }
        /// <summary>
        /// Returns a collection handler if this object is a collection
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual CollectionHandler GetCollectionHandler()
        {
            if (IsCollection()) {
                return collectionHandler;
            } else {
                throw new InvalidOperationException("Type " + ForType + " is not recognized as a collection.  A collection handler (ICollectionHandler) may be necessary");
            }            
        }

        protected override IJsonTypeConverter CreateTypeConverter()
        {
            IJsonTypeConverter converter = CreateTypeConverter(ForType);
            if (converter == null)
                return TypeConverterAdapter.GetAdapter(ForType);
                //return null;
            else
                return converter;
        }

        public override IJsonTypeConverter TypeConverter
        {
            get
            {
                if (ForType.IsPrimitive || ForType == typeof(string))
                {
                    converterCreated = true;
                    return null;
                }
                else
                    return base.TypeConverter;
            }
            set
            {
                base.TypeConverter = value;
            }
        }
    }

}
