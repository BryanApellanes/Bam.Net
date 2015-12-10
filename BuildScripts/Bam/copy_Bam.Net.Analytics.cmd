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
MD Bam.Net.Analytics\lib\%LIB%
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Analytics.dll Bam.Net.Analytics\lib\%LIB%\Bam.Net.Analytics.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Analytics.xml Bam.Net.Analytics\lib\%LIB%\Bam.Net.Analytics.xml
GOTO %NEXT%

:END


