using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Razor;
using Bam.Net.ServiceProxy;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi
{
    public class OpenApiFixedFieldModel
    {
        public string FieldName { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public string ClrType
        {
            get
            {
                if (Type.Trim().StartsWith("Map", StringComparison.InvariantCultureIgnoreCase))
                {
                    return "Dictionary<string, object>";
                }
                else if (Type.Trim().StartsWith("Any"))
                {
                    return "object";
                }
                else
                {
                    string[] split = Type.DelimitSplit("[", "]", "<", ">");
                    string clrType = split.Length > 1 ? split[1].PascalCase() : split[0];
                    if (clrType.Equals("boolean"))
                    {
                        return "bool";
                    }
                    return clrType;
                }
            }
        }

        public string PropertyName
        {
            get
            {
                return FieldName.Replace("$", "").PascalCase(true, " ");
            }
        }

        public string Render()
        {
            List<Assembly> referenceAssemblies = new List<Assembly>
            {
                typeof(OpenApiObjectDatabase).Assembly,
                    typeof(ServiceProxyHelper).Assembly,
                    typeof(Args).Assembly
            };

            RazorParser<RazorTemplate<OpenApiFixedFieldModel>> parser = new RazorParser<RazorTemplate<OpenApiFixedFieldModel>>();
            string result = parser.ExecuteResource("FixedField.tmpl", "Bam.Net.Services.OpenApi.Templates.", typeof(OpenApiObjectDatabase).Assembly, new { Model = this }, referenceAssemblies.ToArray());
            return result;
        }
    }
}
