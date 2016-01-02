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
MD Bam.Net.Automation\lib\%LIB%
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Automation.dll Bam.Net.Automation\lib\%LIB%\Bam.Net.Automation.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Automation.xml Bam.Net.Automation\lib\%LIB%\Bam.Net.Automation.xml
GOTO %NEXT%

:END


