/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Yaml;
using System.IO;
using Bam.Net;
using Bam.Net.Yaml;

namespace Bam.Net.Server.Renderers
{
    public class YamlRenderer: Renderer
    {
        public YamlRenderer()
            : base("application/x-yaml", ".yaml")
        { }

        public override void Render(object toRender, Stream output)
        {
            string yaml = toRender.ToYaml();

            byte[] data = Encoding.UTF8.GetBytes(yaml);
            output.Write(data, 0, data.Length);
        }
    }
}
