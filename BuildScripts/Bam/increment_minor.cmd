@echo off
call remove_nuspec_ro.cmd
nuver /minor /path:BamToolkit\BamToolkit.nuspec
nuver /minor /path:Bam.Net\Bam.Net.nuspec 
nuver /minor /path:Bam.Net.Analytics\Bam.Net.Analytics.nuspec
nuver /minor /path:Bam.Net.Automation\Bam.Net.Automation.nuspec
nuver /minor /path:Bam.Net.CommandLine\Bam.Net.CommandLine.nuspec 
nuver /minor /path:Bam.Net.Data\Bam.Net.Data.nuspec
nuver /minor /path:Bam.Net.Data.Dynamic\Bam.Net.Data.Dynamic.nuspec
nuver /minor /path:Bam.Net.Data.Repositories\Bam.Net.Data.Repositories.nuspec
nuver /minor /path:Bam.Net.Distributed\Bam.Net.Distributed.nuspec
nuver /minor /path:Bam.Net.Drawing\Bam.Net.Drawing.nuspec
nuver /minor /path:Bam.Net.Dust\Bam.Net.Dust.nuspec
nuver /minor /path:Bam.Net.Encryption\Bam.Net.Encryption.nuspec
nuver /minor /path:Bam.Net.Html\Bam.Net.Html.nuspec
nuver /minor /path:Bam.Net.Incubation\Bam.Net.Incubation.nuspec 
nuver /minor /path:Bam.Net.Javascript\Bam.Net.Javascript.nuspec
nuver /minor /path:Bam.Net.Logging\Bam.Net.Logging.nuspec 
nuver /minor /path:Bam.Net.Messaging\Bam.Net.Messaging.nuspec
nuver /minor /path:Bam.Net.Net\Bam.Net.Net.nuspec
nuver /minor /path:Bam.Net.Profiguration\Bam.Net.Profiguration.nuspec
nuver /minor /path:Bam.Net.Schema.Org\Bam.Net.Schema.Org.nuspec
nuver /minor /path:Bam.Net.Server\Bam.Net.Server.nuspec 
nuver /minor /path:Bam.Net.ServiceProxy\Bam.Net.ServiceProxy.nuspec 
nuver /minor /path:Bam.Net.SourceControl\Bam.Net.SourceControl.nuspec 
nuver /minor /path:Bam.Net.Syndication\Bam.Net.Syndication.nuspec 
nuver /minor /path:Bam.Net.Testing\Bam.Net.Testing.nuspec 
nuver /minor /path:Bam.Net.UserAccounts\Bam.Net.UserAccounts.nuspec
nuver /minor /path:Bam.Net.Yaml\Bam.Net.Yaml.nuspec

set /p minor=<minor.ver
set /a num=%minor%+1
echo %num% > minor.ver