@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net45
SET VER=v4.5

MD Bam.Net.CommandLine\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.CommandLine.dll Bam.Net.CommandLine\lib\%LIB%\Bam.Net.CommandLine.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.CommandLine.xml Bam.Net.CommandLine\lib\%LIB%\Bam.Net.CommandLine.xml