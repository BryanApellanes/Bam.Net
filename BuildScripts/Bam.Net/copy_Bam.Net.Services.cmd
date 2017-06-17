@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net462
SET VER=v4.6.2

MD Bam.Net.Services\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Services.dll Bam.Net.Services\lib\%LIB%\Bam.Net.Services.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Services.xml Bam.Net.Services\lib\%LIB%\Bam.Net.Services.xml