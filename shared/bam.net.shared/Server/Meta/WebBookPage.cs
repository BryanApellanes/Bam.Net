/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Meta
{    
    [Obsolete("This class is obsolete; use Bam.Net.Presentation.WebBookPage instead.")]
    public class WebBookPage
    {
        public WebBookPage()
        {
            this.Layout = "default";
        }
        public string Name { get; set; }
        public string Layout { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is WebBookPage page)
            {
                return page.Name.Equals(this.Name) && page.Layout.Equals(this.Layout);
            }
            else
            {
                return base.Equals(obj);
            }
        }
        public override int GetHashCode()
        {
            return "{0}.{1}"._Format(this.Name, this.Layout).GetHashCode();
        }
    }

}
