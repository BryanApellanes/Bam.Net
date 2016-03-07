@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net45
SET VER=v4.5

MD Bam.Net.Server\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Server.dll Bam.Net.Server\lib\%LIB%\Bam.Net.Server.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Server.xml Bam.Net.Server\lib\%LIB%\Bam.Net.Server.xml