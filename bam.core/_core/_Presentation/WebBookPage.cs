/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Presentation
{    
    public class WebBookPage
    {
        public WebBookPage()
        {
            this.Layout = "default";
        }
        public string Name { get; set; }
        public string Layout { get; set; }

        public object ViewModel { get; set; }
        
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
