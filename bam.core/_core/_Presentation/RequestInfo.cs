using Bam.Net.Server;

namespace Bam.Net.Presentation
{
    public class RequestInfo
    {
        public string RequestPath { get; set; }
        public string RelativePath { get; set; }
        public RouteInfo RouteInfo { get; set; }

        /// <summary>
        /// Determines if the file referenced by RelativePath exists in the specified application
        /// </summary>
        /// <param name="appConf"></param>
        /// <returns></returns>
        public bool FileExists(AppConf appConf)
        {
            Args.ThrowIfNull(appConf, "appConf");
            Args.ThrowIfNull(appConf.AppRoot, "appConf.AppRoot");
            
            return appConf.AppRoot.FileExists(RelativePath);
        }

        public bool FileExists(AppConf appConf, out string absolutePath)
        { 
            Args.ThrowIfNull(appConf, "appConf");
            Args.ThrowIfNull(appConf.AppRoot, "appConf.AppRoot");
            
            return appConf.AppRoot.FileExists(RelativePath, out absolutePath);
        }
    }
}