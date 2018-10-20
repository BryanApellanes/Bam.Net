/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Server.Renderers
{
    public class CsvRenderer: Renderer
    {
        public CsvRenderer()
            : base("application/vnd.ms-excel", ".csv")
        {
            this.FileName = "Data";
        }

        public string FileName
        {
            get;
            set;
        }

        public override void SetContentType(IResponse response)
        {
            response.AddHeader("Content-Disposition", "attachment;filename=" + FileName + ".csv");
            base.SetContentType(response);
        }

        public override void Render(object toRender, Stream output)
        {
            CsvWriter csv = new CsvWriter(toRender);
            csv.WriteCsv(output);
        }
    }
}
