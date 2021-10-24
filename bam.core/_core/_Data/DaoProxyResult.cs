using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Yahoo.Yui.Compressor;

namespace Bam.Net.Data
{
    public class DaoProxyResult: ActionResult
    {
        public DaoProxyResult(bool min)
        {
            this.Script = DaoProxyRegistration.GetScript().ToString();
            if (min) Compress();
        }

        public string Script { get; set; }

        private void Compress()
        {
            JavaScriptCompressor jsc = new JavaScriptCompressor();
            this.Script = jsc.Compress(this.Script);
        }
    }
}
