using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Yaml;

namespace Bam.Net.Data.Dynamic
{
    public interface IDynamicTypeNameResolver
    {
        string ResolveJsonTypeName(string json);
        string ResolveYamlTypeName(string yaml);
        string ResolveTypeName(JObject jobject);
    }
}
