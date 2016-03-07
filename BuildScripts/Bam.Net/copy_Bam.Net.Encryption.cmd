@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net45
SET VER=v4.5

MD Bam.Net.Encryption\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Encryption.dll Bam.Net.Encryption\lib\%LIB%\Bam.Net.Encryption.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Encryption.xml Bam.Net.Encryption\lib\%LIB%\Bam.Net.Encryption.xml