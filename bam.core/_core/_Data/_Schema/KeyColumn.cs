using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Data.Schema
{
    public partial class KeyColumn
    {
        protected internal override string RenderClassProperty()
        {
            return Bam.Net.Handlebars.Render("KeyProperty", this);
        }
    }
}
