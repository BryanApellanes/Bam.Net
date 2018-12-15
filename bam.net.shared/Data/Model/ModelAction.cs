/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Bam.Net.Data.Model
{
    public class ModelAction
    {
        public ModelAction(object owner, MethodInfo method)
        {
            this.Owner = owner;
            this.Method = method;
            this.Name = string.Format("{0}.{1}", owner.GetType().Name, method.Name);
        }

        public string Name
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public object Run(params object[] parameters)
        {
            return Run<object>();
        }

        public T Run<T>(params object[] parameters)
        {
            T val = default(T);
            val = (T)Method.Invoke(Owner, parameters);
            this.LastResult = val;
            return val;
        }

        public object Owner
        {
            get;
            private set;
        }

        public object LastResult
        {
            get;
            private set;
        }

        public MethodInfo Method
        {
            get;
            private set;
        }

        public override string ToString()
        {
            string value = Description;
            if (string.IsNullOrEmpty(value))
            {
                value = Name;
            }

            if (string.IsNullOrEmpty(value))
            {
                value = base.ToString();
            }

            return value;
        }
    }
}
