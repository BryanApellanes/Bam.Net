@echo off

SET LIB=net40
SET VER=v4.0
SET NEXT=NEXT
GOTO COPY

:NEXT
SET LIB=net45
SET VER=v4.5
SET NEXT=END
GOTO COPY

:COPY
MD Bam.Net.CommandLine\lib\%LIB%
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.CommandLine.dll Bam.Net.CommandLine\lib\%LIB%\Bam.Net.CommandLine.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.CommandLine.xml Bam.Net.CommandLine\lib\%LIB%\Bam.Net.CommandLine.xml
GOTO %NEXT%

:END


