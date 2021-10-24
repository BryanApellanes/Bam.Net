using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.Web;
using Bam.Net.Presentation.Html;
using Bam.Net;
using Bam.Net.Logging;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;
using Bam.Net.Presentation;
namespace Bam.Net.Server.Renderers
{
    public partial class WebRendererFactory // core
    {
        public override void Render(object toRender, Stream output)
        {
            // if there is no extension ensure the result is a string
            // use our own Render method
            string msg = "No renderer was found and the specified object to render was not a string or byte array: ({0}):: ({1})"._Format(toRender.GetType().Name, toRender.ToString());
            if (toRender == null)
            {
                msg = "toRender was null";
            }

            byte[] toRenderAsByteArray = toRender as byte[];
            if (toRender is string toRenderAsString)
            {
                toRenderAsByteArray = Encoding.UTF8.GetBytes(toRenderAsString);
            }

            output.Write(toRenderAsByteArray, 0, toRenderAsByteArray.Length);
        }

        private void AddBaseRenderers()
        {
            AddRenderer(() => new JsonRenderer());
            AddRenderer(() => new XmlRenderer());
            AddRenderer(() => new CsvRenderer());
            AddRenderer(() => new TxtRenderer());
        }
    }
}
