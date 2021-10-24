using System;
using System.IO;
using System.Text;

using Bam.Net.Presentation.Html;

namespace Bam.Net.Server.Renderers
{
    // TODO: implement this, see fx implementation for reference.  Fx version uses TagBuilder from the Mvc framework
    // new implementation should not depend on Mvc Framework

    /// <summary>
    /// The renderer used to render a template for 
    /// a given object.  In other words writes
    /// the default template for an object.
    /// </summary>    
    public partial class CommonTemplateRenderer : WebRenderer // core
    {
        protected internal static string FieldSetFor(string json, string legendText = null, object wrapperAttrs = null, bool setValues = false)
        {
            throw new NotImplementedException("CommonTemplateRender.FieldSetFor is not implemented for the current platform");
        }

        protected internal static string FieldSetFor(dynamic obj, string legendText, object wrapperAttrs = null, bool setValues = false)
        {
            throw new NotImplementedException("CommonTemplateRender.FieldSetFor is not implemented for the current platform");
        }

        public static string FieldSetFor(Type type, object defaults = null, string legendText = null, object wrapperAttrs = null)
        {
            throw new NotImplementedException("CommonTemplateRender.FieldSetFor is not implemented for the current platform");
        }

        protected internal static string InputsFor(Type type, object defaults = null, object wrapperAttrs = null)
        {
            throw new NotImplementedException("CommonTemplateRender.FieldSetFor is not implemented for the current platform");
        }
    }
}
