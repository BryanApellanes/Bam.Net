/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Extensions;
using Naizari.Test;

namespace Naizari.Javascript.JsonControls
{
    /// <summary>
    /// This class may need to be marked obsolete.  It's usefulness
    /// is still questionable.  Keeping it for now.
    /// </summary>
    public class JsonFunctionDelegate: JsonFunction
    {
        List<string> functionIds;

        public JsonFunctionDelegate()
            : base()
        {
            this.functionIds = new List<string>();
        }

        public int Count
        {
            get
            {
                return this.functionIds.Count;
            }
        }
        /// <summary>
        /// A comma separated list of JsonFunction JsonIds and/or function names.  If 
        /// specifying one or more client side function names ValidateFunctions must be 
        /// set to false.
        /// </summary>
        public string JsonFunctionJsonIds
        {
            get
            {
                return StringExtensions.ToCommaDelimited(this.functionIds.ToArray());
            }
            set
            {
                this.functionIds.Clear();
                this.functionIds.AddRange(StringExtensions.CommaSplit(value));
            }
        }

        //TODO: change the name of this to AddClientDelegate
        public void AddDelegate(JsonFunction jsonFunction)
        {
            if (!this.functionIds.Contains(jsonFunction.JsonId))
                this.functionIds.Add(jsonFunction.JsonId);
        }

        public void AddDelegate(string jsonId)
        {
            if (!this.functionIds.Contains(jsonId))
                this.functionIds.Add(jsonId);
        }
        /// <summary>
        /// Set to true if this delegate should only take server side JsonFunctions.
        /// If false any list of function names can be specified in the JsonFunctionJsonIds
        /// property
        /// </summary>
        public bool ValidateFunctions { get; set; }

        public override void WireScriptsAndValidate()
        {
            base.WireScriptsAndValidate();
            this.functionIds.AddRange(StringExtensions.CommaSplit(this.JsonFunctionJsonIds));

            string[] ids = StringExtensions.RemoveDuplicates(this.functionIds.ToArray());

            this.PrependScript(string.Format("window.{0}Delegate = [];", this.JsonId));

            foreach (string id in ids)
            {
                if (this.ValidateFunctions)
                {
                    Validate(id);
                }

                this.PrependScript(string.Format("{0}Delegate.push({1});", this.JsonId, id));
            }

            this.FunctionBody = string.Format("JSUI.FireDelegate({0}Delegate);", this.JsonId);
        }

        private void Validate(string id)
        {
            try
            {
                Expect.IsNotNull(this.ParentJavascriptPage);
                Expect.IsNotNull(this.ParentJavascriptPage.FindJsonControlAs<JsonFunction>(id));
            }
            catch (ExpectFailedException efe)
            {
                throw new JsonInvalidOperationException("JsonFunctionDelegate validation failed, make sure that all JsonFunctions have been properly added to the page: " + this.ToString(), efe);
            }
        }
    }
}
