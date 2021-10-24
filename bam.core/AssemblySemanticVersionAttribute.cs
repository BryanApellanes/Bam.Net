using System;

namespace Bam.Net
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class AssemblySemanticVersionAttribute : Attribute
    {
        public AssemblySemanticVersionAttribute(string version)
        {
            Version = version;
        }
        
        public string Version { get; set; }
    }
}