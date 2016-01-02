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
MD Bam.Net.Server\lib\%LIB%
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Server.dll Bam.Net.Server\lib\%LIB%\Bam.Net.Server.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Server.xml Bam.Net.Server\lib\%LIB%\Bam.Net.Server.xml
GOTO %NEXT%

:END


