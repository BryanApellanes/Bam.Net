/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Naizari.Extensions
{
    internal class CustomPropertyInfo: _PropertyInfo
    {
        internal CustomPropertyInfo(string name, Type propertyType)
        {
            this.Name = name;
            this.PropertyType = propertyType;
        }
        #region _PropertyInfo Members

        public System.Reflection.PropertyAttributes Attributes
        {
            get { throw new NotImplementedException(); }
        }

        public bool CanRead
        {
            get { throw new NotImplementedException(); }
        }

        public bool CanWrite
        {
            get { throw new NotImplementedException(); }
        }

        public Type DeclaringType
        {
            get { throw new NotImplementedException(); }
        }

        public System.Reflection.MethodInfo[] GetAccessors()
        {
            throw new NotImplementedException();
        }

        public System.Reflection.MethodInfo[] GetAccessors(bool nonPublic)
        {
            throw new NotImplementedException();
        }

        public object[] GetCustomAttributes(bool inherit)
        {
            throw new NotImplementedException();
        }

        public object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }

        public System.Reflection.MethodInfo GetGetMethod()
        {
            throw new NotImplementedException();
        }

        public System.Reflection.MethodInfo GetGetMethod(bool nonPublic)
        {
            throw new NotImplementedException();
        }

        public void GetIDsOfNames(ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
        {
            throw new NotImplementedException();
        }

        public System.Reflection.ParameterInfo[] GetIndexParameters()
        {
            throw new NotImplementedException();
        }

        public System.Reflection.MethodInfo GetSetMethod()
        {
            throw new NotImplementedException();
        }

        public System.Reflection.MethodInfo GetSetMethod(bool nonPublic)
        {
            throw new NotImplementedException();
        }

        public void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
        {
            throw new NotImplementedException();
        }

        public void GetTypeInfoCount(out uint pcTInfo)
        {
            throw new NotImplementedException();
        }

        public object GetValue(object obj, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, object[] index, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object GetValue(object obj, object[] index)
        {
            throw new NotImplementedException();
        }

        public void Invoke(uint dispIdMember, ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
        {
            throw new NotImplementedException();
        }

        public bool IsDefined(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }

        public bool IsSpecialName
        {
            get { throw new NotImplementedException(); }
        }

        public System.Reflection.MemberTypes MemberType
        {
            get { throw new NotImplementedException(); }
        }

        public string Name
        {
            get;
            set;
        }

        public Type PropertyType
        {
            get;
            set;
        }

        public Type ReflectedType
        {
            get { throw new NotImplementedException(); }
        }

        public void SetValue(object obj, object value, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, object[] index, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public void SetValue(object obj, object value, object[] index)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
