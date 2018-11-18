/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.IO;
using Bam.Net;
using Bam.Net.Presentation.Html;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Js;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace Bam.Net.Presentation.Dust
{
    public class DustController: Controller
    {
        static List<string> _nameProperties = new List<string>(new string[] { 
            "type", "Type",
            "typeName", "TypeName",
            "class", "Class", 
            "className", "ClassName",
            "tableName", "TableName"
        });

        public const string AppDustRoot = "~/bam/apps/{appName}/dust/";
        public const string CommonDustRoot = "~/bam/dust/";

        public ActionResult SaveDustTemplate(string appName, string templateName, string dustTemplate)
        {
            try
            {
                string path = Server.MapPath(CommonDustRoot);
                dustTemplate.SafeWriteToFile(path, (o) => o.ClearFileAccessLocks());
                return Json(new { Success = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }

        public ActionResult SaveAppDustTemplate(string appName, string templateName, string dustTemplate)
        {
            try
            {
                string path = Server.MapPath(AppDustRoot.NamedFormat(new { appName = appName }));
                dustTemplate.SafeWriteToFile(path, (o) => o.ClearFileAccessLocks());
                return Json(new { Success = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }

        public ActionResult WriteTemplate(string appName, string json, bool setValues = false)
        {
            try
            {
                object rehydrated = JsonConvert.DeserializeObject(json);
                
                string name = GetName((JObject)rehydrated);
                string path = Server.MapPath("~/bam/apps/{0}/dust/{1}.dust"._Format(appName, name));
                string html = HtmlHelperExtensions.FieldsetFor(null, rehydrated, name, null, setValues).ToString().XmlToHumanReadable();
                FileInfo file = new System.IO.FileInfo(path);
                int i = 1;
                while (System.IO.File.Exists(path))
                {
                    path = Path.Combine(file.Directory.FullName, "{0}_{1}.dust"._Format(Path.GetFileNameWithoutExtension(file.Name), i));
                    i++;
                }
                html.SafeWriteToFile(path);
                return Json(new { Success = true });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }
        
        private string GetName(JObject rehydrated)
        {
            string name = string.Empty;
            foreach (string nameProp in _nameProperties)
            {
                if (!(rehydrated[nameProp] is JArray))                
                {
                    name = (string)rehydrated[nameProp];
                }

                if (!string.IsNullOrEmpty(name))
                {
                    break;
                }
            }
            if (string.IsNullOrEmpty(name))
            {
                name = DeriveTableName(rehydrated);
            }
            return name;
        }

        private string DeriveTableName(JObject val)
        {
            StringBuilder sb = new StringBuilder();
            foreach (JProperty prop in val.Properties())
            {
                string name = prop.Name.ToLowerInvariant();
                if (!_nameProperties.Contains(name))
                {
                    sb.Append(name);
                }
            }

            return sb.ToString().Sha1();
        }

        public ActionResult For(string json)
        {
            try
            {
                dynamic rehydrated = JsonConvert.DeserializeObject<dynamic>(json);
                return For(GetName(rehydrated), rehydrated);
            }
            catch (Exception ex)
            {
                return Json(new { Sucess = false, Message = ex.Message });
            }
        }

        protected ActionResult For(string legend, dynamic value)
        {
            try
            {
                return new DustTemplateResult(legend, value);
            }
            catch (Exception ex)
            {
                return new DustTemplateResult(new { Sucess = false, Message = ex.Message });
            }
        }

        public ActionResult Template(string templateName)
        {
            templateName = templateName.ToLowerInvariant();
            if (!Dust.TemplateIsRegistered(templateName))
            {
                throw Args.Exception<InvalidOperationException>("The specified template ({0}) was not found", templateName);
            }
            return JavaScript(Dust.CompiledTemplates[templateName]);
        }

        public ActionResult Templates()
        {
            return JavaScript(string.Format("dust.loadSource('{0}');", Dust.AllCompiledTemplates));
        }

        static Dictionary<string, string> _bamAppTemplates;
        static object _bamAppTemplatesLock = new object();
        public ActionResult BamTemplates(string appName, string callBack = "", string fileExt = "dust")
        {
            string appDustRoot = Server.MapPath("~/bam/apps/{0}/dust/"._Format(appName));
            string commonDustRoot = Server.MapPath("~/bam/dust/");
            Dictionary<string, string> templateContainer = _bamAppTemplatesLock.DoubleCheckLock(ref _bamAppTemplates, () => new Dictionary<string, string>());
            StringBuilder script = GetTemplateScripts("{0}."._Format(appName), fileExt, appDustRoot)
                .A(GetTemplateScripts("", fileExt, commonDustRoot).ToString());
            
            if (!string.IsNullOrEmpty(callBack))
            {
                script.AppendLine(";{0}"._Format(callBack));
            }

            return JavaScript(script.ToString());
        }

        private static StringBuilder GetTemplateScripts(string appName, string fileExt, string dustRoot)
        {
            DirectoryInfo dustDir = new DirectoryInfo(dustRoot);
            StringBuilder script = new StringBuilder();
            if (dustDir.Exists)
            {
                FileInfo[] files = dustDir.GetFiles("*.{0}"._Format(fileExt));
                foreach (FileInfo file in files)
                {
                    DustTemplate template = new DustTemplate(file, "{0}{1}"._Format(appName, Path.GetFileNameWithoutExtension(file.FullName)));
                    script.AppendLine(Regex.Unescape(template.CompiledScript));
                }
            }
            return script;
        }

        public ActionResult Js(bool min = false)
        {
            ResourceScripts.LoadScripts(typeof(Dust));
            return JavaScript(ResourceScripts.Get("dust-full-0.3.0.js", min));
        }
    }
}
