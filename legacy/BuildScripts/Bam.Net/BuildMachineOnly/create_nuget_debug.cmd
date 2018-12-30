rem %1 major, %2 minor, %3 patch, %4 root
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
call build_bamcore.cmd Debug %ROOT%
call generate_bam_dot_exe_script.cmd
call generate_bam_dot_exe.cmd
call pack_debug.cmd

echo "Debug Nuget packages created"