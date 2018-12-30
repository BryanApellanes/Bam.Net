@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release

SET ROOT=%2
IF [%2]==[] SET ROOT=C:\src

call %ROOT%\Bam.Net\Bam.Net.Server\zip_base_app_and_content.cmd %ROOT%
call .\restore.cmd
SET OutputPath=%ROOT%\Bam.Net\BuildScripts\Bam.Net\BuildOutput\%CONFIG%\

SET VER=%3
IF [%3]==[] SET VER=v4.6.2

rmdir /Q /S %OutputPath%%VER%
mkdir %OutputPath%%VER%
.\MSBuild\MSBuild.exe /t:Build /filelogger /p:AutoGenerateBindingRedirects=true;GenerateDocumentation=true;OutputPath=%OutputPath%%VER%;Configuration=%CONFIG%;Platform="x64";TargetFrameworkVersion=%VER%;CompilerVersion=%VER% %ROOT%\Bam.Net\Bam.Net.Nuget.sln /m:1

EXIT /b %ERRORLEVEL%