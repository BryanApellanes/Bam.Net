/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

using System.Web;
using Bam.Net.Presentation.Html;
using Yahoo.Yui.Compressor;
using System.Threading.Tasks;

namespace Bam.Net.Javascript
{
    public static class Extensions
    {
        public static Task<MinifyResult> MinifyAsync(this string script)
        {
            return Task.Run(() =>
            {
                MinifyResult result;
                script.TryMinify(out result);
                return result;
            });
        }
        public static bool TryMinify(this string script, out MinifyResult result)
        {
            result = new MinifyResult(script);
            return result.Success;
        }

        /// <summary>
        /// Use YuiCompressor to compress the specified javascript
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        public static string Minify(this string script)
        {
          JavaScriptCompressor compressor = new JavaScriptCompressor(); // TODO: use gulp 
          return compressor.Compress(script);
        }

        public static JsContext RunJavascript(this string javascriptSource, params CliProvider[] cliProviders)
        {
            JsContext ctx = new JsContext();
            foreach (CliProvider o in cliProviders)
            {
                ctx.SetCliValue(o.VarName, o.Provider);
            }

            ctx.Run(javascriptSource);
            return ctx;
        }

        public static JsContext RunJavascriptFile(this string javascriptFilePath, params CliProvider[] cliProviders)
        {
            using (TextReader r = new StreamReader(javascriptFilePath))
            {
                return RunJavascript(r.ReadToEnd());
            }
        }

        /// <summary>
        /// Get a json string by reading an object from a javascript file
        /// </summary>
        /// <param name="javascriptFilePath"></param>
        /// <param name="objName"></param>
        /// <returns></returns>
        public static string JsonFromJsLiteralFile(this string javascriptFilePath, string objName)
        {
            return JsonFromJsLiteralFile(new FileInfo(javascriptFilePath), objName);
        }

        /// <summary>
        /// Get a json string by reading an object from a javascript file
        /// </summary>
        /// <param name="jsLiteralFile"></param>
        /// <param name="objName"></param>
        /// <returns></returns>
        public static string JsonFromJsLiteralFile(this FileInfo jsLiteralFile, string objName)
        {
            string json = Bam.Net.Javascript.ResourceScripts.Get("json2.js");
            string database = File.ReadAllText(jsLiteralFile.FullName);
            string command = string.Format("\r\n;var objJson = JSON.stringify({0});", objName);

            string script = "{0}{1}{2}"._Format(json, database, command);
            JsContext context = script.RunJavascript();
            string result = context.GetValue<string>("objJson");
            return result;
        }

        public static dynamic JsonToDynamic(this string json)
        {
            return JsonConvert.DeserializeObject<dynamic>(json);
        }
    }
}
