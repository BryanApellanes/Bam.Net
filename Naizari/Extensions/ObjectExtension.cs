/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Data;
using System.Reflection;
using System.Reflection.Emit;
using Naizari.Helpers;
using Naizari.Configuration;
using System.Data;
using System.Runtime.InteropServices;
using Naizari.Testing;

namespace Naizari.Extensions
{
    public static class ObjectExtension
    {
        public static object ToAnonymous<T>(this T target, Func<T, object> anonymizer)
        {
            return anonymizer(target);
        }

        public static object[] ToAnonymous<T>(this IEnumerable<T> target, Func<T, object> anonymizer)
        {
            List<object> values = new List<object>();
            foreach (T item in target)
            {
                values.Add(anonymizer(item));
            }

            return values.ToArray();
        }

        public static object[] ToJsonSafe(this DaoObject[] daoObjects)
        {
            List<object> values = new List<object>();
            foreach (DaoObject dao in daoObjects)
            {
                values.Add(dao.ToJsonSafe());
            }

            return values.ToArray();
        }

        /// <summary>
        /// Creates an in memory dynamic type representing
        /// the specified DaoObject's DaoColumns only.
        /// </summary>
        /// <param name="daoObject"></param>
        /// <returns></returns>
        public static object ToJsonSafe(this DaoObject daoObject)
        {
            Type jsonSafeType = CreateDynamicType<DaoColumn>(daoObject, false);
            ConstructorInfo ctor = jsonSafeType.GetConstructor(new Type[] { });
            object jsonSafeInstance = ctor.Invoke(null);//Activator.CreateInstance(jsonSafeType);
            DefaultConfiguration.CopyProperties(daoObject, jsonSafeInstance);
            return jsonSafeInstance;
        }

        public static string[] ToLowerInvariant(this string[] stringArray)
        {
            for (int i = stringArray.Length -1; i >= 0; i--)
            {
                stringArray[i] = stringArray[i].ToLowerInvariant();
            }

            return stringArray;
        }

        /// <summary>
        /// Cast the object to the specified generic type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <returns></returns>
        public static T Cast<T>(this object o)
        {
            return (T)o;
        }
        /// <summary>
        /// Gets the property with the specified propertyName as generic type PropType.
        /// </summary>
        /// <typeparam name="T">The type of the current instance</typeparam>
        /// <typeparam name="PropType">The type of the specified propertyName</typeparam>
        /// <param name="dataObj"></param>
        /// <param name="propertyName">The name of the property</param>
        /// <returns></returns>
        public static PropType DataProp<T, PropType>(this T dataObj, string propertyName)
        {
            PropertyInfo prop = typeof(T).GetProperty(propertyName);
            if (prop != null)
            {
                return (PropType)prop.GetValue(dataObj, null);
            }

            return default(PropType);
        }

        public static string PropertiesToString<T>(this T obj)
        {
            return PropertiesToString<T>(obj, "\r\n");
        }

        public static string PropertiesToString<T>(this T obj, string separator)
        {
            StringBuilder returnValue = new StringBuilder();
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                try
                {
                    object value = property.GetValue(obj, null);
                    if (value != null)
                    {
                        returnValue.AppendFormat("{0}: {1}{2}", property.Name, value.ToString(), separator);
                    }
                }
                catch(Exception ex)
                {
                    returnValue.AppendFormat("{0}: ({1}){2}", property.Name, ex.Message, separator);
                }
            }

            return returnValue.ToString();
        }

        public static void HydrateFromDataRow(this object target, DataRow row)
        {
            DatabaseAgent.FromDataRow(target, row);
        }

        public static void HydrateFromDataRow(this object target, DataRow row, Dictionary<string, string> propertyToColumnMap)
        {
            DatabaseAgent.FromDataRow(target, row, propertyToColumnMap);
        }

        public static T ToType<T>(this DataRow row) where T: new()
        {
            T retVal = new T();
            foreach (DataColumn column in row.Table.Columns)
            {
                PropertyInfo prop = typeof(T).GetProperty(column.ColumnName);
                if (prop != null)
                {
                    prop.SetValue(retVal, row[column], null);
                }
            }

            return retVal;
        }


        /// <summary>
        /// Creates an in memory clone of the specified objectToClone.  The 
        /// clone will only have the properties of objectToClone that are
        /// addorned with the specified PropertyAttributeFilter generic type.
        /// </summary>
        /// <typeparam name="PropertyAttributeFilter">The attribute to look for when copying properties</typeparam>
        /// <param name="objectToClone">The object to clone</param>
        /// <returns>An in memory type that is not persisted to disk.</returns>
        public static Type CreateDynamicType<PropertyAttributeFilter>(this object objectToClone) where PropertyAttributeFilter : Attribute, new()
        {
            AssemblyBuilder ignore;
            return CreateDynamicType<PropertyAttributeFilter>(objectToClone, out ignore);
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
        public static Type CreateDynamicType<PropertyAttributeFilter>(this object objectToClone, bool concreteAttribute) where PropertyAttributeFilter : Attribute, new()
        {
            AssemblyBuilder ignore;
            return CreateDynamicType<PropertyAttributeFilter>(objectToClone, out ignore, concreteAttribute);
        }

        /// <summary>
        /// Creates an in memory clone of the specified objectToClone.  The 
        /// clone will only have the properties of objectToClone that are
        /// addorned with the specified PropertyAttributeFilter generic type.
        /// </summary>
        /// <typeparam name="PropertyAttributeFilter">The attribute to look for when copying properties</typeparam>
        /// <param name="objectToClone">The object to clone</param>
        /// <returns>An in memory type that is not persisted to disk.</returns>
        public static Type CreateDynamicType<PropertyAttributeFilter>(this object objectToClone, out AssemblyBuilder assemblyBuilder) where PropertyAttributeFilter : Attribute, new()
        {
            return CreateDynamicType<PropertyAttributeFilter>(objectToClone, out assemblyBuilder, false);
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
        public static Type CreateDynamicType<PropertyAttributeFilter>(this object objectToClone, out AssemblyBuilder assemblyBuilder, bool concreteAttribute) where PropertyAttributeFilter : Attribute, new()
        {
            Type objType = objectToClone.GetType();
            string typeName = objType.Namespace + "." + objType.Name;
            if (DynamicTypeStore.Current.ContainsTypeInfo(typeName) && DynamicTypeStore.Current[typeName].DynamicType != null)
            {
                assemblyBuilder = DynamicTypeStore.Current[typeName].AssemblyBuilder;
                return DynamicTypeStore.Current[typeName].DynamicType;
            }
            else
            {
                TypeBuilder typeBuilder;
                GetAssemblyAndTypeBuilder(objectToClone, out assemblyBuilder, out typeBuilder);

                foreach (PropertyInfo property in objType.GetProperties())
                {
                    PropertyAttributeFilter attr;
                    if (CustomAttributeExtension.HasCustomAttributeOfType<PropertyAttributeFilter>(property, true, out attr, concreteAttribute))
                    {
                        AddPropertyToDynamicType(typeBuilder, property);
                    }
                }

                Type jsonSafeType = typeBuilder.CreateType();
                Expect.IsNotNull(DynamicTypeStore.Current[typeName], "DynamicTypeInfo was unexpectedly null.");
                DynamicTypeStore.Current[typeName].DynamicType = jsonSafeType;
                return jsonSafeType;
            }
        }

        public static object ToDynamicInstance(this DataRow row, string typeName)
        {
            AssemblyBuilder ignore;
            Type dynamicType = ToDynamicType(row, typeName, out ignore);
            ConstructorInfo ctor = dynamicType.GetConstructor(new Type[] { });
            object instance = ctor.Invoke(null);//Activator.CreateInstance(dynamicType);
            DatabaseAgent.FromDataRow(instance, row);
            return instance;
        }

        public static Type ToDynamicType(this DataRow row, string typeName, out AssemblyBuilder assemblyBuilder)
        {
            if (DynamicTypeStore.Current.ContainsTypeInfo(typeName) && DynamicTypeStore.Current[typeName] != null)
            {
                DynamicTypeInfo info = DynamicTypeStore.Current[typeName];
                assemblyBuilder = info.AssemblyBuilder;
                return info.DynamicType;
            }
            else
            {
                TypeBuilder typeBuilder;
                GetAssemblyAndTypeBuilder(typeName, out assemblyBuilder, out typeBuilder);

                foreach (DataColumn column in row.Table.Columns)
                {
                    CustomPropertyInfo propInfo = new CustomPropertyInfo(column.ColumnName, row[column].GetType());
                    AddPropertyToDynamicType(typeBuilder, propInfo);
                }

                Type returnType = typeBuilder.CreateType();
                Expect.IsNotNull(DynamicTypeStore.Current[typeName], "DynamicTypeInfo was unexpectedly null.");
                DynamicTypeStore.Current[typeName].DynamicType = returnType;
                return returnType;
            }
        }

        private static void GetAssemblyAndTypeBuilder(object objectToClone, out AssemblyBuilder assemblyBuilder, out TypeBuilder typeBuilder)
        {
            Type type = objectToClone.GetType();
            GetAssemblyAndTypeBuilder(type.Namespace + "." + type.Name, out assemblyBuilder, out typeBuilder);
        }

        private static void GetAssemblyAndTypeBuilder(string typeName, out AssemblyBuilder assemblyBuilder, out TypeBuilder typeBuilder)
        {
            if (DynamicTypeStore.Current.ContainsTypeInfo(typeName))
            {
                DynamicTypeInfo info = DynamicTypeStore.Current[typeName];
                assemblyBuilder = info.AssemblyBuilder;
                typeBuilder = info.TypeBuilder;
            }
            else
            {
                string name = typeName;
                AssemblyName assemblyName = new AssemblyName("DynamicGenerator");
                assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
                ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name, assemblyName.Name + ".dll");

                typeBuilder = moduleBuilder.DefineType(name, TypeAttributes.Public);

                DynamicTypeStore.Current.AddType(typeName, new DynamicTypeInfo { AssemblyBuilder = assemblyBuilder, TypeBuilder = typeBuilder, TypeName = typeName });
            }
        }

        private static void AddPropertyToDynamicType(TypeBuilder typeBuilder, _PropertyInfo property)
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
