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
MD Bam.Net.Testing\lib\%LIB%
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Testing.dll Bam.Net.Testing\lib\%LIB%\Bam.Net.Testing.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Testing.xml Bam.Net.Testing\lib\%LIB%\Bam.Net.Testing.xml
GOTO %NEXT%

:END


