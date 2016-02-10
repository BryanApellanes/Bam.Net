rem %1 major, %2 minor, %3 patch
C:
cd C:\src\Bam.Net\BuildScripts\Bam.Net
call clean.cmd
call set_assembly_version.cmd %1 %2 %3
call set_nuspec_info.cmd %1 %2 %3
call build_solution.cmd Release v4.5 c:\src
call build_toolkit.cmd
call pack.cmd

echo "Ready to push Nuget packages"

echo "type 'push %1.%2.%3' when ready"