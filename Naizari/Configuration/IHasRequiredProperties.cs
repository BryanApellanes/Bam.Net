/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace Naizari.Configuration
{
    /// <summary>
    /// This interface is intended to be implemented by classes
    /// who wish to use the DefaultConfiguration class and 
    /// CascadeConfiguration class to set its properties.
    /// </summary>
    public interface IHasRequiredProperties
    {
        /// <summary>
        /// When implemented in a derived class should
        /// return an array of strings containing the 
        /// name of each property of the object which 
        /// is required to be set prior to execution.
        /// </summary>
        string[] RequiredProperties { get; }
    }
}
