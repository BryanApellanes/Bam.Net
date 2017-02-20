/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Javascript
{
    public class MinifyResult
    {
        public MinifyResult(string script)
        {
            this.Script = script;
            try
            {
                this.MinScript = script.Minify();
                this.Success = true;
            }
            catch (Exception ex)
            {
                this.Success = false;
                this.MinScript = script;
                this.Exception = ex;
            }
        }

        public bool Success { get; set; }
        public string Script { get; set; }
        public string MinScript { get; set; }

        public Exception Exception
        {
            get;
            set;
        }
    }
}
