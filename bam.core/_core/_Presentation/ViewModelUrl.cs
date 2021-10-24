using Bam.Net.Data.Repositories;

namespace Bam.Net.Presentation
{
    public class ViewModelUrl: KeyedAuditRepoData
    {
        // "bam://OrganizationName::ApplicationName@HostName/RelativeFilePath"
        public static implicit operator string(ViewModelUrl viewModelUrl)
        {
            return viewModelUrl.ToString();
        }

        public static implicit operator ViewModelUrl(string viewModelUrl)
        {
            return ViewModelUrl.FromString(viewModelUrl);
        }
        
        public string OrganizationName { get; set; }
        public string ApplicationName { get; set; }
        public string RelativeFilePath { get; set; }
        public string HostName { get; set; }

        public override string ToString()
        {
            string hostName = !string.IsNullOrEmpty(HostName) ? $"@{HostName}" : string.Empty;
            return $"bam://{OrganizationName ?? ApplicationDiagnosticInfo.PublicOrganization}::{ApplicationName ?? ApplicationDiagnosticInfo.UnknownApplication}{hostName}/{RelativeFilePath}";
        }

        public static ViewModelUrl FromString(string viewModelUrl)
        {
            string beginning = viewModelUrl.ReadUntil("//", out string orgAppPrefixedUrl);
            string organizationName = orgAppPrefixedUrl.ReadUntil("::", out string appPrefixedUrl);
            string applicationName = string.Empty;
            string hostName = string.Empty;
            string relativeFilePath = string.Empty;
            if (appPrefixedUrl.Contains("@"))
            {
                applicationName = appPrefixedUrl.ReadUntil("@", out string hostPrefixedUrl);
                hostName = hostPrefixedUrl.ReadUntil("/", out string filePath);
                relativeFilePath = filePath;
            }
            else
            {
                applicationName = appPrefixedUrl.ReadUntil("/", out string filePath);
                relativeFilePath = filePath;
            }
            
            return new ViewModelUrl{OrganizationName = organizationName, ApplicationName = applicationName, HostName = hostName, RelativeFilePath = relativeFilePath};
        }
    }
}