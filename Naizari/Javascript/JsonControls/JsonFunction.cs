/*
	Copyright © Bryan Apellanes 2015  
*/
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Naizari.Extensions;

[assembly: TagPrefix("Naizari.Javascript.JsonControls", "json")]
namespace Naizari.Javascript.JsonControls
{
    [ParseChildren(typeof(string), ChildrenAsProperties=true, DefaultProperty="FunctionBody")]
    [ToolboxData("<{0}:JsonFunction JsonId='JsonId' runat=\"server\"></{0}:JsonFunction>")]
    public class JsonFunction: JsonControl
    {
        protected List<string> parameters;
        public event ParameterAddedDelegate ParameterAdded;
        private string functionBody;
        protected JavascriptExecutionTypes executionMode;
        protected List<string> appendToBody;
        protected List<string> prependScript;
        protected List<string> appendScript;

        protected List<string> scriptText;
        /// <summary>
        /// Creates a new instance of the JsonFunction class.
        /// </summary>
        public JsonFunction()
            : base()
        {
            this.scriptText = new List<string>();
            this.parameters = new List<string>();
            this.prependScript = new List<string>();
            this.appendScript = new List<string>();
            this.FileNotFoundAction = FileNotFoundAction.UseEmptyString;
            this.functionBody = string.Empty;
            this.executionMode = JavascriptExecutionTypes.OnParse;
            this.appendToBody = new List<string>();
            this.IncludeScriptTags = true;
            //this.AddRequiredScript("naizari.javascript.jsoncontrols.jsonfunction.js");
            this.AddRequiredScript(typeof(JsonFunction));
        }

        public JsonFunction(string body)
            : this()
        {
            this.functionBody = body;
        }

        private void OnParameterAdded(string parameterName)
        {
            if (this.ParameterAdded != null)
                this.ParameterAdded(this, parameterName);
        }

        public void ClearBody()
        {
            this.functionBody = string.Empty;
            this.appendToBody.Clear();
        }

        public bool IncludeScriptTags
        {
            get;
            set;
        }

        public virtual JavascriptExecutionTypes ExecutionType
        {
            get
            {
                return this.executionMode;
            }
            set
            {
                this.executionMode = value;
                this.scriptsCreated = false;
            }
        }

        public override void WireScriptsAndValidate()
        {
            if (this.Parent is UserControl)
                this.AddJsonFunction(this);
        }
        /// <summary>
        /// Add script text to be placed before the function declaration.
        /// </summary>
        /// <param name="scriptToPrepend"></param>
        public void PrependScript(string scriptToPrepend)
        {
            this.prependScript.Add(scriptToPrepend);
        }

        string prepend;
        public string Prepend
        {
            get { return this.prepend; }
            set
            {
                if (string.IsNullOrEmpty(prepend))
                {
                    this.prepend = value;
                    this.PrependScript(value);
                }
            }
        }

        string append;
        public string Append
        {
            get { return this.append; }
            set
            {
                if (string.IsNullOrEmpty(this.append))
                {
                    this.append = value;
                    this.AppendScript(value);
                }
            }
        }
        /// <summary>
        /// Add script text to be placed after the function declaration.
        /// </summary>
        /// <param name="scriptToAppend"></param>
        public void AppendScript(string scriptToAppend)
        {
            this.appendScript.Add(scriptToAppend);
        }
        /// <summary>
        /// A comma separated list of named javascript function parameters/arguments.
        /// </summary>
        public string Parameters
        {
            get
            {
                return StringExtensions.ToDelimited(parameters.ToArray(), ",");
            }
            set
            {
                parameters.Clear();
                //parameters.AddRange(StringExtensions.CommaSplit(value));
                // need to make sure the ParameterAdded event is raised
                foreach (string parameter in StringExtensions.CommaSplit(value))
                {
                    AddParameter(parameter);
                }
            }
        }

        public FileNotFoundAction FileNotFoundAction
        {
            get;
            set;
        }

        public string VirtualFilePath
        {
            get;
            set;
        }

        /// <summary>
        /// Represents the javascript text contained in the body of the function.
        /// </summary>
        public string FunctionBody 
        {
            get
            {
                return this.GetFunctionBody();
            }
            set
            {
                this.functionBody = value;
            }
        }

        public bool Refresh
        {
            get;
            set;
        }

        public string GetFunctionBody()
        {
            return GetFunctionBody(this.Refresh);
        }

        bool readFile;
        public string GetFunctionBody(bool reload)
        {
            if ((!string.IsNullOrEmpty(VirtualFilePath) && !readFile) || reload)
            {
                string filePath = HttpContext.Current.Server.MapPath(VirtualFilePath);

                if (!File.Exists(filePath))
                {
                    switch (FileNotFoundAction)
                    {
                        case FileNotFoundAction.Invalid:
                            throw new JsonInvalidOperationException("Invalid FileNotFoundAction specified.");
                        case FileNotFoundAction.ThrowError:
                            throw new JsonFileNotFoundException(VirtualFilePath);
                        case FileNotFoundAction.UseEmptyString:
                            return string.Empty;
                        case FileNotFoundAction.UsePrevious:
                            if (!string.IsNullOrEmpty(functionBody))
                                return functionBody;
                            else
                                return string.Empty;
                    }
                }
                
                this.functionBody = StringExtensions.SafeReadFile(filePath);
                this.readFile = true;
            }
            return functionBody + GetAppendToBody();
        }

        private string GetAppendToBody()
        {
            string retVal = "\r\n";
            foreach (string appended in this.appendToBody)
            {
                retVal += appended;
            }

            return retVal;
        }
        /// <summary>
        /// Add a named javascript function parameter/argument.
        /// </summary>
        /// <param name="parameterName">The name of the parameter.</param>
        public void AddParameter(string parameterName)
        {
            parameters.Add(parameterName);
            OnParameterAdded(parameterName);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            //script.Attributes.Add("id", this.DomId);
            this.CreateScript();
            if (this.IncludeScriptTags && !string.IsNullOrEmpty(this.FunctionBody.Trim()))
            {
                HtmlGenericControl script = new HtmlGenericControl();
                script.TagName = "script";
                script.Attributes.Add("type", "text/javascript");
                script.Attributes.Add("jsonid", this.JsonId);
                script.Attributes.Add("language", "javascript");
                script.Controls.Add(new LiteralControl(this.ScriptText));
                script.RenderControl(writer);
            }
            else if(!string.IsNullOrEmpty(this.FunctionBody.Trim()))
            {
                writer.Write(this.ScriptText);
            }


        }

        protected bool scriptsCreated;
        protected virtual void CreateScript()
        {
            if (scriptsCreated)
                return;

            scriptsCreated = true;

            foreach (string scriptLine in this.prependScript)
            {
                AddScriptTextLine(scriptLine);
            }

            if (!string.IsNullOrEmpty(FunctionBody))
            {
                AddScriptText(string.Format("JSUI.RegisterFunction('{0}', function({1})", this.JsonId, GetFunctionParameters()));
                AddScriptTextLine("{");
                AddScriptTextLine(FunctionBody);
                AddScriptTextLine("});");
                AddScriptTextLine(string.Format("window.{0} = JSUI.Functions['{0}'];", this.JsonId));

                switch (ExecutionType)
                {
                    case JavascriptExecutionTypes.Invalid:
                        break;
                    case JavascriptExecutionTypes.OnWindowLoad:
                        AddScriptTextLine(string.Format("JSUI.addOnWindowLoad({0});", this.JsonId));
                        break;
                    case JavascriptExecutionTypes.OnWindowResize:
                        AddScriptTextLine(string.Format("JSUI.addOnWindowResize({0});", this.JsonId));
                        break;
                    case JavascriptExecutionTypes.OnWindowScroll:
                        AddScriptTextLine(string.Format("JSUI.addOnWindowScroll({0});", this.JsonId));
                        break;
                    case JavascriptExecutionTypes.OnParse:
                        if (this.parameters.Count > 0)
                        {
                            throw new JsonInvalidOperationException(
                                string.Format("The JsonFunction {0} cannot be executed on parse because it takes {1} parameters.", this.ToString(), this.parameters.Count));
                        }
                        if (!string.IsNullOrEmpty(FunctionBody))
                            AddScriptTextLine(string.Format("window.{0}();", this.JsonId));
                        break;
                }
            }

            foreach (string scriptLine in this.appendScript)
            {
                AddScriptTextLine(scriptLine);
            }           
            
        }

        /// <summary>
        /// Represents all the script text of the current instance
        /// without the opening and closing script tags.  If CreateScript()
        /// has not been called this will return an empty string.
        /// </summary>
        public string ScriptText
        {
            get
            {
                if (!scriptsCreated)
                    return string.Empty;

                return StringExtensions.ToDelimited(scriptText.ToArray(), "");
            }
        }
        /// <summary>
        /// Appends the FunctionBody of the specified script to the FunctionBody
        /// of the current instance.
        /// </summary>
        /// <param name="javascriptFunction">The script to append.</param>
        public void AppendScriptBody(JsonFunction javascriptFunction)
        {
            this.AppendScriptBody(javascriptFunction.FunctionBody);        
        }

        public void AppendScriptBodyFormat(string formatString, params string[] args)
        {
            this.AppendScriptBody(string.Format(formatString, args));
        }

        /// <summary>
        /// Appends the specified script text to the body of the current
        /// instance.
        /// </summary>
        /// <param name="bodyToAppend"></param>
        public void AppendScriptBody(string bodyToAppend)
        {
            this.appendToBody.Add(bodyToAppend);
        }

        private string GetFunctionParameters()
        {
            return StringExtensions.ToDelimited(parameters.ToArray(), ",");
        }

        /// <summary>
        /// Adds a line of script to the Javascript to output.
        /// </summary>
        /// <param name="scriptText"></param>
        public void AddScriptTextLine(string scriptText)
        {
            AddScriptText(scriptText + "\r\n");
        }

        /// <summary>
        /// Adds script to the Javascript output.
        /// </summary>
        /// <param name="scriptText"></param>
        public void AddScriptText(string scriptText)
        {
            this.scriptText.Add(scriptText);//script.Controls.Add(new LiteralControl(scriptText));
        }


        public override bool Equals(object obj)
        {
            if (obj is JsonFunction)
            {
                JsonFunction func = (JsonFunction)obj;
                if (!func.scriptsCreated)
                    func.CreateScript();

                return func.ScriptText.Equals(this.ScriptText);
            }
            else
            {
                return base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return this.ScriptText.GetHashCode();
        }
    }
}
