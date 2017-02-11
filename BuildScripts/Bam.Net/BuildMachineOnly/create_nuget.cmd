rem %1 major, %2 minor, %3 patch
SET ROOT=%4
IF [%4]==[] SET ROOT=C:\src
C:
cd %ROOT%\Bam.Net\BuildScripts\Bam.Net
call clean.cmd
call set_assembly_version.cmd %1 %2 %3
call set_nuspec_info.cmd %1 %2 %3 %4
call generate_nuget_scripts.cmd
call set_msi_version.cmd %1.%2.%3 %ROOT%
call build_solution.cmd Release %ROOT%
call build_bamcore.cmd Release %ROOT%
call generate_bam_dot_exe_script.cmd
call generate_bam_dot_exe.cmd
call pack.cmd

echo "Ready to push Nuget packages"

echo "type 'push %1.%2.%3' when ready"