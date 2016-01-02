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
    public class DynamicPropertyData : PropertyData
    {
        private DynamicMethodUtil.GenericGetter _getter;
        private DynamicMethodUtil.GenericSetter _setter;

        public DynamicPropertyData(PropertyInfo PropertyInfo)
            : base(PropertyInfo)
        {
            Initialize();
        }

        public DynamicPropertyData(PropertyInfo PropertyInfo, int Position)
            : base(PropertyInfo, Position)
        {
            Initialize();
        }

        private void Initialize()
        {
            _getter = FirstCallGetter;
            _setter = FirstCallSetter;
        }

        public override object GetValue(object instance)
        {
            return _getter(instance);
        }

        
        public override void SetValue(object instance, object value)
        {
            _setter(instance, value);
        }

        private object FirstCallGetter(object instance)
        {
            _getter = DynamicMethodUtil.CreatePropertyGetter(this.Property);
            return _getter(instance);
        }

        private void FirstCallSetter(object instance, object value)
        {
            _setter = DynamicMethodUtil.CreatePropertySetter(this.Property);
            _setter(instance, value);
        }

    }
}
