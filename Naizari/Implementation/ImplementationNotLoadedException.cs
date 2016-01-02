/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace Naizari.Implementation
{
    public class ImplementationNotLoadedException: Exception        
    {
        public ImplementationNotLoadedException(Type t)
            : base(string.Format("Specified type {1} must be an interface and it was {0}  Implementation was not loaded for type {1}.", t.IsInterface ? "an interface.": "not an interface.", t.Name))
        { 
            
        }
    }
}
