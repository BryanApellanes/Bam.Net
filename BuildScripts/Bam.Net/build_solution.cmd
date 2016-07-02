@echo on
SET ROOT=%1
IF [%1]==[] SET CONFIG=Release

SET ROOT=%2
IF [%2]==[] SET ROOT=C:\src

call %ROOT%\Bam.Net\Bam.Net.Server\zip_base_app_and_content.cmd %ROOT%
call .\restore.cmd
SET OutputPath=%ROOT%\Bam.Net\BuildScripts\Bam.Net\BuildOutput\%CONFIG%\

SET VER=v4.5
SET NEXT=END

rmdir /Q /S %OutputPath%%VER%
mkdir %OutputPath%%VER%
.\MSBuild\MSBuild.exe /t:Build /filelogger /p:AutoGenerateBindingRedirects=true;GenerateDocumentation=true;OutputPath=%OutputPath%%VER%;Configuration=%CONFIG%;Platform="x64";CompilerVersion=%VER% %2\Bam.Net\Bam.Net.Nuget.sln /m:4

EXIT /b %ERRORLEVEL%