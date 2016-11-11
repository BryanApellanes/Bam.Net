SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET ROOT=%2
IF [%2]==[] SET ROOT=C:\src
SET OutputPath=%ROOT%\Bam.Net\BuildScripts\Bam.Net\BuildOutput\%CONFIG%\

xcopy .\BamCoreProject\* .\BuildOutput\%CONFIG%\v4.6.2\ /S /Y

.\MSBuild\MSBuild.exe /t:Build /filelogger /p:OutputPath=%OutputPath%v4.6.2;Configuration=%CONFIG%;CompilerVersion=v4.6.2 %OutputPath%\v4.6.2\BamCore.sln /m:4