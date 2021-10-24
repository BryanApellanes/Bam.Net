/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Schema;
using Bam.Net.Server.Renderers;
using System.Reflection;
using System.IO;
using Bam.Net.Presentation;

namespace Bam.Net.Server
{
    public abstract partial class TemplateInitializer : Loggable, IInitialize<TemplateInitializer>, IPostServerInitialize
    {
        protected internal static void RenderEachTable(IWebRenderer renderer, DaoProxyRegistration daoProxyReg)
        {
            Assembly currentAssembly = daoProxyReg.Assembly;
            Type[] tableTypes = currentAssembly.GetTypes().Where(type => type.HasCustomAttributeOfType<TableAttribute>()).ToArray();
            tableTypes.Each(type =>
            {
                renderer.Render(type.Construct());
            });
        }
    }
}
