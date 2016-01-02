/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Bam.Net;
using Bam.Net.Logging;
using Ionic.Zip;
using System.CodeDom.Compiler;
using System.Web;
using System.IO;

namespace Bam.Net.Data.Schema
{
    public static class DaoGeneratorResults
    {
        static Dictionary<string, DaoGeneratorResult> _results;
        static DaoGeneratorResults()
        {
            _results = new Dictionary<string, DaoGeneratorResult>();
        }

        public static void Set(DaoGeneratorResult result)
        {
            if (_results.ContainsKey(result.Name))
            {
                _results[result.Name] = result;
            }
            else
            {
                _results.Add(result.Name, result);
            }
        }

        public static DaoGeneratorResult Get(string name)
        {
            DaoGeneratorResult result = null;
            if (_results.ContainsKey(name))
            {
                result = _results[name];
            }

            return result;
        }
    }

    public class DaoGeneratorResult: ActionResult
    {
        public DaoGeneratorResult(string json)
        {
            this.Json = json;
            this.Name = json.Sha1();
            this._statuses = new List<string>();
            this.User = "ANON";
        }

        public DaoGeneratorResult(string name, string json)
            : this(json)
        {
            this.Name = name;
        }

        public string Name { get; set; }
        public string Json { get; set; }

        public ZipFile Data { get; set; }

        public bool Complete { get; set; }
        public bool Success { get; set; }
        public string User { get; set; }
        public string Status { get; set; }

        List<string> _statuses;
        public string[] Statuses { get { return _statuses.ToArray(); } }

        private object Util { get; set; }

        private void SetStatus(string status)
        {
            Status = status;
            _statuses.Add(status);
        }

        bool _started;
        object _generateLock = new object();
        private void Generate()
        {
            try
            {
                if (!_started)
                {
                    _started = true;

                    Exec.Start(this.Name, () =>
                    {
                        try
                        {
                            SetStatus("Code generation for {0} started"._Format(this.Name));
                            SchemaManager schemaMgr = new SchemaManager();
                            DirectoryInfo compileTo = new DirectoryInfo(schemaMgr.BinDir);
                            string tmpDirPath = "{0}/{1}/tmp/"._Format(Util.GetAppDataFolder(), User);

                            SchemaResult schemaResult = schemaMgr.GenerateDao(Json, compileTo, false, tmpDirPath);
                            SetStatus(schemaResult.Message);
                            if (schemaResult.Success)
                            {
                                SetStatus("Zipping...");
                                ZipFile daoDll = new ZipFile();
                                daoDll.AddFile(Path.Combine(compileTo.FullName, "{0}.dll"._Format(schemaResult.Namespace)));
                                Data = daoDll;
                            }

                            Complete = true;
                            Success = schemaResult.Success;
                        }
                        catch (Exception e)
                        {
                            Log.AddEntry("An error occurred in DaoGeneratorResult generation thread: {0}", e, e.Message);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                Log.AddEntry("An error occurred in DaoGeneratorResult: {0}", ex, ex.Message);
            }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            DaoGeneratorResult result = DaoGeneratorResults.Get(this.Name);
            if (result == null)
            {
                DaoGeneratorResults.Set(this);
                result = this;
            }

            if (result.Complete)
            {
                HttpResponseBase response = context.HttpContext.Response;
                HttpRequestBase request = context.HttpContext.Request;

                response.Clear();
                response.AddHeader("Content-Disposition", string.Format("attachment: filename={0}", result.Data.Name));
                response.ContentType = "application/x-zip-compressed";
                result.Data.Save(response.OutputStream);
            }
            else
            {
                JsonResult json = new JsonResult();
                json.Data = new { Success = true, Message = "{0} is still in progress..."._Format(result.Name) };
                json.ExecuteResult(context);
            }                
        }
    }
}
