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
MD Bam.Net.Profiguration\lib\%LIB%
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Profiguration.dll Bam.Net.Profiguration\lib\%LIB%\Bam.Net.Profiguration.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Profiguration.xml Bam.Net.Profiguration\lib\%LIB%\Bam.Net.Profiguration.xml
GOTO %NEXT%

:END


