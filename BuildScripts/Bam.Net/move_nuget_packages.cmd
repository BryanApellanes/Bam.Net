For /f "tokens=2-4 delims=/ " %%a in ('date /t') do (set mydate=%%c-%%a-%%b)
For /f "tokens=1-2 delims=/:" %%a in ('time /t') do (set mytime=%%a%%b)

MD "Z:\Workspace\NugetPackages\%mydate%_%mytime%"
copy *.nupkg "Z:\Workspace\NugetPackages\PUSH
move /Y *.nupkg "Z:\Workspace\NugetPackages\%mydate%_%mytime%"

MD "Z:\Workspace\Msi\%mydate%_%mytime%"
copy .\BamToolkit\lib\net45\BamToolkit.msi "Z:\Workspace\Msi\%mydate%_%mytime%"
copy /Y .\BamToolkit\lib\net45\BamToolkit.msi "Z:\Workspace\Msi"