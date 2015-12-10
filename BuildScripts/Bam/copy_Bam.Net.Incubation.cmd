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
MD Bam.Net.Incubation\lib\%LIB%
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Incubation.dll Bam.Net.Incubation\lib\%LIB%\Bam.Net.Incubation.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Incubation.xml Bam.Net.Incubation\lib\%LIB%\Bam.Net.Incubation.xml
GOTO %NEXT%

:END


