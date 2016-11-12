@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net462
SET VER=v4.6.2

MD Bam.Net.Net\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Net.dll Bam.Net.Net\lib\%LIB%\Bam.Net.Net.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Net.xml Bam.Net.Net\lib\%LIB%\Bam.Net.Net.xml