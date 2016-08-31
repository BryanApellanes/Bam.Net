
@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net45
cd .\BuildOutput\%CONFIG%\%VER%
md ..\..\..\BamDotExe\lib\%LIB%\
..\\..\\..\\ilmerge.exe bam.exe BamCore.dll Bam.Net.Analytics.dll Bam.Net.Automation.dll Bam.Net.dll Bam.Net.CommandLine.dll Bam.Net.CoreServices.dll Bam.Net.Data.Dynamic.dll Bam.Net.Data.Repositories.dll Bam.Net.Distributed.dll Bam.Net.Documentation.dll Bam.Net.Drawing.dll Bam.Net.Dust.dll Bam.Net.Encryption.dll Bam.Net.Html.dll Bam.Net.Incubation.dll Bam.Net.Javascript.dll Bam.Net.Logging.dll Bam.Net.Messaging.dll Bam.Net.Net.dll Bam.Net.Profiguration.dll Bam.Net.Schema.Org.dll Bam.Net.Server.dll Bam.Net.ServiceProxy.dll Bam.Net.Syndication.dll Bam.Net.Testing.dll Bam.Net.UserAccounts.dll Bam.Net.Yaml.dll /closed /targetplatform:v4 /lib:"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5" /out:..\..\..\BamDotExe\lib\%LIB%\bam.exe