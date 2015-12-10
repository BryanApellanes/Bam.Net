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
MD Bam.Net.Html\lib\%LIB%
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Html.dll Bam.Net.Html\lib\%LIB%\Bam.Net.Html.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Html.xml Bam.Net.Html\lib\%LIB%\Bam.Net.Html.xml
GOTO %NEXT%

:END


