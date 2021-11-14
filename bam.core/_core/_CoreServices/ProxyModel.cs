using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Bam.Net.CoreServices
{
    public partial class ProxyModel
    {
        public string Render()
        {
            return Bam.Net.Handlebars.Render("Proxy", this);
        }
    }
}
