/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Extensions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

[assembly: TagPrefix("Naizari.Javascript.JsonControls", "json")]
namespace Naizari.Javascript.JsonControls
{
    internal class JsonMessageBox: JsonControl
    {
        string titleId;
        string boxId;
        string messageId;

        internal JsonMessageBox()
            : base()
        {
            titleId = StringExtensions.RandomString(8, false, false);
            boxId = StringExtensions.RandomString(8, false, false);
            messageId = StringExtensions.RandomString(8, false, false);
            Title = "Set the title using Page.MessageBoxTitle = \"your title\"";
            
            this.AddRequiredScript("naizari.javascript.jsui.messagebox.js");
            this.AutoRegisterScript = true;
        }

        internal string Title
        {
            get;
            set;
        }

        internal string BoxCssClass { get; set; }
        internal string TitleCssClass { get; set; }
        internal string MessageCssClass { get; set; }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            controlToRender.TagName = "div";
            controlToRender.Attributes.CssStyle.Add("display", "none");

            if (!string.IsNullOrEmpty(BoxCssClass))
                controlToRender.Attributes.Add("class", BoxCssClass);
            else
            {
                controlToRender.Style.Add("width", "450px");
                controlToRender.Style.Add("height", "250px");
                controlToRender.Style.Add("background-color", "#FFFFFF");
                controlToRender.Style.Add("border", "1px solid #000000");
            }

            controlToRender.ID = boxId;

            HtmlGenericControl title = new HtmlGenericControl("div");
            title.ID = titleId;
            title.InnerHtml = Title;
            if (!string.IsNullOrEmpty(TitleCssClass))
            {
                title.Attributes.Add("class", TitleCssClass);
            }
            else
            {
                title.Style.Add("background-color", "#0000FF");
                title.Style.Add("border", "1px solid #000000");
                title.Style.Add("margin", "1px");
                title.Style.Add("padding", "3px");
            }

            
            
            controlToRender.Controls.Add(title);

            HtmlGenericControl messageText = JavascriptPage.CreateDiv(messageId);

            if (!string.IsNullOrEmpty(this.MessageCssClass))
            {
                messageText.Attributes.Add("class", MessageCssClass);
            }
            else
            {
                messageText.Style.Add("height", "175px");
            }

            controlToRender.Controls.Add(messageText);

            JsonFunction registration = new JsonFunction();
            registration.ExecutionType = JavascriptExecutionTypes.OnWindowLoad;
            string body = "\r\nif(MessageBox != 'undefined'){";
            body += string.Format("\tMessageBox.RegisterMessageBox('{0}','{1}','{2}');", boxId, titleId, messageId);
            body += "}";
            registration.FunctionBody = body;

            controlToRender.RenderControl(writer);
            registration.RenderControl(writer);

            //    <div id="messageDiv" style="display: none; border:1px solid black; background-color: White; width: 400px;" class="modal">
            //        <div id="messageDrag" style="background-color: #FFCC99; padding: 3px; border:1px solid black; "><b>Patterns Setup Message</b></div>
            //        <div id="messageText"></div><br /><br />        
            //    </div>
        }


    }
}
