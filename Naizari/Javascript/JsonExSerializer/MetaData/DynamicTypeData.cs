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
    public class DynamicTypeData : TypeData
    {
        private delegate object ConstructDelegate();
        private ConstructDelegate _noArgConstructor;

        public DynamicTypeData(Type t, SerializationContext Context)
            : base(t, Context)
        {
        }

        protected override PropertyData CreatePropertyHandler(PropertyInfo Property)
        {
            return new DynamicPropertyData(Property);
        }

        public override object CreateInstance(object[] args)
        {
            if ((args == null || args.Length == 0) && !this.ForType.IsValueType)
                return DynamicConstruct();
            else
                return base.CreateInstance(args);
        }

        private object DynamicConstruct()
        {
            if (_noArgConstructor == null)
            {
                _noArgConstructor = BuildObjectConstructor();
            }
            return _noArgConstructor();
        }

        private ConstructDelegate BuildObjectConstructor()
        {
            ConstructorInfo cInfo = this.ForType.GetConstructor(Type.EmptyTypes);
            DynamicMethod method = new DynamicMethod(string.Concat("_ctor", this.ForType.Name, "_"), typeof(object), Type.EmptyTypes, this.ForType);
            ILGenerator generator = method.GetILGenerator();
            // declare return value
            generator.DeclareLocal(typeof(object));
            generator.Emit(OpCodes.Newobj, cInfo);
            //generator.Emit(OpCodes.Stloc_0);
            generator.Emit(OpCodes.Ret);

            return (ConstructDelegate) method.CreateDelegate(typeof(ConstructDelegate));
        }
    }
}
