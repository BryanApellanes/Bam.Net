@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net462
SET VER=v4.6.2

MD Bam.Net.Incubation\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Incubation.dll Bam.Net.Incubation\lib\%LIB%\Bam.Net.Incubation.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Incubation.xml Bam.Net.Incubation\lib\%LIB%\Bam.Net.Incubation.xml