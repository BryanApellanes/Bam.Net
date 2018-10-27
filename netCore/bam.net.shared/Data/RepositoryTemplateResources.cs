using System.Reflection;

namespace Bam.Net.Data
{
    public static class RepositoryTemplateResources
    {
        public static string Path
        {
            get
            {
                return $"{System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().GetFilePath())}.Data.Repositories.Templates.";
            }
        }
    }
}
