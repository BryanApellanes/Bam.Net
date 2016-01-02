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
MD Bam.Net.Encryption\lib\%LIB%
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Encryption.dll Bam.Net.Encryption\lib\%LIB%\Bam.Net.Encryption.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Encryption.xml Bam.Net.Encryption\lib\%LIB%\Bam.Net.Encryption.xml
GOTO %NEXT%

:END


