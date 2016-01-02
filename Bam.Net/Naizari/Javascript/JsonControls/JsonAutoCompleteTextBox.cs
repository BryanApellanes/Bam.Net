/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KLGates.Javascript.BoxControls;
using System.Web.UI;
using KLGates.Helpers;
using KLGates.Test;

namespace KLGates.Javascript.JsonControls
{
    /// <summary>
    /// An auto complete textbox.
    /// </summary>
    [Obsolete("This component has a number of bugs that manifest mostly in IE.  It should be refactored using *Init.js style initialization.")]
    public class JsonAutoCompleteTextBox: DataBox, IJsonInput
    {
        JsonTextBox input;
        JsonFunction preSearch;
        JsonFunction postSearch;
        JsonFunction attacher;
        JsonScriptObjectEventSource eventSource;
        JsonImageSequence images;

        public JsonAutoCompleteTextBox()
            : base()
        {
            this.input = new JsonTextBox();
            this.DataTemplateName = "AutoComplete";
            this.images = new JsonImageSequence();
            this.eventSource = new JsonScriptObjectEventSource();
            
            this.AddJsonFunction(this.eventSource);

            this.preSearch = new JsonFunction();
            this.preSearch.ExecutionType = JavascriptExecutionTypes.Call;
            
            this.postSearch = new JsonFunction();
            this.postSearch.ExecutionType = JavascriptExecutionTypes.Call;
            
            this.attacher = new JsonFunction();
            this.attacher.ExecutionType = JavascriptExecutionTypes.OnWindowLoad;
            

            this.MinimumInputLength = 2;

            this.Styles["display"] = "none";
            this.ItemTagName = "div";
            this.InvokeOnce = false;
        }

        public string IdleImage { get; set; }
        public string WorkingImage { get; set; }

        public string ImageCssClass { get; set; }
        public string TextBoxCssClass { get; set; }
        public string TextBoxStyle { get; set; }
        public string ResultsStyle { get; set; }
        public string ResultsCssClass
        {
            get
            {
                return this.CssClass;
            }
            set
            {
                this.CssClass = value;
            }
        }

        /// <summary>
        /// Gets or sets the tagname to expect results to be wrapped in.  The default
        /// is 'div'.
        /// </summary>
        public string ItemTagName { get; set; }

        /// <summary>
        /// Gets or sets the background color to use when an item is highlighted.
        /// </summary>
        public string HighlightBackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the text color to use when an item is highlighted.
        /// </summary>
        public string HighlightForeColor { get; set; }

        /// <summary>
        /// Gets or sets the text color to use when an item is not highlighted.
        /// </summary>
        public string ForeColor { get; set; }

        /// <summary>
        /// Gets or sets the background color to use when an item is not highlighted.
        /// </summary>
        public string BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the mininimum number of characters accepted before a search is initiated.
        /// </summary>
        public int MinimumInputLength { get; set; }

        public JsonFunction SelectHandler { get; set; }

        public string SelectHandlerJsonId { get; set; }

        public override void WireScriptsAndValidate()
        {
            if (!string.IsNullOrEmpty(this.ImageCssClass))
                this.images.CssClass = this.ImageCssClass;

            if (!string.IsNullOrEmpty(this.TextBoxCssClass))
                this.input.CssClass = this.TextBoxCssClass;
            
            this.eventSource.JsonId = this.JsonId + "_eventsource";
            this.eventSource.DomId = this.JsonId + "_eventsource";
            this.eventSource.ClientEventName = this.JsonId + "_eventname";
            
            #region setup images
            this.images.JsonId = this.JsonId + "_images";
            if (!string.IsNullOrEmpty(this.IdleImage))
            {
                this.images.InitialImageSource = this.IdleImage;
                //this.images.AddImage(this.JsonId + "_idleimage", this.IdleImage);
            }

            if (!string.IsNullOrEmpty(this.WorkingImage))
            {
                this.images.AddImage(this.JsonId + "_workingimage", this.WorkingImage);
            }
            
            #endregion

            this.input.JsonId = this.JsonId + "_input";
            this.input.DomId = this.input.JsonId;

            this.EventSourceDomIds = this.eventSource.DomId;

            this.preSearch.JsonId = this.JsonId + "_presearch";
            this.preSearch.Prepend = "var " + this.JsonId + "_currentinput = '';";
            this.preSearch.FunctionBody = @" var searchBox = JSUI.GetElement('"+ this.input.JsonId + @"');
    if(searchBox.value.length < " + this.MinimumInputLength + @"){
        return false;
    }
    else{
        if(searchBox.value != " + this.JsonId + @"_currentinput){
            " + this.JsonId + @"_currentinput = searchBox.value;
            JSUI.GetImageSequence('" + this.images.DomId + @"').SetImageIndex(1);            
            JSUI.FireEvent('" + this.eventSource.DomId + @"', '" + this.eventSource.ClientEventName + @"');            
        }
    }";
            this.AddJsonFunction(this.preSearch);

            this.postSearch.JsonId = this.JsonId + "_postsearch";
            string postSearchFunctionBody = "JSUI.GetImageSequence('" + this.images.JsonId + @"').SetImageIndex(0);
var resultsList = new ArrowList.ArrowListClass('" + this.DomId +@"','" + this.input.DomId + @"', '" + this.ItemTagName + @"');";
            if (!string.IsNullOrEmpty(this.HighlightBackgroundColor))
                postSearchFunctionBody += "resultsList.HighlightBackgroundColor = '" + this.HighlightBackgroundColor + "';";

            if(!string.IsNullOrEmpty(this.BackgroundColor))
                postSearchFunctionBody += "resultsList.BackgroundColor = '" + this.BackgroundColor + "';";

            if(!string.IsNullOrEmpty(this.HighlightForeColor))
                postSearchFunctionBody += "resultsList.HighlightForeColor = '" + this.HighlightForeColor + "';";

            if(!string.IsNullOrEmpty(this.ForeColor))
                postSearchFunctionBody += "resultsList.ForeColor = '" + this.ForeColor + "';";

            postSearchFunctionBody += @"resultsList.Initialize();
resultsList.AddResetListener(function(){
        JSUI.HideElement('" + this.DomId + @"');
        var textbox = JSUI.GetElement('" + this.input.DomId + @"');
        textbox.value = '';
        textbox.focus();
    }
);";

            if (!string.IsNullOrEmpty(this.SelectHandlerJsonId))
            {
                if (this.ParentJavascriptPage == null)
                    ExceptionHelper.Throw<JsonInvalidOperationException>("Unable to set the SelectHandler because the page is not a JavascriptPage: {0}", this.ToString());

                this.SelectHandler = this.ParentJavascriptPage.FindJsonControlAs<JsonFunction>(this.SelectHandlerJsonId);

                if (this.SelectHandler == null)
                    ExceptionHelper.Throw<JsonInvalidOperationException>("The specified SelectHandler with JsonId '{0}' could not be found: {1}", this.SelectHandlerJsonId, this.ToString());

                if (this.SelectHandler.Parameters.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length > 1)
                    ExceptionHelper.Throw<JsonInvalidOperationException>("The specified SelectHandlerJsonId can not be used as the Select handler because it takes more than one parameter: {0}", this.ToString());
            }

            if (this.SelectHandler != null)
            {
                postSearchFunctionBody += "resultsList.AddSelectListener(" + this.SelectHandler.JsonId + ");";
            }

            this.postSearch.FunctionBody = postSearchFunctionBody;

            this.AddJsonFunction(this.postSearch);

            this.attacher.JsonId = this.JsonId + "_attacher";
            this.attacher.FunctionBody = string.Format("JSUI.AddEventHandler('{0}', {1}, {2});JSUI.DockThisTo('{3}','{4}');", 
                this.input.JsonId, 
                this.preSearch.JsonId, 
                "'keyup'",
                this.DomId,
                this.input.DomId);
            this.AddJsonFunction(attacher);

            this.EventSourceDomIds = this.eventSource.DomId;
            this.ClientEventName = this.eventSource.ClientEventName;
            this.AddParameterSource(this.input);

            this.PreInvokeJsonIds = this.preSearch.JsonId;
            this.PostInvokeJsonIds = this.postSearch.JsonId;

            this.eventSource.WireScriptsAndValidate();
            this.images.WireScriptsAndValidate();
            base.WireScriptsAndValidate();
        }

        public override string CssStyle
        {
            get
            {
                return this.input.CssStyle;
            }
            set
            {
                this.input.CssStyle = value;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            this.input.RenderScripts = this.RenderScripts;
            if (!string.IsNullOrEmpty(this.TextBoxStyle))
                this.input.Attributes.SetAttribute("style", this.TextBoxStyle);
            
            this.images.RenderScripts = this.RenderScripts;
            this.eventSource.RenderScripts = this.RenderScripts;
            
            this.input.RenderControl(writer);
            this.images.RenderControl(writer);

            if (!string.IsNullOrEmpty(this.ResultsStyle))
                this.controlToRender.Attributes.Add("style", this.ResultsStyle);

            base.Render(writer);
        }

        #region IJsonInput Members

        public string InputJsonId
        {
            get
            {
                return this.input.JsonId;
            }
            set
            {
                this.input.JsonId = value;
            }
        }

        public bool CanEdit
        {
            get;
            set;
        }

        #endregion
    }
}
