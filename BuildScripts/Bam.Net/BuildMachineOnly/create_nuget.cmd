rem %1 major, %2 minor, %3 patch
call clean.cmd
call set_ver.cmd %1 %2 %3
call build_solution.cmd Release v4.5 c:\src
call build_toolkit.cmd
call set_owners_and_authors.cmd "Bryan Apellanes"
call set_major_minor_patch.cmd
call pack.cmd

echo "Ready to push Nuget packages"
echo "use 'show' to see nuspec details"

echo "type 'push' when ready"