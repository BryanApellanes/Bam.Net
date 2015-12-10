/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Configuration;

namespace Naizari.Service
{
    [Serializable]
    public class SoapConfig: IHasRequiredProperties
    {
        public string Port { get; set; }

        #region IHasRequiredProperties Members

        public string[] RequiredProperties
        {
            get { return new string[] { "Port" }; }
        }

        #endregion    
    }
}
