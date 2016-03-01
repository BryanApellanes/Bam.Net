SET ROOT=%1
IF [%1]==[] SET ROOT=C:\src
C:
cd %ROOT%\Bam.Net\
NuGet.exe restore Bam.Net.Nuget.sln