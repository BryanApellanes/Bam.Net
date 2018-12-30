/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Js;
using J = Bam.Net.Javascript;
using System.Text.RegularExpressions;
using System.IO;
using Bam.Net.Logging;
using Bam.Net.Presentation;

namespace Bam.Net.Server.Renderers
{
    public static class DustScript
    {
        static DustScript()
        {
            J.ResourceScripts.LoadScripts(typeof(DustScript));
        }

        public static string Get()
        {
            StringBuilder script = new StringBuilder();
            script.Append(";\r\n");
            script.Append(J.ResourceScripts.Get("json2.js", typeof(DustScript)));
            script.Append(J.ResourceScripts.Get("dust.custom.js", typeof(DustScript)));
            script.Append(";\r\n");
            return script.ToString();
        }

        public static string CompileTemplates(string directoryPath, string fileSearchPattern = "*.dust", ILogger logger = null)
        {
            return CompileTemplates(new DirectoryInfo(directoryPath), fileSearchPattern, SearchOption.TopDirectoryOnly, "", logger);
        }

        /// <summary>
        /// Compiles all the dust templates in the specified directory returning the combined 
        /// result as a string.
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="fileSearchPattern"></param>
        /// <param name="searchOption"></param>
        /// <param name="templateNamePrefix"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static string CompileTemplates(DirectoryInfo directory, string fileSearchPattern = "*.dust", SearchOption searchOption = SearchOption.TopDirectoryOnly, string templateNamePrefix = "", ILogger logger = null)
        {
            return CompileTemplates(directory, out List<ICompiledTemplate> ignore, fileSearchPattern, searchOption, templateNamePrefix, logger);
        }

        /// <summary>
        /// Compiles all the dust templates in the specified directory returning the combined 
        /// result as a string and each individual template in a list as an out parameter.
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="templates"></param>
        /// <param name="fileSearchPattern"></param>
        /// <param name="searchOption"></param>
        /// <param name="templateNamePrefix"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static string CompileTemplates(this DirectoryInfo directory, out List<ICompiledTemplate> templates, string fileSearchPattern = "*.dust", SearchOption searchOption = SearchOption.TopDirectoryOnly, string templateNamePrefix = "", ILogger logger = null)
        {
            StringBuilder compiled = new StringBuilder();
            logger = logger ?? Log.Default;
            templates = new List<ICompiledTemplate>();
            if (directory.Exists)
            {
                logger.AddEntry("DustScript::Compiling Dust Directory: {0}", directory.FullName);
                FileInfo[] files = directory.GetFiles(fileSearchPattern, searchOption);                                
                foreach (FileInfo file in files)
                {
                    string templateName = Path.GetFileNameWithoutExtension(file.Name);
                    if (searchOption == SearchOption.AllDirectories)
                    {
                        if (file.Directory.FullName.Replace("\\", "/") != directory.FullName.Replace("\\", "/"))
                        {
                            string prefix = file.Directory.FullName.TruncateFront(directory.FullName.Length + 1).Replace("\\", "_") + "_";
                            templateName = prefix + templateName;
                        }
                    }

                    string templateFullName = templateNamePrefix + templateName;
                    ICompiledTemplate template = new CompiledDustTemplate(file.FullName, templateFullName);
                    logger.AddEntry("DustScript::Starting Dust compile: fileName={0}, templateName={1}", file.FullName, templateFullName);
                    compiled.Append(";\r\n");
                    // TODO: do Regex.Unescape(tempalte.Compiled) to prevent having to do it everywhere else that references the resulting "compiled.ToString()"
                    // such as below in Render.  Do it here and it shouldn't be necessary anywhere else.
                    compiled.Append(template.Compiled);
                    compiled.Append(";\r\n");
                    logger.AddEntry("DustScript::Finished Dust compile: fileName={0}, templateName={1}", file.FullName, templateFullName);
                    templates.Add(template);
                }
            }

            return compiled.ToString();
        }

        public static string Compile(string templateSource, string templateName)
        {
            J.JsContext scriptContext = new J.JsContext();
            scriptContext.Load(Get());
            scriptContext.SetCliValue("templateSource", templateSource.Replace("\r", "").Replace("\n", ""));
            scriptContext.SetCliValue("templateName", templateName);
            scriptContext.Run("var compiled = dust.compile(templateSource, templateName);");
            return scriptContext.GetValue<string>("compiled");
        }

        public static string Render(string compiled, string templateName, object data)
        {
            J.JsContext scriptContext = new J.JsContext();
            scriptContext.Load(Get());
			scriptContext.SetCliValue("compiled", Regex.Unescape(compiled));
            scriptContext.SetCliValue("templateName", templateName);
            scriptContext.SetCliValue("jsonData", data.ToJson());
            scriptContext.Run(@"
var output;
var error;
dust.loadSource(compiled);
dust.render(templateName, JSON.parse(jsonData), function(err, out){
    error = err;
    output = out;
});");
            object error = scriptContext.GetValue<object>("error");
            if (error != null)
            {
                throw new Exception("An error occurred rendering dust template: {0}"._Format(error.ToString()));
            }

            return scriptContext.GetValue<string>("output");
        }

        public static string Render(string templateDirectory, string templateName, object data, string searchPattern = "*.*")
        {
            return Render(new DirectoryInfo(templateDirectory), templateName, data, searchPattern);
        }

        public static string Render(DirectoryInfo templateDirectory, string templateName, object data, string searchPattern = "*.*")
        {
            string compiled = CompileTemplates(templateDirectory, searchPattern);
            return Render(compiled, templateName, data);
        }
    }
}
