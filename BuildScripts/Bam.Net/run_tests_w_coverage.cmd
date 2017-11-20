cd .\BuildOutput\Debug\v4.6.2
"..\..\..\OpenCover\OpenCover.Console.exe" -target:".\bamtestrunner.exe" -targetargs:"/dir:. /search:*.Tests.exe /data:z:\Workspace\TestOutput /dataPrefix:%1 /type:Unit" -register -filter:"+[Bam.Net*]* -[*].Data.* -[*Test*].Tests.*" -output:.\CodeCoverage.xml
cd ..\..