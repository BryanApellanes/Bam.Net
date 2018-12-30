/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace Naizari.Extensions
{
    public class DynamicTypeInfo
    {
        public DynamicTypeInfo()
        {
            
        }

        public Type DynamicType
        {
            get;
            internal set;
        }

        public string TypeName
        {
            get;
            internal set;
        }

        public AssemblyBuilder AssemblyBuilder
        {
            get;
            internal set;
        }

        public TypeBuilder TypeBuilder
        {
            get;
            internal set;
        }


    }
}
