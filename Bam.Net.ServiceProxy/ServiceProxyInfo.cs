using Bam.Net.Incubation;
using Bam.Net.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.ServiceProxy
{
    public class ServiceProxyInfo
    {
        public ServiceProxyInfo(Type type)
        {
            this.ClassName = "{0}.{1}"._Format(type.Namespace, type.Name);
            this.VarName = ServiceProxySystem.GetVarName(type);
            this.Type = type.FullName;
                
        }
        public string ClassName { get; set; }
        public string VarName { get; set; }
        public string Type { get; set; }
    }
}
