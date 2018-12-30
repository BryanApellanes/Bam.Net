/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.ServiceProxy
{
    internal class GeneratedServiceProxyInfo
    {
        public GeneratedServiceProxyInfo()
        {
            this.HeaderFormat = @"/**
This file was generated from {0}serviceproxy/csharpproxies.  This file should not be modified directly
**/

";

        }
        public StringBuilder Header { get; private set; }
        public string HeaderFormat { get; private set; }
        public StringBuilder Using { get; private set; }
        public string UsingFormat { get; private set; }

        public StringBuilder Namespace { get; private set; }
        public string NamespaceFormat { get; private set; }
        public StringBuilder Class { get; private set; }
        public string ClassFormat { get; set; }
        public StringBuilder SecureClass { get; private set; }
        public string SecureClassFormat { get; private set; }
        public StringBuilder Interface { get; private set; }
        public string InterfaceFormat { get; private set; }
    }
}
