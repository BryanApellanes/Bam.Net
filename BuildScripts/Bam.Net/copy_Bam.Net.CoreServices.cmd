@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net45
SET VER=v4.5

MD Bam.Net.CoreServices\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.CoreServices.dll Bam.Net.CoreServices\lib\%LIB%\Bam.Net.CoreServices.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.CoreServices.xml Bam.Net.CoreServices\lib\%LIB%\Bam.Net.CoreServices.xml