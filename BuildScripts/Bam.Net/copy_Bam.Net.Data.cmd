@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net462
SET VER=v4.6.2

MD Bam.Net.Data\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Data.dll Bam.Net.Data\lib\%LIB%\Bam.Net.Data.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Data.xml Bam.Net.Data\lib\%LIB%\Bam.Net.Data.xml