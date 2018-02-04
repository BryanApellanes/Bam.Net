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
