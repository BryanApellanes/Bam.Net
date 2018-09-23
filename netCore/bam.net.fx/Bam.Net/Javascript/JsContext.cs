/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.Incubation;
using EcmaScript.NET.Types;
using EcmaScript.NET.Types.Cli;
using EcmaScript.NET;

namespace Bam.Net.Javascript
{
    public class JsContext
    {
        Context _context;
        ScriptableObject _scope;
        StringBuilder _loaded;

        public JsContext()
        {
            Refresh();
        }

        public void Refresh()
        {
            this._context = Context.Enter();
            this._scope = _context.InitStandardObjects();
            this._loaded = new StringBuilder();
        }

        public void SetCliValue(string varName, object instance)
        {
            ScriptableObject.PutProperty(_scope, varName, instance);
        }

        /// <summary>
        /// Preload the specified script text
        /// </summary>
        /// <param name="script"></param>
        public void Load(string script)
        {
            _loaded.AppendLine(";");
            _loaded.Append(script);
            _loaded.AppendLine(";");
        }

        public T GetCliValue<T>(string varName)
        {
            CliObject cli = GetValue<CliObject>(varName);
            return (T)cli.Object;
        }

        public T GetValue<T>(string varName)
        {
            return (T)ScriptableObject.GetProperty(_scope, varName);            
        }
        
        public string Script(string add)
        {
            StringBuilder script = new StringBuilder(_loaded.ToString());
            script.AppendLine(";");
            script.Append(add);
            return script.ToString();
        }

        public void Run(string script, string name = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                name = "script_".RandomLetters(8);
            }
            StringBuilder toRun = new StringBuilder(_loaded.ToString());
            toRun.Append(script);
            _context.EvaluateString(_scope, toRun.ToString(), name, 0, null);
        }
    }
}
