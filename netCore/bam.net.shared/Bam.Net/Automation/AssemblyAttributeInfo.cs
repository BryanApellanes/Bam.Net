using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Automation.Nuget;
using Bam.Net;

namespace Bam.Net.Automation
{
    public class AssemblyAttributeInfo
    {
        /// <summary>
        /// The name of the assembly attribute to set
        /// </summary>
        public string AttributeName { get; set; }

        /// <summary>
        /// The name of the nuspec property to set the value to or null
        /// </summary>
        public string NuspecProperty { get; set; }

        string _value;
        public string Value
        {
            get
            {
                if(!string.IsNullOrEmpty(NuspecProperty) && NuspecFile != null)
                {
                    return NuspecFile.Property<string>(NuspecProperty, false).Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ").Trim();
                }

                return _value;
            }
            set
            {
                _value = value;
            }
        }
        public string AssemblyAttribute
        {
            get
            {
                return "[assembly: {AttributeName}(\"{Value}\")]".NamedFormat(this);
            }
        }

        public string StartsWith
        {
            get
            {
                return "[assembly: {AttributeName}".NamedFormat(this);
            }
        }
        public NuspecFile NuspecFile { get; set; }
        internal bool WroteInfo { get; set; }
    }
}
