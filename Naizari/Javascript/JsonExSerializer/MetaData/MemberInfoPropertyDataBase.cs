/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Naizari.Javascript.JsonExSerialization.TypeConversion;
using Naizari.Javascript.JsonExSerialization.Framework;

namespace Naizari.Javascript.JsonExSerialization.MetaData
{
    /// <summary>
    /// Base class for Properties and Fields of a Type
    /// </summary>
    public abstract class MemberInfoPropertyDataBase : AbstractPropertyData
    {
        protected MemberInfo member;
        protected MemberInfoPropertyDataBase(MemberInfo member)
            : base(member.DeclaringType)
        {
            this.member = member;
        }

        protected MemberInfoPropertyDataBase(MemberInfo member, int position)
            : this(member)
        {
            this.position = position;
        }

        protected MemberInfoPropertyDataBase(MemberInfo member, string constructorParameterName)
            : this(member)
        {
            this.constructorParameterName = constructorParameterName;
        }

        protected MemberInfoPropertyDataBase(MemberInfo member, bool isConstructorParameter)
            : base(member.DeclaringType)
        {
            this.member = member;
            if (isConstructorParameter)
                this.constructorParameterName = member.Name;
        }

        protected void Initialize(ICustomAttributeProvider member)
        {
            ConstructorParameterAttribute ctorAttr = ReflectionUtils.GetAttribute<ConstructorParameterAttribute>(member, false);
            if (ctorAttr != null)
            {
                if (ctorAttr.Position >= 0)
                    position = ctorAttr.Position;
                else if (!string.IsNullOrEmpty(ctorAttr.Name))
                    this.constructorParameterName = ctorAttr.Name;
                else
                    this.constructorParameterName = this.Name;
            }
        }

        /// <summary>
        ///  The name of the property
        /// </summary>
        public override string Name
        {
            get { return member.Name; }
        }

        protected override IJsonTypeConverter CreateTypeConverter()
        {
            return CreateTypeConverter(member);
        }
    }
}
