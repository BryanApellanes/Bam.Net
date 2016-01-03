
call clean.cmd
call set_ver.cmd %1 %2 %3
call build_solution.cmd Release v4.5 c:\src
call build_toolkit.cmd
call set_owners_and_authors.cmd "Bryan Apellanes"
call set_major_minor_patch.cmd

echo "Ready to build Nuget packages"
echo "use 'show' to see nuspec details"
echo "call pack"
echo "call push"