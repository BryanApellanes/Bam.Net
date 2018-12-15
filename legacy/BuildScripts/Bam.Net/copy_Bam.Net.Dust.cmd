@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net462
SET VER=v4.6.2

MD Bam.Net.Dust\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Dust.dll Bam.Net.Dust\lib\%LIB%\Bam.Net.Dust.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Dust.xml Bam.Net.Dust\lib\%LIB%\Bam.Net.Dust.xml