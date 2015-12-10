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
MD Bam.Net.ApplicationServices\lib\%LIB%
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.ApplicationServices.dll Bam.Net.ApplicationServices\lib\%LIB%\Bam.Net.ApplicationServices.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.ApplicationServices.xml Bam.Net.ApplicationServices\lib\%LIB%\Bam.Net.ApplicationServices.xml
GOTO %NEXT%

:END


