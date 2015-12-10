@echo on
SET EnableNuGetPackageRestore=true
Z:
cd \Workspace\Build
call nugetrestore.cmd
Z:
cd \Workspace\Build


For /f "tokens=2-4 delims=/ " %%a in ('date /t') do (set mydate=%%c-%%a-%%b)
For /f "tokens=1-2 delims=/:" %%a in ("%TIME%") do (set mytime=%%a%%b)

echo %mydate%_%mytime% Step 2 Complete - Nuget Restore

echo %mydate%_%mytime% Step 2 Complete - Nuget Restore >> Z:\Workspace\build.log
