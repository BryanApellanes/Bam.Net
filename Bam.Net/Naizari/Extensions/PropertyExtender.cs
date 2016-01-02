/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Naizari.Extensions
{
    public class PropertyExtender
    {
        protected Type type;
        protected object arg;
        protected Dictionary<string, object> extensions;

        public PropertyExtender(object arg)
        {
            this.arg = arg;
            this.type = arg.GetType();
            this.extensions = new Dictionary<string, object>();
        }

        public object Object
        {
            get { return arg; }
        }

        public virtual object GetExt(string propertyName)
        {
            PropertyInfo prop = type.GetProperty(propertyName);

            if (prop == null)
                return null;
            else
            {
                if (this.extensions.ContainsKey(propertyName))
                    return this.extensions[propertyName];
                else
                    return null;
            }
        }

        public virtual void SetExt(string propertyName, object ext)
        {
            if (extensions.ContainsKey(propertyName))
                extensions[propertyName] = ext;
            else
                extensions.Add(propertyName, ext);
        }
    }
}
