@echo on
call copy_all.cmd Debug

nuget pack Bam.Net.Data\Bam.Net.Data.nuspec
nuget pack BamDotExe\BamDotExe.nuspec
nuget pack BamCore\BamCore.nuspec
nuget pack Bam.Net.Analytics\Bam.Net.Analytics.nuspec
nuget pack Bam.Net.Automation\Bam.Net.Automation.nuspec
nuget pack Bam.Net\Bam.Net.nuspec
nuget pack Bam.Net.CommandLine\Bam.Net.CommandLine.nuspec
nuget pack Bam.Net.CoreServices\Bam.Net.CoreServices.nuspec
nuget pack Bam.Net.Data.Dynamic\Bam.Net.Data.Dynamic.nuspec
nuget pack Bam.Net.Data.Repositories\Bam.Net.Data.Repositories.nuspec
nuget pack Bam.Net.Documentation\Bam.Net.Documentation.nuspec
nuget pack Bam.Net.Drawing\Bam.Net.Drawing.nuspec
nuget pack Bam.Net.Dust\Bam.Net.Dust.nuspec
nuget pack Bam.Net.Encryption\Bam.Net.Encryption.nuspec
nuget pack Bam.Net.Html\Bam.Net.Html.nuspec
nuget pack Bam.Net.Incubation\Bam.Net.Incubation.nuspec
nuget pack Bam.Net.Javascript\Bam.Net.Javascript.nuspec
nuget pack Bam.Net.Logging\Bam.Net.Logging.nuspec
nuget pack Bam.Net.Messaging\Bam.Net.Messaging.nuspec
nuget pack Bam.Net.Net\Bam.Net.Net.nuspec
nuget pack Bam.Net.Profiguration\Bam.Net.Profiguration.nuspec
nuget pack Bam.Net.Schema.Org\Bam.Net.Schema.Org.nuspec
nuget pack Bam.Net.Server\Bam.Net.Server.nuspec
nuget pack Bam.Net.ServiceProxy\Bam.Net.ServiceProxy.nuspec
nuget pack Bam.Net.Syndication\Bam.Net.Syndication.nuspec
nuget pack Bam.Net.Testing\Bam.Net.Testing.nuspec
nuget pack Bam.Net.UserAccounts\Bam.Net.UserAccounts.nuspec
nuget pack Bam.Net.Yaml\Bam.Net.Yaml.nuspec
nuget pack Bam.Net.ServiceProxy.Tests\Bam.Net.ServiceProxy.Tests.nuspec
nuget pack Bam\Bam.nuspec
call build_toolkit_debug.cmd
