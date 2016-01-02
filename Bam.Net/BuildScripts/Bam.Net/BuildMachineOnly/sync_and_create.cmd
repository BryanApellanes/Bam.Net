rem %1 - from (Bam.Net|Bam.Net), %2 - to (Bam.Net|Bam.Net), %3 - major, %4 - minor, %5 patch
C:
CD \

RMDIR /S /Q C:\src\%2

Z:
CD Z:\Workspace\Build
call sync-%1-%2.cmd

CD c:\src\%2\BuildScripts\%2

call clean.cmd
call set_ver.cmd %3 %4 %5
call build_solution.cmd Release v4.5 c:\src
call build_toolkit.cmd
call set_owners_and_authors.cmd "Bam DotNet"
call set_major_minor_patch.cmd

echo "Ready to build Nuget packages"
echo "use 'show' to see nuspec details"
echo "call pack"
echo "call push"