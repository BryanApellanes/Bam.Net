using System;
using System.IO;
using System.Text;
using System.Web.Mvc;
using Bam.Net.Presentation.Html;

namespace Bam.Net.Server.Renderers
{
    /// <summary>
    /// The renderer used to render a template for 
    /// a given object.  In other words writes
    /// the default template for an object.
    /// </summary>
    public partial class CommonTemplateRenderer : Renderer // fx
    {
        protected internal static MvcHtmlString FieldSetFor(string json, string legendText = null, object wrapperAttrs = null, bool setValues = false)
        {
            return HtmlHelperExtensions.FieldsetFor(null, json, legendText, wrapperAttrs, setValues);
        }

        protected internal static MvcHtmlString FieldSetFor(dynamic obj, string legendText, object wrapperAttrs = null, bool setValues = false)
        {
            return HtmlHelperExtensions.FieldsetFor(null, obj, legendText, wrapperAttrs, setValues);
        }

        public static MvcHtmlString FieldSetFor(Type type, object defaults = null, string legendText = null, object wrapperAttrs = null)
        {
            return HtmlHelperExtensions.FieldSetFor(null, type, defaults, legendText, wrapperAttrs);
        }

        protected internal static MvcHtmlString InputsFor(Type type, object defaults = null, object wrapperAttrs = null)
        {
            return HtmlHelperExtensions.InputsFor(null, type, defaults, wrapperAttrs);
        }
    }
}
