@echo on
C:
cd C:\src\Bam.Net\BuildScripts\Bam
call generate_coverage_report.cmd
Z:
cd \Workspace\Build

For /f "tokens=2-4 delims=/ " %%a in ('date /t') do (set mydate=%%c-%%a-%%b)
For /f "tokens=1-2 delims=/:" %%a in ("%TIME%") do (set mytime=%%a%%b)

echo %mydate%_%mytime% Step 5 Complete - Generate Coverage Report
echo %mydate%_%mytime% Step 5 Complete - Generate Coverage Report >> Z:\Workspace\build.log