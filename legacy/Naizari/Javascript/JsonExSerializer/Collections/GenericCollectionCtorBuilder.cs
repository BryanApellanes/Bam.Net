/*
	Copyright © Bryan Apellanes 2015  
*/
/*
 * Copyright (c) 2007, Ted Elliott
 * Code licensed under the New BSD License:
 * http://code.google.com/p/jsonexserializer/wiki/License
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;

namespace Naizari.Javascript.JsonExSerialization.Collections
{
    public class GenericCollectionCtorBuilder<T> : GenericCollectionBuilder<T>
    {
        private Type _finalType;
        public GenericCollectionCtorBuilder(Type finalType)
            : base(typeof(List<T>))
        {
            _finalType = finalType;
        }

        public override object GetResult()
        {
            object result = base.GetResult();
            foreach (ConstructorInfo ctor in _finalType.GetConstructors())
            {
                ParameterInfo[] parms = ctor.GetParameters();
                if (parms.Length == 1)
                {
                    Type parmType = parms[0].ParameterType;

                    if (parmType.IsInstanceOfType(result))
                    {
                        return ctor.Invoke(new object[] { result });
                    } 
                }
            }
            throw new InvalidOperationException("No suitable constructor found for " + _finalType);
        }
    }
}
