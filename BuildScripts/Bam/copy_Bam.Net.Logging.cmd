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
MD Bam.Net.Logging\lib\%LIB%
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Logging.dll Bam.Net.Logging\lib\%LIB%\Bam.Net.Logging.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Logging.xml Bam.Net.Logging\lib\%LIB%\Bam.Net.Logging.xml
GOTO %NEXT%

:END


