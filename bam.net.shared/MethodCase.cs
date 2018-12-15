/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net
{
    /// <summary>
    /// Determines whether generated JavaScript methods 
    /// will be camel case (camel has his head down; first letter lowercase)
    /// or pascal case 
    /// </summary>
    public enum MethodCase
    {
        Invalid,
        CamelCase,
        PascalCase,
        Both
    }
}
