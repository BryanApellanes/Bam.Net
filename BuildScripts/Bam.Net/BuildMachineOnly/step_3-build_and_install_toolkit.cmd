@echo off
C:
cd C:\src\Bam.Net\BuildScripts\Bam.Net
git checkout %1
call build_and_install_toolkit.cmd c:\src
Z:
cd \Workspace\Build

For /f "tokens=2-4 delims=/ " %%a in ('date /t') do (set mydate=%%c-%%a-%%b)
For /f "tokens=1-2 delims=/:" %%a in ("%TIME%") do (set mytime=%%a%%b)

echo %mydate%_%mytime% Step 3 Complete - Build and Install Toolkit
echo %mydate%_%mytime% Step 3 Complete - Build and Install Toolkit >> Z:\Workspace\build.log