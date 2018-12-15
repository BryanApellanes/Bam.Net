/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Javascript.JsonControls;
using System.Web.UI;
using Naizari.Helpers.Web;
using Naizari.Extensions;

namespace Naizari.Javascript.BoxControls
{
    internal class UsingResourceScriptChain: Control
    {
        List<string> scriptNames;
        public UsingResourceScriptChain()
        {
            this.scriptNames = new List<string>();
            this.FunctionBody = string.Empty;
        }

        public void AddScript(string script)
        {
            this.scriptNames.Add(script);
        }

        public void AddScripts(string[] scripts)
        {
            this.scriptNames.AddRange(scripts);
        }

        public string[] ScriptNames
        {
            get { return this.scriptNames.ToArray(); }
            set
            {
                this.scriptNames.Clear();
                this.scriptNames.AddRange(value);
            }
        }

        public string FunctionBody { get; set; }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.WriteLine("JSUI.Conf.usingResource([" + StringExtensions.ToDelimited(this.ScriptNames, ",", true) + "], function(scrArr){");
            writer.WriteLine(this.FunctionBody);
            writer.WriteLine("});");            
        }

    }
}
