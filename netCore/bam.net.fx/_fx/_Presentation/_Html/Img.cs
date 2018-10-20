/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Presentation.Html
{
    public class Img: Tag
    {
        public Img(string src)
            : base("img", new { src = src })
        {
        }         
    }
}
