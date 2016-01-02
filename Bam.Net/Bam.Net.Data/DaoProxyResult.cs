/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Collections;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Incubation;
using Yahoo.Yui.Compressor;

namespace Bam.Net.Data
{
    public class DaoProxyResult: JavaScriptResult
    {
        Incubator _serviceProvider = Incubator.Default;
        public DaoProxyResult(bool min)
        {
            this.Script = DaoProxyRegistration.GetScript().ToString();
            if (min) Compress();
        }

        private void Compress()
        {
            JavaScriptCompressor jsc = new JavaScriptCompressor();
            this.Script = jsc.Compress(this.Script);
        }

    }
}
