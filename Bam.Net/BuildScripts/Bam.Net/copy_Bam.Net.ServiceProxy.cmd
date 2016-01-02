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
MD Bam.Net.ServiceProxy\lib\%LIB%
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.ServiceProxy.dll Bam.Net.ServiceProxy\lib\%LIB%\Bam.Net.ServiceProxy.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.ServiceProxy.xml Bam.Net.ServiceProxy\lib\%LIB%\Bam.Net.ServiceProxy.xml
GOTO %NEXT%

:END


