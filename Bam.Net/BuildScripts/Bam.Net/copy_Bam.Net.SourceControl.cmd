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
MD Bam.Net.SourceControl\lib\%LIB%
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.SourceControl.dll Bam.Net.SourceControl\lib\%LIB%\Bam.Net.SourceControl.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.SourceControl.xml Bam.Net.SourceControl\lib\%LIB%\Bam.Net.SourceControl.xml
GOTO %NEXT%

:END


