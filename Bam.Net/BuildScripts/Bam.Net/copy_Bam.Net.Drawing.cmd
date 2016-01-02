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
MD Bam.Net.Drawing\lib\%LIB%
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Drawing.dll Bam.Net.Drawing\lib\%LIB%\Bam.Net.Drawing.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Drawing.xml Bam.Net.Drawing\lib\%LIB%\Bam.Net.Drawing.xml
GOTO %NEXT%

:END


