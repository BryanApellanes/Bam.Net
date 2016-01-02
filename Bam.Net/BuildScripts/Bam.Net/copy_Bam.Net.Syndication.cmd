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
MD Bam.Net.Syndication\lib\%LIB%
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Syndication.dll Bam.Net.Syndication\lib\%LIB%\Bam.Net.Syndication.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Syndication.xml Bam.Net.Syndication\lib\%LIB%\Bam.Net.Syndication.xml
GOTO %NEXT%

:END


