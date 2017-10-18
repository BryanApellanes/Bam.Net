/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Html
{
    public class Script: Tag
    {
        public Script(string src)
            : base("script", new { type = "text/javascript" })
        {
            this.Attr("src", src);
        }
    }
}
