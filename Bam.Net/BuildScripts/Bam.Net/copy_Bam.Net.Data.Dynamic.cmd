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
MD Bam.Net.Data.Dynamic\lib\%LIB%
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Data.Dynamic.dll Bam.Net.Data.Dynamic\lib\%LIB%\Bam.Net.Data.Dynamic.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Data.Dynamic.xml Bam.Net.Data.Dynamic\lib\%LIB%\Bam.Net.Data.Dynamic.xml
GOTO %NEXT%

:END


