/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Naizari.Helpers.Web;

namespace Naizari.Javascript.JsonControls
{
    public class JsonSearchInput: JsonHiddenInput
    {
        JsonSearcher searcher;
        JsonFunction resultSelectedListener;
        JsonFunction blurListener;
        JsonHiddenInput hiddenTextValue;
        public JsonSearchInput()
            : base()
        {
            this.searcher = new JsonSearcher();
            this.Controls.Add(searcher);
            this.resultSelectedListener = new JsonFunction();
            this.resultSelectedListener.ExecutionType = JavascriptExecutionTypes.Call;
            this.CanEdit = true;
            this.resultIdClass = "objectid";
            //this.resultTypeClass = "objecttype";
            this.resultTextClass = "objecttext";

            this.blurListener = new JsonFunction();
            this.blurListener.ExecutionType = JavascriptExecutionTypes.OnParse;

            
            this.AddJsonFunction(this.resultSelectedListener);
            
            this.AddJsonFunction(this.blurListener);

            this.hiddenTextValue = new JsonHiddenInput();
            this.AddRequiredScript(typeof(JsonSearcher));//"naizari.javascript.jsoncontrols.jsonsearcher.js");
        }

        public string OnResultSelected
        {
            get { return this.searcher.OnResultSelected; }
            set { this.searcher.OnResultSelected = value; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            this.WireScriptsAndValidate();
            base.OnPreRender(e);
        }

        public int MinimumCharacterCount
        {
            get { return this.searcher.MinimumCharacterCount; }
            set { this.searcher.MinimumCharacterCount = value; }
        }

        string resultIdClass;
        /// <summary>
        /// The css class name applied to the input element which represents the significant 
        /// value of each result.  The css class specified here should be defined in the search result 
        /// template used for the return type of the SearchMethodName. For example,
        /// &lt;input id="employeeId_$$EmployeeID$$" class="objectid" type="hidden" value="$$EmployeeID$$" /&gt;
        /// &lt;input id="employeeType_$$EmployeeID$$" class="objecttype" type="hidden" value="employee" /&gt;
        /// </summary>
        public string ResultIdClass
        {
            get{return this.resultIdClass;}
            set{this.resultIdClass = value;}
            
        }

        public string WorkingDomId
        {
            get;
            set;
        }

        public string ResultDivCssClass
        {
            get;
            set;
        }

        string resultTextClass;
        /// <summary>
        /// Used to identify the selcted text in the selected result element.
        /// </summary>
        public string ResultTextClass
        {
            get { return this.resultTextClass; }
            set { this.resultTextClass = value; }
        }
        
        public string SearchMethodName
        {
            get{return this.searcher.SearchMethodName;}
            set{this.searcher.SearchMethodName = value;}
        }

        public override string InputJsonId
        {
            get
            {
                return this.JsonId;
            }
            set
            {
                this.JsonId = value;
            }
        }
        
        bool wired;
        public override void WireScriptsAndValidate()
        {
            this.hiddenTextValue.JsonId = this.JsonId + "_text";
            this.hiddenTextValue.Value = this.Text;

            if (this.CanEdit && this.Visible)
            {
                if (!wired)
                {
                    wired = true;
                    this.resultSelectedListener.JsonId = this.JsonId + "_result_selected";
                    this.blurListener.JsonId = this.JsonId + "_blur";
                    this.searcher.JsonId = this.JsonId + "_searcher";
                    this.searcher.OnResultSelected = this.resultSelectedListener.JsonId;
                    this.searcher.JsonMethodProvider = this.Parent;
                    this.searcher.InitializationType = JavascriptExecutionTypes.OnParse;
                    this.searcher.CssClass = this.CssClass;
                    this.searcher.Text = this.Text;
                    this.searcher.WorkingDomId = this.WorkingDomId;
                    this.searcher.ResultDivCssClass = this.ResultDivCssClass;

                   

                    this.resultSelectedListener.AddParameter("selectedElement");
                    this.resultSelectedListener.AppendScriptBody("try{");
                    this.resultSelectedListener.AppendScriptBodyFormat("JSUI.HideElement('{0}');", this.searcher.ResultsId);
                    this.resultSelectedListener.AppendScriptBodyFormat("var idElement = JSUI.GetElementsByClassName(selectedElement, '{0}')[0];", this.ResultIdClass);
                    //this.resultSelectedListener.AppendScriptBodyFormat("var typeElement = JSUI.GetElementsByClassName(selectedElement, '{0}')[0];", this.ResultTypeClass);
                    this.resultSelectedListener.AppendScriptBodyFormat("var textElement = JSUI.GetElementsByClassName(selectedElement, '{0}')[0];", this.ResultTextClass);
                    this.resultSelectedListener.AppendScriptBody("var textValue = JSUI.GetTextContent(textElement).trim();");
                    this.resultSelectedListener.AppendScriptBodyFormat("JSUI.GetElement('{0}').value = idElement.value;", this.JsonId);
                    this.resultSelectedListener.AppendScriptBodyFormat("JSUI.GetElement('{0}').value = textValue;", this.searcher.InputId);
                    this.resultSelectedListener.AppendScriptBodyFormat("JSUI.GetElement('{0}').value = textValue;", this.hiddenTextValue.JsonId);
                    this.resultSelectedListener.AppendScriptBody("}catch(e){};");

                    this.searcher.WireScriptsAndValidate();
                    foreach (JsonFunction function in this.searcher.Scripts)
                    {
                        this.AddJsonFunction(function);
                    }

                   
                    

                    string blurFunc = @"function(){" +
                        "JSUI.GetElement('" + this.searcher.InputId + "').value = JSUI.GetElement('" + this.hiddenTextValue.JsonId + "').value;}";
                    this.blurListener.AppendScriptBodyFormat("JSUI.AddEventHandler('{0}', {1}, 'blur');", this.searcher.InputId, blurFunc);
                }
            }
            else
            {
                this.searcher.Visible = false;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            this.hiddenTextValue.RenderControl(writer);
            if (this.CanEdit)
            {
                this.searcher.RenderScripts = this.RenderScripts;
                this.searcher.RenderControl(writer);

                base.Render(writer);
            }
            else
            {
                JsonHiddenInput readOnlyValue = new JsonHiddenInput();
                readOnlyValue.JsonId = this.JsonId;
                readOnlyValue.Value = this.Value;
                readOnlyValue.RenderControl(writer);
                CreateRegistrationScript();
                ControlHelper.NewSpan(this.Text, this.CssClass).RenderControl(writer);
            }
            

            

            //if (this.RenderScripts)
            //{
            //    this.RenderConglomerateScript(writer);
            //    //this.searcher.RenderConglomerateScript(writer);
            //}
        }
    }
}
