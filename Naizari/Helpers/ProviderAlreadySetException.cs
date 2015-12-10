/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Helpers
{
    public class ProviderAlreadySetException: CustomException 
    {
        public ProviderAlreadySetException(string message)
            : base(message)
        {
        }

        public ProviderAlreadySetException(string message, params object[] args) 
            : this(string.Format(message, args))
        {
        }

        public ProviderAlreadySetException(Type t)
            : this("The provider of type {0} has already been set", t.Name)
        {
        }
    }
}
