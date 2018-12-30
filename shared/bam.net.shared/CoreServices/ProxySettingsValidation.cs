using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    public class ProxySettingsValidation
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public MethodInfo[] NonVirtualMethods { get; set; }                
    }
}
