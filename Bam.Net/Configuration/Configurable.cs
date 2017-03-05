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
    public abstract class Configurable: IConfigurable, IHasRequiredProperties
    {
        /// <summary>
        /// When implemented in a derived class should
        /// return an array of strings containing the 
        /// name of each property of the object which 
        /// are required to be set prior to execution.
        /// </summary>
        public abstract string[] RequiredProperties { get; set; }

        #region IConfigurable Members

        public virtual void Configure(IConfigurer configurer)
        {
            configurer.Configure(this);
            this.CheckRequiredProperties();
        }

        public virtual void Configure(object configuration)
        {
            this.CopyProperties(configuration);
            this.CheckRequiredProperties();
        }

        #endregion
    }
}
