/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Data.Schema
{
    public class NamespaceNotSpecifiedException: Exception
    {
        public NamespaceNotSpecifiedException() : base("Namespace was not specified") { }
    }
}
