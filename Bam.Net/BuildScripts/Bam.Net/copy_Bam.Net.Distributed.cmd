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
MD Bam.Net.Distributed\lib\%LIB%
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Distributed.dll Bam.Net.Distributed\lib\%LIB%\Bam.Net.Distributed.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Distributed.xml Bam.Net.Distributed\lib\%LIB%\Bam.Net.Distributed.xml
GOTO %NEXT%

:END


