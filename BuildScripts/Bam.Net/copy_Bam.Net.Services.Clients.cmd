@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net462
SET VER=v4.6.2

MD Bam.Net.Services.Clients\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Services.Clients.dll Bam.Net.Services.Clients\lib\%LIB%\Bam.Net.Services.Clients.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Services.Clients.xml Bam.Net.Services.Clients\lib\%LIB%\Bam.Net.Services.Clients.xml