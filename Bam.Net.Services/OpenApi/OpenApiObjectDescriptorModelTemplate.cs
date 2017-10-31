using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Razor;

namespace Bam.Net.Services.OpenApi
{
    public abstract class OpenApiObjectDescriptorModelTemplate : RazorTemplate<OpenApiObjectDescriptorModel>
    {
        public void WriteFixedField(OpenApiFixedFieldModel fixedField)
        {
            Write(fixedField.Render());
        }
    }
}
