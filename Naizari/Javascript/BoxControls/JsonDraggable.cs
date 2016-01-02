/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Javascript.JsonControls;
using System.Web.UI.HtmlControls;
using Naizari.Helpers.Web;
using Naizari.Helpers;
using System.Web.UI;

namespace Naizari.Javascript.BoxControls
{
    public class JsonDraggable: JsonControl
    {
        public JsonDraggable()
            : base()
        {
            this.IsModal = true; // this should be defaulted to false when JsonDraggable gets renamed to JsonDialog.  no time now to do this 3/9/10
        }

        public string OKDomId { get; set; }
        public string CancelDomId { get; set; }
        public string DraggableDomId { get; set; }
        public string DragHandleDomId { get; set; }
        public bool RenderDialogButtons { get; set; }
        public bool IsModal { get; set; }
        public override void WireScriptsAndValidate()
        {
            if (string.IsNullOrEmpty(DragHandleDomId))
                throw ExceptionHelper.CreateException<JsonInvalidOperationException>("The DragHandleDomId was not specified for : {0}", this.ToString());

            if (string.IsNullOrEmpty(DraggableDomId))
                throw ExceptionHelper.CreateException<JsonInvalidOperationException>("The DraggableDomId was not specified for : {0}", this.ToString());

            JsonFunction createDraggableFunction = new JsonFunction();
            createDraggableFunction.JsonId = this.JsonId + "_init";
            string modal = this.IsModal ? "true" : "false";
            string body = "BoxMgr.CreateDialog('" + this.DraggableDomId + "', {dragHandle: '" + this.DragHandleDomId + "', modal: " + modal + "});";//, this.DraggableDomId, this.DragHandleDomId, this.IsModal ? "true": "false")
            if (RenderDialogButtons)
                body += string.Format("BoxMgr.Dialogs['{0}'].RenderDialogButtons();", this.DraggableDomId);

            if (!string.IsNullOrEmpty(this.OKDomId))
                body += string.Format("BoxMgr.Dialogs['{0}'].SetOkId('{1}');", this.DraggableDomId, this.OKDomId);

            if (!string.IsNullOrEmpty(this.CancelDomId))
                body += string.Format("BoxMgr.Dialogs['{0}'].SetCancelId('{1}');", this.DraggableDomId, this.CancelDomId);

            createDraggableFunction.ExecutionType = JavascriptExecutionTypes.OnWindowLoad;
            createDraggableFunction.FunctionBody = body;
            this.AddJsonFunction(createDraggableFunction);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            //base.Render(writer);

            if (this.RenderScripts)
                this.RenderConglomerateScript(writer);
           
        }
    }
}
