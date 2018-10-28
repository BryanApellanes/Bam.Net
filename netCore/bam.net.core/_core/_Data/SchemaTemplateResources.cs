using System.Reflection;

namespace Bam.Net.Data
{
    public static class SchemaTemplateResources
    {
        public static string Path
        {
            get
            {
                return $"{typeof(SchemaTemplateResources).Namespace}.Schema.Templates.";
            }
        }
    }
}
