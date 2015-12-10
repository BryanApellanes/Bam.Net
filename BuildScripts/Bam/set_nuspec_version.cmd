echo off
SET VERSIONKIND=%1
if %VERSIONKIND%=="" GOTO END
echo %VERSIONKIND%

nuver %VERSIONKIND% /path:BamToolkit\BamToolkit.nuspec 
nuver %VERSIONKIND% /path:Bam.Net\Bam.Net.nuspec 
nuver %VERSIONKIND% /path:Bam.Net.Analytics\Bam.Net.Analytics.nuspec
nuver %VERSIONKIND% /path:Bam.Net.ApplicationServices\Bam.Net.ApplicationServices.nuspec
nuver %VERSIONKIND% /path:Bam.Net.Automation\Bam.Net.Automation.nuspec
nuver %VERSIONKIND% /path:Bam.Net.CommandLine\Bam.Net.CommandLine.nuspec 
nuver %VERSIONKIND% /path:Bam.Net.Data\Bam.Net.Data.nuspec
nuver %VERSIONKIND% /path:Bam.Net.Data.Dynamic\Bam.Net.Data.Dynamic.nuspec
nuver %VERSIONKIND% /path:Bam.Net.Data.Repositories\Bam.Net.Data.Repositories.nuspec
nuver %VERSIONKIND% /path:Bam.Net.Distributed\Bam.Net.Distributed.nuspec
nuver %VERSIONKIND% /path:Bam.Net.Drawing\Bam.Net.Drawing.nuspec
nuver %VERSIONKIND% /path:Bam.Net.Dust\Bam.Net.Dust.nuspec
nuver %VERSIONKIND% /path:Bam.Net.Encryption\Bam.Net.Encryption.nuspec
nuver %VERSIONKIND% /path:Bam.Net.Html\Bam.Net.Html.nuspec
nuver %VERSIONKIND% /path:Bam.Net.Incubation\Bam.Net.Incubation.nuspec 
nuver %VERSIONKIND% /path:Bam.Net.Javascript\Bam.Net.Javascript.nuspec
nuver %VERSIONKIND% /path:Bam.Net.Logging\Bam.Net.Logging.nuspec 
nuver %VERSIONKIND% /path:Bam.Net.Management\Bam.Net.Management.nuspec
nuver %VERSIONKIND% /path:Bam.Net.Messaging\Bam.Net.Messaging.nuspec
nuver %VERSIONKIND% /path:Bam.Net.Net\Bam.Net.Net.nuspec
nuver %VERSIONKIND% /path:Bam.Net.Profiguration\Bam.Net.Profiguration.nuspec
nuver %VERSIONKIND% /path:Bam.Net.Schema.Org\Bam.Net.Schema.Org.nuspec
nuver %VERSIONKIND% /path:Bam.Net.Server\Bam.Net.Server.nuspec 
nuver %VERSIONKIND% /path:Bam.Net.ServiceProxy\Bam.Net.ServiceProxy.nuspec 
nuver %VERSIONKIND% /path:Bam.Net.SourceControl\Bam.Net.SourceControl.nuspec 
nuver %VERSIONKIND% /path:Bam.Net.Syndication\Bam.Net.Syndication.nuspec 
nuver %VERSIONKIND% /path:Bam.Net.Testing\Bam.Net.Testing.nuspec 
nuver %VERSIONKIND% /path:Bam.Net.UserAccounts\Bam.Net.UserAccounts.nuspec
nuver %VERSIONKIND% /path:Bam.Net.Yaml\Bam.Net.Yaml.nuspec

:END