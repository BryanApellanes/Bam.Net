@echo on
call %2\Bam.Net\Bam.Net.Server\zip_base_app_and_content.cmd %2
call .\restore.cmd
SET OutputPath=%2\Bam.Net\BuildScripts\Bam.Net\BuildOutput\%1\

SET VER=v4.5
SET NEXT=END

rmdir /Q /S %OutputPath%%VER%
mkdir %OutputPath%%VER%
.\MSBuild\MSBuild.exe /t:Build /filelogger /p:AutoGenerateBindingRedirects=true;GenerateDocumentation=true;OutputPath=%OutputPath%%VER%;Configuration=%1;Platform="x64";CompilerVersion=%VER% %2\Bam.Net\Bam.Net.Nuget.sln /m:4

EXIT /b %ERRORLEVEL%