using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Bam.Net.Data.Repositories
{
    public partial class DtoModel
    {
        public string Render()
        {
            return Bam.Net.Handlebars.Render("Dto", this);
        }

    }
}
