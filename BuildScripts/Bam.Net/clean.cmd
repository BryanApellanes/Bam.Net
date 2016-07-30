@echo on
SET LIB=net45
SET VER=v4.5
SET NEXT=END

RMDIR /S /Q .\BuildOutput
RMDIR /S /Q ..\..\Products\BUILDRMDIR /S /Q ..\..\BamCore\obj\
del /F /Q .\BamCore\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam.Net.Analytics\obj\
del /F /Q .\Bam.Net.Analytics\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam.Net.Automation\obj\
del /F /Q .\Bam.Net.Automation\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam.Net\obj\
del /F /Q .\Bam.Net\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam.Net.CommandLine\obj\
del /F /Q .\Bam.Net.CommandLine\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam.Net.CoreServices\obj\
del /F /Q .\Bam.Net.CoreServices\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam.Net.Data.Dynamic\obj\
del /F /Q .\Bam.Net.Data.Dynamic\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam.Net.Data.Repositories\obj\
del /F /Q .\Bam.Net.Data.Repositories\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam.Net.Distributed\obj\
del /F /Q .\Bam.Net.Distributed\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam.Net.Documentation\obj\
del /F /Q .\Bam.Net.Documentation\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam.Net.Drawing\obj\
del /F /Q .\Bam.Net.Drawing\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam.Net.Dust\obj\
del /F /Q .\Bam.Net.Dust\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam.Net.Encryption\obj\
del /F /Q .\Bam.Net.Encryption\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam.Net.Html\obj\
del /F /Q .\Bam.Net.Html\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam.Net.Incubation\obj\
del /F /Q .\Bam.Net.Incubation\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam.Net.Javascript\obj\
del /F /Q .\Bam.Net.Javascript\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam.Net.Logging\obj\
del /F /Q .\Bam.Net.Logging\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam.Net.Messaging\obj\
del /F /Q .\Bam.Net.Messaging\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam.Net.Net\obj\
del /F /Q .\Bam.Net.Net\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam.Net.Profiguration\obj\
del /F /Q .\Bam.Net.Profiguration\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam.Net.Schema.Org\obj\
del /F /Q .\Bam.Net.Schema.Org\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam.Net.Server\obj\
del /F /Q .\Bam.Net.Server\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam.Net.ServiceProxy\obj\
del /F /Q .\Bam.Net.ServiceProxy\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam.Net.Syndication\obj\
del /F /Q .\Bam.Net.Syndication\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam.Net.Testing\obj\
del /F /Q .\Bam.Net.Testing\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam.Net.UserAccounts\obj\
del /F /Q .\Bam.Net.UserAccounts\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam.Net.Yaml\obj\
del /F /Q .\Bam.Net.Yaml\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam.Net.ServiceProxy.Tests\obj\
del /F /Q .\Bam.Net.ServiceProxy.Tests\lib\%LIB%\*
RMDIR /S /Q ..\..\Bam\obj\
del /F /Q .\Bam\lib\%LIB%\*
