rem %1 major, %2 minor, %3 patch
SET ROOT=%4
IF [%4]==[] SET ROOT=C:\src
C:
cd %ROOT%\Bam.Net\BuildScripts\Bam.Net
call clean.cmd
call set_assembly_version.cmd %1 %2 %3
call set_nuspec_info.cmd %1 %2 %3
call generate_nuget_scripts.cmd
call set_msi_version_debug.cmd %1.%2.%3 %ROOT%
call build_solution.cmd Debug %ROOT%
call pack_debug.cmd

echo "Debug Nuget packages created"