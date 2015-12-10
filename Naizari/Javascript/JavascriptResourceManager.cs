/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Javascript.JsonControls;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Reflection;

namespace Naizari.Javascript
{
    /// <summary>
    /// The JavascriptPageManager is responsible for writing script tags to the
    /// containing page that are retrieved from the JavascriptServer.  This class
    /// exists to separate script loading/registration logic from the page.
    /// </summary>
    public class JavascriptResourceManager
    {
        List<string> coreScripts;
        List<string> resourceHeaderScripts;
        List<string> resourceBodyScripts;
        List<string> siteHeaderScripts;
        List<string> siteBodyScripts;
        JavascriptPage page;

        Dictionary<string, JsonFunction> javascriptFunctions;

        public JavascriptResourceManager()
        {
            coreScripts = new List<string>();
            resourceHeaderScripts = new List<string>();
            resourceBodyScripts = new List<string>();
            siteHeaderScripts = new List<string>();
            siteBodyScripts = new List<string>();

            javascriptFunctions = new Dictionary<string, JsonFunction>();

            Resources.Load(Assembly.GetExecutingAssembly());
            //childControlsCreated = false;
        }

        public JavascriptResourceManager(JavascriptPage page)
            : this()
        {
            this.page = page;
            this.page.JavascriptResourceManager = this;            
        }

        public void ClearCoreScripts()
        {
            this.coreScripts.Clear();
        }

        public JsonFunction this[string functionJsonId]
        {
            get
            {
                if (javascriptFunctions.ContainsKey(functionJsonId))
                    return javascriptFunctions[functionJsonId];

                return null;
            }
        }

        public void LoadResourceScripts(Assembly assemblyToLoadFrom)
        {
            Resources.Load(assemblyToLoadFrom);
        }

        public void AddCoreScript(string scriptName)
        {
            if (!coreScripts.Contains(scriptName))
                coreScripts.Add(scriptName);
        }

        /// <summary>
        /// Include the specified script in the head section of the page.
        /// </summary>
        /// <param name="resourceQualifiedScriptName">The fully qualified resource name.  For
        /// example if you have a project "A" and a script in the folder /scripts/support.js the
        /// the name would be a.scripts.support.js</param>
        public void AddResourceHeaderScript(string resourceQualifiedScriptName)
        {
            //Resources.JavaScriptFriendlyNames
            if (!resourceHeaderScripts.Contains(resourceQualifiedScriptName))
                resourceHeaderScripts.Add(resourceQualifiedScriptName);
        }

        public void AddResourceBodyScript(string scriptName)
        {
            if (!resourceBodyScripts.Contains(scriptName))
                resourceBodyScripts.Add(scriptName);
        }

        public void AddSiteHeaderScript(string pathRelativeToApplicationRoot)
        {
            if (!siteHeaderScripts.Contains(pathRelativeToApplicationRoot))
                siteHeaderScripts.Add(pathRelativeToApplicationRoot);
        }

        public void AddSiteBodyScript(string pathRelativeToApplicationRoot)
        {
            if (!siteBodyScripts.Contains(pathRelativeToApplicationRoot))
                siteBodyScripts.Add(pathRelativeToApplicationRoot);
        }


        public void WritePageScripts()
        {
            JavascriptPage target = this.page;
            // add core scripts.  the core script will always go in the header
            foreach (string scriptName in coreScripts)
            {
                if (target.Header != null)
                    target.Header.Controls.AddAt(0, CreateResourceScriptControlFromQualifiedName(scriptName));
            }

            // add header resource scripts
            foreach (string scriptName in resourceHeaderScripts)
            {
                if (target.Header != null)
                    target.Header.Controls.Add(CreateResourceScriptControlFromQualifiedName(scriptName));
            }
            // add body resource scripts
            foreach (string scriptName in resourceBodyScripts)
            {
                if (target.Form != null)
                    target.Form.Controls.Add(CreateResourceScriptControlFromQualifiedName(scriptName));
            }

            // add header site scripts
            foreach (string scriptPath in siteHeaderScripts)
            {
                if (target.Header != null)
                    target.Header.Controls.Add(CreateScriptControl(target.Request.ApplicationPath + scriptPath));
            }
            // add body site scripts
            foreach (string scriptPath in siteBodyScripts)
            {
                if (target.Form != null)
                    target.Form.Controls.Add(CreateScriptControl(target.Request.ApplicationPath + scriptPath));
            }

            
        }

        //internal Control CreateResourceScriptControl(string scriptName)
        //{
        //    string actualScriptName = Resources.JavaScriptFriendlyNames[scriptName];
        //    return CreateResourceScriptControlFromQualifiedName(actualScriptName);
        //}

        internal Control CreateResourceScriptControlFromQualifiedName(string namespaceQualifiedScriptName)
        {            
            HtmlGenericControl script = CreateScriptControl(JavascriptServer.GetResourceScriptPath(this.page.Request.Url, namespaceQualifiedScriptName));
            return script;
        }

        internal static HtmlGenericControl CreateScriptControl(string srcUrl)
        {
            HtmlGenericControl script = new HtmlGenericControl("script");
            script.Attributes.Add("type", "text/javascript");
            script.Attributes.Add("src", srcUrl);
            return script;
        }


    }
}
