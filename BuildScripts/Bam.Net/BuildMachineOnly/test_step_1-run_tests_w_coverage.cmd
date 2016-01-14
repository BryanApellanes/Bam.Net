@echo off
C:
cd C:\src\Bam.Net\BuildScripts\Bam.Net
git checkout %1
call build_and_run_tests_w_coverage.cmd C:\src %1
Z:
cd \Workspace\Build


For /f "tokens=2-4 delims=/ " %%a in ('date /t') do (set mydate=%%c-%%a-%%b)
For /f "tokens=1-2 delims=/:" %%a in ("%TIME%") do (set mytime=%%a%%b)

echo %mydate%_%mytime% Step 4 Complete - Build and Run Tests With Coverage
echo %mydate%_%mytime% Step 4 Complete - Build and Run Tests With Coverage >> Z:\Workspace\build.log