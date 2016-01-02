/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.IO;

namespace Bam.Net.Dust
{
    public class DustViewEngine: IViewEngine
    {
        #region IViewEngine Members

        public ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            return GetResult(controllerContext, partialViewName);
        }
        
        public ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            return GetResult(controllerContext, viewName, masterName);
        }

        public void ReleaseView(ControllerContext controllerContext, IView view)
        {
            // no need for disposal here
        }

        #endregion

        private ViewEngineResult GetResult(ControllerContext controllerContext, string viewName, string masterName = null)
        {
            string controllerName = controllerContext.RouteData.GetRequiredString("controller");
            string templateName = string.Format("{0}.{1}", controllerName, viewName);
            if (Dust.TemplateIsRegistered(templateName))
            {
                DustView view = new DustView(templateName);
                return new ViewEngineResult(view, this);
            }
            else if (Dust.TemplateIsRegistered(viewName))
            {
                DustView view = new DustView(viewName);
                return new ViewEngineResult(view, this);
            }
            else
            {
                return new ViewEngineResult(new string[] { Dust.DustRoot });
            }            
        }
    }
}
