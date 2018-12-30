@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net462
SET VER=v4.6.2

MD Bam.Net.Distributed\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Distributed.dll Bam.Net.Distributed\lib\%LIB%\Bam.Net.Distributed.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Distributed.xml Bam.Net.Distributed\lib\%LIB%\Bam.Net.Distributed.xml