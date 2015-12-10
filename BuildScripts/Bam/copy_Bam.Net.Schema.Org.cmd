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
MD Bam.Net.Schema.Org\lib\%LIB%
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Schema.Org.dll Bam.Net.Schema.Org\lib\%LIB%\Bam.Net.Schema.Org.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Schema.Org.xml Bam.Net.Schema.Org\lib\%LIB%\Bam.Net.Schema.Org.xml
GOTO %NEXT%

:END


