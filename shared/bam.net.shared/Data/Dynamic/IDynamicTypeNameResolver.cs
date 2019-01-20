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
        string[] ResolveYamlTypeNames(string yaml);
        string ResolveTypeName(JObject jobject);
        string ResolveTypeName(YamlNode yaml);
    }
}
