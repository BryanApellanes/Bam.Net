/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Globalization;

namespace Bam.Net
{
    public class CustomPropertyInfo: _PropertyInfo
    {
        internal CustomPropertyInfo(string name, Type propertyType)
        {
            this.Name = name;
            this.PropertyType = propertyType;
        }
        #region _PropertyInfo Members

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

        public object[] GetCustomAttributes(bool inherit)
        {
            throw new NotImplementedException();
        }

        public object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }


        public void GetIDsOfNames(ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
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

        MemberTypes _PropertyInfo.MemberType => throw new NotImplementedException();

        PropertyAttributes _PropertyInfo.Attributes => throw new NotImplementedException();
                
        public void SetValue(object obj, object value, object[] index)
        {
            throw new NotImplementedException();
        }

        public object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        MethodInfo[] _PropertyInfo.GetAccessors(bool nonPublic)
        {
            throw new NotImplementedException();
        }

        MethodInfo _PropertyInfo.GetGetMethod(bool nonPublic)
        {
            throw new NotImplementedException();
        }

        MethodInfo _PropertyInfo.GetSetMethod(bool nonPublic)
        {
            throw new NotImplementedException();
        }

        ParameterInfo[] _PropertyInfo.GetIndexParameters()
        {
            throw new NotImplementedException();
        }

        MethodInfo[] _PropertyInfo.GetAccessors()
        {
            throw new NotImplementedException();
        }

        MethodInfo _PropertyInfo.GetGetMethod()
        {
            throw new NotImplementedException();
        }

        MethodInfo _PropertyInfo.GetSetMethod()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
