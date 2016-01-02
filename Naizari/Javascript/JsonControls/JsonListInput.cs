/*
	Copyright © Bryan Apellanes 2015  
*/
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Naizari.Extensions;

[assembly: TagPrefix("Naizari.Javascript.JsonControls", "json")]
namespace Naizari.Javascript.JsonControls
{
    public class JsonListInput: JsonInput
    {
        JsonFunction initializer;
        List<JsonDataItem> initialItems;
        public JsonListInput()
            : base()
        {
            this.initializer = new JsonFunction();
            this.initializer.ExecutionType = JavascriptExecutionTypes.OnWindowLoad;
            this.initialItems = new List<JsonDataItem>();
            this.AllowRemove = true;
            this.AutoRegisterScript = true;
        }

        public override string JsonId
        {
            get
            {
                return base.JsonId;
            }
            set
            {
                base.JsonId = value;
                this.InputJsonId = value;
            }
        }

        /// <summary>
        /// Gets or sets the text of the delete link for each row.
        /// </summary>
        public string RemoveRowText
        {
            get;
            set;
        }

        public void AddItem(JsonDataItem item)
        {
            this.initialItems.Add(item);
        }

        public JsonDataItem[] InitialItems
        {
            get { return this.initialItems.ToArray(); }
            set
            {
                this.initialItems.Clear();
                this.initialItems.AddRange(value);
            }
        }

        public bool AllowRemove
        {
            get;
            set;
        }

        public override void WireScriptsAndValidate()
        {
            this.initializer.JsonId = this.JsonId + "_init";
            StringBuilder script = new StringBuilder();
            script.AppendFormat("var {0} = ListInput.New('{0}', '{0}');", this.JsonId);
            script.AppendFormat("{0}.Style.width = '100%';", this.JsonId);
            if (!string.IsNullOrEmpty(this.JsonProperty))
                script.AppendFormat("{0}.JsonProperty = '{1}';", this.JsonId, this.JsonProperty);
            if (!string.IsNullOrEmpty(this.RemoveRowText) && this.AllowRemove)
                script.AppendFormat("{0}.RemoveRowText = '{1}';", this.JsonId, this.RemoveRowText);
            if (!this.AllowRemove)
                script.AppendFormat("{0}.RemoveRowText = '';", this.JsonId);

            foreach(JsonDataItem item in this.initialItems)
            {
                script.AppendFormat("{0}.AddItem({1}Text: \"{2}\", Value: \"{3}\", Html: \"{4}\"{5});", this.JsonId, "{", 
                    item.Text, 
                    item.Value, 
                    item.Html,
                    "}"); 
            }

            this.initializer.FunctionBody = script.ToString();
            this.AddJsonFunction(this.initializer);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            JsonControl.ApplyAttributesAndStyles(this.controlToRender, this.Styles, this.Attributes, this.CssClass);
            this.controlToRender.TagName = "DIV";
            this.controlToRender.Attributes["jsonid"] = this.JsonId;
            this.controlToRender.Attributes["id"] = this.DomId;
            this.controlToRender.Attributes["jsonproperty"] = this.JsonProperty;
            this.controlToRender.Attributes["type"] = "ListInput";
            this.controlToRender.RenderControl(writer);
            if (this.RenderScripts)
                this.RenderConglomerateScript(writer);
        }
    }
}
