/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace Naizari.Javascript.JsonExSerialization.MetaData
{
    /// <summary>
    /// Utility class for light weigh code-gen methods
    /// </summary>
    public static class DynamicMethodUtil
    {
        public delegate void GenericSetter(object target, object value);
        public delegate object GenericGetter(object target);

        public static GenericGetter CreatePropertyGetter(PropertyInfo Property)
        {
            if (Property.DeclaringType.IsValueType)
                return CreateValueTypePropertyGet(Property);
            else
                return CreateReferenceTypePropertyGet(Property);
        }

        private static GenericGetter CreateValueTypePropertyGet(PropertyInfo Property)
        {
            Type DeclaringType = Property.DeclaringType;

            // should only be called for structs
            if (!DeclaringType.IsValueType)
                throw new ArgumentException("DeclaringType is not a Value Type: " + DeclaringType);

            /*
             * If there's no getter return null
             */
            MethodInfo getMethod = Property.GetGetMethod();
            if (getMethod == null)
                throw new ArgumentException("Property has no Get method:" + Property.Name);


            /*
             * Create the dynamic method
             */
            Type[] arguments = new Type[1];
            arguments[0] = typeof(object);


            DynamicMethod getter = new DynamicMethod(
                String.Concat("_Get", Property.Name, "_"),
                typeof(object), arguments, DeclaringType);
            ILGenerator generator = getter.GetILGenerator();
            generator.DeclareLocal(typeof(object)); // return value
            generator.DeclareLocal(DeclaringType); // struct instance

            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Unbox_Any, DeclaringType);
            generator.Emit(OpCodes.Stloc_1); // get the unboxed value from the stack
            //generator.Emit(OpCodes.Ldloc_1);
            generator.Emit(OpCodes.Ldloca_S, 1);
            generator.EmitCall(OpCodes.Call, getMethod, null);

            if (!Property.PropertyType.IsClass)
                generator.Emit(OpCodes.Box, Property.PropertyType);

            generator.Emit(OpCodes.Ret);

            /*
             * Create the delegate and return it
             */
            return (GenericGetter)getter.CreateDelegate(typeof(GenericGetter));
        }

        private static GenericGetter CreateReferenceTypePropertyGet(PropertyInfo Property)
        {

            Type DeclaringType = Property.DeclaringType;

            // should only be called for Reference type
            if (DeclaringType.IsValueType)
                throw new ArgumentException("DeclaringType is not a reference type: " + DeclaringType);

            /*
             * If there's no getter return null
             */
            MethodInfo getMethod = Property.GetGetMethod();
            if (getMethod == null)
                throw new ArgumentException("Property has no Get method:" + Property.Name);

            /*
             * Create the dynamic method
             */
            Type[] arguments = new Type[1];
            arguments[0] = typeof(object);

            DynamicMethod getter = new DynamicMethod(
                String.Concat("_Get", Property.Name, "_"),
                typeof(object), arguments, DeclaringType);

            ILGenerator generator = getter.GetILGenerator();

            generator.DeclareLocal(typeof(object));
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Castclass, DeclaringType);
            generator.EmitCall(OpCodes.Callvirt, getMethod, null);

            if (!Property.PropertyType.IsClass)
                generator.Emit(OpCodes.Box, Property.PropertyType);

            generator.Emit(OpCodes.Ret);

            /*
             * Create the delegate and return it
             */
            return (GenericGetter)getter.CreateDelegate(typeof(GenericGetter));
        }

        public static GenericSetter CreatePropertySetter(PropertyInfo Property)
        {

            /*
             * If there's no setter return null
             */
            MethodInfo setMethod = Property.GetSetMethod();
            if (setMethod == null)
                throw new ArgumentException("Property has no set method:" + Property.Name);


            return GetSetterInvoker(Property.Name, setMethod);
        }

        public static GenericSetter GetSetterInvoker(string Name, MethodInfo MethodToInvoke)
        {
            Type DeclaringType = MethodToInvoke.DeclaringType;
            Type firstArg = MethodToInvoke.GetParameters()[0].ParameterType;

            /*
             * Create the dynamic method
             */
            Type[] arguments = new Type[2];
            arguments[0] = arguments[1] = typeof(object);

            DynamicMethod setter = new DynamicMethod(
                String.Concat("_Call", Name, "_"),
                typeof(void), arguments, DeclaringType);
            ILGenerator generator = setter.GetILGenerator();

            // load the target object
            generator.Emit(OpCodes.Ldarg_0);
            // cast or unbox it
            if (DeclaringType.IsClass)
                generator.Emit(OpCodes.Castclass, DeclaringType);
            else
                generator.Emit(OpCodes.Unbox_Any, DeclaringType);

            // load the value
            generator.Emit(OpCodes.Ldarg_1);

            // cast if necessary
            if (firstArg.IsClass)
                generator.Emit(OpCodes.Castclass, firstArg);
            else
                generator.Emit(OpCodes.Unbox_Any, firstArg);

            // call the setter
            generator.EmitCall(OpCodes.Callvirt, MethodToInvoke, null);

            // return
            generator.Emit(OpCodes.Ret);

            /*
             * Create the delegate and return it
             */
            return (GenericSetter)setter.CreateDelegate(typeof(GenericSetter));
        }
    }
}
