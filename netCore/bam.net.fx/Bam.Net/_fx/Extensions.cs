using Bam.Net;
using Bam.Net.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net
{
    public static partial class Extensions
    {
        public static T BinaryClone<T>(this T from) where T : class, new()
        {
            byte[] bytes = from.ToBinaryBytes();
            return bytes.FromBinaryBytes<T>();
        }

        /// <summary>
        /// Copy the specified instance to a dynamic instance where the new
        /// instance only has the properties addorned with the specified 
        /// custom attribute T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static dynamic CopyAsDynamic<T>(this object instance) where T : Attribute
        {
            return instance.CopyAsDynamic(p => p.HasCustomAttributeOfType<T>());
        }

        public static dynamic CopyAsDynamic(this object instance, Func<PropertyInfo, bool> propertySelector)
        {
            return instance.ToDynamicType(instance.GetType().Name, propertySelector).Construct();
        }

        /// <summary>
        /// Creates an in memory clone of the specified objectToClone.  The 
        /// clone will only have the properties of objectToClone that are
        /// addorned with the specified PropertyAttributeFilter generic type.
        /// </summary>
        /// <typeparam name="PropertyAttributeFilter">The attribute to look for when copying properties</typeparam>
        /// <param name="objectToClone">The object to clone</param>
        /// <returns>An in memory type that is not persisted to disk.</returns>
        public static Type BuildDynamicType<PropertyAttributeFilter>(this object objectToClone) where PropertyAttributeFilter : Attribute, new()
        {
            AssemblyBuilder ignore;
            return BuildDynamicType<PropertyAttributeFilter>(objectToClone, out ignore);
        }

        /// <summary>
        /// Creates an in memory clone of the specified objectToClone.  The 
        /// clone will only have the properties of objectToClone that are
        /// addorned with the specified PropertyAttributeFilter generic type.
        /// </summary>
        /// <typeparam name="PropertyAttributeFilter">The attribute to look for when copying properties</typeparam>
        /// <param name="typeToClone"></param>
        /// <returns></returns>
        public static Type BuildDynamicType<PropertyAttributeFilter>(this Type typeToClone) where PropertyAttributeFilter : Attribute, new()
        {
            return BuildDynamicType<PropertyAttributeFilter>(typeToClone, out AssemblyBuilder ignore, false);
        }

        /// <summary>
        /// Creates an in memory clone of the specified objectToClone.  The 
        /// clone will only have the properties of objectToClone that are
        /// addorned with the specified PropertyAttributeFilter generic type.
        /// </summary>
        /// <typeparam name="PropertyAttributeFilter">The attribute to look for when copying properties</typeparam>
        /// <param name="objectToClone">The object to clone</param>
        /// <param name="concreteAttribute">If true the attributes must be of the specified type and not extenders of the type.</param>
        /// <returns>An in memory type that is not persisted to disk.</returns>
        public static Type BuildDynamicType<PropertyAttributeFilter>(this object objectToClone, bool concreteAttribute) where PropertyAttributeFilter : Attribute, new()
        {
            return BuildDynamicType<PropertyAttributeFilter>(objectToClone, out AssemblyBuilder ignore, concreteAttribute);
        }

        /// <summary>
        /// Creates an in memory clone of the specified objectToClone.  The 
        /// clone will only have the properties of objectToClone that are
        /// addorned with the specified PropertyAttributeFilter generic type.
        /// </summary>
        /// <typeparam name="PropertyAttributeFilter">The attribute to look for when copying properties</typeparam>
        /// <param name="objectToClone">The object to clone</param>
        /// <returns>An in memory type that is not persisted to disk.</returns>
        public static Type BuildDynamicType<PropertyAttributeFilter>(this object objectToClone, out AssemblyBuilder assemblyBuilder) where PropertyAttributeFilter : Attribute, new()
        {
            return BuildDynamicType<PropertyAttributeFilter>(objectToClone, out assemblyBuilder, false);
        }

        /// <summary>
        /// Creates an in memory clone of the specified objectToClone.  The 
        /// clone will only have the properties of objectToClone that are
        /// addorned with the specified PropertyAttributeFilter generic type.
        /// </summary>
        /// <typeparam name="PropertyAttributeFilter">The attribute to look for when copying properties</typeparam>
        /// <param name="objectToClone">The object to clone</param>
        /// <param name="concreteAttribute">If true the attributes must be of the specified type and not extenders of the type.</param>
        /// <returns>An in memory type that is not persisted to disk.</returns>
        public static Type BuildDynamicType<PropertyAttributeFilter>(this object objectToClone, out AssemblyBuilder assemblyBuilder, bool concreteAttribute) where PropertyAttributeFilter : Attribute, new()
        {
            Type objType = objectToClone.GetType();
            return BuildDynamicType<PropertyAttributeFilter>(objType, out assemblyBuilder, concreteAttribute);
        }

        public static Type BuildDynamicType<PropertyAttributeFilter>(this Type objType, out AssemblyBuilder assemblyBuilder, bool concreteAttribute) where PropertyAttributeFilter : Attribute, new()
        {
            lock (_buildDynamicTypeLock)
            {
                string typeName = objType.Namespace + "." + objType.Name;
                if (DynamicTypeStore.Current.ContainsTypeInfo(typeName) && DynamicTypeStore.Current[typeName].DynamicType != null)
                {
                    return GetExistingDynamicType(typeName, out assemblyBuilder);
                }
                else
                {
                    TypeBuilder typeBuilder;
                    GetAssemblyAndTypeBuilder(typeName, out assemblyBuilder, out typeBuilder);

                    foreach (PropertyInfo property in objType.GetProperties())
                    {
                        PropertyAttributeFilter attr;
                        if (CustomAttributeExtension.HasCustomAttributeOfType<PropertyAttributeFilter>(property, true, out attr, concreteAttribute))
                        {
                            AddPropertyToDynamicType(typeBuilder, property);
                        }
                    }
                    return CreateDynamicType(typeName, typeBuilder);
                }
            }
        }

        /// <summary>
        /// Converts a DataRow to a dynamic instance where the proeprty names
        /// are the names of the columns in the row
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static dynamic ToDynamic(this DataRow row)
        {
            string typeName = 8.RandomLetters();
            if (row.Table != null)
            {
                typeName = row.Table.TableName.Or(typeName);
            }

            return ToDynamic(row, typeName);
        }

        /// <summary>
        /// Converts a DataRow to a dynamic object instance.  
        /// </summary>
        /// <param name="row">The row to convert</param>
        /// <param name="typeName">The name of the type to use in reflection operations</param>
        /// <returns></returns>
        public static dynamic ToDynamic(this DataRow row, string typeName)
        {
            AssemblyBuilder ignore;
            Type dynamicType = ToDynamicType(row, typeName, out ignore);
            ConstructorInfo ctor = dynamicType.GetConstructor(new Type[] { });
            object instance = ctor.Invoke(null);
            instance.CopyValues(row);
            return instance;
        }

        public static dynamic ToDynamic(this object instance, Func<PropertyInfo, bool> propertyPredicate, out Type dynamicType)
        {
            Type instanceType = instance.GetType();
            string newTypeName = "ValuesOf.{0}.{1}"._Format(instanceType.Namespace, instanceType.Name);
            dynamicType = instance.ToDynamicType(newTypeName, propertyPredicate, out AssemblyBuilder ignore);
            ConstructorInfo ctor = dynamicType.GetConstructor(new Type[] { });
            object filteredProperties = ctor.Invoke(null);
            DefaultConfiguration.CopyProperties(instance, filteredProperties);
            return filteredProperties;
        }

        /// <summary>
        /// Creates a dynamic object from the specified instance populating only
        /// the properties that are of value types
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static dynamic ValuePropertiesToDynamic(this object instance)
        {
            return ValuePropertiesToDynamic(instance, out Type ignore);
        }

        /// <summary>
        /// Creates a dynamic object from the specified instance populating only
        /// the properties that are of value types
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static dynamic ValuePropertiesToDynamic(this object instance, out Type dynamicType)
        {
            Type instanceType = instance.GetType();
            string newTypeName = "ValuesOf.{0}.{1}"._Format(instanceType.Namespace, instanceType.Name);
            dynamicType = ValuePropertiesToDynamicType(instance, newTypeName, out AssemblyBuilder ignore);
            ConstructorInfo ctor = dynamicType.GetConstructor(new Type[] { });
            object valuesOnlyInstance = ctor.Invoke(null);
            DefaultConfiguration.CopyProperties(instance, valuesOnlyInstance);
            return valuesOnlyInstance;
        }

        public static IEnumerable<T> DataClone<T>(this IEnumerable<T> values) where T : new()
        {
            return values.Select(t => t.DataClone());
        }

        /// <summary>
        /// Clone the specified instance to a dynamic object instance
        /// copying only properties
        /// that are represented in the Bam.Net.Data.Schema.DataTypes enum
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static object ToDynamicData(this object instance)
        {
            return ToDynamicData(instance, instance.GetType().Name);
        }

        /// <summary>
        /// Clone the specified instance copying only properties
        /// that are represented in the Bam.Net.Data.Schema.DataTypes enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static T DataClone<T>(this T instance) where T : new()
        {
            T result = new T();
            object temp = ToDynamicData(instance, nameof(T));
            temp.CopyProperties(instance);
            result.CopyProperties(temp);
            return result;
        }

        public static object DataClone(this object instance, Type type)
        {
            object result = type.Construct();
            object temp = ToDynamicData(instance, type.Name);
            result.CopyProperties(temp);
            return result;
        }

        public static object ToDynamicData(this object instance, string typeName)
        {
            Type type = instance.ToDynamicType(typeName, DataTypeFilter);
            object temp = type.Construct();
            temp.CopyProperties(instance);
            return temp;
        }

        public static object ToDynamicData(this Dictionary<string, object> instance, string typeName)
        {
            Type type = instance.ToDynamicType(typeName);
            object temp = type.Construct();
            foreach (string key in instance.Keys)
            {
                temp.Property(key, instance[key]);
            }
            return temp;
        }


        public static object ValuePropertiesToDynamicInstance(this Type type, out AssemblyBuilder assemblyBuilder)
        {
            object instance = type.Construct();
            Type dynamicType = instance.ValuePropertiesToDynamicType(type.Name, out assemblyBuilder, false);
            return dynamicType.Construct();
        }

        public static Type ValuePropertiesToDynamicType(this object instance, string typeName, bool useCache = true)
        {
            AssemblyBuilder ignore;
            return ValuePropertiesToDynamicType(instance, typeName, out ignore, useCache);
        }

        public static Type ValuePropertiesToDynamicType(this object instance, string typeName, out AssemblyBuilder assemblyBuilder, bool useCache = true)
        {
            return instance.ToDynamicType(typeName, p => p.PropertyType.IsValueType, out assemblyBuilder, useCache);
        }

        /// <summary>
        /// Combines the current instance with the specified toMerge values
        /// creating a new type with all the properties of each and value 
        /// set to the last one in
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="toMerge"></param>
        /// <returns></returns>
        public static object Combine(this object instance, params object[] toMerge)
        {
            Type combinedType = CombineToDynamicType(instance, toMerge);
            object newInstance = combinedType.Construct();
            newInstance.CopyProperties(instance);
            toMerge.Each(obj =>
            {
                newInstance.CopyProperties(obj);
            });
            return newInstance;
        }

        public static Type BuildDynamicType(this string typeName, string nameSpace, out AssemblyBuilder assemblyBuilder, params string[] propertyNames)
        {
            lock (_buildDynamicTypeLock)
            {

                string fullName = string.IsNullOrWhiteSpace(nameSpace) ? typeName : $"{nameSpace}.{typeName}";
                if (DynamicTypeStore.Current.ContainsTypeInfo(fullName) && DynamicTypeStore.Current[fullName] != null)
                {
                    return GetExistingDynamicType(fullName, out assemblyBuilder);
                }
                else
                {
                    TypeBuilder typeBuilder;
                    GetAssemblyAndTypeBuilder(fullName, out assemblyBuilder, out typeBuilder);
                    foreach (string propertyName in propertyNames)
                    {
                        AddPropertyToDynamicType(typeBuilder, new CustomPropertyInfo(propertyName, typeof(object)));
                    }
                    return CreateDynamicType(fullName, typeBuilder);
                }
            }
        }

        public static void GetAssemblyAndTypeBuilder(string typeName, out AssemblyBuilder assemblyBuilder, out TypeBuilder typeBuilder, bool useCache = true)
        {
            if (DynamicTypeStore.Current.ContainsTypeInfo(typeName) && useCache)
            {
                DynamicTypeInfo info = DynamicTypeStore.Current[typeName];
                assemblyBuilder = info.AssemblyBuilder;
                typeBuilder = info.TypeBuilder;
            }
            else
            {
                string name = typeName;
                AssemblyName assemblyName = new AssemblyName("Bam.Net.DynamicGenerator");
                assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
                ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name, assemblyName.Name + ".dll");

                typeBuilder = moduleBuilder.DefineType(name, TypeAttributes.Public);

                DynamicTypeStore.Current.AddType(typeName, new DynamicTypeInfo { AssemblyBuilder = assemblyBuilder, TypeBuilder = typeBuilder, TypeName = typeName });
            }
        }

        public static Type MergeToDynamicType(this Type type, params Type[] types)
        {
            List<object> instances = new List<object>();
            instances.Add(type.Construct());
            types.Each(t =>
            {
                instances.Add(t.Construct());
            });
            return instances.MergeToDynamicType(type.Name, 0, true);
        }

        public static Type MergeToDynamicType(this List<object> objects, string typeName, int recursionThusFar, bool useCache = true)
        {
            return MergeToDynamicType(objects, typeName, recursionThusFar, out AssemblyBuilder ignore, useCache);
        }

        public static Type MergeToDynamicType(this List<object> objects, string typeName, int recursionThusFar, out AssemblyBuilder assemblyBuilder, bool useCache = true)
        {
            lock (_buildDynamicTypeLock)
            {
                ThrowIfLimitReached(recursionThusFar);

                if (DynamicTypeStore.Current.ContainsTypeInfo(typeName) && DynamicTypeStore.Current[typeName] != null && useCache)
                {
                    return GetExistingDynamicType(typeName, out assemblyBuilder);
                }
                else
                {
                    TypeBuilder typeBuilder;
                    GetAssemblyAndTypeBuilder(typeName, out assemblyBuilder, out typeBuilder);

                    // foreach object get the type
                    foreach (object obj in objects)
                    {
                        Type currentType = obj.GetType();
                        // if it's a Dictionary<object, object> use ToDynamicType to get a type representing it
                        if (currentType == typeof(Dictionary<object, object>))
                        {
                            Type dynamicDictionaryType = ((Dictionary<object, object>)obj).ToDynamicType(typeName, ++recursionThusFar, false);
                            AddPropertiesToDynamicType(typeBuilder, dynamicDictionaryType);
                        }
                        else if (currentType.IsArray)// if it's an array increment recursion and call self
                        {
                            Type mergedArrayType = ((object[])obj).ToList().MergeToDynamicType(typeName, ++recursionThusFar, false);
                            AddPropertiesToDynamicType(typeBuilder, mergedArrayType);
                        }
                        else// otherwise add the valueproperties					
                        {
                            Type valueProps = obj.ValuePropertiesToDynamicType(typeName, false);
                            AddPropertiesToDynamicType(typeBuilder, valueProps);
                        }
                    }

                    return CreateDynamicType(typeName, typeBuilder);
                }
            }
        }

        /// <summary>
        /// Convert the table into a list of dynamic objects
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<dynamic> ToDynamicList(this DataTable table)
        {
            return ToDynamicList(table, table.TableName.Or(8.RandomLetters()));
        }

        /// <summary>
        /// Conver the table into a list of dynamic objects with the specified typeName
        /// used for reflection
        /// </summary>
        /// <param name="table"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static List<dynamic> ToDynamicList(this DataTable table, string typeName)
        {
            List<dynamic> instances = new List<dynamic>();
            foreach (DataRow row in table.Rows)
            {
                instances.Add(row.ToDynamic(typeName));
            }

            return instances;
        }

        public static IEnumerable<dynamic> ToDynamicEnumerable(this DataTable table, string typeName = null)
        {
            typeName = typeName ?? table.TableName.Or(8.RandomLetters());
            foreach (DataRow row in table.Rows)
            {
                yield return row.ToDynamic(typeName);
            }
        }

        public static Type ToDynamicType(this DataRow row)
        {
            string typeName = 8.RandomLetters();
            if (row.Table != null)
            {
                typeName = row.Table.TableName.Or(typeName);
            }

            return ToDynamicType(row, typeName, out AssemblyBuilder ignore);
        }

        public static Type ToDynamicType(this DataRow row, string typeName, out AssemblyBuilder assemblyBuilder)
        {
            lock (_buildDynamicTypeLock)
            {
                if (DynamicTypeStore.Current.ContainsTypeInfo(typeName) && DynamicTypeStore.Current[typeName] != null)
                {
                    return GetExistingDynamicType(typeName, out assemblyBuilder);
                }
                else
                {
                    GetAssemblyAndTypeBuilder(typeName, out assemblyBuilder, out TypeBuilder typeBuilder);

                    foreach (DataColumn column in row.Table.Columns)
                    {
                        CustomPropertyInfo propInfo = new CustomPropertyInfo(column.ColumnName, row[column].GetType());
                        AddPropertyToDynamicType(typeBuilder, propInfo);
                    }

                    return CreateDynamicType(typeName, typeBuilder);
                }
            }
        }

        public static Type ToDynamicType(this Dictionary<string, object> dictionary, string typeName)
        {
            return ToDynamicType(dictionary, typeName, out AssemblyBuilder ignore);
        }

        public static Type ToDynamicType(this Dictionary<string, object> dictionary, string typeName, out AssemblyBuilder assemblyBuilder)
        {
            lock (_buildDynamicTypeLock)
            {
                if (DynamicTypeStore.Current.ContainsTypeInfo(typeName) && DynamicTypeStore.Current[typeName] != null)
                {
                    return GetExistingDynamicType(typeName, out assemblyBuilder);
                }
                else
                {
                    GetAssemblyAndTypeBuilder(typeName, out assemblyBuilder, out TypeBuilder typeBuilder);

                    foreach (string propertyName in dictionary.Keys)
                    {
                        CustomPropertyInfo propInfo = new CustomPropertyInfo(propertyName, dictionary[propertyName].GetType());
                        AddPropertyToDynamicType(typeBuilder, propInfo);
                    }

                    return CreateDynamicType(typeName, typeBuilder);
                }
            }
        }

        public static Type BuildDynamicType(this string typeName, params string[] propertyNames)
        {
            return BuildDynamicType(typeName, string.Empty, propertyNames);
        }

        public static Type BuildDynamicType(this string typeName, string nameSpace, params string[] propertyNames)
        {
            return BuildDynamicType(typeName, nameSpace, out AssemblyBuilder ignore, propertyNames);
        }

        static object _buildDynamicTypeLock = new object();
        public static Type CombineToDynamicType(this object instance, params object[] toMerge)
        {
            return CombineToDynamicType(instance, "DynamicType_".RandomLetters(8), toMerge);
        }

        public static Type CombineToDynamicType(this object instance, string typeName, params object[] toMerge)
        {
            lock (_buildDynamicTypeLock)
            {
                GetAssemblyAndTypeBuilder(typeName, out AssemblyBuilder assemblyBuilder, out TypeBuilder typeBuilder);
                List<object> all = new List<object>
                {
                    instance
                };
                all.AddRange(toMerge);
                all.Each(obj =>
                {
                    Type type = obj.GetType();
                    AddPropertiesToDynamicType(typeBuilder, type);
                });

                return CreateDynamicType(typeName, typeBuilder);
            }
        }

        /// <summary>
        /// Create a dynamic type for the object with the specified typeName
        /// using the specified propertyPredicate to determine what properties
        /// of the original type to include
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="typeName"></param>
        /// <param name="propertyPredicate"></param>
        /// <returns></returns>
        public static Type ToDynamicType(this object instance, string typeName, Func<PropertyInfo, bool> propertyPredicate)
        {
            return ToDynamicType(instance, typeName, propertyPredicate, out AssemblyBuilder ignore);
        }

        /// <summary>
        /// Create a dynamic type for the object with the specified typeName
        /// using the specified propertyPredicate to determine what properties
        /// of the original type to include
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="typeName"></param>
        /// <param name="propertyPredicate"></param>
        /// <param name="assemblyBuilder"></param>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public static Type ToDynamicType(this object instance, string typeName, Func<PropertyInfo, bool> propertyPredicate, out AssemblyBuilder assemblyBuilder, bool useCache = true)
        {
            lock (_buildDynamicTypeLock)
            {
                if (DynamicTypeStore.Current.ContainsTypeInfo(typeName) && DynamicTypeStore.Current[typeName] != null && useCache)
                {
                    return GetExistingDynamicType(typeName, out assemblyBuilder);
                }
                else
                {
                    GetAssemblyAndTypeBuilder(typeName, out assemblyBuilder, out TypeBuilder typeBuilder);

                    Type actualType = instance.GetType();
                    PropertyInfo[] properties = actualType.GetProperties();
                    properties.Each(p =>
                    {
                        if (propertyPredicate(p))
                        {
                            AddPropertyToDynamicType(typeBuilder, p);
                        }
                    });

                    return CreateDynamicType(typeName, typeBuilder);
                }
            }
        }

        public static Type ToDynamicType(this Dictionary<object, object> dictionary, string typeName, List<Type> created)
        {
            return ToDynamicType(dictionary, typeName, 0, created, out AssemblyBuilder ignore, false);
        }

        public static Type ToDynamicType(this Dictionary<object, object> dictionary, string typeName, bool useCache = true)
        {
            return ToDynamicType(dictionary, typeName, 0, useCache);
        }

        internal static Type ToDynamicType(this Dictionary<object, object> dictionary, string typeName, int recursionThusFar, bool useCache = true)
        {
            return dictionary.ToDynamicType(typeName, recursionThusFar, out AssemblyBuilder ignore, useCache);
        }

        internal static Type ToDynamicType(this Dictionary<object, object> dictionary, string typeName, int recursionThusFar, out AssemblyBuilder assemblyBuilder, bool useCache = true)
        {
            return ToDynamicType(dictionary, typeName, recursionThusFar, new List<Type>(), out assemblyBuilder, useCache);
        }

        internal static Type ToDynamicType(this Dictionary<object, object> dictionary, string typeName, int recursionThusFar, List<Type> createdTypes, out AssemblyBuilder assemblyBuilder, bool useCache = true)
        {
            ThrowIfLimitReached(recursionThusFar);

            if (DynamicTypeStore.Current.ContainsTypeInfo(typeName) && DynamicTypeStore.Current[typeName] != null && useCache)
            {
                return GetExistingDynamicType(typeName, out assemblyBuilder);
            }
            else
            {
                return BuildDynamicType(dictionary, typeName, recursionThusFar, createdTypes, out assemblyBuilder);
            }
        }
        private static Type BuildDynamicType(Dictionary<object, object> dictionary, string typeName, int recursionThusFar, List<Type> createdTypes, out AssemblyBuilder assemblyBuilder)
        {
            lock (_buildDynamicTypeLock)
            {
                TypeBuilder typeBuilder;
                GetAssemblyAndTypeBuilder(typeName, out assemblyBuilder, out typeBuilder, false);

                foreach (object key in dictionary.Keys)
                {
                    string propertyName = key as string;
                    if (propertyName == null)
                    {
                        Args.Throw<InvalidOperationException>("Key was ({0}), expected string", key.GetType().Name);
                    }
                    propertyName = propertyName.PascalCase();
                    object value = dictionary[key];
                    if (value == null)
                    {
                        AddPropertyToDynamicType(typeBuilder, propertyName, typeof(string));
                    }
                    else
                    {
                        Type valueType = value.GetType();
                        if (valueType.IsArray)
                        {
                            AddPropertyToDynamicType(typeBuilder, propertyName, typeof(object[]));
                        }
                        else if (valueType == typeof(Dictionary<object, object>))
                        {
                            string childTypeName = "{0}_{1}"._Format(typeName, propertyName);
                            Type childType = ((Dictionary<object, object>)value).ToDynamicType(childTypeName, ++recursionThusFar, false);
                            createdTypes.Add(childType);
                            AddPropertyToDynamicType(typeBuilder, propertyName, childType);
                        }
                        else if (valueType.IsPrimitive || valueType == typeof(string) || valueType.IsValueType)
                        {
                            AddPropertyToDynamicType(typeBuilder, propertyName, valueType);
                        }
                    }
                }

                Type created = CreateDynamicType(typeName, typeBuilder);
                createdTypes.Add(created);
                return created;
            }
        }

        private static void ThrowIfLimitReached(int recursionThusFar)
        {
            if (DynamicTypeInfo.RecursionLimit <= recursionThusFar)
            {
                throw new DynamicTypeRecursionLimitReachedException(DynamicTypeInfo.RecursionLimit);
            }
        }

        private static void AddPropertiesToDynamicType(TypeBuilder typeBuilder, Type type)
        {
            foreach (PropertyInfo prop in type.GetProperties())
            {
                AddPropertyToDynamicType(typeBuilder, prop);
            }
        }

        private static void AddPropertyToDynamicType(TypeBuilder typeBuilder, PropertyInfo p)
        {
            CustomPropertyInfo propInfo = new CustomPropertyInfo(p.Name, p.PropertyType);
            AddPropertyToDynamicType(typeBuilder, propInfo);
        }

        private static void AddPropertyToDynamicType(TypeBuilder typeBuilder, string propertyName, Type propertyType)
        {
            CustomPropertyInfo propInfo = new CustomPropertyInfo(propertyName, propertyType);
            AddPropertyToDynamicType(typeBuilder, propInfo);
        }

        private static Type CreateDynamicType(string typeName, TypeBuilder typeBuilder)
        {
            Type returnType = typeBuilder.CreateType();
            if (DynamicTypeStore.Current[typeName] == null)
            {
                throw new InvalidOperationException("DynamicTypeInfo was null");
            }
            DynamicTypeStore.Current[typeName].DynamicType = returnType;
            return returnType;
        }

        private static Type GetExistingDynamicType(string typeName, out AssemblyBuilder assemblyBuilder)
        {
            DynamicTypeInfo info = DynamicTypeStore.Current[typeName];
            assemblyBuilder = info.AssemblyBuilder;
            return info.DynamicType;
        }

        internal static void AddPropertyToDynamicType(TypeBuilder typeBuilder, _PropertyInfo property)
        {
            FieldBuilder propertyField = typeBuilder.DefineField("_" + property.Name.ToLower(), property.PropertyType, FieldAttributes.Private);

            PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(property.Name, System.Reflection.PropertyAttributes.HasDefault, property.PropertyType, Type.EmptyTypes);
            MethodAttributes getSetAttr = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;
            MethodBuilder getMethodBuilder = typeBuilder.DefineMethod("get_" + property.Name, getSetAttr, property.PropertyType, Type.EmptyTypes);
            ILGenerator propertyGetIL = getMethodBuilder.GetILGenerator();
            propertyGetIL.Emit(OpCodes.Ldarg_0);
            propertyGetIL.Emit(OpCodes.Ldfld, propertyField);
            propertyGetIL.Emit(OpCodes.Ret);

            MethodBuilder setMethodBuilder = typeBuilder.DefineMethod("set_" + property.Name, getSetAttr, null, new Type[] { property.PropertyType });
            ILGenerator propertySetIL = setMethodBuilder.GetILGenerator();
            propertySetIL.Emit(OpCodes.Ldarg_0);
            propertySetIL.Emit(OpCodes.Ldarg_1);
            propertySetIL.Emit(OpCodes.Stfld, propertyField);
            propertySetIL.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getMethodBuilder);
            propertyBuilder.SetSetMethod(setMethodBuilder);
        }
    }
}
