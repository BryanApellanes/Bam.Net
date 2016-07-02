@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net45
SET VER=v4.5

MD Bam.Net\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\BamCore.dll Bam.Net\lib\%LIB%\BamCore.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\BamCore.xml Bam.Net\lib\%LIB%\BamCore.xml