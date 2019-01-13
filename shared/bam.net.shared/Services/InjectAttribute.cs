using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Services
{
    [AttributeUsage(AttributeTargets.Property)]
    public class InjectAttribute: Attribute
    {
        public InjectAttribute() { }
        public InjectAttribute(Type typeToUse, bool required = false)
        {
            TypeToUse = typeToUse;
            Required = required;
        }

        public Type TypeToUse { get; set; }
        public bool Required { get; set; }
    }
}
