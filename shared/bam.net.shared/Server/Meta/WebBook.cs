/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsQuery;

namespace Bam.Net.Server.Meta
{
    [Obsolete("This class is obsolete, use Bam.Net.Presentation.WebBook instead.")]
	public class WebBook
	{
        public WebBook()
        {
            this.Pages = new HashSet<WebBookPage>();
        }

		public string Name { get; set; }

		public HashSet<WebBookPage> Pages { get; set; }
	}
}
