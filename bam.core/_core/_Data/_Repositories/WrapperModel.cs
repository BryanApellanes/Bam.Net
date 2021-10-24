using Bam.Net.CommandLine;
using Bam.Net.Data.Schema;
using Bam.Net.ServiceProxy;
using Bam.Net.Testing;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Bam.Net.Data.Repositories
{
    public partial class WrapperModel
    {
        public virtual string Render()
        {
            return Bam.Net.Handlebars.Render("Wrapper", this);
        }
    }
}
