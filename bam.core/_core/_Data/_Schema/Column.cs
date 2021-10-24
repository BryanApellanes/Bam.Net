using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Data.Schema
{
    public partial class Column
    {
        protected internal virtual string RenderClassProperty()
        {
            if (this.Key)
            {
                return Render("KeyProperty");
            }
            else
            {
                return Render("Property");
            }
        }

        protected internal virtual string RenderColumnsClassProperty()
        {
            return Render("ColumnsProperty");
        }

        protected string Render(string templateName)
        {
            return Bam.Net.Handlebars.Render(templateName, this);
        }
    }
}
