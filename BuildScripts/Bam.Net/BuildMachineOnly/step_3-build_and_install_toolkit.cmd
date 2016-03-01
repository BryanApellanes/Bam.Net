@echo off
SET ROOT=%1
IF [%1]==[] SET ROOT=C:\src
C:
cd %ROOT%\Bam.Net\BuildScripts\Bam.Net
call build_and_install_toolkit.cmd %ROOT%
Z:
cd \Workspace\Build

For /f "tokens=2-4 delims=/ " %%a in ('date /t') do (set mydate=%%c-%%a-%%b)
For /f "tokens=1-2 delims=/:" %%a in ("%TIME%") do (set mytime=%%a%%b)

echo %mydate%_%mytime% Step 3 Complete - Build and Install Toolkit
echo %mydate%_%mytime% Step 3 Complete - Build and Install Toolkit >> Z:\Workspace\build.log