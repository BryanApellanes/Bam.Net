For /f "tokens=2-4 delims=/ " %%a in ('date /t') do (set mydate=%%c-%%a-%%b)
For /f "tokens=1-2 delims=/:" %%a in ('time /t') do (set mytime=%%a%%b)

MD "Z:\Workspace\NugetPackages\%mydate%_%mytime%"
copy *.nupkg "Z:\Workspace\NugetPackages\Debug
move /Y *.nupkg "Z:\Workspace\NugetPackages\%mydate%_%mytime%"

MD "Z:\Workspace\Msi-debug\%mydate%_%mytime%"
copy .\BamToolkit-debug\lib\net45\BamToolkit-debug.msi "Z:\Workspace\Msi-debug\%mydate%_%mytime%"
copy /Y .\BamToolkit-debug\lib\net45\BamToolkit-debug.msi "Z:\Workspace\Msi-debug"