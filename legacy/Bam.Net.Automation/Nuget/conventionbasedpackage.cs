/*
	Copyright © Bryan Apellanes 2015  
*/
using System.Xml.Serialization;

namespace Bam.Net.Automation.Nuget
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/packaging/2011/08/nuspec.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(ElementName = "package", Namespace = "http://schemas.microsoft.com/packaging/2011/08/nuspec.xsd", IsNullable = false)]
    public partial class conventionbasedpackage
    {

        private packageMetadata metadataField;

        private packageFile[] filesField;

        /// <remarks/>
        public packageMetadata metadata
        {
            get
            {
                return this.metadataField;
            }
            set
            {
                this.metadataField = value;
            }
        }
    }
}