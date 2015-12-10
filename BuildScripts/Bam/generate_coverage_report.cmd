For /f "tokens=2-4 delims=/ " %%a in ('date /t') do (set mydate=%%c-%%a-%%b)
For /f "tokens=1-2 delims=/:" %%a in ('time /t') do (set mytime=%%a%%b)

".\ReportGenerator\ReportGenerator.exe" -reports:.\BuildOutput\Debug\v4.5\CodeCoverage.xml -targetdir:"Z:\Workspace\TestOutput\CodeCoverageReports\%mydate%_%mytime%"