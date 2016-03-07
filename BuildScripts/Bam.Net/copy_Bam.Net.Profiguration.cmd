@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net45
SET VER=v4.5

MD Bam.Net.Profiguration\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Profiguration.dll Bam.Net.Profiguration\lib\%LIB%\Bam.Net.Profiguration.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Profiguration.xml Bam.Net.Profiguration\lib\%LIB%\Bam.Net.Profiguration.xml