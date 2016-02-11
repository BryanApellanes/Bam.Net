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
MD Bam.Net.UserAccounts\lib\%LIB%
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.UserAccounts.dll Bam.Net.UserAccounts\lib\%LIB%\Bam.Net.UserAccounts.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.UserAccounts.xml Bam.Net.UserAccounts\lib\%LIB%\Bam.Net.UserAccounts.xml
GOTO %NEXT%

:END


