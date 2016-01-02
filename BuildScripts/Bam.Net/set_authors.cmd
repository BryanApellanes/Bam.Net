@echo off
call remove_nuspec_ro.cmd
nuver /a:%1 /path:BamToolkit\BamToolkit.nuspec
nuver /a:%1 /path:Bam.Net\Bam.Net.nuspec 
nuver /a:%1 /path:Bam.Net.Analytics\Bam.Net.Analytics.nuspec
nuver /a:%1 /path:Bam.Net.Automation\Bam.Net.Automation.nuspec
nuver /a:%1 /path:Bam.Net.CommandLine\Bam.Net.CommandLine.nuspec 
nuver /a:%1 /path:Bam.Net.Data\Bam.Net.Data.nuspec
nuver /a:%1 /path:Bam.Net.Data.Repositories\Bam.Net.Data.Repositories.nuspec
nuver /a:%1 /path:Bam.Net.Distributed\Bam.Net.Distributed.nuspec
nuver /a:%1 /path:Bam.Net.Drawing\Bam.Net.Drawing.nuspec
nuver /a:%1 /path:Bam.Net.Dust\Bam.Net.Dust.nuspec
nuver /a:%1 /path:Bam.Net.Encryption\Bam.Net.Encryption.nuspec
nuver /a:%1 /path:Bam.Net.Html\Bam.Net.Html.nuspec
nuver /a:%1 /path:Bam.Net.Incubation\Bam.Net.Incubation.nuspec 
nuver /a:%1 /path:Bam.Net.Javascript\Bam.Net.Javascript.nuspec
nuver /a:%1 /path:Bam.Net.Logging\Bam.Net.Logging.nuspec 
nuver /a:%1 /path:Bam.Net.Messaging\Bam.Net.Messaging.nuspec
nuver /a:%1 /path:Bam.Net.Net\Bam.Net.Net.nuspec
nuver /a:%1 /path:Bam.Net.Profiguration\Bam.Net.Profiguration.nuspec
nuver /a:%1 /path:Bam.Net.Schema.Org\Bam.Net.Schema.Org.nuspec
nuver /a:%1 /path:Bam.Net.Server\Bam.Net.Server.nuspec 
nuver /a:%1 /path:Bam.Net.ServiceProxy\Bam.Net.ServiceProxy.nuspec 
nuver /a:%1 /path:Bam.Net.SourceControl\Bam.Net.SourceControl.nuspec 
nuver /a:%1 /path:Bam.Net.Syndication\Bam.Net.Syndication.nuspec 
nuver /a:%1 /path:Bam.Net.Testing\Bam.Net.Testing.nuspec 
nuver /a:%1 /path:Bam.Net.UserAccounts\Bam.Net.UserAccounts.nuspec
nuver /a:%1 /path:Bam.Net.Yaml\Bam.Net.Yaml.nuspec