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

namespace Naizari.Javascript.JsonExSerialization.Framework.Visitors
{
    /// <summary>
    /// Base implementation of the Visitor class that achieves multi-dispatch
    /// using reflection.  Methods starting with "Visit" will be found and
    /// matched with the incoming type.
    /// </summary>
    public class VisitorBase : IVisitor
    {
        private IDictionary<Type, MethodInfo> _methodCache;
        public void Visit(object o)
        {
            if (_methodCache == null)
                BuildMethodCache();

            Type t = o.GetType();
            Visit(t, o);
        }

        protected void Visit(Type t, object o)
        {
            if (t == null || t == typeof(object))
            {
                VisitorNotFound(t, o);
                return;
            }
            if (_methodCache.ContainsKey(t))
                try
                {
                    _methodCache[t].Invoke(this, new object[] { o });
                }
                catch (TargetInvocationException e)
                {
                    if (e.InnerException != null)
                        throw e.InnerException;
                    else
                        throw;
                }
            else
                Visit(t.BaseType, o);
        }

        protected virtual void VisitorNotFound(Type t, object o)
        {
        }

        private void BuildMethodCache()
        {
            _methodCache = new Dictionary<Type, MethodInfo>();
            foreach (MethodInfo m in this.GetType().GetMethods())
            {
                if (!m.Name.ToLower().StartsWith("visit"))
                    continue;
                ParameterInfo[] parms = m.GetParameters();
                if (parms.Length != 1)
                    continue;
                if (parms[0].ParameterType == typeof(object))
                    continue;

                _methodCache.Add(parms[0].ParameterType, m);
            }
        }
    }
}
