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
MD Bam.Net.Dust\lib\%LIB%
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Dust.dll Bam.Net.Dust\lib\%LIB%\Bam.Net.Dust.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Dust.xml Bam.Net.Dust\lib\%LIB%\Bam.Net.Dust.xml
GOTO %NEXT%

:END


