rem %1 major, %2 minor, %3 patch
IF [%1]==[] EXIT /B
IF [%2]==[] EXIT /B
IF [%3]==[] EXIT /B

SET ROOT=%4
IF [%4]==[] SET ROOT=C:\bam\src
C:
cd %ROOT%\Bam.Net\BuildScripts\Bam.Net

rem set assembly version
baminf /sai /v:"%1.%2.%3" /root:..\..\ /nuspecRoot:"."

rem set nuspec info
baminf /baminfo.json:%ROOT%\Bam.Net\baminfo.json /root:%ROOT%\Bam.Net /nuspecRoot:"." /v:%1.%2.%3

rem generate nuget scripts
baminf /generateNugetScripts /dnf:.\dll_names.txt /enf:.\exe_names.txt /tf:.\copy_template.txt

call build_solution.cmd Release %ROOT%
call generate_bam_dot_exe_script.cmd
call generate_bam_dot_exe.cmd
call pack.cmd

echo "Ready to push Nuget packages"

echo "type 'push %1.%2.%3' when ready"