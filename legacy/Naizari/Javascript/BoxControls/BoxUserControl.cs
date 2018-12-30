/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Javascript.JsonControls;
using System.Web.UI;
using System.Reflection;
using System.Text;
using System.Web.UI;
using System.Web;
using Naizari.Helpers;
using Naizari.Extensions;

[assembly: TagPrefix("Naizari.Javascript.BoxControls", "box")]
namespace Naizari.Javascript.BoxControls
{
    public class BoxUserControl: UserControl
    {

        internal JsonClientParentProxy proxy;
        private object data;

        public BoxUserControl()
            : base()
        {
            proxy = new JsonClientParentProxy();
            
            proxy.VarName = JavascriptServer.GetDefaultVarName(this.GetType());
            this.Controls.Add(proxy);
        }

        internal void Inject(object data)
        {
            this.data = data;
        }

        public T Data<T>() where T: class
        {
            return (T)this.data;
        }

        public string Template<T, PropType>(string propertyName, string templateName) where T: class
        {
            PropType propVal = Data<T>().DataProp<T, PropType>(propertyName);
            if (propVal == null)
                return string.Empty;
            string path = BoxServer.GetVirtualDataBoxPath(HttpContext.Current, typeof(PropType), templateName);
            BoxResponse resp = BoxServer.GetBoxResponse(HttpContext.Current, path, propVal);
            return resp.Html;
        }

        public string TemplateIEnumerable<T, PropType>(string propertyName, string templateName) where T: class
        {
            string retVal = string.Empty;
            IEnumerable<PropType> arr = Data<T>().DataProp<T, IEnumerable<PropType>>(propertyName);
            if(arr == null)
                return string.Empty;
            string path = BoxServer.GetVirtualDataBoxPath(HttpContext.Current, typeof(PropType), templateName);
            foreach (PropType p in arr)
            {
                retVal += BoxServer.GetBoxResponse(HttpContext.Current, path, p).Html;
            }

            return retVal;
        }

        public void PostBoxLoad()
        {
            this.PostBoxLoad(true);
        }

        public void PostBoxLoad(bool renderScripts)
        {
            PostBoxLoad(renderScripts, string.Empty);
        }

        internal void PostBoxLoad(bool renderScripts, string requesterId)
        {
            PostBoxLoad(renderScripts, requesterId, false);
        }

        /// <summary>
        /// This method is called by the BoxServer so the JsonControls can
        /// find each other and do their necessary wiring.
        /// </summary>
        /// <param name="renderScripts">true to render scripts.</param>
        internal void PostBoxLoad(bool renderScripts, string requesterId, bool postWindowLoad)
        {
            List<JsonControl> jsonControls = GetAllJsonControls(this.Page);
            JavascriptPage page = this.Page as JavascriptPage;

            foreach (JsonControl control in jsonControls)
            {
                if (!string.IsNullOrEmpty(requesterId.Trim()))
                    control.DomId = string.Format("{0}_{1}", requesterId, control.DomId);

                if (control is Box)
                {
                    throw new BoxValidationException(this.Parent.AppRelativeTemplateSourceDirectory);
                }

                control.RenderScripts = renderScripts;
                control.PostWindowLoad = postWindowLoad;
                page.AddJsonControl(control);
            }
            page.WireJsonControls(false);
        }

        internal string[] RequiredScripts
        {
            get
            {
                List<string> returnValue = new List<string>();
                foreach(JsonControl control in GetAllJsonControls(this.Page))
                {
                    foreach (string scriptName in control.RequiredScripts)
                        returnValue.Add(scriptName);
                }
                return returnValue.ToArray();
            }
        }

        public static List<JsonControl> GetAllJsonControls(Control parent)
        {
            List<JsonControl> results = new List<JsonControl>();

            foreach (Control control in parent.Controls)
            {
                if (control is UserControl)
                {
                    results.AddRange(GetAllJsonControls(control));
                }

                if (control is JsonControl)
                {
                    JsonControl jsonControl = (JsonControl)control;
                    results.Add(jsonControl);
                }
            }

            return results;
        }

    }
}
