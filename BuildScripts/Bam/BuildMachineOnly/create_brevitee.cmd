C:
CD \

RMDIR /S /Q C:\src\Brevitee

CD c:\src\Bam.Net\sync
call sync-BA-Brevitee.cmd

CD c:\src\Brevitee\BuildScripts\Brevitee

call clean.cmd

call build_solution.cmd Release v4.5 c:\src
call build_toolkit.cmd

echo "Ready to build Nuget packages"
echo "use 'show' to see nuspec details"
echo "use 'set_owners_and_authors' -> provide name"
echo "call set_major_minor_patch after setting each in the files of same name"
echo "call pack"
echo "call push"
