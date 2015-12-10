rem @echo off

SET LIB=net40
SET VER=v4.0
SET NEXT=NEXT
GOTO COPY

:NEXT
SET LIB=net45
SET VER=v4.5
SET NEXT=END
GOTO COPY

:COPY
del /F /Q .\Bam.Net\lib\%LIB%\*
del /F /Q .\Bam.Net.Analytics\lib\%LIB%\*
del /F /Q .\Bam.Net.Automation\lib\%LIB%\*
del /F /Q .\Bam.Net.CommandLine\lib\%LIB%\*
del /F /Q .\Bam.Net.Data\lib\%LIB%\*
del /F /Q .\Bam.Net.Data.Repositories\lib\%LIB%\*
del /F /Q .\Bam.Net.Distributed\lib\%LIB%\*
del /F /Q .\Bam.Net.Drawing\lib\%LIB%\*
del /F /Q .\Bam.Net.Dust\lib\%LIB%\*
del /F /Q .\Bam.Net.Encryption\lib\%LIB%\*
del /F /Q .\Bam.Net.Html\lib\%LIB%\Bam.Net.Html.*
del /F /Q .\Bam.Net.Incubation\lib\%LIB%\*
del /F /Q .\Bam.Net.Javascript\lib\%LIB%\*
del /F /Q .\Bam.Net.Logging\lib\%LIB%\*
del /F /Q .\Bam.Net.Management\lib\%LIB%\*
del /F /Q .\Bam.Net.Messaging\lib\%LIB%\*
del /F /Q .\Bam.Net.Net\lib\%LIB%\*
del /F /Q .\Bam.Net.Profiguration\lib\%LIB%\*
del /F /Q .\Bam.Net.Schema.Org\lib\%LIB%\*
del /F /Q .\Bam.Net.Server\lib\%LIB%\*
del /F /Q .\Bam.Net.ServiceProxy\lib\%LIB%\*
del /F /Q .\Bam.Net.SourceControl\lib\%LIB%\*
del /F /Q .\Bam.Net.Syndication\lib\%LIB%\*
del /F /Q .\Bam.Net.Testing\lib\%LIB%\*
del /F /Q .\Bam.Net.UserAccounts\lib\%LIB%\*
del /F /Q .\Bam.Net.Yaml\lib\%LIB%\*
del /F /Q .\BamToolkit\lib\%LIB%\*

GOTO %NEXT%

:END

rmdir /S /Q .\BuildOutput
rmdir /S /Q ..\..\Products\BUILD

RMDIR /S /Q ..\..\Bam.Net\obj\
RMDIR /S /Q ..\..\Bam.Net.Analytics\obj\
RMDIR /S /Q ..\..\Bam.Net.Automation\obj\
RMDIR /S /Q ..\..\Bam.Net.CommandLine\obj\
RMDIR /S /Q ..\..\Bam.Net.Data\obj\
RMDIR /S /Q ..\..\Bam.Net.Distributed\obj\
RMDIR /S /Q ..\..\Bam.Net.Drawing\obj\
RMDIR /S /Q ..\..\Bam.Net.Dust\obj\
RMDIR /S /Q ..\..\Bam.Net.Encryption\obj\
RMDIR /S /Q ..\..\Bam.Net.Html\obj\
RMDIR /S /Q ..\..\Bam.Net.Incubation\obj\
RMDIR /S /Q ..\..\Bam.Net.Javascript\obj\
RMDIR /S /Q ..\..\Bam.Net.Logging\obj\
RMDIR /S /Q ..\..\Bam.Net.Management\obj\
RMDIR /S /Q ..\..\Bam.Net.Messaging\obj\
RMDIR /S /Q ..\..\Bam.Net.Net\obj\
RMDIR /S /Q ..\..\Bam.Net.Profiguration\obj\
RMDIR /S /Q ..\..\Bam.Net.Schema.Org\obj\
RMDIR /S /Q ..\..\Bam.Net.Server\obj\
RMDIR /S /Q ..\..\Bam.Net.ServiceProxy\obj\
RMDIR /S /Q ..\..\Bam.Net.SourceControl\obj\
RMDIR /S /Q ..\..\Bam.Net.Syndication\obj\
RMDIR /S /Q ..\..\Bam.Net.Testing\obj\
RMDIR /S /Q ..\..\Bam.Net.UserAccounts\obj\
RMDIR /S /Q ..\..\Bam.Net.Yaml\obj\

