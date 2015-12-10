/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Schema;
using Bam.Net.Configuration;
using Bam.Net.Logging;
using Bam.Net.Incubation;
using Bam.Net.Javascript;
using Bam.Net.ServiceProxy;
using Yahoo.Yui.Compressor;


namespace Bam.Net.Server.Renderers
{
    public class DaoProxyScriptsRenderer: RendererBase
    {
        public DaoProxyScriptsRenderer()
            : base("application/javascript", ".js")
        {
        }
        
        public override void Render(object toRender, Stream output)
        {
            //DaoProxyRegistration.GetScript()
        }

        public void RegisterDaoTypes()
        {
            //RegisterDaoTypes(this.Con)
        }

        public void RegisterDaoType(Fs fsRoot)
        {

        }

        public void Generate(string dbJsPath, Fs fsRoot)
        {
            SchemaManager schemaManager = new SchemaManager();
            FileInfo dbFile = new FileInfo(dbJsPath);
            schemaManager.RootDir = fsRoot.GetAbsolutePath("~/dao");
            schemaManager.Generate(dbFile, true, false);
        }
         //this.Script = DaoProxyRegistration.GetScript().ToString();
         //   if (min) Compress();
    }
}
