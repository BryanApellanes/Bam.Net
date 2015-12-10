/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Javascript.JsonControls;
using Naizari.Helpers.Web;

namespace Naizari.Javascript.Test
{
    public class QUnitPage : JavascriptPage
    {
        public QUnitPage()
            : base()
        {
            JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.qunit.js");
            this.Title = "QUnit Tests";
        }

        protected override void OnPreRender(EventArgs e)
        {
            this.Controls.AddAt(0, ControlHelper.CreateControl("h1", "qunit-header", this.Title));
            this.Controls.AddAt(1, ControlHelper.CreateControl("h2", "qunit-banner", string.Empty));
            this.Controls.AddAt(2, ControlHelper.CreateControl("ol", "qunit-tests", string.Empty));
            base.OnPreRender(e);
        }
    }
}
