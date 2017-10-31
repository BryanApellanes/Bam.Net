using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Razor;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Services.OpenApi
{
    public class OpenApiObjectDescriptorModel
    {
        public string Namespace { get; set; }
        public string ObjectName { get; set; }
        public string FormattedDescription
        {
            get
            {
                return ObjectDescription.Replace("\n", "\n\t/// ");
            }
        }

        public string ObjectDescription { get; set; }

        public List<OpenApiFixedFieldModel> FixedFields { get; set; }

        public string Render()
        {
            List<Assembly> referenceAssemblies = new List<Assembly>
            {
                typeof(OpenApiObjectDatabase).Assembly,
                    typeof(ServiceProxyHelper).Assembly,
                    typeof(Args).Assembly
            };

            RazorParser<OpenApiObjectDescriptorModelTemplate> parser = new RazorParser<OpenApiObjectDescriptorModelTemplate>();
            
            string result = parser.ExecuteResource("ObjectDescriptor.tmpl", "Bam.Net.Services.OpenApi.Templates.", typeof(OpenApiObjectDatabase).Assembly, new { Model = this }, referenceAssemblies.ToArray());
            return result;
        }
    }
}
