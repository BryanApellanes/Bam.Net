cd .\BuildOutput\Debug\v4.5
"..\..\..\OpenCover\OpenCover.Console.exe" -target:".\bamtestrunner.exe" -targetargs:"/dir:. /search:*.Tests.exe /data:z:\Workspace\TestOutput /dataPrefix:%1" -register -filter:"+[Bam.Net*]*" -output:.\CodeCoverage.xml
cd ..\..