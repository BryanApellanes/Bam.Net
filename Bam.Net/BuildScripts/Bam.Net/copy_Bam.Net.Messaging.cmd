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
MD Bam.Net.Messaging\lib\%LIB%
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Messaging.dll Bam.Net.Messaging\lib\%LIB%\Bam.Net.Messaging.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Messaging.xml Bam.Net.Messaging\lib\%LIB%\Bam.Net.Messaging.xml
GOTO %NEXT%

:END


