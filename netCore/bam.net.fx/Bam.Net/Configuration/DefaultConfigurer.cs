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
    /// Configures IConfigurables by using DefaultConfiguration
    /// </summary>
    public class DefaultConfigurer: IConfigurer
    {
        public void Configure(IConfigurable configurable)
        {
            DefaultConfiguration.SetProperties(configurable);
            DefaultConfiguration.CheckRequiredProperties(configurable);
        }
    }
}
