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
MD Bam.Net.Net\lib\%LIB%
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Net.dll Bam.Net.Net\lib\%LIB%\Bam.Net.Net.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Net.xml Bam.Net.Net\lib\%LIB%\Bam.Net.Net.xml
GOTO %NEXT%

:END


