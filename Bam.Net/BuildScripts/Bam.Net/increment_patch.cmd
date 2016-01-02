@echo off
call remove_nuspec_ro.cmd
nuver /patch /path:BamToolkit\BamToolkit.nuspec
nuver /patch /path:Bam.Net\Bam.Net.nuspec 
nuver /patch /path:Bam.Net.Analytics\Bam.Net.Analytics.nuspec
nuver /patch /path:Bam.Net.Automation\Bam.Net.Automation.nuspec
nuver /patch /path:Bam.Net.CommandLine\Bam.Net.CommandLine.nuspec 
nuver /patch /path:Bam.Net.Data\Bam.Net.Data.nuspec
nuver /patch /path:Bam.Net.Data.Dynamic\Bam.Net.Data.Dynamic.nuspec
nuver /patch /path:Bam.Net.Data.Repositories\Bam.Net.Data.Repositories.nuspec
nuver /patch /path:Bam.Net.Distributed\Bam.Net.Distributed.nuspec
nuver /patch /path:Bam.Net.Drawing\Bam.Net.Drawing.nuspec
nuver /patch /path:Bam.Net.Dust\Bam.Net.Dust.nuspec
nuver /patch /path:Bam.Net.Encryption\Bam.Net.Encryption.nuspec
nuver /patch /path:Bam.Net.Html\Bam.Net.Html.nuspec
nuver /patch /path:Bam.Net.Incubation\Bam.Net.Incubation.nuspec 
nuver /patch /path:Bam.Net.Javascript\Bam.Net.Javascript.nuspec
nuver /patch /path:Bam.Net.Logging\Bam.Net.Logging.nuspec 
nuver /patch /path:Bam.Net.Messaging\Bam.Net.Messaging.nuspec
nuver /patch /path:Bam.Net.Net\Bam.Net.Net.nuspec
nuver /patch /path:Bam.Net.Profiguration\Bam.Net.Profiguration.nuspec
nuver /patch /path:Bam.Net.Schema.Org\Bam.Net.Schema.Org.nuspec
nuver /patch /path:Bam.Net.Server\Bam.Net.Server.nuspec 
nuver /patch /path:Bam.Net.ServiceProxy\Bam.Net.ServiceProxy.nuspec 
nuver /patch /path:Bam.Net.SourceControl\Bam.Net.SourceControl.nuspec 
nuver /patch /path:Bam.Net.Syndication\Bam.Net.Syndication.nuspec 
nuver /patch /path:Bam.Net.Testing\Bam.Net.Testing.nuspec 
nuver /patch /path:Bam.Net.UserAccounts\Bam.Net.UserAccounts.nuspec
nuver /patch /path:Bam.Net.Yaml\Bam.Net.Yaml.nuspec


set /p patch=<patch.ver
set /a num=%patch%+1
echo %num% > patch.ver