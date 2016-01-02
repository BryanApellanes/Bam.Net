@echo off
call %3\Bam.Net\Bam.Net.Server\zip_base_app_and_content.cmd %3
call .\restore.cmd
SET OutputPath=%3\Bam.Net\BuildScripts\Bam.Net\BuildOutput\%1\

if "%2" == "v4.5" GOTO NEXT

SET VER=v4.0
SET NEXT=NEXT
GOTO BUILD

:NEXT
SET VER=v4.5
SET NEXT=END
GOTO BUILD

:BUILD

rmdir /Q /S %OutputPath%%VER%
mkdir %OutputPath%%VER%
.\MSBuild\MSBuild.exe /t:Build /filelogger /p:AutoGenerateBindingRedirects=true;GenerateDocumentation=true;OutputPath=%OutputPath%%VER%;Configuration=%1;Platform="x64";CompilerVersion=%VER% %3\Bam.Net\Bam.Net.Nuget.sln /m

IF ERRORLEVEL 1 GOTO END

GOTO %NEXT%

:END
EXIT /b %ERRORLEVEL%