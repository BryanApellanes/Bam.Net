/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace Naizari.Implementation
{
    [AttributeUsage(AttributeTargets.Class)]
    public class BusinessObjectImplementation: Attribute
    {
        Type interfaceName;

        public BusinessObjectImplementation(Type interfaceName)
        {
            this.interfaceName = interfaceName;
        }

        public Type Interface
        {
            get { return interfaceName; }
            set { interfaceName = value; }
        }
    }

    
}
