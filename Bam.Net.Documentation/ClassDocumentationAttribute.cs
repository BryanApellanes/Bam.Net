/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Documentation
{
    /// <summary>
    /// Used to programatically expose
    /// documentation
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class ClassDocumentationAttribute: Attribute
    {
        public ClassDocumentationAttribute(string summary)
        {
            this.Summary = summary;
            this.ParameterDescriptions = new string[] { };
        }

        public ClassDocumentationAttribute(string summary, params string[] parameterDescriptions)
            : this(summary)
        {
            this.ParameterDescriptions = parameterDescriptions;
        }

        public string Summary { get; set; }

        public string[] ParameterDescriptions { get; set; }
    }
}
