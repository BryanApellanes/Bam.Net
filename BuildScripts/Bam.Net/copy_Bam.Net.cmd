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
MD Bam.Net\lib\%LIB%\
copy /Y /D .\BuildOutput\Release\%VER%\Bam.Net.dll Bam.Net\lib\%LIB%\Bam.Net.dll
copy /Y /D .\BuildOutput\Release\%VER%\Bam.Net.xml Bam.Net\lib\%LIB%\Bam.Net.xml
GOTO %NEXT%

:END


