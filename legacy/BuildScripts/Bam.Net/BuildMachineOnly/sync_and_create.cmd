rem %1 - from (Bam.Net|Bam.Net), %2 - to (Bam.Net|Bam.Net), %3 - major, %4 - minor, %5 patch
C:
CD \

RMDIR /S /Q C:\src\%2

Z:
CD Z:\Workspace\Build
call sync-%1-%2.cmd

CD c:\src\%2\BuildScripts\%2

call create_nuget.cmd %3 %4 %5
