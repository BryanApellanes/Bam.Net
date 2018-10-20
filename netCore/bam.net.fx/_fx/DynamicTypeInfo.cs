/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace Bam.Net
{
    public class DynamicTypeInfo
    {
        public DynamicTypeInfo()
        {
            
        }

		static int _recursionLimit;
		public static int RecursionLimit
		{
			get
			{
				if (_recursionLimit <= 0)
				{
					_recursionLimit = 5;
				}

				return _recursionLimit;
			}
			set
			{
				_recursionLimit = value;
			}
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
