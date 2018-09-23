/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConfigurer
    {
        /// <summary>
        /// Configures the specified configurable.
        /// </summary>
        /// <param name="configurable">The configurable.</param>
        void Configure(IConfigurable configurable);
    }
}
