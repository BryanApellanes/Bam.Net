/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Helpers;
using System.Web.UI;
// BoxControls and JsonControls should probably be merged, or more logically separated
using Naizari.Javascript.BoxControls;

namespace Naizari.Javascript.JsonControls
{
    public class JsonSearcher: JsonControl
    {
        // need a textbox and a databox
        JsonTextBox inputBox;
        DataBox searchResults;
        JsonFunction attacher;
        JsonScriptObjectEventSource startSearchEvent;
        public JsonSearcher()
            : base()
        {
            this.inputBox = new JsonTextBox();
            this.searchResults = new DataBox();
            this.startSearchEvent = new JsonScriptObjectEventSource();
            this.MinimumCharacterCount = 3;
            this.attacher = new JsonFunction();
            this.attacher.ExecutionType = JavascriptExecutionTypes.OnWindowLoad;
            this.AddJsonFunction(this.attacher);
            this.AutoRegisterScript = true;
            this.Controls.Add(inputBox);
            this.Controls.Add(searchResults);
            this.Controls.Add(startSearchEvent);

            this.AddRequiredScript(typeof(JsonSearcher));//"naizari.javascript.jsoncontrols.jsonsearcher.js");
        }

        public JavascriptExecutionTypes InitializationType
        {
            get { return this.attacher.ExecutionType; }
            set { this.attacher.ExecutionType = value; }
        }
        protected override void OnPreRender(EventArgs e)
        {
            this.WireScriptsAndValidate();
            base.OnPreRender(e);
        }

        public string ResultsId
        {
            get { return this.JsonId + "_searchresults"; }
        }

        public string InputId
        {
            get { return this.JsonId + "_input"; }
        }

        public object JsonMethodProvider
        {
            get
            {
                return this.searchResults.JsonMethodProvider;
            }
            set
            {
                this.searchResults.JsonMethodProvider = value;
            }
        }

        public string ResultDivCssClass { get; set; }

        public string WorkingDomId { get; set; }
        bool wired;
        public override void WireScriptsAndValidate()
        {
            if (!wired)
            {
                wired = true;
                
                this.inputBox.JsonId = this.InputId;//this.JsonId + "_input";
                this.inputBox.Text = this.Text;
                if (!string.IsNullOrEmpty(this.CssClass))
                    this.inputBox.CssClass = this.CssClass;

                this.inputBox.WireScriptsAndValidate();

                this.searchResults.JsonId = this.ResultsId;//this.JsonId + "_searchresults";
                this.searchResults.DomId = this.ResultsId;//this.JsonId + "_searchresults";

                this.attacher.JsonId = this.JsonId + "_attacher";
                this.startSearchEvent.DomId = this.JsonId + "_startsearchevent";

                string resultSelectedFunction = "";
                if (!string.IsNullOrEmpty(this.OnResultSelected))
                    resultSelectedFunction = string.Format(", {0}", this.OnResultSelected);
                this.attacher.AppendScriptBodyFormat("JsonSearchers.Searcherfy('{0}', '{1}', '{2}', {3}{4});", this.JsonId, this.inputBox.JsonId, this.searchResults.JsonId, this.MinimumCharacterCount.ToString(), resultSelectedFunction);
                this.attacher.AppendScriptBodyFormat("JsonInput.RegisterAsSource('{0}', 'input');", this.inputBox.JsonId);
                string clickFunction = "function(){JSUI.GetElement('" + this.inputBox.JsonId + "').select();}";
                this.attacher.AppendScriptBodyFormat("JSUI.AddEventHandler('{0}', {1}, 'click');", this.inputBox.JsonId, clickFunction);

                if (string.IsNullOrEmpty(this.SearchMethodName))
                    throw ExceptionHelper.CreateException<JsonInvalidOperationException>("SearchMethodName not specified: {0}", this.ToString());

                this.searchResults.MethodName = this.SearchMethodName;
                this.searchResults.DataTemplateName = "SearchResult";
                this.searchResults.AddParameterSource(this.inputBox);

                this.searchResults.InvokeOnce = false;
                this.searchResults.HideWhileWorking = true;
                if (!string.IsNullOrEmpty(this.WorkingDomId))
                    this.searchResults.WorkingDomId = this.WorkingDomId;
                this.searchResults.PostInvokeJsonIds = string.Format("JsonSearchers['{0}'].SearchComplete", this.JsonId);
                this.searchResults.EventSourceDomIds = this.startSearchEvent.DomId;
                this.searchResults.ClientEventName = this.JsonId + "_startsearch";
                this.searchResults.CssStyle = "display: none;";
                if (!string.IsNullOrEmpty(this.ResultDivCssClass))
                    this.searchResults.CssClass = this.ResultDivCssClass;
                if (this.Parent is UserControl && this.searchResults.JsonMethodProvider == null)
                    this.searchResults.JsonMethodProvider = this.Parent;
                else if(this.searchResults.JsonMethodProvider == null)
                    this.searchResults.JsonMethodProvider = this.ParentJavascriptPage;

                this.searchResults.WireScriptsAndValidate();
                foreach (JsonFunction func in this.searchResults.Scripts)
                {
                    this.AddJsonFunction(func);
                }


                //    <box:DataBox ID="globalSearchResults" JsonId="globalSearchResults" 
                //            DomId="globalSearchResults"
                //            EventSourceDomIds="GlobalSearchEventSource"
                //            ClientEventName="StartGlobalSearch"
                //            MethodName="Search"
                //            DataTemplateName="SearchResult"
                //            ParameterSourceJsonIds="jsonTextBoxGlobalSearch" 
                //            InvokeOnce="false"                                    
                //            HideWhileWorking="true"
                //            CssStyle="display:none; position: absolute; top: 24px; left: 750px; z-index: 99; width: 200px; border: 1px solid black; background-color: white; text-align: left;"
                //            WorkingDomId="globalSearchWorking"
                //            PostInvokeJsonIds="postGlobalSearch"
                //            runat="server">
                //    </box:DataBox>
                
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            this.inputBox.RenderScripts = this.RenderScripts;
            this.searchResults.RenderScripts = this.RenderScripts;
            this.startSearchEvent.RenderScripts = this.RenderScripts;

            
            this.inputBox.RenderControl(writer);
            this.startSearchEvent.RenderControl(writer);
            this.searchResults.RenderControl(writer);            

            if (this.RenderScripts)
            {
                this.inputBox.RenderConglomerateScript(writer);
                this.searchResults.RenderConglomerateScript(writer);
                this.startSearchEvent.RenderConglomerateScript(writer);
                this.RenderConglomerateScript(writer);
            }
        }

        public string SearchMethodName { get; set; }
        public int MinimumCharacterCount { get; set; }
        public string OnResultSelected { get; set; }
    }
}
