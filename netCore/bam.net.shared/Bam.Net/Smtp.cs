/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.Configuration;

namespace Bam.Net
{
    public class Smtp: IHasRequiredProperties
    {
        public Smtp()
        {
            try
            {
                DefaultConfiguration.SetProperties(this, true);
            }
            catch (RequiredPropertyNotSetException e)
            {
                this.Exception = e;
            }
        }

        public Exception Exception
        {
            get;
            set;
        }

        public bool Success
        {
            get { return Exception == null; }
        }

        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password{get;set;}

        #region IHasRequiredProperties Members

        public string[] RequiredProperties
        {
            get { return new string[] { "Host", "UserName", "Password" }; }
        }

        #endregion
    }
}
