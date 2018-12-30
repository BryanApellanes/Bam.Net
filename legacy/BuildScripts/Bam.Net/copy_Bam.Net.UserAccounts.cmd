@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net462
SET VER=v4.6.2

MD Bam.Net.UserAccounts\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.UserAccounts.dll Bam.Net.UserAccounts\lib\%LIB%\Bam.Net.UserAccounts.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.UserAccounts.xml Bam.Net.UserAccounts\lib\%LIB%\Bam.Net.UserAccounts.xml