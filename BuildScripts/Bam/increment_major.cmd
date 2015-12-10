@echo off
call remove_nuspec_ro.cmd
nuver /major /path:BamToolkit\BamToolkit.nuspec
nuver /major /path:Bam.Net\Bam.Net.nuspec 
nuver /major /path:Bam.Net.Analytics\Bam.Net.Analytics.nuspec
nuver /major /path:Bam.Net.Automation\Bam.Net.Automation.nuspec
nuver /major /path:Bam.Net.CommandLine\Bam.Net.CommandLine.nuspec 
nuver /major /path:Bam.Net.Data\Bam.Net.Data.nuspec
nuver /major /path:Bam.Net.Data.Dynamic\Bam.Net.Data.Dynamic.nuspec
nuver /major /path:Bam.Net.Data.Repositories\Bam.Net.Data.Repositories.nuspec
nuver /major /path:Bam.Net.Distributed\Bam.Net.Distributed.nuspec
nuver /major /path:Bam.Net.Drawing\Bam.Net.Drawing.nuspec
nuver /major /path:Bam.Net.Dust\Bam.Net.Dust.nuspec
nuver /major /path:Bam.Net.Encryption\Bam.Net.Encryption.nuspec
nuver /major /path:Bam.Net.Html\Bam.Net.Html.nuspec
nuver /major /path:Bam.Net.Incubation\Bam.Net.Incubation.nuspec 
nuver /major /path:Bam.Net.Javascript\Bam.Net.Javascript.nuspec
nuver /major /path:Bam.Net.Logging\Bam.Net.Logging.nuspec 
nuver /major /path:Bam.Net.Messaging\Bam.Net.Messaging.nuspec
nuver /major /path:Bam.Net.Net\Bam.Net.Net.nuspec
nuver /major /path:Bam.Net.Profiguration\Bam.Net.Profiguration.nuspec
nuver /major /path:Bam.Net.Schema.Org\Bam.Net.Schema.Org.nuspec
nuver /major /path:Bam.Net.Server\Bam.Net.Server.nuspec 
nuver /major /path:Bam.Net.ServiceProxy\Bam.Net.ServiceProxy.nuspec 
nuver /major /path:Bam.Net.SourceControl\Bam.Net.SourceControl.nuspec 
nuver /major /path:Bam.Net.Syndication\Bam.Net.Syndication.nuspec 
nuver /major /path:Bam.Net.Testing\Bam.Net.Testing.nuspec 
nuver /major /path:Bam.Net.UserAccounts\Bam.Net.UserAccounts.nuspec
nuver /major /path:Bam.Net.Yaml\Bam.Net.Yaml.nuspec

set /p maj=<major.ver
set /a num=%maj%+1
echo %num% > major.ver